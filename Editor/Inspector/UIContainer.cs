using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GFramework.Util;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;

namespace GFramework.EditorExtern
{
    [CustomEditor(typeof(UI.UIContainer))]
    public class UIContainer : Editor
    {
        static GLogger logger = new GLogger("UIBinder");


        private UI.UIContainer binder;
        private SerializedProperty varsArr;
        private const string prefixPath = "Assets/Res/Prefabs/UI/";

        private void OnEnable()
        {
            this.binder = this.target as UI.UIContainer;
            this.varsArr = this.serializedObject.FindProperty("varsArr");
        }

        private void UpdateTargetView()
        {
            string path = EditorUtility.OpenFilePanel("选择目标View.cs文件", Application.dataPath, "cs");
            this.binder.targetViewPath = path;
        }

        private void UpdateViewCode()
        {
            string assetPath = null;
            // Project中的Prefab是Asset不是Instance
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this.target))
            {
                // 预制体资源就是自身
                assetPath = AssetDatabase.GetAssetPath(this.target);
            }

            // Scene中的Prefab Instance是Instance不是Asset
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(this.target))
            {
                // 获取预制体资源
                var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(this.target);
                assetPath = UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);
            }

            // PrefabMode中的GameObject既不是Instance也不是Asset
            var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(this.binder.gameObject);
            if (prefabStage != null)
            {
                // 预制体资源：prefabAsset = prefabStage.prefabContentsRoot
                assetPath = prefabStage.prefabAssetPath;
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                logger.E("请先创建预制体！！！");
                return;
            }

            string prefabPath = assetPath.Substring(prefixPath.Length);
            string[] lines = File.ReadAllLines(this.binder.targetViewPath);
            StringBuilder sb = new StringBuilder();
            bool flag = true;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Contains("// --"))
                {
                    flag = true;
                }
                if (!flag) continue;
                sb.AppendLine(lines[i]);
                if (lines[i].Contains("// ++"))
                {
                    flag = false;
                    sb.AppendLine($"    public override string BindingPath()");
                    sb.AppendLine($"    {{");
                    sb.AppendLine($"        return \"{prefabPath}\";");
                    sb.AppendLine($"    }}");

                    List<Tuple<string, string>> fieldTuples = new List<Tuple<string, string>>();
                    for (int j = 0; j < this.varsArr.arraySize; ++j)
                    {
                        SerializedProperty fieldNameProperty = this.varsArr.GetArrayElementAtIndex(j).FindPropertyRelative("fieldName");
                        SerializedProperty componentProperty = this.varsArr.GetArrayElementAtIndex(j).FindPropertyRelative("component");
                        string fieldName = fieldNameProperty.stringValue;
                        Component component = componentProperty.objectReferenceValue as Component;
                        string componentName = component.GetType().ToString().GetLastFileNameWithoutSuffix('.');
                        fieldTuples.Add(new Tuple<string, string>(fieldName, componentName));
                    }

                    for (int j = 0; j < fieldTuples.Count; ++j)
                    {
                        string fieldName = fieldTuples[j].Item1;
                        string fieldType = fieldTuples[j].Item2;
                        sb.AppendLine($"    private {fieldType} {fieldName};");
                    }

                    sb.AppendLine($"    protected override void BindVars() {{");
                    for (int j = 0; j < fieldTuples.Count; ++j)
                    {
                        string fieldName = fieldTuples[j].Item1;
                        string fieldType = fieldTuples[j].Item2;
                        sb.AppendLine($"        {fieldName} = this.GetVar<{fieldType}>({j});");
                    }
                    sb.AppendLine($"    }}");
                }
            }
            File.WriteAllText(this.binder.targetViewPath, sb.ToString());
            AssetDatabase.Refresh();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            bool updateView = GUILayout.Button("Update View");
            bool updateCode = GUILayout.Button("Update Code");
            EditorGUILayout.EndHorizontal();

            if (updateView)
            {
                this.UpdateTargetView();
            }

            if (updateCode)
            {
                if (string.IsNullOrEmpty(this.binder.targetViewPath) || !File.Exists(this.binder.targetViewPath)) this.UpdateTargetView();
                this.UpdateViewCode();
            }
            EditorGUILayout.LabelField(this.binder.targetViewPath.Substring(Application.dataPath.Length));
            base.OnInspectorGUI();
            EditorGUILayout.LabelField("----------Vars----------");
            EditorGUILayout.BeginVertical();
            // 获取当期已经绑定的对象列表
            for (int i = 0; i < this.varsArr.arraySize; ++i)
            {
                SerializedProperty fieldName = this.varsArr.GetArrayElementAtIndex(i).FindPropertyRelative("fieldName");
                SerializedProperty gameObject = this.varsArr.GetArrayElementAtIndex(i).FindPropertyRelative("gameObject");
                SerializedProperty component = this.varsArr.GetArrayElementAtIndex(i).FindPropertyRelative("component");

                EditorGUILayout.BeginHorizontal();
                // 引用对象
                EditorGUILayout.PropertyField(gameObject, new GUIContent(), GUILayout.MaxWidth(100));

                string newFieldName = EditorGUILayout.TextField(fieldName.stringValue, GUILayout.MaxWidth(100));
                if (newFieldName != fieldName.stringValue)
                {
                    fieldName.stringValue = newFieldName;
                    EditorUtility.SetDirty(this.target);
                    this.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    this.serializedObject.UpdateIfRequiredOrScript();
                }

                // 组件选项
                GameObject go = gameObject.objectReferenceValue as GameObject;
                Component defaultComponent = component.objectReferenceValue as Component;
                string defaultName = defaultComponent.GetType().ToString().GetLastFileNameWithoutSuffix('.');
                if (EditorGUILayout.DropdownButton(new GUIContent(defaultName), FocusType.Passive))
                {
                    GenericMenu menu = new GenericMenu();
                    Component[] comps = go.GetComponents<Component>();
                    for (int j = 0; j < comps.Length; ++j)
                    {
                        Component option = comps[j];
                        string itemName = option.GetType().ToString().GetLastFileNameWithoutSuffix('.');
                        menu.AddItem(new GUIContent(itemName),
                            defaultComponent.GetType() == option.GetType(), (obj) =>
                            {
                                var pair = (KeyValuePair<int, Component>)obj;
                                this.varsArr.GetArrayElementAtIndex(pair.Key).FindPropertyRelative("component")
                                    .objectReferenceValue = pair.Value;
                                EditorUtility.SetDirty(this.target);
                                this.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                                this.serializedObject.UpdateIfRequiredOrScript();
                            }, new KeyValuePair<int, Component>(i, option));
                    }
                    menu.ShowAsContext();
                }

                // 删除引用
                if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
                {
                    this.varsArr.DeleteArrayElementAtIndex(i);
                    go.name = go.name.Trim('#');

                    EditorUtility.SetDirty(this.target);
                    this.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    this.serializedObject.UpdateIfRequiredOrScript();
                }

                EditorGUILayout.EndHorizontal();
            }


            Rect rect = EditorGUILayout.GetControlRect();
            if (rect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                if (Event.current.type == EventType.DragExited)
                {
                    Object[] objs = DragAndDrop.objectReferences;
                    for (int i = 0; i < objs.Length; ++i)
                    {
                        GameObject go = objs[i] as GameObject;
                        if (!go) return;
                        string goName = go.name.Trim('#');
                        this.varsArr.InsertArrayElementAtIndex(this.varsArr.arraySize);
                        SerializedProperty element = this.varsArr.GetArrayElementAtIndex(this.varsArr.arraySize - 1);
                        element.FindPropertyRelative("fieldName").stringValue = goName;
                        element.FindPropertyRelative("gameObject").objectReferenceValue = go;
                        element.FindPropertyRelative("component").objectReferenceValue = go.transform;
                        go.name = '#' + go.name.Trim('#');
                        EditorUtility.SetDirty(this.target);
                        this.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                        this.serializedObject.UpdateIfRequiredOrScript();
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}
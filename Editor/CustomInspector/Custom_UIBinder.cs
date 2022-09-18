using System;
using System.Collections.Generic;
using GFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

[CustomEditor(typeof(UIBinder))]
public class Custom_UIBinder : Editor
{
    private SerializedProperty varsArr;

    private void OnEnable()
    {
        this.varsArr = this.serializedObject.FindProperty("varsArr");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.TextField(AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromOriginalSource((this.target as UIBinder).gameObject)));
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
            string defaultName = PathUtil.Suffix(defaultComponent.GetType().ToString(), '.', false);
            if (EditorGUILayout.DropdownButton(new GUIContent(defaultName),
                    FocusType.Passive))
            {
                GenericMenu menu = new GenericMenu();
                Component[] comps = go.GetComponents<Component>();
                for (int j = 0; j < comps.Length; ++j)
                {
                    Component option = comps[j];
                    string itemName = PathUtil.Suffix(option.GetType().ToString(), '.', false);
                    menu.AddItem(new GUIContent(itemName),
                        defaultComponent.GetType() == option.GetType(), (obj) =>
                        {
                            var pair = (KeyValuePair<int, Component>) obj;
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
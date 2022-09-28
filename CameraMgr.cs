using UnityEngine;

namespace GFramework
{
    public class CameraMgr : Singleton<CameraMgr>
    {
        public Camera uiCamera;

        public void Setup()
        {
            GameObject uiCam = new GameObject("UICamera", typeof(Camera));
            uiCam._SetParent(GameObject.Find("Game").transform);
            this.uiCamera = uiCam.GetComponent<Camera>();
            this.uiCamera.cullingMask = LayerMask.GetMask("UI");
            this.uiCamera.orthographic = true;
            this.uiCamera.clearFlags = CameraClearFlags.Color;
            this.uiCamera.backgroundColor = Color.black;
        }
    }
}

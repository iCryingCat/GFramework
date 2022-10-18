using System.Security.Cryptography.X509Certificates;
using GameLogic;
using UnityEngine;

namespace GFramework
{
    public class CameraMgr : Singleton<CameraMgr>
    {
        public Camera uiCamera;
        public Camera mainCamera;

        public void GenUICamera()
        {
            GameObject uiCam = new GameObject("UICamera", typeof(Camera));
            uiCam._SetParent(GameObject.Find("Game").transform);
            this.uiCamera = uiCam.GetComponent<Camera>();
            this.uiCamera.cullingMask = LayerMask.GetMask("UI");
            this.uiCamera.orthographic = true;
            this.uiCamera.clearFlags = CameraClearFlags.Color;
            this.uiCamera.backgroundColor = Color.black;
        }

        public void GenMainCamera()
        {
            GameObject mainCam = new GameObject("MainCamera", typeof(Camera));
            mainCam._SetParent(GameObject.Find("Game").transform);
            this.mainCamera = mainCam.GetComponent<Camera>();
            this.mainCamera.tag = "MainCamera";
        }

        public static void SetCameraFollow(Camera camera, Transform followTarget)
        {
            CameraFollower follower = camera.gameObject.AddComponent<CameraFollower>();
            follower.BindTarget(followTarget);
        }

    }
}

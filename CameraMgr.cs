/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/7/16 14:55:37
 * 描述：
 *  -------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace GFramework
{
    public class CameraMgr : Singleton<CameraMgr>
    {
        public Camera uiCamera;

        public void Initialize()
        {
            GameObject uiCam = new GameObject("UICamera", typeof(Camera));
            uiCam._SetParent(GameObject.Find("Game").transform);
            this.uiCamera = uiCam.GetComponent<Camera>();
            this.uiCamera.cullingMask = LayerMask.GetMask("UI");
            this.uiCamera.orthographic = true;
        }
    }
}

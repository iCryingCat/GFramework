using System;

using UnityEngine;

using static Shortcuts;

namespace GFramework
{
    public class UnityInputSystem : IInputSystem
    {
        public bool GetKeyDown(string keyName)
        {
            KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            return Input.GetKeyDown(code);
        }

        public bool GetKey(string keyName)
        {
            KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            return Input.GetKey(code);
        }

        public bool GetKeyUp(string keyName)
        {
            KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            return Input.GetKeyUp(code);
        }

        public float GetAxis(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        public bool GetMouseButtonDown(string keyName)
        {
            int code = (int)(EMouseKeyCode)Enum.Parse(typeof(EMouseKeyCode), keyName);
            return Input.GetMouseButtonDown(code);
        }

        public bool GetMouseButton(string keyName)
        {
            int code = (int)(EMouseKeyCode)Enum.Parse(typeof(EMouseKeyCode), keyName);
            return Input.GetMouseButton(code);
        }

        public bool GetMouseButtonUp(string keyName)
        {
            int code = (int)(EMouseKeyCode)Enum.Parse(typeof(EMouseKeyCode), keyName);
            return Input.GetMouseButtonUp(code);
        }
    }
}
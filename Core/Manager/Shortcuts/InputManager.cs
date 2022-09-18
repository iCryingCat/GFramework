using System;
using UnityEditor;
using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// io管理器
    /// </summary>
    public class InputManager
    {
        public static event OnAxisTouch Forward;
        public static event OnAxisTouch Right;
        public static event OnKeyPress Jump;
        public static event OnAxisTouch MouseX;
        public static event OnAxisTouch MouseY;
        public static event OnKeyPress MouseLeft;
        public static event OnKeyPress MouseRight;
        public static event OnKeyPress MouseLeftDown;
        public static event OnKeyPress MouseRightDown;
    }
}
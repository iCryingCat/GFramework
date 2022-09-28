using System;

using UnityEngine;

namespace GFramework
{
    public class InputHandler
    {
        public static float Forward { get => Input.GetAxis(Shortcuts.VERTICAL); }
        public static float Right { get => Input.GetAxis(Shortcuts.HORIZONTAL); }
        public static float MouseX { get => Input.GetAxis((Shortcuts.MOUSE_X)); }
        public static float MouseY { get => Input.GetAxis((Shortcuts.MOUSE_Y)); }
        public static bool Attack { get => Input.GetMouseButton((Shortcuts.MOUSE_BTN_LEFT)); }
        public static bool Jump { get => Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), Shortcuts.JUMP)); }
    }
}
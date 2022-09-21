using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFramework.Log;
#if UNITY_STANDALONE_WIN
using UnityEngine;
#endif
namespace GFramework
{
    public static class GLog
    {
        public static bool logEnable = true;

        public static void P(string module, object info)
        {
            string infoData = LogDumper.DumpAsString(info);
            string msg = PrintFormat($"[[{module}]] ==> {infoData}");
            Log(msg);
        }

        public static void W(string module, object info)
        {
            string infoData = LogDumper.DumpAsString(info);
            string msg = WarningFormat($"[[{module}]] ==> {infoData}");
            Log(msg);
        }

        public static void E(string module, object info)
        {
            string infoData = LogDumper.DumpAsString(info);
            string msg = ErrorFormat($"[[{module}]] ==> {infoData}");
            Log(msg);
        }

        private static string PrintFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
#if UNITY_STANDALONE_WIN
            msg = ColorFormat("green", msg);
#endif
            return msg;
        }

        private static string WarningFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
#if UNITY_STANDALONE_WIN
            msg = ColorFormat("yellow", msg);
#endif
            return msg;
        }

        private static string ErrorFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
#if UNITY_STANDALONE_WIN
            msg = ColorFormat("red", msg);
#endif
            return msg;
        }

        private static string ThrowFormat(string msg)
        {
            return $"<color=purple>{msg}</color>";
        }

        private static string ColorFormat(string color, string msg)
        {
            return $"<color={color}>{msg}</color>";
        }

        private static void Log(string msg)
        {
            if (!logEnable) return;
#if UNITY_STANDALONE_WIN
            Debug.Log(msg);
#else
            Console.WriteLine(msg);
#endif
        }
    }
}
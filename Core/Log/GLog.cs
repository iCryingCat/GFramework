using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFramework.Log;
using UnityEngine;

namespace GFramework
{
    public static class GLog
    {
        private static bool logEnable = true;

        public static void P(string module, object info)
        {
            string infoData = LogDumper.DumpAsString(info);
            string msg = PrintFormat($"[[{module}]] ==> {infoData}");
            Log(msg);
        }

        public static void W(string module, object info)
        {
            string msg = WarningFormat($"[[{module}]] ==> {info}");
            Log(msg);
        }

        public static void E(string module, object info)
        {
            string msg = ErrorFormat($"[[{module}]] ==> {info}");
            Log(msg);
        }

        private static string PrintFormat(string msg)
        {
            return $"<color=green>{msg}</color>";
        }

        private static string WarningFormat(string msg)
        {
            return $"<color=yellow>{msg}</color>";
        }

        private static string ErrorFormat(string msg)
        {
            return $"<color=red>{msg}</color>";
        }

        private static string ThrowFormat(string msg)
        {
            return $"<color=purple>{msg}</color>";
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
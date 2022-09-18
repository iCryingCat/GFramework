using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GFramework
{
    public class GLog
    {
        public static void P(string msg){
            Debug.Log(msg);
        }

        public static void P(string module, string msg){
            Debug.Log(msg);
        }

        public static void W(string msg){
            Debug.Log(msg);
        }

        public static void W(string module, string msg){
            Debug.Log(msg);
        }

        public static void E(string msg){
            Debug.Log(msg);
        }

        public static void E(string module, string msg){
            Debug.Log(msg);
        }

        public static void Throw(string msg){

        }
    }
}
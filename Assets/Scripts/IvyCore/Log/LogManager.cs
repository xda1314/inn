using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    public class LogManager {
        public static void Log(string log)
        {
            Debug.Log(log);
        }
        public static void Log(string format,params object[] args)
        {
            Debug.LogFormat(format, args);
        }
    }
}
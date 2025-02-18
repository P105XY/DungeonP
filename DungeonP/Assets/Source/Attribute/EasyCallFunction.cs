using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Playables;
using UnityEngine.Diagnostics;

#if UNITY_EDITOR
namespace EasyCallFunctionNamespace
{
    [InitializeOnLoad]
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    class EasyCallingFunction : System.Attribute
    {
        public List<string> IntMethodList = new List<string>();
        public List<string> FloatMethodList = new List<string>();
        public List<string> BooleanMethodList = new List<string>();
        public List<string> ActionMethodList = new List<string>();
         Dictionary<string, List<string>> MethodDict = new Dictionary<string, List<string>>();

        public EasyCallingFunction(string Paramtype, string MethodName)
        {
            if (Paramtype == null)
            {
                return;
            }

            if (Paramtype.Equals("int", StringComparison.OrdinalIgnoreCase))
            {
                IntMethodList.Add(MethodName);
            }

            if (Paramtype.Equals("float", StringComparison.OrdinalIgnoreCase))
            {
                FloatMethodList.Add(MethodName);
            }

            if (Paramtype.Equals("boolean", StringComparison.OrdinalIgnoreCase))
            {
                BooleanMethodList.Add(MethodName);
            }

            if (Paramtype.Equals("action", StringComparison.OrdinalIgnoreCase))
            {
                ActionMethodList.Add(MethodName);
            }

            string MethodDBPath = Path.Combine(Application.dataPath, "EasyCallFunction", "MethodTable.csv");
            if(!File.Exists(MethodDBPath))
            {
                
            }
        }
    }

    

    class EasyExcuteFunction
    {
        public void ExcuteEasyCallingFunctionInt(UnityEngine.Object obj, int param)
        {
            if(obj == null)
            {
                return;
            }
            MethodInfo callingMethod = null;
            List<string> InMethodList = new List<string>();

            foreach (string methodName in InMethodList)
            {
                MethodInfo callingmethod = obj.GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
                if (callingmethod == null)
                {
                    continue;
                }

                callingMethod = callingmethod;
            }

            object[] invokeParam = {param};
            callingMethod.Invoke(obj, invokeParam);
        }

        public void ExcuteEasyCallingFunctionFloat(UnityEngine.Object obj, float param)
        {
           if(obj == null)
            {
                return;
            }
            MethodInfo callingMethod = null;
            List<string> InMethodList = new List<string>();

            foreach (string methodName in InMethodList)
            {
                MethodInfo callingmethod = obj.GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
                if (callingmethod == null)
                {
                    continue;
                }

                callingMethod = callingmethod;
            }

            object[] invokeParam = {param};
            callingMethod.Invoke(obj, invokeParam);
        }

        public void ExcuteEasyCallingFunctionBoolean(UnityEngine.Object obj, bool param)
        {
           if(obj == null)
            {
                return;
            }
            MethodInfo callingMethod = null;
            List<string> InMethodList = new List<string>();

            foreach (string methodName in InMethodList)
            {
                MethodInfo callingmethod = obj.GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
                if (callingmethod == null)
                {
                    continue;
                }

                callingMethod = callingmethod;
            }

            object[] invokeParam = {param};
            callingMethod.Invoke(obj, invokeParam);
        }

        public void ExcuteEasyCallingFunctionAction(UnityEngine.Object obj)
        {
            if(obj == null)
            {
                return;
            }
            MethodInfo callingMethod = null;
            List<string> InMethodList = new List<string>();

            foreach (string methodName in InMethodList)
            {
                MethodInfo callingmethod = obj.GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
                if (callingmethod == null)
                {
                    continue;
                }

                callingMethod = callingmethod;
            }

            callingMethod.Invoke(obj, null);
        }
    }

    [InitializeOnLoad]
    class CompileChecker
    {
        static CompileChecker()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
        }

        private static void OnBeforeAssemblyReload()
        {
            Debug.Log("어셈블리 재로딩 전입니다.");
        }

        private static void OnAfterAssemblyReload()
        {
            Debug.Log("어셈블리 재로딩 후입니다.");
        }
    }
}
#endif
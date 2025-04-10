using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Playables;
using UnityEngine.Diagnostics;
using Unity.VisualScripting;

#if UNITY_EDITOR
namespace EasyCallFunctionNamespace
{
    [InitializeOnLoad]
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    class EasyCallingFunction : System.Attribute
    {
        public EasyCallingFunction() { }
        public EasyCallingFunction(string Paramtype, string MethodName)
        {

        }
    }

    class EasyExcuteFunction
    {
        public List<string> IntMethodList = new List<string>();
        public List<string> FloatMethodList = new List<string>();
        public List<string> BooleanMethodList = new List<string>();
        public List<string> ActionMethodList = new List<string>();
        Dictionary<string, List<string>> MethodDict = new Dictionary<string, List<string>>();

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

        EasyExcuteFunction easyExcuteFunction;

        private void Init()
        {
            easyExcuteFunction = new EasyExcuteFunction();
        }

        private EasyExcuteFunction GetEasyFunction()
        {
            return easyExcuteFunction;
        }

        private static void OnBeforeAssemblyReload()
        {
            Debug.Log("어셈블리 재로딩 전입니다.");
        }

        private static void OnAfterAssemblyReload()
        {
            Debug.Log("어셈블리 재로딩 후입니다.");

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                var currentAssemblyType = assembly.GetType();
                if (currentAssemblyType == null)
                {
                    continue;
                }

                MemberInfo[] memberMethods = currentAssemblyType.GetMembers();
                if(memberMethods.Length <= 0)
                {
                    continue;
                }

                foreach (MemberInfo memberInfo in memberMethods)
                {
                    bool bHasAttributeMethod = memberInfo.IsDefined(typeof(EasyCallingFunction), false);
                    if (bHasAttributeMethod)
                    {

                    }
                }
            }
        }
    }

    class FunctionCallManager
    {

    }
}
#endif
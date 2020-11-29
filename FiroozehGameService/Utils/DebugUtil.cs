// <copyright file="DebugUtil.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>


/**
* @author Alireza Ghodrati
*/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using Debug = FiroozehGameService.Models.EventArgs.Debug;

namespace FiroozehGameService.Utils
{
    /// <summary>
    ///   Represents Game Service DebugUtil
    /// </summary>
    internal static class DebugUtil
    {
        
        internal static void LogNormal(Type type,DebugLocation where,string callingMethod,string data)
        {
            var callingClass = type.Name;
            new Debug
            {
                LogTypeType = LogType.Normal, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Normal + " F:" + callingMethod + "] : " + data
            }.Invoke();
        }
        
        
        internal static void LogNormal<TClass>(DebugLocation where,string callingMethod,string data)
           where TClass : class
        {
            var callingClass = typeof(TClass).Name;
            new Debug
            {
                LogTypeType = LogType.Normal, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Normal + " F:" + callingMethod + "] : " + data
            }.Invoke();
        }

        
        internal static void LogError(DebugLocation where,string callingClass,string callingMethod,string data) 
        {
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + data
            }.Invoke();
        }
        
        internal static void LogError(DebugLocation where,string callingClass,string callingMethod,Exception exception)
        {
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + exception
            }.Invoke();
        }


        internal static GameServiceException LogException<TClass>(this GameServiceException exception,DebugLocation where,string callingMethod)
         where TClass : class
        {
            var callingClass = typeof(TClass).Name;
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" +callingMethod + "] : " + exception.Message
            }.Invoke();
            
            return exception;
        }
        
        
        internal static GameServiceException LogException(this GameServiceException exception,Type type,DebugLocation where,string callingMethod)
        {
            var callingClass = type.Name;
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" +callingMethod + "] : " + exception.Message
            }.Invoke();
            
            return exception;
        }


        private static string GetTime()
        {
            return DateTime.Now.ToString("yyyy-M-d  h:mm:ss tt");
        }
        
        private static void Invoke(this Debug debug)
        {
            GameService.OnDebugReceived?.Invoke(null,debug);
        }
        
        
        private static MethodBase GetCallingMethodBase(StackFrame stackFrame)
        {
            return stackFrame == null ? MethodBase.GetCurrentMethod() : stackFrame.GetMethod();
        }
        
        private static StackFrame FindStackFrame(string methodName)
        {
            var stackTrace = new StackTrace(true);
            for (var i = 0; i < stackTrace.GetFrames().Length; i++)
            {
                var methodBase = stackTrace.GetFrame(i).GetMethod();
                var name = MethodBase.GetCurrentMethod().Name;
                
                if (!methodBase.Name.Equals(methodName) && !methodBase.Name.Equals(name))
                    return new StackFrame(i, true);
            }
            return null;
        }
    }
}
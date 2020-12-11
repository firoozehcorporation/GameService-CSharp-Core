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
using System.Linq;
using System.Reflection;
using FiroozehGameService.Core;
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
            if(!CanDebug(LogType.Normal,where)) return;
            
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
            if(!CanDebug(LogType.Normal,where)) return;

            var callingClass = typeof(TClass).Name;
            new Debug
            {
                LogTypeType = LogType.Normal, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Normal + " F:" + callingMethod + "] : " + data
            }.Invoke();
        }

        
        internal static void LogError<TClass>(DebugLocation where,string callingMethod,string data) 
        {
            if(!CanDebug(LogType.Error,where)) return;

            var callingClass = typeof(TClass).Name;
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + data
            }.Invoke();
        }
        
        internal static void LogError(Type type,DebugLocation where,string callingMethod,Exception exception)
        {
            if(!CanDebug(LogType.Error,where)) return;

            var callingClass = type.Name;
            new Debug
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + exception
            }.Invoke();
        }


        internal static Exception LogException<TClass>(this Exception exception,DebugLocation where,string callingMethod)
         where TClass : class
        {
            if(!CanDebug(LogType.Exception,where)) return exception;
            
            var callingClass = typeof(TClass).Name;
            new Debug
            {
                LogTypeType = LogType.Exception, 
                Exception = exception,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Exception + " F:" +callingMethod + "] : " + exception.Message
            }.Invoke();
            
            return exception;
        }
        
        
        internal static Exception LogException(this Exception exception,Type type,DebugLocation where,string callingMethod)
        {
            if(!CanDebug(LogType.Exception,where)) return exception;

            var callingClass = type.Name;
            new Debug
            {
                LogTypeType = LogType.Exception, 
                Exception = exception,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Exception + " F:" +callingMethod + "] : " + exception.Message
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


        private static bool CanDebug(LogType type , DebugLocation where)
        {
            if (GameService.DebugConfiguration == null) return false;

            var can = false;
            switch (type)
            {
                case LogType.Normal:
                    if (GameService.DebugConfiguration.EnableDebug) can = true;
                    break;
                case LogType.Error:
                    if (GameService.DebugConfiguration.EnableError) can = true;
                    break;
                case LogType.Exception:
                    if (GameService.DebugConfiguration.EnableException) can = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            if (GameService.DebugConfiguration.DebugLocations == null)
                return can;
            if(GameService.DebugConfiguration.DebugLocations.Contains(DebugLocation.All))
                return can;

            can &= GameService.DebugConfiguration.DebugLocations.ToList().Any(dl => dl == where);
            return can;
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
// <copyright file="LogUtil.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Utils
{

    internal class Log
    {
        internal Log(LogType type, string txt)
        {
            Type = type;
            Txt = txt;
        }

        public LogType Type { get; }
        public string Txt { get; }
    }

    internal class LogUtil
    {
        private const bool IsDebug = false;
        private static EventHandler<Log> LogEventHandler;


        internal static void Log(object where, string txt)
        {
            if (!IsDebug) return;
            LogEventHandler?.Invoke(where,
                new Log(LogType.Normal, DateTime.Now.ToString("h:mm:ss tt") + "--" + txt));
        }

        internal static void LogError(object where, string err)
        {
            if (!IsDebug) return;
            LogEventHandler?.Invoke(where,
                new Log(LogType.Error, DateTime.Now.ToString("h:mm:ss tt") + "--" + err));
        }
    }
}
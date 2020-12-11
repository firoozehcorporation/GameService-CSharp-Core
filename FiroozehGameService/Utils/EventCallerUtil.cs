// <copyright file="EventCallerUtil.cs" company="Firoozeh Technology LTD">
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
using System.Timers;
using FiroozehGameService.Core;

namespace FiroozehGameService.Utils
{
    /// <summary>
    ///  Represents Event In Game Service
    /// </summary>
    public class EventUtil
    {
        private readonly long _interval;
        /// <summary>
        /// Set New EventHandler by Observer
        /// </summary>
        public EventHandler<EventUtil> EventHandler;
        internal Timer Timer;

        internal EventUtil(long interval)
        {
            _interval = interval;
        }

        /// <summary>
        /// Start EventHandler
        /// </summary>
        public void Start()
        {
            Timer = new Timer
            {
                Interval = _interval,
                Enabled = false
            };

            Timer.Elapsed += (sender, args) =>
            {
                GameService.SynchronizationContext?.Send(
                    delegate { EventHandler?.Invoke(this, this); }, null);
            };
            Timer.Start();
        }

        /// <summary>
        /// Dispose EventHandler
        /// </summary>
        public void Dispose()
        {
            Timer?.Stop();
            Timer?.Close();
        }
    }


    /// <summary>
    /// Represents Event In Game Service Observer
    /// </summary>
    public static class EventCallerUtil
    {
        /// <summary>
        /// CreateNewEvent by Observer
        /// </summary>
        /// <param name="interval">interval time</param>
        /// <returns></returns>
        public static EventUtil CreateNewEvent(long interval)
        {
            var newEvent = new EventUtil(interval);
            return newEvent;
        }
    }
}
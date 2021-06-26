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
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Utils
{
    /// <summary>
    ///     Represents Event In Game Service
    /// </summary>
    public class EventUtil
    {
        private readonly bool _isInternal;
        private double _interval;

        private Timer _timer;

        /// <summary>
        ///     Set New EventHandler by Observer
        /// </summary>
        public EventHandler<EventUtil> EventHandler;


        internal EventUtil(bool isInternal)
        {
            _isInternal = isInternal;
            UpdateInterval();
        }

        private void UpdateInterval()
        {
            _interval = 1000f / RealTimeHandler.GetSerializationRate();
            if (_isInternal) _interval += 10 * RealTimeConst.MinObserverThreshold;
        }

        /// <summary>
        ///     Start EventHandler
        /// </summary>
        public void Start()
        {
            _timer = new Timer
            {
                Interval = _interval,
                Enabled = false
            };

            _timer.Elapsed += (sender, args) =>
            {
                GameService.SynchronizationContext?.Send(
                    delegate { EventHandler?.Invoke(this, this); }, null);


                UpdateInterval();
                if (!(Math.Abs(_timer.Interval - _interval) > 1f)) return;

                _timer.Stop();
                _timer.Interval = _interval;
                _timer.Start();
            };
            _timer.Start();
        }

        /// <summary>
        ///     Dispose EventHandler
        /// </summary>
        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Close();
            EventHandler = null;
        }
    }


    /// <summary>
    ///     Represents Event In Game Service Observer
    /// </summary>
    public static class EventCallerUtil
    {
        /// <summary>
        ///     CreateNewEvent by Observer
        /// </summary>
        public static EventUtil CreateNewEvent()
        {
            var newEvent = new EventUtil(false);
            return newEvent;
        }
    }
}
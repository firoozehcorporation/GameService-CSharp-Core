// <copyright file="ObjectCallerUtil.cs" company="Firoozeh Technology LTD">
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
using System.Timers;

namespace FiroozehGameService.Utils
{
    internal class ObjectCallerUtil
    {
        private readonly Timer _timer;
        internal EventHandler<object> Caller;

        internal ObjectCallerUtil(int interval, object body)
        {
            _timer = new Timer
            {
                Interval = interval,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { Caller?.Invoke(null, body); };
        }

        internal void Start()
        {
            _timer?.Start();
        }

        internal void Stop()
        {
            _timer?.Stop();
        }


        internal void Dispose()
        {
            Caller = null;
            _timer?.Close();
            _timer?.Dispose();
        }
    }
}
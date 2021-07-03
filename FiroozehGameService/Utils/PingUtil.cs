// <copyright file="PingUtil.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Utils
{
    internal static class PingUtil
    {
        private const int Interval = 2000;
        private const float Wq = 0.2f;
        private static short _lastPing = 60;
        private static Timer _timer;

        internal static EventHandler RequestPing;


        internal static void Init()
        {
            if (_timer != null) return;
            _timer = new Timer
            {
                Interval = Interval,
                Enabled = true
            };
            _timer.Elapsed += (sender, args) => { RequestPing?.Invoke(null, null); };
        }

        internal static short GetLastPing()
        {
            return _lastPing;
        }

        internal static void SetLastPing(long one, long two)
        {
            var diff = Math.Abs(one - two);
            var newPing = (1 - Wq) * _lastPing + Wq * diff;
            _lastPing = (short) newPing;
        }

        internal static void Dispose()
        {
            _lastPing = -1;
            _timer?.Close();
            _timer?.Dispose();
            RequestPing = null;
        }
    }
}
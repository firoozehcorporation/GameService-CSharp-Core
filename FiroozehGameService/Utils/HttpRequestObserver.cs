// <copyright file="HttpRequestObserver.cs" company="Firoozeh Technology LTD">
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

using System.Timers;

namespace FiroozehGameService.Utils
{
    internal class HttpRequestObserver
    {
        internal const int Reset = 3;
        internal const int MaxRequest = 20;
        private readonly Timer _timer;
        private int _counter;
        internal bool IsDisposed;


        public HttpRequestObserver()
        {
            _timer = new Timer
            {
                Interval = Reset * 1000,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { _counter = 0; };
            _timer.Start();
            IsDisposed = false;
        }


        public bool Increase()
        {
            if (_counter > MaxRequest) return false;
            _counter++;
            return true;
        }


        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Close();
            IsDisposed = true;
        }
    }
}
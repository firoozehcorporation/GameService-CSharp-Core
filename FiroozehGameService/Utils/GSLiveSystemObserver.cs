// <copyright file="GsLiveSystemObserver.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal class GsLiveSystemObserver
    {
        internal const int RestLimit = 1000;
        private readonly Timer _timer;
        private readonly GSLiveType _type;
        private int _counter;


        public GsLiveSystemObserver(GSLiveType type)
        {
            _type = type;
            _timer = new Timer
            {
                Interval = RestLimit,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { Reset(); };
            _timer.Start();
        }

        private void Reset()
        {
            _counter = 0;
        }

        public bool Increase(bool isCritical)
        {
            if (isCritical) return true;

            switch (_type)
            {
                case GSLiveType.NotSet:
                    break;
                case GSLiveType.TurnBased:
                    if (_counter <= TurnBasedConst.TurnBasedLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                case GSLiveType.RealTime:
                    if (_counter <= RealTimeConst.RealTimeSendLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                case GSLiveType.Command:
                    if (_counter <= CommandConst.CommandLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                default:
                    return false;
            }

            return false;
        }


        public int GetMaxRequestSupport()
        {
            switch (_type)
            {
                case GSLiveType.TurnBased:
                    return TurnBasedConst.TurnBasedLimit;
                case GSLiveType.RealTime:
                    return RealTimeConst.RealTimeSendLimit;
                case GSLiveType.Command:
                    return TurnBasedConst.TurnBasedLimit;
                case GSLiveType.NotSet:
                    return -1;
                default:
                    return -1;
            }
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Close();
        }
    }
}
// <copyright file="ScheduleBuilder.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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


namespace FiroozehGameService.Models.GSLive
{
    /// <summary>
    ///     Represents ScheduleBuilder Class
    /// </summary>
    public class ScheduleBuilder
    {
        private const int Second = 1;
        private const int Minute = 60 * Second;
        private const int Hour = 60 * Minute;
        private const int Day = 24 * Hour;
        private const int Week = 7 * Day;

        /// <summary>
        ///     the Value Data
        /// </summary>
        private readonly int _value;


        internal ScheduleBuilder()
        {
        }

        /// <summary>
        ///     Abstract Value Constructor Function
        /// </summary>
        /// <param name="value"></param>
        internal ScheduleBuilder(int value)
        {
            _value = value;
        }


        /// <summary>
        ///     returns next Seconds Time in Seconds
        /// </summary>
        /// <returns></returns>
        public SchedulerTime Seconds()
        {
            return new SchedulerTime(Second * _value);
        }


        /// <summary>
        ///     returns next Minutes Time in Seconds
        /// </summary>
        /// <returns></returns>
        public SchedulerTime Minutes()
        {
            return new SchedulerTime(Minute * _value);
        }


        /// <summary>
        ///     returns next Hours Time in Seconds
        /// </summary>
        /// <returns></returns>
        public SchedulerTime Hours()
        {
            return new SchedulerTime(Hour * _value);
        }

        /// <summary>
        ///     returns next Days Time in Seconds
        /// </summary>
        /// <returns></returns>
        public SchedulerTime Days()
        {
            return new SchedulerTime(Day * _value);
        }


        /// <summary>
        ///     returns next Weeks Time in Seconds
        /// </summary>
        /// <returns></returns>
        public SchedulerTime Weeks()
        {
            return new SchedulerTime(Week * _value);
        }
    }
}
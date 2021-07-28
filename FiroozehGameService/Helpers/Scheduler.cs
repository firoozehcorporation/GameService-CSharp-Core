// <copyright file="Scheduler.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Helpers
{
    /// <summary>
    ///     Represents Game Service Time Scheduler Class
    /// </summary>
    public static class Scheduler
    {
        private const int Second = 1;
        private const int Minute = 60 * Second;
        private const int Hour = 60 * Minute;
        private const int Day = 24 * Hour;
        private const int Week = 7 * Day;

        /// <summary>
        ///     returns the AbstractValue Of Time to Work With Scheduler Functions
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static AbstractValue<int> Next(int time)
        {
            return new AbstractValue<int>(time);
        }

        /// <summary>
        ///     returns next Seconds Time in Seconds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SchedulerTime Seconds(this AbstractValue<int> data)
        {
            return new SchedulerTime(Second * data.Value);
        }


        /// <summary>
        ///     returns next Minutes Time in Seconds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SchedulerTime Minutes(this AbstractValue<int> data)
        {
            return new SchedulerTime(Minute * data.Value);
        }


        /// <summary>
        ///     returns next Hours Time in Seconds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SchedulerTime Hours(this AbstractValue<int> data)
        {
            return new SchedulerTime(Hour * data.Value);
        }

        /// <summary>
        ///     returns next Days Time in Seconds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SchedulerTime Days(this AbstractValue<int> data)
        {
            return new SchedulerTime(Day * data.Value);
        }


        /// <summary>
        ///     returns next Weeks Time in Seconds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SchedulerTime Weeks(this AbstractValue<int> data)
        {
            return new SchedulerTime(Week * data.Value);
        }
    }
}
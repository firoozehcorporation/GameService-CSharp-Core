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


using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Helpers
{
    /// <summary>
    ///     Represents Game Service Time Scheduler Class
    /// </summary>
    public static class Scheduler
    {
        /// <summary>
        ///     returns the ScheduleBuilder Of Time to Work With Scheduler Functions
        /// </summary>
        /// <param name="time"> the Time Value</param>
        /// <returns></returns>
        public static ScheduleBuilder Next(int time)
        {
            if (time <= 0)
                throw new GameServiceException("Invalid Time, Time Must Be Greater Than Zero").LogException(
                    typeof(Scheduler), DebugLocation.Internal, "Next");

            return new ScheduleBuilder(time);
        }


        /// <summary>
        ///     returns next Date Time Offset in Seconds
        ///     NOTE : Input DateTimeOffset Must Set With UTC Time
        /// </summary>
        /// <param name="dateTimeOffset">the DateTimeOffset To Schedule</param>
        /// <returns></returns>
        public static SchedulerTime FromDate(DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset == null)
                throw new GameServiceException("dateTimeOffset Cant Be Null").LogException(
                    typeof(Scheduler), DebugLocation.Internal, "FromDate");


            var seconds = dateTimeOffset
                .ToUniversalTime()
                .Subtract(DateTimeOffset.UtcNow)
                .TotalSeconds;

            if (seconds <= 0)
                throw new GameServiceException("Invalid Time, Time Must Be Greater Than Zero").LogException(
                    typeof(Scheduler), DebugLocation.Internal, "FromDate");


            return new SchedulerTime((int) seconds);
        }
    }
}
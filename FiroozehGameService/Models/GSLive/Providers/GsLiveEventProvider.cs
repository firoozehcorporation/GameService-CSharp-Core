// <copyright file="GsLiveEventProvider.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service Push Event System
    /// </summary>
    public abstract class GsLiveEventProvider
    {
        /// <summary>
        ///     Push an Event To Member Id With Data
        /// </summary>
        /// <param name="memberId">(NOTNULL)ID of Member You Want To Sent an Event to it</param>
        /// <param name="data">(NOTNULL)The main Event Data </param>
        /// <param name="pushEventBufferType">The Type of Event Buffering</param>
        public abstract void PushEventById(string memberId, string data,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering);


        /// <summary>
        ///     Push an Event To Member Id With Data and scheduler
        ///     NOTE : The Event will Trigger at the Specified Time.
        /// </summary>
        /// <param name="memberId">(NOTNULL)ID of Member You Want To Sent an Event to it</param>
        /// <param name="data">(NOTNULL)The main Event Data </param>
        /// <param name="schedulerTime">(NOTNULL)The Event Scheduler Time Data</param>
        /// <param name="pushEventBufferType">The Type of Event Buffering</param>
        public abstract void PushEventById(string memberId, string data, SchedulerTime schedulerTime,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering);


        /// <summary>
        ///     Push an Event To Member Tag With Data
        /// </summary>
        /// <param name="memberTag">(NOTNULL)Tag of Member You Want To Sent an Event to it</param>
        /// <param name="data">(NOTNULL)The main Event Data </param>
        /// <param name="pushEventBufferType">The Type of Event Buffering</param>
        public abstract void PushEventByTag(string memberTag, string data,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering);


        /// <summary>
        ///     Push an Event To Member Tag With Data and scheduler
        ///     NOTE : The Event will Trigger at the Specified Time.
        /// </summary>
        /// <param name="memberTag">(NOTNULL)Tag of Member You Want To Sent an Event to it</param>
        /// <param name="data">(NOTNULL)The main Event Data </param>
        /// <param name="schedulerTime">(NOTNULL)The Event Scheduler Time Data</param>
        /// <param name="pushEventBufferType">The Type of Event Buffering</param>
        public abstract void PushEventByTag(string memberTag, string data, SchedulerTime schedulerTime,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering);
    }
}
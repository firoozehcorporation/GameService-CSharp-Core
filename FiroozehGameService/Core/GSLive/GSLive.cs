// <copyright file="GSLive.cs" company="Firoozeh Technology LTD">
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


using System.Threading.Tasks;
using FiroozehGameService.Handlers;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.TurnBased;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    ///     Represents Game Service MultiPlayer System (GSLive)
    /// </summary>
    public class GSLive
    {
        private const string Tag = "GSLive";
        internal static GsHandler Handler;


        internal GSLive()
        {
            Handler = new GsHandler();
            RealTime = new GSLiveRT();
            Chat = new GSLiveChat();
            TurnBased = new GSLiveTB();
        }

        /// <summary>
        /// The GameService RealTime System
        /// </summary>
        public GSLiveRT RealTime { get; }
        
        /// <summary>
        /// The GameService TurnBased System
        /// </summary>
        public GSLiveTB TurnBased { get; }
        
        
        /// <summary>
        /// The GameService Chat System
        /// </summary>
        public GSLiveChat Chat { get; }


        public static async Task Init()
        {
            await Handler.Init();
        }

        /// <summary>
        ///     check if Command Services are Available
        /// </summary>
        /// <returns>returns true if Command Services are Available</returns>
        public bool IsCommandAvailable()
        {
            return CommandHandler.IsAvailable;
        }

        /// <summary>
        ///     check if Realtime Services are Available
        /// </summary>
        /// <returns>returns true if Realtime Services are Available</returns>
        public bool IsRealTimeAvailable()
        {
            return RealTimeHandler.IsAvailable;
        }


        /// <summary>
        ///     check if TurnBased Services are Available
        /// </summary>
        /// <returns>returns true if TurnBased Services are Available</returns>
        public bool IsTurnBasedAvailable()
        {
            return TurnBasedHandler.IsAvailable;
        }


        internal static void Dispose()
        {
            Handler?.Dispose();
        }
    }
}
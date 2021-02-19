// <copyright file="GsLiveProvider.cs" company="Firoozeh Technology LTD">
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


using System.Threading.Tasks;
using FiroozehGameService.Handlers;

namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service Multiplayer Provider (GSLive)
    /// </summary>
    public abstract class GsLiveProvider
    {
        internal abstract Task Init();

        internal abstract void Dispose();

        internal abstract GsHandler GetGsHandler();

        /// <summary>
        ///     The GameService RealTime System Provider
        /// </summary>
        public abstract GsLiveRealTimeProvider RealTime();


        /// <summary>
        ///     The GameService TurnBased System Provider
        /// </summary>
        public abstract GsLiveTurnBasedProvider TurnBased();


        /// <summary>
        ///     The GameService Chat System Provider
        /// </summary>
        public abstract GsLiveChatProvider Chat();


        /// <summary>
        ///     check if Command Services are Available
        /// </summary>
        /// <returns>returns true if Command Services are Available</returns>
        public abstract bool IsCommandAvailable();


        /// <summary>
        ///     check if Realtime Services are Available
        /// </summary>
        /// <returns>returns true if Realtime Services are Available</returns>
        public abstract bool IsRealTimeAvailable();


        /// <summary>
        ///     check if TurnBased Services are Available
        /// </summary>
        /// <returns>returns true if TurnBased Services are Available</returns>
        public abstract bool IsTurnBasedAvailable();
    }
}
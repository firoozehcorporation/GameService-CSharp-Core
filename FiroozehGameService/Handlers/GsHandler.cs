// <copyright file="GsHandler.cs" company="Firoozeh Technology LTD">
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
using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.TurnBased;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils.Serializer;

namespace FiroozehGameService.Handlers
{
    internal class GsHandler
    {
        internal GsHandler()
        {
            CommandHandler = new CommandHandler();
            CoreEventHandlers.GsLiveSystemStarted += OnJoinRoom;
            CoreEventHandlers.Dispose += OnDispose;
        }


        private void OnDispose(object sender, DisposeData disposeData)
        {
            switch (disposeData.Type)
            {
                case GSLiveType.RealTime:
                    RealTimeHandler?.Dispose(disposeData.IsGraceful);
                    RealTimeHandler = null;
                    GsSerializer.CurrentPlayerLeftRoom?.Invoke(this, null);
                    break;
                case GSLiveType.TurnBased:
                    TurnBasedHandler?.Dispose(disposeData.IsGraceful);
                    TurnBasedHandler = null;
                    break;
                case GSLiveType.NotSet:
                case GSLiveType.Command:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(disposeData), disposeData, null);
            }
        }

        private async void OnJoinRoom(object sender, StartPayload startPayload)
        {
            switch (startPayload.Room.GsLiveType)
            {
                case GSLiveType.TurnBased:
                    TurnBasedHandler = new TurnBasedHandler(startPayload);
                    await TurnBasedHandler.Init();
                    break;
                case GSLiveType.RealTime:
                    RealTimeHandler = new RealTimeHandler(startPayload);
                    RealTimeHandler.Init();
                    break;

                case GSLiveType.Command:
                case GSLiveType.NotSet:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        internal async Task Init()
        {
            await CommandHandler.Init();
        }

        internal void Dispose()
        {
            CommandHandler?.Dispose(false);
            RealTimeHandler?.Dispose(false);
            TurnBasedHandler?.Dispose(false);

            CommandHandler = null;
            RealTimeHandler = null;
            TurnBasedHandler = null;

            CoreEventHandlers.GsLiveSystemStarted = null;
            CoreEventHandlers.Dispose = null;

            try
            {
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region GsHandlerRegion

        public CommandHandler CommandHandler { get; private set; }
        public RealTimeHandler RealTimeHandler { get; private set; }
        public TurnBasedHandler TurnBasedHandler { get; private set; }

        #endregion
    }
}
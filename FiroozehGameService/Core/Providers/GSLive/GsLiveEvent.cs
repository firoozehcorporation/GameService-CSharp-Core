// <copyright file="GsLiveEvent.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLiveEvent : GsLiveEventProvider
    {
        public override void PushEventById(string memberId, string data,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (data.Length > EventConst.MaxDataLength)
                throw new GameServiceException("The data is Too Long, Max Data Length Is " +
                                               EventConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveEvent>(DebugLocation.Event, "PushEventById");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(PushEventHandler.Signature,
                new PushEventMessage(
                    PushEventSendType.MemberId, memberId, data, default, pushEventBufferType
                ));
        }

        public override void PushEventById(string memberId, string data, SchedulerTime schedulerTime)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            if (data.Length > EventConst.MaxDataLength)
                throw new GameServiceException("The data is Too Long, Max Data Length Is " +
                                               EventConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveEvent>(DebugLocation.Event, "PushEventById");

            if (schedulerTime == null)
                throw new GameServiceException("schedulerTime Cant Be Null").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventById");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(PushEventHandler.Signature,
                new PushEventMessage(
                    PushEventSendType.MemberId, memberId, data, schedulerTime.TimeInSecs,
                    PushEventBufferType.WithBuffering
                ));
        }

        public override void PushEventByTag(string memberTag, string data,
            PushEventBufferType pushEventBufferType = PushEventBufferType.NoBuffering)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (string.IsNullOrEmpty(memberTag))
                throw new GameServiceException("memberTag Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (data.Length > EventConst.MaxDataLength)
                throw new GameServiceException("The data is Too Long, Max Data Length Is " +
                                               EventConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveEvent>(DebugLocation.Event, "PushEventByTag");


            GameService.GSLive.GetGsHandler().CommandHandler.Send(PushEventHandler.Signature,
                new PushEventMessage(
                    PushEventSendType.MemberTag, memberTag, data, default, pushEventBufferType
                ));
        }

        public override void PushEventByTag(string memberTag, string data, SchedulerTime schedulerTime)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (string.IsNullOrEmpty(memberTag))
                throw new GameServiceException("memberTag Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be NullOrEmpty").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            if (data.Length > EventConst.MaxDataLength)
                throw new GameServiceException("The data is Too Long, Max Data Length Is " +
                                               EventConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveEvent>(DebugLocation.Event, "PushEventByTag");

            if (schedulerTime == null)
                throw new GameServiceException("schedulerTime Cant Be Null").LogException<GsLiveEvent>(
                    DebugLocation.Event, "PushEventByTag");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(PushEventHandler.Signature,
                new PushEventMessage(
                    PushEventSendType.MemberTag, memberTag, data, schedulerTime.TimeInSecs,
                    PushEventBufferType.WithBuffering
                ));
        }

        public override void GetBufferedPushEvents()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveEvent>(
                    DebugLocation.Event, "GetBufferedPushEvents");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(BufferedPushEventsHandler.Signature);
        }
    }
}
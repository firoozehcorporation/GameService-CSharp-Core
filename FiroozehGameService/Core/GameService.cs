// <copyright file="FiroozehGameService.cs" company="Firoozeh Technology LTD">
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
using System.Threading;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Core.Providers.BasicAPI;
using FiroozehGameService.Core.Providers.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.BasicApi.Social.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core
{
    /// <summary>
    ///     Represents Game Service Main Class
    /// </summary>
    public static class GameService
    {
        /// <summary>
        ///     Set configuration For Initialize Game Service.
        /// </summary>
        /// <param name="configuration">(NOTNULL)configuration For Initialize Game Service</param>
        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {
            if (configuration == null)
                throw new GameServiceException("Configuration Cant Be Null").LogException(typeof(GameService),
                    DebugLocation.Internal, "ConfigurationInstance");

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            SynchronizationContext = SynchronizationContext.Current;

            if (IsAuthenticated())
                throw new GameServiceException("Must Logout First To ReConfiguration").LogException(typeof(GameService),
                    DebugLocation.Internal, "ConfigurationInstance");

            Configuration = configuration;
            HandlerType = EventHandlerType.UnityContext;
            DownloadManager = new DownloadManager(Configuration);

            Achievement = new AchievementProvider();
            Assets = new AssetsProvider();
            Data = new DataProvider();
            Table = new TableProvider();
            CloudFunction = new CloudFunctionProvider();
            Leaderboard = new LeaderboardProvider();
            LoginOrSignUp = new LoginOrSignUpProvider();
            Player = new PlayerProvider();
            Save = new SaveProvider();

            GSLive = new GsLive();
            Social = new Social.Social();
        }


        /// <summary>
        ///     Set configuration For Game Service Debug System
        /// </summary>
        /// <param name="configuration">(NOTNULL)configuration For Game Service Debug System</param>
        public static void ConfigurationDebug(GameServiceDebugConfiguration configuration)
        {
            DebugConfiguration = configuration ??
                                 throw new GameServiceException("Configuration Cant Be Null").LogException(
                                     typeof(GameService), DebugLocation.Internal, "ConfigurationDebug");
        }


        /// <summary>
        ///     Set Event Handlers Type to Work Chat System with it
        /// </summary>
        /// <param name="handlerType">Type of Event Handler You Want to Set</param>
        public static void SetEventHandlersType(EventHandlerType handlerType)
        {
            HandlerType = handlerType;
        }


        /// <summary>
        ///     Check if Current User Authenticated
        /// </summary>
        /// <value> return True if Current User Authenticated Before </value>
        public static bool IsAuthenticated()
        {
            return IsAvailable;
        }


        /// <summary>
        ///     Get The Current GameService Version
        /// </summary>
        /// <value> return The Current GameService Version </value>
        public static string Version()
        {
            return "8.3.0";
        }


        /// <summary>
        ///     Logout From Game Service
        /// </summary>
        public static void Logout()
        {
            UserToken = null;
            CurrentInternalGame = null;
            PlayToken = null;
            CommandInfo = null;
            IsAvailable = false;
            IsGuest = false;
            GSLive?.Dispose();
            GsWebRequest.Dispose();
        }

        #region GameServiceProviderRegion

        /// <summary>
        ///     Represents Achievement Provider Model In Game Service Basic API
        /// </summary>
        public static IAchievementProvider Achievement { get; private set; }

        /// <summary>
        ///     Represents Leaderboard Provider Model In Game Service Basic API
        /// </summary>
        public static ILeaderboardProvider Leaderboard { get; private set; }

        /// <summary>
        ///     Represents Assets Provider Model In Game Service Basic API
        /// </summary>
        public static IAssetsProvider Assets { get; private set; }


        /// <summary>
        ///     Represents Data Provider In Game Service Basic API
        /// </summary>
        public static IDataProvider Data { get; private set; }


        /// <summary>
        ///     Represents Table Provider Model In Game Service Basic API
        /// </summary>
        public static ITableProvider Table { get; private set; }


        /// <summary>
        ///     Represents CloudFunction Provider Model In Game Service Basic API
        /// </summary>
        public static ICloudFunctionProvider CloudFunction { get; private set; }

        /// <summary>
        ///     Represents ILoginOrSignUpProvider Model In Game Service Basic API
        /// </summary>
        public static ILoginOrSignUpProvider LoginOrSignUp { get; private set; }


        /// <summary>
        ///     Represents Player Provider Model In Game Service Basic API
        /// </summary>
        public static IPlayerProvider Player { get; private set; }


        /// <summary>
        ///     Represents SaveGame Provider Data Model In Game Service Basic API
        /// </summary>
        public static ISaveProvider Save { get; private set; }


        /// <summary>
        ///     The GameService GsLive System Provider (like TurnBased , RealTime , ...)
        /// </summary>
        public static GsLiveProvider GSLive { get; private set; }

        /// <summary>
        ///     The GameService Social System Provider (like Friends , Parties)
        /// </summary>
        public static SocialProvider Social { get; private set; }

        #endregion


        #region GameServiceCoreRegion

        /// <summary>
        ///     Returns Debug Data
        /// </summary>
        public static EventHandler<Debug> OnDebugReceived;


        internal static bool IsAvailable;
        internal static bool IsGuest;

        internal static string UserToken;
        internal static string PlayToken;

        internal static InternalGame CurrentInternalGame;
        internal static EventHandlerType HandlerType;
        internal static CommandInfo CommandInfo;
        internal static SynchronizationContext SynchronizationContext;
        internal static GameServiceClientConfiguration Configuration { get; private set; }
        internal static GameServiceDebugConfiguration DebugConfiguration { get; private set; }

        internal static DownloadManager DownloadManager;

        #endregion
    }
}
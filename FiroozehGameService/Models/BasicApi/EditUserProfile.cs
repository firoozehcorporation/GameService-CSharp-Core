// <copyright file="EditUserProfile.cs" company="Firoozeh Technology LTD">
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

using System;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    ///     Represents EditUserProfile In Game Service Basic API
    /// </summary>
    [Serializable]
    public class EditUserProfile
    {
        /// <summary>
        ///     Set Allow Auto Add To Game For CurrentPlayer.
        /// </summary>
        /// <value> Allow Auto Add To Game For CurrentPlayer</value>
        internal bool AllowAutoAddToGame;

        /// <summary>
        ///     Set New ProfileLogo(BytesBuffer) For CurrentPlayer.
        /// </summary>
        /// <value> New ProfileLogo For CurrentPlayer</value>
        internal byte[] Logo;

        /// <summary>
        ///     Set New NickName For CurrentPlayer.
        /// </summary>
        /// <value> New NickName For CurrentPlayer</value>
        internal string NickName;

        /// <summary>
        ///     Set Show Group Activity For CurrentPlayer.
        /// </summary>
        /// <value> Allow Show Group Activity For CurrentPlayer</value>
        internal bool ShowGroupActivity;

        /// <summary>
        ///     Set Show Public Activity For CurrentPlayer.
        /// </summary>
        /// <value> Allow Show Public Activity For CurrentPlayer</value>
        internal bool ShowPublicActivity;


        /// <summary>
        ///     EditUserProfile Data Model
        /// </summary>
        /// <param name="nickName">the value of nickName you want to update</param>
        /// <param name="logo">the value of logo you want to update</param>
        /// <param name="allowAutoAddToGame">the value of allowAutoAddToGame you want to update</param>
        /// <param name="showPublicActivity">the value of showPublicActivity you want to update</param>
        /// <param name="showGroupActivity">the value of showGroupActivity you want to update</param>
        public EditUserProfile(
            string nickName = null,
            byte[] logo = null,
            bool allowAutoAddToGame = false,
            bool showPublicActivity = false,
            bool showGroupActivity = false
        )
        {
            NickName = nickName;
            Logo = logo;
            AllowAutoAddToGame = allowAutoAddToGame;
            ShowPublicActivity = showPublicActivity;
            ShowGroupActivity = showGroupActivity;
        }
    }
}
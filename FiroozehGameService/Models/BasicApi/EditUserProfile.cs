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
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    /// Represents EditUserProfile In Game Service Basic API
    /// </summary>
    [Serializable]
    public class EditUserProfile
    {
        
        /// <summary>
        /// Set New NickName For CurrentPlayer.
        /// </summary>
        /// <value> New NickName For CurrentPlayer</value>
        public string NickName { get; }
        
        /// <summary>
        /// Set New ProfileLogo(BytesBuffer) For CurrentPlayer.
        /// </summary>
        /// <value> New ProfileLogo For CurrentPlayer</value>
        public byte[] Logo { get; }
                       
        /// <summary>
        /// Set Allow Auto Add To Game For CurrentPlayer.
        /// </summary>
        /// <value> Allow Auto Add To Game For CurrentPlayer</value>
        public bool AllowAutoAddToGame { get; }
        
        /// <summary>
        /// Set Show Public Activity For CurrentPlayer.
        /// </summary>
        /// <value> Allow Show Public Activity For CurrentPlayer</value>
        public bool ShowPublicActivity { get; }
        
        /// <summary>
        /// Set Show Group Activity For CurrentPlayer.
        /// </summary>
        /// <value> Allow Show Group Activity For CurrentPlayer</value>
        public bool ShowGroupActivity { get; }
        
        
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
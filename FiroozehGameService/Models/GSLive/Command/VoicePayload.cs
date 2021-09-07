// <copyright file="VoicePayload.cs" company="Firoozeh Technology LTD">
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
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class VoicePayload : Payload
    {
        [JsonProperty("2")] internal string Desc;
        [JsonProperty("6")] internal string Ice;
        [JsonProperty("0")] internal string Id;
        [JsonProperty("8")] internal bool IsDeafen;
        [JsonProperty("7")] internal bool IsMute;
        [JsonProperty("9")] internal bool IsPermanent;
        [JsonProperty("3")] internal string Key;
        [JsonProperty("4")] internal string MemberId;
        [JsonProperty("1")] internal string Name;
        [JsonProperty("5")] internal string Sdp;

        internal VoicePayload(string id = null, string name = null, string desc = null,
            string key = null, string memberId = null, string sdp = null, string ice = null, bool isMute = false,
            bool isDeafen = false, bool isPermanent = false)
        {
            Id = id;
            Name = name;
            Desc = desc;
            Key = key;
            MemberId = memberId;
            Sdp = sdp;
            Ice = ice;
            IsMute = isMute;
            IsDeafen = isDeafen;
            IsPermanent = isPermanent;
        }
    }
}
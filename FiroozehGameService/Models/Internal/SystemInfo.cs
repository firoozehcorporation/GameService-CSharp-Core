// <copyright file="SystemInfo.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.Internal
{
    /// <summary>
    ///     Represents System Info In Game Service
    /// </summary>
    [Serializable]
    public class SystemInfo
    {
        [JsonProperty("DeviceModel")] public string DeviceModel;
        [JsonProperty("DeviceName")] public string DeviceName;
        [JsonProperty("DeviceType")] public string DeviceType;
        [JsonProperty("DeviceId")] public string DeviceUniqueId;
        [JsonProperty("GraphicsDeviceName")] public string GraphicsDeviceName;
        [JsonProperty("GraphicsDeviceVendor")] public string GraphicsDeviceVendor;
        [JsonProperty("GraphicsMemorySize")] public int GraphicsMemorySize;
        [JsonProperty("NetworkType")] public string NetworkType;
        [JsonProperty("OperatingSystem")] public string OperatingSystem;
        [JsonProperty("ProcessorCount")] public int ProcessorCount;
        [JsonProperty("ProcessorFrequency")] public int ProcessorFrequency;
        [JsonProperty("ProcessorType")] public string ProcessorType;
    }
}
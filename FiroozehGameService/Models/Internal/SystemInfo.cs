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
    /// Represents System Info In Game Service
    /// </summary>
    [Serializable]
    public class SystemInfo
    {
        [JsonProperty("DeviceName")]
        public string DeviceName { internal get; set; }
        
        [JsonProperty("DeviceModel")]
        public string DeviceModel { internal get; set; }
        
        [JsonProperty("DeviceType")]
        public string DeviceType { internal get; set; }
        
        [JsonProperty("OperatingSystem")]
        public string OperatingSystem { internal get; set; }
        
        [JsonProperty("NetworkType")]
        public string NetworkType { internal get; set; }
        
        [JsonProperty("ProcessorType")]
        public string ProcessorType  { internal get; set; }
        
        [JsonProperty("ProcessorCount")]
        public int ProcessorCount  { internal get; set; }
        
        [JsonProperty("ProcessorFrequency")]
        public int ProcessorFrequency  { internal get; set; }
        
        [JsonProperty("GraphicsDeviceName")]
        public string GraphicsDeviceName  { internal get; set; }
        
        [JsonProperty("GraphicsDeviceVendor")]
        public string GraphicsDeviceVendor  { internal get; set; }
        
        [JsonProperty("GraphicsMemorySize")]
        public int GraphicsMemorySize  { internal get; set; }
        
    }
}
// <copyright file="GsSerializer.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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
using System.Collections.Generic;
using FiroozehGameService.Models;
using FiroozehGameService.Utils.Serializer.Abstracts;
using FiroozehGameService.Utils.Serializer.Helpers;
using FiroozehGameService.Utils.Serializer.Interfaces;
using FiroozehGameService.Utils.Serializer.Utils;

namespace FiroozehGameService.Utils.Serializer
{
    /// <summary>
    /// Represents GsSerializer In Game Service Binary Serializer
    /// </summary>
    public static class GsSerializer
    {
        /// <summary>
        /// Represents TypeRegistry In Gs Serializer Class
        /// </summary>
        public static class TypeRegistry
        {
            
            /// <summary>
            /// NOTE : Dont Use This Function , This Function Called By GsLiveRealtime SDK.
            /// Dispose The TypeUtil
            /// </summary>
            public static void Dispose()
            {
                TypeUtil.Dispose();
            }

            
            /// <summary>
            /// Register New Type You Want To Working With Game Service Serializer
            /// </summary>
            /// <param name="serializer"> Your Object Serializer</param>
            /// <typeparam name="T"> Your Object Base Type</typeparam>
            /// <exception cref="GameServiceException"> Throw If invalid Action Happened</exception>
            public static void RegisterSerializer<T>(ObjectSerializer<T> serializer)
            {
                TypeUtil.RegisterNewType(serializer);
            }
        }
        
        /// <summary>
        /// NOTE : Dont Use This Class Functions , Functions Calls By GsLiveRealtime SDK.
        /// Represents Function In Gs Serializer Class
        /// </summary>
        public static class Function
        {
            /// <summary>
            /// NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
            /// Get Buffer For object params
            /// </summary>
            /// <param name="data">the Objects</param>
            /// <returns></returns>
            /// <exception cref="GameServiceException">Throw If invalid Action Happened</exception>
            public static byte[] SerializeParams(params object[] data)
            {
                try
                {
                    var stream = TypeUtil.GetWriteStreamForParams(data);
                    return SerializerUtil.Serialize(stream);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Get Parameters From Buffer
            /// </summary>
            /// <param name="buffer">The Parameter Buffer</param>
            /// <returns>the Parameters</returns>
            public static object[] DeserializeParams(byte[] buffer)
            {
                var objects = new List<object>();
                var readStream = SerializerUtil.Deserialize(buffer);
                while (readStream.CanRead())
                    objects.Add(readStream.ReadNext());

                return objects.ToArray();
            }

            /// <summary>
            /// NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
            /// Get String Object Types
            /// </summary>
            /// <param name="parameters">the Objects</param>
            /// <returns></returns>
            public static string GetParameterTypes(object[] parameters)
            {
                return TypeUtil.GetParameterTypes(parameters);
            }
        }
        
        
        /// <summary>
        /// NOTE : Dont Use This Class Functions , Functions Calls By GsLiveRealtime SDK.
        /// Represents Object In Gs Serializer Class
        /// </summary>
        public static class Object
        {
            /// <summary>
            /// NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
            /// Get Buffer From IGsLiveSerializable
            /// </summary>
            /// <param name="serializable"> The serializable Object</param>
            /// <returns></returns>
            /// <exception cref="GameServiceException">Throw If invalid Action Happened</exception>
            public static byte[] GetBuffer(IGsLiveSerializable serializable)
            {
                if(serializable == null)
                    throw new GameServiceException("GsSerializer Err -> serializable cant be Null");
            
                var writeStream = new GsWriteStream();
                serializable.OnGsLiveWrite(writeStream);
                return SerializerUtil.Serialize(writeStream);
            }

            /// <summary>
            /// NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
            /// Update a Serializable Object With Buffer
            /// </summary>
            /// <param name="serializable">Serializable Object</param>
            /// <param name="buffer">the Update Buffer</param>
            public static void CallReadStream(IGsLiveSerializable serializable,byte[] buffer)
            {
                serializable?.OnGsLiveRead(GetReadStream(buffer));
            }
            
            
            private static GsReadStream GetReadStream(byte[] buffer)
            {
                if(buffer == null)
                    throw new GameServiceException("GsSerializer Err -> buffer cant be Null");

                return SerializerUtil.Deserialize(buffer);
            }
            
        }
        
    }
}
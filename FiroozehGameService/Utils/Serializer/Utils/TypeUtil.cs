// <copyright file="TypeUtil.cs" company="Firoozeh Technology LTD">
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
using System.Linq;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils.Serializer.Abstracts;
using FiroozehGameService.Utils.Serializer.Helpers;

namespace FiroozehGameService.Utils.Serializer.Utils
{
    /// <summary>
    ///     Represents TypeUtil In Game Service Binary Serializer
    /// </summary>
    internal static class TypeUtil
    {
        private static readonly Dictionary<int, BaseSerializer> ObjectsCache = new Dictionary<int, BaseSerializer>();
        private static readonly Dictionary<int, Type> HashToType = new Dictionary<int, Type>();
        private static readonly Dictionary<Type, int> TypeToHash = new Dictionary<Type, int>();

        /// <summary>
        ///     Register New Type You Want To Working With Game Service Serializer
        /// </summary>
        /// <param name="serializer"> Your Object Serializer</param>
        /// <typeparam name="T"> Your Object Base Type</typeparam>
        /// <exception cref="GameServiceException"> Throw If invalid Action Happened</exception>
        internal static void RegisterNewType<T>(ObjectSerializer<T> serializer)
        {
            var type = typeof(T);
            if (TypeToHash.ContainsKey(type))
                throw new GameServiceException("The Type " + type + " is Exist!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "RegisterNewType");


            var typeHash = HashUtil.GetHashFromType(type);
            if (HashToType.ContainsKey(typeHash))
                throw new GameServiceException("The Type " + type + " Hash is Exist!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "RegisterNewType");


            TypeToHash.Add(type, typeHash);
            HashToType.Add(typeHash, type);
            ObjectsCache.Add(typeHash, serializer);
        }


        /// <summary>
        ///     NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
        ///     Internal Get Hash And WriteStream of Registered Object
        /// </summary>
        /// <param name="obj"> Your Object You Want To Get Stream</param>
        /// <returns></returns>
        /// <exception cref="GameServiceException">Throw If invalid Action Happened</exception>
        internal static Tuple<int, GsWriteStream> GetWriteStream(object obj)
        {
            var type = obj.GetType();

            if (!TypeToHash.ContainsKey(type))
                throw new GameServiceException("The Type " + type + " is Not Registered as New Type!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetWriteStream");


            var serializer = ObjectsCache[TypeToHash[type]];

            if (!serializer.CanSerializeModel(obj))
                throw new GameServiceException("The Type " + type + " Not Serializable!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetWriteStream");


            var writeStream = new GsWriteStream();
            serializer.SerializeObject(obj, writeStream);
            return Tuple.Create(TypeToHash[type], writeStream);
        }


        /// <summary>
        ///     NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
        ///     Get Object From Hash and GsReadStream
        /// </summary>
        /// <param name="hash">the Object Hash</param>
        /// <param name="readStream">The Read Stream To Read Object</param>
        /// <returns></returns>
        /// <exception cref="GameServiceException">Throw If invalid Action Happened</exception>
        internal static object GetFinalObject(int hash, GsReadStream readStream)
        {
            if (!HashToType.ContainsKey(hash))
                throw new GameServiceException("Type With Hash " + hash + " is Not Registered!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetFinalObject");


            var serializer = ObjectsCache[hash];
            if (!serializer.CanSerializeModel(HashToType[hash]))
                throw new GameServiceException("Type With Hash " + hash + " is Not Serializable!")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetFinalObject");

            return serializer.DeserializeObject(readStream);
        }


        /// <summary>
        ///     NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
        ///     Get WriteStream For object params
        /// </summary>
        /// <param name="data">the Objects</param>
        /// <returns></returns>
        /// <exception cref="GameServiceException">Throw If invalid Action Happened</exception>
        internal static GsWriteStream GetWriteStreamFromParams(params object[] data)
        {
            if (data == null)
                throw new GameServiceException("Params Cant Be Null")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetWriteStreamFromParams");

            if (data.Length == 0)
                throw new GameServiceException("Params Cant Be Empty")
                    .LogException(typeof(TypeUtil), DebugLocation.RealTime, "GetWriteStreamFromParams");


            var writeStream = new GsWriteStream();
            foreach (var obj in data)
                writeStream.WriteNext(obj);

            return writeStream;
        }


        /// <summary>
        ///     NOTE : Dont Use This Function, This Function Called By GsLiveRealtime SDK.
        ///     Get String Object Types
        /// </summary>
        /// <param name="parameters">the Objects</param>
        /// <returns></returns>
        internal static string GetParameterTypes(object[] parameters)
        {
            var typeString = "(";
            if (parameters == null)
                return typeString + ")";

            typeString =
                parameters.Aggregate(typeString, (current, parameter) => current + (parameter.GetType() + ","));
            return typeString.Remove(typeString.Length - 1) + ")";
        }

        /// <summary>
        ///     NOTE : Dont Use This Function , This Function Called By GsLiveRealtime SDK.
        ///     Dispose The TypeUtil
        /// </summary>
        internal static void Dispose()
        {
            ObjectsCache?.Clear();
            HashToType?.Clear();
            TypeToHash?.Clear();
        }

        internal static bool HaveType(object obj)
        {
            return TypeToHash.ContainsKey(obj.GetType());
        }
    }
}
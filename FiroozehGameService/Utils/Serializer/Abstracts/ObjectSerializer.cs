// <copyright file="ObjectSerializer.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Utils.Serializer.Helpers;

namespace FiroozehGameService.Utils.Serializer.Abstracts
{
    /// <summary>
    ///     Represents ObjectSerializer In Game Service Binary Serializer
    ///     You Can Make Your Object Serializable With Implement this Class
    /// </summary>
    /// <typeparam name="T"> The Type of Object You Want To Serializable</typeparam>
    public abstract class ObjectSerializer<T> : BaseSerializer
    {
        internal override bool CanSerializeModel(object obj)
        {
            return obj.GetType() == typeof(T);
        }

        internal override bool CanSerializeModel(Type type)
        {
            return type == typeof(T);
        }

        internal override void SerializeObject(object obj, GsWriteStream writeStream)
        {
            WriteObject((T) obj, writeStream);
        }

        internal override object DeserializeObject(GsReadStream readStream)
        {
            return ReadObject(readStream);
        }


        /// <summary>
        ///     the WriteObject Function You Can Write Every things Of T To writeStream
        /// </summary>
        /// <param name="obj">Your Serializable Object</param>
        /// <param name="writeStream">The Write Stream for Writes Data</param>
        protected abstract void WriteObject(T obj, GsWriteStream writeStream);

        /// <summary>
        ///     the ReadObject Function You Can Read Every things Writes
        /// </summary>
        /// <param name="readStream">he Write Stream for Reads Data</param>
        /// <returns></returns>
        protected abstract T ReadObject(GsReadStream readStream);
    }
}
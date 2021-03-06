// <copyright file="BaseSerializer.cs" company="Firoozeh Technology LTD">
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
    ///     Represents BaseSerializer In Game Service Binary Serializer
    /// </summary>
    public abstract class BaseSerializer
    {
        internal BaseSerializer()
        {
        }

        internal abstract void SerializeObject(object obj, GsWriteStream writeStream);

        internal abstract object DeserializeObject(GsReadStream readStream);

        internal abstract bool CanSerializeModel(object obj);

        internal abstract bool CanSerializeModel(Type type);
    }
}
// <copyright file="FindByElement.cs" company="Firoozeh Technology LTD">
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

using System;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.BasicApi.Buckets.Options
{
    /// <summary>
    ///     Represents ElementOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class FindByElement<T> : BucketOption
    {
        private string _name;
        private T _value;

        /// <summary>
        ///     FindByElement BucketOption
        /// </summary>
        /// <param name="name">the name of Element you want to find it</param>
        /// <param name="value">the value of Element you want to find it</param>
        public FindByElement(string name, T value)
        {
            _name = string.IsNullOrEmpty(name)
                ? throw new GameServiceException("Name Cant Be EmptyOrNull").LogException<FindByElement<T>>(
                    DebugLocation.Internal, "Constructor")
                : _name = name;
            _value = value == null
                ? throw new GameServiceException("Value Cant Be Null").LogException<FindByElement<T>>(
                    DebugLocation.Internal, "Constructor")
                : _value = value;
        }

        internal override string GetParsedData()
        {
            return "&conditionProperty=" + _name + "&conditionValue=" + _value;
        }
    }
}
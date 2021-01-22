// <copyright file="ObjectMatcher.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.Buckets;
using FiroozehGameService.Utils;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets.Matcher
{
    /// <summary>
    ///     Represents ObjectMatcher Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class ObjectMatcher : MatcherCore
    {
        private string _name;
        private ObjectMatcherTypes _type;
        private object _value;


        /// <summary>
        ///     the ObjectMatcher Aggregation
        /// </summary>
        /// <param name="name">the name of Element you want to Match it</param>
        /// <param name="value">the value of Element you want to Match it</param>
        /// <param name="matcherType">The Type of Object Match</param>
        public ObjectMatcher(string name, object value, ObjectMatcherTypes matcherType)
        {
            _name = string.IsNullOrEmpty(name)
                ? throw new GameServiceException("Name Cant Be EmptyOrNull").LogException<ObjectMatcher>(
                    DebugLocation.Internal, "Constructor")
                : _name = name;
            _value = value == null
                ? throw new GameServiceException("Value Cant Be Null").LogException<ObjectMatcher>(
                    DebugLocation.Internal, "Constructor")
                : _value = value;
            _type = matcherType;
        }

        internal override KeyValuePair<string, List<object>> GetMatcher()
        {
            return new KeyValuePair<string, List<object>>(_type.ToStringType(), new List<object> {_name, _value});
        }
    }
}
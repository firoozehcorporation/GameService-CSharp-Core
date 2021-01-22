// <copyright file="NumberMatcher.cs" company="Firoozeh Technology LTD">
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
    ///     Represents NumberMatcher Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class NumberMatcher : MatcherCore
    {
        private string _name;
        private object _numberValue;
        private NumberMatcherTypes _type;


        /// <summary>
        ///     the NumberMatcher Aggregation
        /// </summary>
        /// <param name="name">the name of Element you want to Match it</param>
        /// <param name="numberValue">the numberValue of Element you want to Match it</param>
        /// <param name="matcherType">The Type of Number Match</param>
        public NumberMatcher(string name, object numberValue, NumberMatcherTypes matcherType)
        {
            _name = string.IsNullOrEmpty(name)
                ? throw new GameServiceException("Name Cant Be EmptyOrNull").LogException<NumberMatcher>(
                    DebugLocation.Internal, "Constructor")
                : _name = name;

            if (!BucketUtil.ValidateNumber(numberValue))
                throw new GameServiceException("NumberValue Must Be Have Number Type").LogException<NumberMatcher>(
                    DebugLocation.Internal, "Constructor");

            _numberValue = numberValue == null
                ? throw new GameServiceException("Value Cant Be Null").LogException<NumberMatcher>(
                    DebugLocation.Internal, "Constructor")
                : _numberValue = numberValue;
            _type = matcherType;
        }

        internal override Dictionary<string, List<object>> GetMatcher()
        {
            return new Dictionary<string, List<object>>
                {{_type.ToStringType(), new List<object> {_name, _numberValue}}};
        }
    }
}
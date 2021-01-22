// <copyright file="MatchAggregation.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi.Buckets.Matcher;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets.Aggregations
{
    /// <summary>
    ///     Represents MatchAggregation Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class MatchAggregation : AggregationCore
    {
        private MatcherCore _matcher;

        /// <summary>
        ///     the MatchAggregation Data Class
        /// </summary>
        /// <param name="matcher"></param>
        public MatchAggregation(MatcherCore matcher)
        {
            _matcher = matcher == null
                ? throw new GameServiceException("Matcher Cant Be Null").LogException<MatchAggregation>(
                    DebugLocation.Internal, "Constructor")
                : _matcher = matcher;
        }

        internal override List<Dictionary<string, object>> GetAggregation()
        {
            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> {{"->match", _matcher.GetMatcher()}}
            };
        }
    }
}
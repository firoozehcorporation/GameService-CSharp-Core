// <copyright file="And.cs" company="Firoozeh Technology LTD">
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
using System.Linq;
using FiroozehGameService.Models.BasicApi.Buckets.Matcher;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets.Operators
{
    /// <summary>
    ///     Represents And Operator Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class And : MatcherCore
    {
        private MatcherCore[] _matchers;

        /// <summary>
        ///     the And Aggregation
        /// </summary>
        /// <param name="matchers">the matchers That you Want Match Them With And Operator</param>
        public And(params MatcherCore[] matchers)
        {
            if (matchers == null)
                throw new GameServiceException("Matchers Cant Be Null").LogException<And>(DebugLocation.Internal,
                    "Constructor");
            if (matchers.Length < 2)
                throw new GameServiceException("And Operator Must Have Two or More Matchers").LogException<And>(
                    DebugLocation.Internal,
                    "Constructor");

            _matchers = matchers;
        }

        internal override Dictionary<string, List<object>> GetMatcher()
        {
            var matchersData = _matchers.Select(matcher => matcher.GetMatcher()).Cast<object>().ToList();
            return new Dictionary<string, List<object>> {{"->and", matchersData}};
        }
    }
}
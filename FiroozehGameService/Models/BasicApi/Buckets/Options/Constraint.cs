// <copyright file="Constraint.cs" company="Firoozeh Technology LTD">
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
    ///     Represents ConstraintOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Constraint : BucketOption
    {
        private int _limit;
        private int _skip;

        /// <summary>
        ///     Constraint BucketOption
        /// </summary>
        /// <param name="skip">Skip Value for Constraint BucketOption</param>
        /// <param name="limit">Limit Value for Constraint BucketOption</param>
        public Constraint(int skip, int limit)
        {
            _skip = skip < 0
                ? throw new GameServiceException("Invalid Skip Value").LogException<Constraint>(DebugLocation.Internal,
                    "Constructor")
                : _skip = skip;
            _limit = limit <= 0 || limit > 200
                ? throw new GameServiceException("Invalid Limit Value, Value must Between 0 and 200")
                    .LogException<Constraint>(DebugLocation.Internal, "Constructor")
                : _limit = limit;
        }

        internal override string GetParsedData()
        {
            return "&skip=" + _skip + "&limit=" + _limit;
        }
    }
}
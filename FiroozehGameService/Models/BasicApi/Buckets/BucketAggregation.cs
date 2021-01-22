// <copyright file="BucketAggregation.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi.Buckets.Aggregations;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets
{
    /// <summary>
    ///     Represents BucketAggregation Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class BucketAggregation
    {
        internal BucketAggregationBuilder Builder;

        private BucketAggregation()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="bucketId">the bucket id to create this aggregation</param>
        /// <returns></returns>
        public static BucketAggregationBuilder Of(string bucketId)
        {
            return new BucketAggregationBuilder(bucketId);
        }

        internal static BucketAggregation From(BucketAggregationBuilder builder)
        {
            return new BucketAggregation {Builder = builder};
        }

        /// <summary>
        ///     Represents BucketAggregationBuilder Model In Game Service Basic API
        /// </summary>
        public class BucketAggregationBuilder
        {
            internal readonly string BucketId;

            private ConstraintAggregation _constraintAggregation;
            private MatchAggregation _matchAggregation;
            private ProjectAggregation _projectAggregation;
            private SortAggregation _sortAggregation;
            internal string AggregationData;

            internal BucketAggregationBuilder(string bucketId)
            {
                BucketId = bucketId;
            }

            /// <summary>
            ///     Set MatchAggregation For this BucketAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public BucketAggregationBuilder Match(MatchAggregation aggregation)
            {
                if (_matchAggregation != null)
                    throw new GameServiceException("MatchAggregation Has Already Been Set")
                        .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "Match");

                _matchAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "Match");
                return this;
            }


            /// <summary>
            ///     Set SortAggregation For this BucketAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public BucketAggregationBuilder WithSort(SortAggregation aggregation)
            {
                if (_sortAggregation != null)
                    throw new GameServiceException("SortAggregation Has Already Been Set")
                        .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithSort");

                _sortAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithSort");
                return this;
            }

            /// <summary>
            ///     Set ConstraintAggregation For this BucketAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public BucketAggregationBuilder WithConstraint(ConstraintAggregation aggregation)
            {
                if (_constraintAggregation != null)
                    throw new GameServiceException("ConstraintAggregation Has Already Been Set")
                        .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithConstraint");

                _constraintAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithConstraint");
                return this;
            }


            /// <summary>
            ///     Set ProjectAggregation For this BucketAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public BucketAggregationBuilder WithProject(ProjectAggregation aggregation)
            {
                if (_projectAggregation != null)
                    throw new GameServiceException("ProjectAggregation Has Already Been Set")
                        .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithProject");

                _projectAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "WithProject");
                return this;
            }


            /// <summary>
            ///     Build This BucketAggregationBuilder
            /// </summary>
            public BucketAggregation Build()
            {
                var data = new List<Dictionary<string, object>>();

                if (_matchAggregation == null)
                    throw new GameServiceException("MatchAggregation must be set")
                        .LogException<BucketAggregationBuilder>(DebugLocation.Internal, "GenerateAggregation");

                data.AddRange(_matchAggregation.GetAggregation());

                if (_sortAggregation != null) data.AddRange(_sortAggregation.GetAggregation());
                if (_constraintAggregation != null) data.AddRange(_constraintAggregation.GetAggregation());
                if (_projectAggregation != null) data.AddRange(_projectAggregation.GetAggregation());

                AggregationData = JsonConvert.SerializeObject(data);
                return From(this);
            }
        }
    }
}
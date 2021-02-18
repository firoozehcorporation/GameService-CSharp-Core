// <copyright file="DBaaSAggregation.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Models.BasicApi.DBaaS.Aggregations;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.DBaaS
{
    /// <summary>
    ///     Represents DBaaSAggregation Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class DBaaSAggregation
    {
        internal DBaaSAggregationBuilder Builder;

        private DBaaSAggregation()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="tableId">the table id to create this aggregation</param>
        /// <returns></returns>
        public static DBaaSAggregationBuilder Of(string tableId)
        {
            return new DBaaSAggregationBuilder(tableId);
        }

        internal static DBaaSAggregation From(DBaaSAggregationBuilder builder)
        {
            return new DBaaSAggregation {Builder = builder};
        }

        /// <summary>
        ///     Represents DBaaSAggregationBuilder Model In Game Service Basic API
        /// </summary>
        public class DBaaSAggregationBuilder
        {
            internal readonly string tableId;

            private ConstraintAggregation _constraintAggregation;
            private MatchAggregation _matchAggregation;
            private ProjectAggregation _projectAggregation;
            private SortAggregation _sortAggregation;
            internal string AggregationData;

            internal DBaaSAggregationBuilder(string tableId)
            {
                this.tableId = tableId;
            }

            /// <summary>
            ///     Set MatchAggregation For this DBaaSAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public DBaaSAggregationBuilder WithMatch(MatchAggregation aggregation)
            {
                if (_matchAggregation != null)
                    throw new GameServiceException("MatchAggregation Has Already Been Set")
                        .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "Match");

                _matchAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "Match");
                return this;
            }


            /// <summary>
            ///     Set SortAggregation For this DBaaSAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public DBaaSAggregationBuilder WithSort(SortAggregation aggregation)
            {
                if (_sortAggregation != null)
                    throw new GameServiceException("SortAggregation Has Already Been Set")
                        .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithSort");

                _sortAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithSort");
                return this;
            }

            /// <summary>
            ///     Set ConstraintAggregation For this DBaaSAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public DBaaSAggregationBuilder WithConstraint(ConstraintAggregation aggregation)
            {
                if (_constraintAggregation != null)
                    throw new GameServiceException("ConstraintAggregation Has Already Been Set")
                        .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithConstraint");

                _constraintAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithConstraint");
                return this;
            }


            /// <summary>
            ///     Set ProjectAggregation For this DBaaSAggregationBuilder
            /// </summary>
            /// <returns></returns>
            public DBaaSAggregationBuilder WithProject(ProjectAggregation aggregation)
            {
                if (_projectAggregation != null)
                    throw new GameServiceException("ProjectAggregation Has Already Been Set")
                        .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithProject");

                _projectAggregation = aggregation ?? throw new GameServiceException("Aggregation Cant Be Null")
                    .LogException<DBaaSAggregationBuilder>(DebugLocation.Internal, "WithProject");
                return this;
            }


            /// <summary>
            ///     Build This DBaaSAggregationBuilder
            /// </summary>
            public DBaaSAggregation Build()
            {
                var data = new List<Dictionary<string, object>>();

                if (_matchAggregation != null) data.AddRange(_matchAggregation.GetAggregation());
                if (_sortAggregation != null) data.AddRange(_sortAggregation.GetAggregation());
                if (_constraintAggregation != null) data.AddRange(_constraintAggregation.GetAggregation());
                if (_projectAggregation != null) data.AddRange(_projectAggregation.GetAggregation());

                AggregationData = JsonConvert.SerializeObject(data);
                return From(this);
            }
        }
    }
}
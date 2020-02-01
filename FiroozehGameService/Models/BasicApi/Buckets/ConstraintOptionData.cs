// <copyright file="ConstraintOptionData.cs" company="Firoozeh Technology LTD">
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

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets
{
    /// <summary>
    /// Represents ConstraintOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class ConstraintOptionData : BucketOption
    {
        private int Skip { get; }
        private int Limit { get; }

        public ConstraintOptionData(int skip, int limit)
        {
            Skip = skip < 0 ? throw new GameServiceException("Invalid Skip Value") : Skip = skip;
            Limit = limit <= 0 || limit > 200 ? throw new GameServiceException("Invalid Limit Value") : Limit = limit;
        }
        
        internal override string GetParsedData()
        {
            return "&skip=" + Skip + "&limit=" + Limit;
        }
    }
}
// <copyright file="QueryData.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Specialized;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class QueryData
    {
        internal int Limit;
        internal string Query;
        internal int Skip;

        internal QueryData(string query = null, int skip = 0, int limit = 0)
        {
            Query = query;
            Skip = skip;
            if (limit > 25)
                throw new GameServiceException("QueryData Limit is More Than 25").LogException(typeof(QueryData),
                    DebugLocation.Internal, "Constructor");
            Limit = limit;
        }

        internal NameValueCollection ToCollection()
        {
            var collection = new NameValueCollection();

            if (Query != null) collection.Add("q", Query);
            collection.Add("skip", Skip.ToString());
            collection.Add("limit", Limit.ToString());

            return collection;
        }
    }
}
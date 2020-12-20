// <copyright file="UrlUtil.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
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


using System.Collections.Specialized;
using System.Linq;
using FiroozehGameService.Models.BasicApi.Buckets;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Utils
{
    internal static class UrlUtil
    {
        internal static string ParseBucketUrl(string bucketId,bool isGlobal,BucketOption[] options)
        {
            var first = true;
            string url;
            
            if(isGlobal) url = Api.BucketNonPermission + bucketId;
            else url = Api.Bucket + bucketId;
            
            if (options == null) return url;

            url += "?";

            foreach (var option in options)
                if (first)
                {
                    url += option.GetParsedData().Substring(1);
                    first = false;
                }
                else
                    url += option.GetParsedData();
                

            return url;
        }
        
        
        internal static string ToQueryString(this QueryData queryData)
        {
            return queryData.ToCollection().ToQueryString();
        }
        
        
        private static string ToQueryString(this NameValueCollection nvc)
        {
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select $"{key}={value}"
            ).ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
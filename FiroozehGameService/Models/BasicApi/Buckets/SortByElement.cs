// <copyright file="SortByElement.cs" company="Firoozeh Technology LTD">
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

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.BasicApi.Buckets
{
    /// <summary>
    /// Represents SortByOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class SortByElement : BucketOption
    {
        private string ElementName { get; }
        private BucketSortOrder SortOrder { get; }

        public SortByElement(string elementName, BucketSortOrder sortOrder)
        {
            ElementName = string.IsNullOrEmpty(elementName) ? throw new GameServiceException("ElementName Cant Be EmptyOrNull") : ElementName = elementName;
            SortOrder = sortOrder;
        }
        
        internal override string GetParsedData()
        {
            return "&sortby=" + ElementName + "&sort=" + (int)SortOrder;
        }
    }
}
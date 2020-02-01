// <copyright file="ElementOptionData.cs" company="Firoozeh Technology LTD">
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
    /// Represents ElementOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class ElementOptionData<T> : BucketOption
    {
        private string Name { get; }
        private T Value { get; }

        public ElementOptionData(string name, T value)
        {
            Name = string.IsNullOrEmpty(name) ? throw new GameServiceException("Name Cant Be EmptyOrNull") : Name = name;
            Value = value == null ? throw new GameServiceException("Value Cant Be Null") : Value = value;
        }

        internal override string GetParsedData()
        {
            return "&conditionProperty=" + Name + "&conditionValue=" + Value;
        }
    }
}
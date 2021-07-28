// <copyright file="AbstractValue.cs" company="Firoozeh Technology LTD">
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


/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive
{
    /// <summary>
    ///     Represents AbstractValue Class
    /// </summary>
    public class AbstractValue<T>
    {
        /// <summary>
        ///     the Value Data
        /// </summary>
        internal readonly T Value;


        internal AbstractValue()
        {
        }

        /// <summary>
        ///     Abstract Value Constructor Function
        /// </summary>
        /// <param name="value"></param>
        internal AbstractValue(T value)
        {
            Value = value;
        }
    }
}
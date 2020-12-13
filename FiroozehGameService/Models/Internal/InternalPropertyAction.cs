// <copyright file="InternalPropertyAction.cs" company="Firoozeh Technology LTD">
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


namespace FiroozehGameService.Models.Internal
{
    /// <summary>
    /// Represents Game Service TurnBased Property Actions
    /// </summary>
    internal static class InternalPropertyAction
    {
        /// <summary>
        /// SetOrUpdate the Value of Key
        /// </summary>
        internal const int MemberSetOrUpdate = 1;
        /// <summary>
        /// Delete the Value of Key
        /// </summary>
        internal const int MemberDelete = 2;
        /// <summary>
        /// SetOrUpdate the Value of Key
        /// </summary>
        internal const int RoomSetOrUpdate = 3;
        /// <summary>
        /// Delete the Value of Key
        /// </summary>
        internal const int RoomDelete = 4;
    }
}
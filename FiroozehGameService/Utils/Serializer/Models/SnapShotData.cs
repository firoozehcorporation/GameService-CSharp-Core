// <copyright file="SnapShotData.cs" company="Firoozeh Technology LTD">
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


namespace FiroozehGameService.Utils.Serializer.Models
{

    /// <summary>
    /// Represents SnapShotType Data Model In Game Service Binary Serializer
    /// </summary>
    public enum SnapShotType : byte
    {
        /// <summary>
        /// SnapShot Function Type
        /// </summary>
        Function = 0x0,
        
        
        /// <summary>
        /// SnapShot Object Type
        /// </summary>
        Object = 0x1,
        
        
        /// <summary>
        /// SnapShot Property Type
        /// </summary>
        Property = 0x2
    }
    /// <summary>
    /// Represents SnapShotData Data Model In Game Service Binary Serializer
    /// </summary>
    public class SnapShotData
    {
        /// <summary>
        /// Get Type of SnapShot
        /// </summary>
        public SnapShotType Type;
        
        
        /// <summary>
        /// Get SnapShot Buffer Data
        /// </summary>
        public byte[] Buffer;


        /// <summary>
        /// The Snapshot OwnerId
        /// </summary>
        public string OwnerId;
        
        internal SnapShotData(SnapShotType type, string ownerId,byte[] buffer)
        {
            Type = type;
            Buffer = buffer;
            OwnerId = ownerId;
        }
    }
}
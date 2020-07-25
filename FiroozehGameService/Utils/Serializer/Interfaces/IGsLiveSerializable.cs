// <copyright file="IGsLiveSerializable.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Utils.Serializer.Helpers;

namespace FiroozehGameService.Utils.Serializer.Interfaces
{
    /// <summary>
    ///  Represents IGsLiveSerializable In Game Service Binary Serializer
    ///  You Can Make Your Class Observable Serializable With Implement this Class 
    /// </summary>
    public interface IGsLiveSerializable
    {
        /// <summary>
        /// the OnGsLiveRead Function You Can Read Your Data form readStream
        /// </summary>
        /// <param name="readStream"></param>
        void OnGsLiveRead(GsReadStream readStream);

        
        /// <summary>
        /// the OnGsLiveWrite Function You Can Write Your Data To writeStream
        /// </summary>
        /// <param name="writeStream"></param>
        void OnGsLiveWrite(GsWriteStream writeStream);
    }
}
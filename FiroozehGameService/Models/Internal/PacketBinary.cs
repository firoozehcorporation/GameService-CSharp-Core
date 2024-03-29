// <copyright file="PacketBinary.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Text;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal abstract class PacketBinary
    {
        internal abstract byte[] Serialize(string key, bool isCommand);
        internal abstract void Deserialize(byte[] buffer);
        internal abstract int BufferSize(short prefixLen);
        internal abstract void Encrypt(string key, bool isCommand);


        internal string ConvertToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        internal byte[] ConvertToBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
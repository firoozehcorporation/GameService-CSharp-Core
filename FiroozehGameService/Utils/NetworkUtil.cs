// <copyright file="NetworkUtil.cs" company="Firoozeh Technology LTD">
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


using System.Net;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Utils
{
    internal static class NetworkUtil
    {
        internal static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(Api.CurrentTime))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /* internal static string GetMacAddress()
         {
            return NetworkInterface
                 .GetAllNetworkInterfaces()
                 .Where( nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback )
                 .Select( nic => nic.GetPhysicalAddress().ToString() )
                 .FirstOrDefault();
         }
         */
    }
}
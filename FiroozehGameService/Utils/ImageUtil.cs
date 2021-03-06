// <copyright file="ImageUtil.cs" company="Firoozeh Technology LTD">
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

using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Utils
{
    internal static class ImageUtil
    {
        internal static async Task<ImageUploadResult> UploadProfileImage(byte[] imageBuffer)
        {
            if (imageBuffer.Length > 1000 * 1024)
                throw new GameServiceException("ProfileImage is Too Large").LogException(typeof(ImageUtil),
                    DebugLocation.Internal, "UploadProfileImage");
            return await ApiRequest.UploadUserProfileLogo(imageBuffer);
        }


        internal static async Task<ImageUploadResult> UploadPartyLogo(byte[] imageBuffer, string partyId)
        {
            if (imageBuffer.Length > 1000 * 1024)
                throw new GameServiceException("Party Logo is Too Large").LogException(typeof(ImageUtil),
                    DebugLocation.Internal, "UploadPartyLogo");
            return await ApiRequest.UploadPartyLogo(imageBuffer, partyId);
        }
    }
}
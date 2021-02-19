// <copyright file="FaaSProvider.cs" company="Firoozeh Technology LTD">
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

using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi.FaaS;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.BasicAPI
{
    /// <summary>
    ///     Represents FaaS Provider Model In Game Service Basic API
    /// </summary>
    internal class CloudFunctionProvider : ICloudFunctionProvider
    {
        public async Task<FaaSResponse<TFaaS>> ExecuteFunction<TFaaS>(string functionId,
            object functionParameters = null, bool isPublic = false) where TFaaS : FaaSCore
        {
            if (!isPublic && !GameService.IsAuthenticated())
                throw new GameServiceException("You Must Login First In Private Mode").LogException(
                    typeof(CloudFunctionProvider),
                    DebugLocation.Internal, "ExecuteFunction");
            if (string.IsNullOrEmpty(functionId))
                throw new GameServiceException("functionId Cant Be NullOrEmpty").LogException(
                    typeof(CloudFunctionProvider),
                    DebugLocation.Internal, "ExecuteFunction");
            return await ApiRequest.ExecuteCloudFunction<TFaaS>(functionId, functionParameters, isPublic);
        }
    }
}
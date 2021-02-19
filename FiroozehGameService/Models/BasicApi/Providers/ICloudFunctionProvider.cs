// <copyright file="ICloudFunctionProvider.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi.FaaS;

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents FaaS Provider Model In Game Service Basic API
    /// </summary>
    public interface ICloudFunctionProvider
    {
        /// <summary>
        ///     Execute Function
        ///     note : if Function is public , You Can Call it without Login
        /// </summary>
        /// <param name="functionId">(NOTNULL)Specifies the Function Id that Set in Developers Panel</param>
        /// <param name="functionParameters">(NULLABLE)Specifies the Function Input Parameter Class that Set in Developers Panel</param>
        /// <param name="isPublic">Specifies the Function Visibility Type that Set in Developers Panel</param>
        /// <value> return Result in String </value>
        Task<FaaSResponse<TFaaS>> ExecuteFunction<TFaaS>(string functionId, object functionParameters = null,
            bool isPublic = false) where TFaaS : FaaSCore;
    }
}
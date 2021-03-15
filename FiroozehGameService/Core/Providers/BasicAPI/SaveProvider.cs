// <copyright file="SaveProvider.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.BasicAPI
{
    /// <summary>
    ///     Represents SaveGame Provider Data Model In Game Service Basic API
    /// </summary>
    internal class SaveProvider : ISaveProvider
    {
        public async Task<SaveDetails> SaveGame(string saveName, object saveObj)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(SaveProvider),
                    DebugLocation.Internal, "SaveGame");

            if (string.IsNullOrEmpty(saveName))
                throw new GameServiceException("saveName Cant Be EmptyOrNull").LogException(
                    typeof(SaveProvider),
                    DebugLocation.Internal, "SaveGame");

            if (saveObj == null)
                throw new GameServiceException("saveObj Cant Be Null").LogException(
                    typeof(SaveProvider),
                    DebugLocation.Internal, "SaveGame");

            return await ApiRequest.SaveGame(saveName, saveObj);
        }

        public async Task<T> GetSaveGame<T>(string saveName)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(SaveProvider),
                    DebugLocation.Internal, "GetSaveGame");

            if (string.IsNullOrEmpty(saveName))
                throw new GameServiceException("saveName Cant Be EmptyOrNull").LogException(
                    typeof(SaveProvider),
                    DebugLocation.Internal, "GetSaveGame");

            return await ApiRequest.GetSaveGame<T>(saveName);
        }

        public async Task<bool> RemoveSave(string saveName)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(SaveProvider),
                    DebugLocation.Internal, "RemoveLastSave");

            if (string.IsNullOrEmpty(saveName))
                throw new GameServiceException("saveName Cant Be EmptyOrNull").LogException(
                    typeof(SaveProvider),
                    DebugLocation.Internal, "RemoveSave");


            return await ApiRequest.RemoveSave(saveName);
        }
    }
}
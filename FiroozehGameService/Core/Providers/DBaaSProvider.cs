// <copyright file="DBaaSProvider.cs" company="Firoozeh Technology LTD">
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

using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi.DBaaS;
using FiroozehGameService.Models.BasicApi.DBaaS.Options;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers
{
    /// <summary>
    ///     Represents DBaaS Provider Model In Game Service Basic API
    /// </summary>
    internal class DBaaSProvider : IDBaaSProvider
    {
        public async Task<List<TItem>> GetTableItems<TItem>(string tableId, bool isGlobal = false,
            TableOption[] options = null) where TItem : TableItemHelper
        {
            if (!isGlobal && !GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItems");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItems");
            if (!DBaaSUtil.ValidateOptions(options))
                throw new GameServiceException("Invalid TabletOptions").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItems");
            return await ApiRequest.GetTableItems<TItem>(tableId, isGlobal, options);
        }

        public async Task<DBaaSResult<TItem>> GetTableItems<TItem>(DBaaSAggregation aggregation)
            where TItem : TableItemHelper
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItems");
            if (aggregation == null)
                throw new GameServiceException("Aggregation Cant Be Null").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItems");
            return await ApiRequest.GetTableItems<TItem>(aggregation);
        }

        public async Task<TItem> GetTableItem<TItem>(string tableId, string itemId, bool isGlobal = false)
            where TItem : TableItemHelper
        {
            if (!isGlobal && !GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItem");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItem");
            if (string.IsNullOrEmpty(itemId))
                throw new GameServiceException("TableItemId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "GetTableItem");
            return await ApiRequest.GetTableItem<TItem>(tableId, itemId, isGlobal);
        }

        public async Task<TItem> UpdateTableItem<TItem>(string tableId, string itemId, TItem editedItem)
            where TItem : TableItemHelper
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "UpdateTableItem");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "UpdateTableItem");
            if (string.IsNullOrEmpty(itemId))
                throw new GameServiceException("TableItemId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "UpdateTableItem");
            if (editedItem == null)
                throw new GameServiceException("EditedItem Cant Be Null").LogException(typeof(GameService),
                    DebugLocation.Internal, "UpdateTableItem");
            return await ApiRequest.UpdateTableItem(tableId, itemId, editedItem);
        }

        public async Task<TItem> AddItemToTable<TItem>(string tableId, TItem newItem) where TItem : TableItemHelper
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "AddItemToTable");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "AddItemToTable");
            if (newItem == null)
                throw new GameServiceException("NewItem Cant Be Null").LogException(typeof(GameService),
                    DebugLocation.Internal, "AddItemToTable");
            return await ApiRequest.AddItemToTable(tableId, newItem);
        }

        public async Task<bool> DeleteAllTableItems(string tableId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "DeleteAllTableItems");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "DeleteAllTableItems");
            return await ApiRequest.DeleteAllTableItems(tableId);
        }

        public async Task<bool> DeleteTableItem(string tableId, string itemId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(GameService),
                    DebugLocation.Internal, "DeleteTableItem");
            if (string.IsNullOrEmpty(tableId))
                throw new GameServiceException("TableId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "DeleteTableItem");
            if (string.IsNullOrEmpty(itemId))
                throw new GameServiceException("TableItemId Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "DeleteTableItem");
            return await ApiRequest.DeleteTableItem(tableId, itemId);
        }
    }
}
// <copyright file="ITableProvider.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi.DBaaS;
using FiroozehGameService.Models.BasicApi.DBaaS.Options;

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents Table Provider Model In Game Service Basic API
    /// </summary>
    public interface ITableProvider
    {
        /// <summary>
        ///     This command will return all information about the Table with a specific ID
        /// </summary>
        /// <param name="tableId">(NOTNULL)The ID of Table you Want To get Detail</param>
        /// <param name="isGlobal">(Optional)If this Option Enabled, You Can Get Table Items Without Login</param>
        /// <param name="options">(Optional)The Table Options</param>
        /// <value> return List of all Table Items</value>
        Task<List<TItem>> GetTableItems<TItem>(string tableId, bool isGlobal = false, TableOption[] options = null)
            where TItem : TableItemHelper;


        /// <summary>
        ///     This command will return all information about the Table with a specific ID and Aggregations
        /// </summary>
        /// <param name="aggregation">(NotNULL)The aggregation of Table you Want To get Detail</param>
        /// <value> return the TableResult</value>
        Task<TableResult<TItem>> GetTableItems<TItem>(TableAggregation aggregation) where TItem : TableItemHelper;


        /// <summary>
        ///     This command returns one of the Specific Table information with a specific ID
        /// </summary>
        /// <param name="tableId">(Not NULL)The ID of Table you Want To get Detail</param>
        /// <param name="itemId">(Not NULL)The ID of TableItem you Want To get Detail</param>
        /// <param name="isGlobal">(Optional)If this Option Enabled, You Can Get Table Items Without Login</param>
        /// <value> return a Table Item</value>
        Task<TItem> GetTableItem<TItem>(string tableId, string itemId, bool isGlobal = false)
            where TItem : TableItemHelper;


        /// <summary>
        ///     This command will edit one of the Table information with a specific ID
        /// </summary>
        /// <param name="tableId">(Not NULL)The ID of Table you Want To Edit Details</param>
        /// <param name="itemId">(Not NULL)The ID of TableItem you Want To Edit Details</param>
        /// <param name="editedItem">(Not NULL)The Object of TableItem you Want To Edit Detail</param>
        /// <value> return Edited Table Item</value>
        Task<TItem> UpdateTableItem<TItem>(string tableId, string itemId, TItem editedItem)
            where TItem : TableItemHelper;


        /// <summary>
        ///     This command will Add new Table information with a specific ID
        /// </summary>
        /// <param name="tableId">(Not NULL)The ID of Table you Want To Add Item</param>
        /// <param name="newItem">(Not NULL)The Object of TableItem you Want To Add</param>
        /// <value> return Added Table Item</value>
        Task<TItem> AddItemToTable<TItem>(string tableId, TItem newItem) where TItem : TableItemHelper;


        /// <summary>
        ///     This command will delete All of the Table Items information with a specific ID
        /// </summary>
        /// <param name="tableId">(Not NULL)The ID of Table you Want To Delete All Items</param>
        /// <value> return true if Remove Successfully </value>
        Task<bool> DeleteAllTableItems(string tableId);


        /// <summary>
        ///     This command will delete one of the Table information with a specific ID
        /// </summary>
        /// <param name="tableId">(Not NULL)The ID of Table you Want To Delete one of Items</param>
        /// <param name="itemId">(Not NULL)The ID of TableItem you Want To Delete it</param>
        /// <value> return true if Remove Successfully </value>
        Task<bool> DeleteTableItem(string tableId, string itemId);
    }
}
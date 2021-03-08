// <copyright file="ISaveProvider.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents SaveGame Provider Data Model In Game Service Basic API
    /// </summary>
    public interface ISaveProvider
    {
        /// <summary>
        ///     With this command you can save your Current Status in Game
        /// </summary>
        /// <param name="saveGameName">(NOTNULL)saveGameName</param>
        /// <param name="saveGameObj">the Object that you Want To Save it</param>
        /// <value> return SaveDetails </value>
        Task<SaveDetails> SaveGame(string saveGameName, object saveGameObj);


        /// <summary>
        ///     This command will get you the last save you saved
        /// </summary>
        /// <value> return Player Last Save </value>
        Task<T> GetSaveGame<T>();


        /// <summary>
        ///     This command can remove the last current user saved
        /// </summary>
        /// <value> return true if Remove Successfully </value>
        Task<bool> RemoveLastSave();
    }
}
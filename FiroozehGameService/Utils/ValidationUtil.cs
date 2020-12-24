// <copyright file="ValidationUtil.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi.Buckets;

namespace FiroozehGameService.Utils
{
    internal static class ValidationUtil
    {
        internal static bool ValidateBuckets(IEnumerable<BucketOption> options)
        {
            if (options == null) return true;
            
            var constraintSet = 0;
            var findByElementSet = 0;
            var ownerShipSet = 0;
            var sortByElementSet = 0;

            foreach (var option in options)
            {
                if (option.GetType() == typeof(Constraint)) constraintSet++;
                if (option.GetType().Name == typeof(FindByElement<>).Name) findByElementSet++;
                if (option.GetType() == typeof(Ownership)) ownerShipSet++;
                if (option.GetType() == typeof(SortByElement)) sortByElementSet++;
            }

            return (constraintSet == 0 || constraintSet == 1) &&
                   (findByElementSet == 0 || findByElementSet == 1) &&
                   (ownerShipSet == 0 || ownerShipSet == 1) &&
                   (sortByElementSet == 0 || sortByElementSet == 1);
        }
    }
}
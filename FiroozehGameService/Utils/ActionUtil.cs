// <copyright file="ActionUtil.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal static class ActionUtil
    {
        internal static bool IsInternalAction(int action, GSLiveType type)
        {
            switch (type)
            {
                case GSLiveType.NotSet:
                    break;
                case GSLiveType.TurnBased:
                    return IsInternalTbAction(action);
                case GSLiveType.RealTime:
                    return IsInternalRtAction(action);
                case GSLiveType.Command:
                    return IsInternalCAction(action);
                default:
                    return false;
            }

            return false;
        }

        private static bool IsInternalRtAction(int action)
        {
            return false;
        }

        private static bool IsInternalTbAction(int action)
        {
            return action == TurnBasedConst.ActionPing || action == TurnBasedConst.ActionMirror;
        }

        private static bool IsInternalCAction(int action)
        {
            return action == CommandConst.ActionPing || action == CommandConst.ActionMirror;
        }
    }
}
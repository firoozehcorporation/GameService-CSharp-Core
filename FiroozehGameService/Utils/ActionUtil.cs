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
            return action == TB.ActionPingPong;
        }

        private static bool IsInternalCAction(int action)
        {
            return action == Command.ActionPing;
        }
    }
}
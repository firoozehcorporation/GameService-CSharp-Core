using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal static class KeyTypeUtil
    {
        public static string GetPwd(string pwd,GSLiveType type)
        {
            if (pwd != null) return pwd;
            switch (type)
            {
                case GSLiveType.NotSet:
                    break;
                case GSLiveType.TurnBased:
                    return TB.Pwd;
                case GSLiveType.RealTime:
                    return RT.Pwd;
                case GSLiveType.Core:
                    return Command.Pwd;
                default:
                    return null;
            }
            return null;
        }
    }
}
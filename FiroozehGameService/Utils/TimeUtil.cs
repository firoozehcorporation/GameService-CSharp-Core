using System;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Utils
{
    internal static class TimeUtil
    {
        internal static async Task<GSTime> GetCurrentTime()
        {
            try
            {
                var serverTime = await ApiRequest.GetCurrentServerTime();
                return new GSTime
                {
                    ServerTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(serverTime)),
                    DeviceTime = DateTimeOffset.Now
                };
            }
            catch (Exception)
            {
                return new GSTime
                {
                    ServerTime = DateTimeOffset.Now,
                    DeviceTime = DateTimeOffset.Now
                };
            }
        }
    }
}
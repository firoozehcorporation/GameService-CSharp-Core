using System;
using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    public class AutoMatchEvent
    {
        public AutoMatchStatus Status { get; set; }
        public List<User> Players { get; set; }
    }
}
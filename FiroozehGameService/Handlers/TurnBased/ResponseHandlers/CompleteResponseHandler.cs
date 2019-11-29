﻿
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class CompleteResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnComplete;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.OnComplete?.Invoke(this,JsonConvert.DeserializeObject<Complete>(packet.Data));
        }
      
    }
}

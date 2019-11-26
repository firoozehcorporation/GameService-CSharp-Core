using FiroozehGameService.Models.EventArgs;
using System;

namespace FiroozehGameService.Handlers
{
    //example demostration ONLY
    internal class RoomHandler
    {
        public static EventHandler OnRoomUpdate; //or EventHandler<SOMEARG>
        public static EventHandler OnUserJoin;
        //more EventHandler goes here

        public static void Init(CommandHandler _commandHandler)
        {
            int SOME_ACTION_COMMAND = -1;
            var responseHandler = new CommandServer_ResponseHandlers.ResponseHandler<CommandResponseArgs>() //or some custom args, driven from System.EventArgs
            {
                ActionCommand = SOME_ACTION_COMMAND,
                Signature = string.Empty, //redunant
                EventHandler = new EventHandler<CommandResponseArgs>(
                    (s, e) =>
                {
                    //SOME FUNCTION(e);
                    OnRoomUpdate?.Invoke(_commandHandler, e); //or any custom eventArg
                })
            };
            _commandHandler.AddNewResponseHandler(responseHandler);
        }
    }
}

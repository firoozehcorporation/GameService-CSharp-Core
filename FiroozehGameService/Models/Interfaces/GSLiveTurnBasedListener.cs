using System.Collections.Generic;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Models.Interfaces
{
    /// <summary>
    /// Represents GSLive TurnBased Event Listener
    /// </summary>
    public interface GSLiveTurnBasedListener : GSLiveCommandListeners
    {
        // Major Listeners
        /// <summary>
        /// If a player becomes a member of the game room, the result is returned
        /// </summary>
        /// <param name="joinData">New JoinData information</param>
        /// <param name="type">The type of new player added can be Normal or AutoMatch.</param>
        void OnJoin (JoinData joinData , JoinType type);
        
        /// <summary>
        /// If one of the players leaves the game room, the result returns
        /// </summary>
        /// <param name="leave">Information on the player who has left the game room</param>
        void OnLeave(Leave leave);
        
        /// <summary>
        /// If the player in turn sends a message, this data function is Receive and the sender returns
        /// </summary>
        /// <param name="turn">Information about data Receive</param>
        void OnTakeTurn(Turn turn);
        
        
        /// <summary>
        /// After transferring the turn to the next player, the result returns to this function
        /// </summary>
        /// <param name="whoIsNext">Next player information</param>
        void OnChooseNext(Member whoIsNext);
        
        /// <summary>
        /// If you request players information in the game room, the result will be returned
        /// </summary>
        /// <param name="members">List of room players</param>
        void OnRoomMembersDetail(List<Member> members);
        
        /// <summary>
        /// If another player finishes the game, the player returns the result in this function.
        /// Confirm it. In this function you have to measure the results with your values and if you confirm with the Complete request
        /// </summary>
        /// <param name="finish">Player info and result</param>
        void OnFinish(Finish finish);
        
        
        /// <summary>
        /// If all the Players confirm the results,
        /// the result that gets the most votes is returned in this function.
        /// </summary>
        /// <param name="complete">Information on the number of confirmations and the final result of the game</param>
        void OnComplete(Complete complete);

        // Another Listeners
        /// <summary>
        /// This function sounds like if the process of building the game room is done correctly
        /// </summary>
        void OnSuccess ();
        
        /// <summary>
        /// If this error occurs, this function called
        /// </summary>
        /// <param name="error">error</param>
        void OnTurnBasedError(string error);
    }
}
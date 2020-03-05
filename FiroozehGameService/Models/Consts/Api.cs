namespace FiroozehGameService.Models.Consts
{
    internal sealed class Api
    {
        public const string BaseUrl1 = "https://gamesservice.ir";
        private const string BaseUrl2 = "https://api.gamesservice.ir";

        public const string LoginUser = BaseUrl2 + "/auth/app/login";
        public const string LoginWithGoogle = BaseUrl2 + "/auth/g/callback";
        public const string Start = BaseUrl2 + "/auth/start";
        public const string UserData = BaseUrl2 + "/v1";
             
        public const string SaveGame = BaseUrl2 + "/v1/savegame/";
        public const string Achievements = BaseUrl2 + "/v1/achievement/";
        public const string Leaderboard = BaseUrl2 + "/v1/leaderboard/";
        public const string Bucket = BaseUrl2 + "/v1/bucket/";
        
        
        public const string UserProfileLogo = BaseUrl1 + "/Application/image";
        public const string UserProfile = BaseUrl1 + "/Application";
        
    }
}
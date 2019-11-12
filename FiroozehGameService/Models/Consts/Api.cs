namespace FiroozehGameService.Models.Consts
{
    public sealed class Api
    {
        public const string BaseUrl = "https://gamesservice.ir";
        public const string Wsuri = "wss://gamesservice.ir";

        public const string LoginUser = BaseUrl + "/Auth/app/login";
        public const string Start = BaseUrl + "/Auth/start";
        public const string UserData = BaseUrl + "/Api/v1";


        public const string DeleteLastSave = BaseUrl + "/Api/v1/savegame/delete";
        public const string SetSavegame = BaseUrl + "/Api/v1/savegame/save";
        public const string GetSavegame = BaseUrl + "/Api/v1/savegame";

        public const string GetAchievements = BaseUrl + "/Api/v1/Achievement";
        public const string EarnAchievement = BaseUrl + "/Api/v1/Achievement/unlock/";

        public const string SubmitScore = BaseUrl + "/Api/v1/Leaderboard/submitscore/";
        public const string GetLeaderboard = BaseUrl + "/Api/v1/Leaderboard";

        public const string Bucket = BaseUrl + "/Api/v1/bucket/";
    }
}
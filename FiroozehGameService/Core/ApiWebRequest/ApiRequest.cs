using FiroozehGameService.Builder;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.TResponse;
using FiroozehGameService.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FiroozehGameService.Models.Internal;
using EditUserProfile = FiroozehGameService.Models.BasicApi.EditUserProfile;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal static class ApiRequest
    {
        
        private static string Pt => GameService.PlayToken;
        private static string Ut => GameService.UserToken;
        private static long Sp => GameService.StartPlaying;


        internal static async Task<Download> GetDataPackInfo(string gameId, string tag)
        {
                var url = Models.Consts.Api.BaseUrl + "/game/" + gameId + "/datapack/?tag=" + tag;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Download>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<Login> Authorize(GameServiceClientConfiguration configuration, bool isGuest)
        {
            
                var body = JsonConvert.SerializeObject(CreateAuthorizationDictionary(configuration, Ut, isGuest));
                var response = await GsWebRequest.Post(Models.Consts.Api.Start, body);
                
                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<Login> Login(string email, string password)
        {
            
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, null));
                var response = await GsWebRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<Login> SignUp(string nickName, string email, string password)
        {
            
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, nickName));
                var response = await GsWebRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
            
        }


        internal static async Task<List<LeaderBoard>> GetLeaderBoard()
        {
                var response = await GsWebRequest.Get(Models.Consts.Api.GetLeaderboard, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TLeaderBoard>(await reader.ReadToEndAsync())
                            .LeaderBoards;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
                    
        }


        internal static async Task<List<Achievement>> GetAchievements()
        {
            
           var response = await GsWebRequest.Get(Models.Consts.Api.GetAchievements, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if(response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TAchievement>(await reader.ReadToEndAsync())
                        .Achievements;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
            }
                   
        }


        internal static async Task<T> GetSaveGame<T>()
        {
            const string url = Models.Consts.Api.GetSavegame;
            var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if(response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
            }
            
        }


        internal static async Task<User> GetCurrentPlayer()
        {
           
                var response = await GsWebRequest.Get(Models.Consts.Api.UserData, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TUser>(await reader.ReadToEndAsync())
                            .User;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }

        }
        
        internal static async Task<User> EditCurrentPlayer(EditUserProfile editUserProfile)
        {

            if (editUserProfile.Logo != null)
              await ImageUtil.UploadProfileImage(editUserProfile.Logo);
            
            var body = JsonConvert.SerializeObject(new Models.Internal.EditUserProfile
            {
                NickName = editUserProfile.NickName,
                AllowAutoAddToGame = editUserProfile.AllowAutoAddToGame,
                ShowGroupActivity = editUserProfile.ShowGroupActivity,
                ShowPublicActivity = editUserProfile.ShowPublicActivity
            });
            
            var response = await GsWebRequest.Put(Models.Consts.Api.UserProfile,body,CreateUserTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if(response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TEditUser>(await reader.ReadToEndAsync())
                        .User;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
            }

        }


        internal static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId)
        {
            
           var url = Models.Consts.Api.Bucket + bucketId;
           var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<List<TBucket>>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<Bucket<TBucket>> GetBucketItem<TBucket>(string bucketId, string itemId)
        {
            
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardId)
        {
               var url = Models.Consts.Api.GetLeaderboard + leaderBoardId;
               var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<LeaderBoardDetails>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<SaveDetails> SaveGame(string saveGameName, string saveGameDescription, object saveGameObject)
        {
            
                var body = JsonConvert.SerializeObject(CreateSaveGameDictionary(saveGameName, saveGameDescription
                    , JsonConvert.SerializeObject(saveGameObject)));

                var response = await GsWebRequest.Post(Models.Consts.Api.SetSavegame, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                            .SaveDetails;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }


        internal static async Task<SubmitScoreResponse> SubmitScore(string leaderBoardId, int scoreValue)
        {
                var url = Models.Consts.Api.SubmitScore + leaderBoardId;
                var body = JsonConvert.SerializeObject(CreateSubmitScoreDictionary(scoreValue));

                var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TSubmitScore>(await reader.ReadToEndAsync())
                            .SubmitScoreResponse;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
           
        }


        internal static async Task<Achievement> UnlockAchievement(string achievementId)
        {
                var url = Models.Consts.Api.EarnAchievement + achievementId;
                var response = await GsWebRequest.Post(url, headers: CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TUnlockAchievement>(await reader.ReadToEndAsync())
                        .Achievement;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }           
        }


        internal static async Task<Bucket<TBucket>> UpdateBucketItem<TBucket>(string bucketId, string itemId, TBucket editedBucket)
        {
            
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;
                var body = JsonConvert.SerializeObject(editedBucket);

                var response = await GsWebRequest.Put(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }

        }

        internal static async Task<Bucket<TBucket>> AddBucketItem<TBucket>(string bucketId, TBucket newBucket)
        {
           
                var url = Models.Consts.Api.Bucket + bucketId;
                var body = JsonConvert.SerializeObject(newBucket);

                var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
            
        }


        internal static async Task<bool> RemoveLastSave()
        {
              var response = await GsWebRequest.Delete(Models.Consts.Api.DeleteLastSave, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                            .Status;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);
                }
        }

        internal static async Task<bool> DeleteBucketItems(string bucketId)
        {
            
                var url = Models.Consts.Api.Bucket + bucketId;

                var response = await GsWebRequest.Delete(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if(response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                            .Status;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Message);

                }
           
        }

        internal static async Task<bool> DeleteBucketItem(string bucketId, string itemId)
        {
           
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;

                var response = await GsWebRequest.Delete(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                            .Status;
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                        .Message);

                }
        }
        
        internal static async Task<ImageUploadResult> UploadUserProfileLogo(byte[] imageBuffer)
        {
           
            const string url = Models.Consts.Api.UserProfileLogo;

            var response = await GsWebRequest.DoMultiPartPost(url,imageBuffer,CreateUserTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var data = await reader.ReadToEndAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ImageUploadResult>(data);
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(data).Message);
            }
        }

       

    private static Dictionary<string, object> CreateLoginDictionary(string email, string password, string nickname)
        {
            var param = new Dictionary<string, object>();

            if (nickname == null)
                param.Add("mode", "login");
            else
            {
                param.Add("name", nickname);
                param.Add("mode", "register");
            }

            param.Add("email", email);
            param.Add("password", password);
            return param;
        }

        private static Dictionary<string, object> CreateAuthorizationDictionary(GameServiceClientConfiguration configuration, string userToken, bool isGuest)
        {
            var param = new Dictionary<string, object>();

            if (isGuest)
            {
                param.Add("token", NetworkUtil.GetMacAddress());
                param.Add("mode", "guest");
            }
            else
            {
                param.Add("token", userToken);
                param.Add("mode", "normal");
            }

            param.Add("game", configuration.ClientId);
            param.Add("secret", configuration.ClientSecret);
            param.Add("system_info",JsonConvert.SerializeObject(configuration.SystemInfo));
            return param;
        }

        private static Dictionary<string, object> CreateSaveGameDictionary(string name, string desc, string data)
        {
            return new Dictionary<string, object>
            {
                {"name" , name},
                {"desc" , desc},
                {"data" , data},
                {"playedtime" , Math.Abs((long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - Sp)}
            };
        }

        private static Dictionary<string, object> CreateSubmitScoreDictionary(int score)
        {
            return new Dictionary<string, object>
            {
                {"value" , score}
            };
        }

        private static Dictionary<string, string> CreatePlayTokenHeader()
        {
            return new Dictionary<string, string>
            {
                {"x-access-token", Pt}
            };
        }
        
        private static Dictionary<string, string> CreateUserTokenHeader()
        {
            return new Dictionary<string, string>
            {
                {"x-access-token", Ut}
            };
        }
    }
}
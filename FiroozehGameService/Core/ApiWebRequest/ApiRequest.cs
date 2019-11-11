using FiroozehGameService.Builder;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.TResponse;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal static class ApiRequest
    {
        private static string Pt => GameService.PlayToken;
        private static string Ut => GameService.UserToken;
        private static long Sp => GameService.StartPlaying;


        internal static async Task<Download> GetDataPackInfo(string gameId, string tag)
        {
            try
            {
                var url = Models.Consts.Api.BaseUrl + "/game/" + gameId + "/datapack/?tag=" + tag;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Download>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<Login> Authorize(GameServiceClientConfiguration configuration, bool isGuest)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateAuthorizationDictionary(configuration, Ut, isGuest));
                var response = await GsWebRequest.Post(Models.Consts.Api.Start, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }
        }


        internal static async Task<Login> Login(string email, string password)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, null));
                var response = await GsWebRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }
        }


        internal static async Task<Login> SignUp(string nickName, string email, string password)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, nickName));
                var response = await GsWebRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<List<LeaderBoard>> GetLeaderBoard()
        {
            try
            {
                var response = await GsWebRequest.Get(Models.Consts.Api.GetLeaderboard, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TLeaderBoard>(await reader.ReadToEndAsync())
                        .LeaderBoards;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<List<Achievement>> GetAchievements()
        {
            try
            {
                var response = await GsWebRequest.Get(Models.Consts.Api.GetAchievements, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TAchievement>(await reader.ReadToEndAsync())
                        .Achievements;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<T> GetSaveGame<T>()
        {
            try
            {
                const string url = Models.Consts.Api.GetSavegame;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<User> GetCurrentPlayer()
        {
            try
            {
                var response = await GsWebRequest.Get(Models.Consts.Api.UserData, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TUser>(await reader.ReadToEndAsync())
                        .User;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }

        internal static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<List<TBucket>>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<Bucket<TBucket>> GetBucketItem<TBucket>(string bucketId, string itemId)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;
                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardId)
        {
            try
            {
                var url = Models.Consts.Api.GetLeaderboard + leaderBoardId;

                var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<LeaderBoardDetails>(await reader.ReadToEndAsync());

            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<SaveDetails> SaveGame(string saveGameName, string saveGameDescription, object saveGameObject)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateSaveGameDictionary(saveGameName, saveGameDescription
                    , JsonConvert.SerializeObject(saveGameObject)));

                var response = await GsWebRequest.Post(Models.Consts.Api.SetSavegame, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                        .SaveDetails;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<SubmitScoreResponse> SubmitScore(string leaderBoardId, int scoreValue)
        {
            try
            {
                var url = Models.Consts.Api.SubmitScore + leaderBoardId;
                var body = JsonConvert.SerializeObject(CreateSubmitScoreDictionary(scoreValue));

                var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TSubmitScore>(await reader.ReadToEndAsync())
                        .SubmitScoreResponse;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<Achievement> UnlockAchievement(string achievementId)
        {
            try
            {
                var url = Models.Consts.Api.EarnAchievement + achievementId;

                var response = await GsWebRequest.Post(url, headers: CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TUnlockAchievment>(await reader.ReadToEndAsync())
                        .Achievement;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<Bucket<TBucket>> UpdateBucketItem<TBucket>(string bucketId, string itemId, TBucket editedBucket)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;
                var body = JsonConvert.SerializeObject(editedBucket);

                var response = await GsWebRequest.Put(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }

        internal static async Task<Bucket<TBucket>> AddBucketItem<TBucket>(string bucketId, TBucket newBucket)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId;
                var body = JsonConvert.SerializeObject(newBucket);

                var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Bucket<TBucket>>(await reader.ReadToEndAsync());
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }


        internal static async Task<bool> RemoveLastSave()
        {
            try
            {
                var response = await GsWebRequest.Delete(Models.Consts.Api.DeleteLastSave, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                        .Status;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }

        internal static async Task<bool> DeleteBucketItems(string bucketId)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId;

                var response = await GsWebRequest.Delete(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                        .Status;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }

        }

        internal static async Task<bool> DeleteBucketItem(string bucketId, string itemId)
        {
            try
            {
                var url = Models.Consts.Api.Bucket + bucketId + '/' + itemId;

                var response = await GsWebRequest.Delete(url, CreatePlayTokenHeader());

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                        .Status;
            }
            catch (Exception e)
            {
                await HandleWebException(e);
                throw new GameServiceException();
            }
        }


        private static async Task HandleWebException(Exception e)
        {
            if (!(e is WebException ee)) throw new GameServiceException(e.Message);
            if (ee.Status != WebExceptionStatus.ProtocolError) throw new GameServiceException(e.Message);
            using (var reader = new StreamReader(ee.Response.GetResponseStream()))
            {
                var err = JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync());
                throw new GameServiceException(err.Message);
            }
        }

        private static Dictionary<string, object> CreateLoginDictionary(string email, string password, string nickname)
        {
            var param = new Dictionary<string, object>();

            if (nickname == null)
            {
                param.Add("mode", "login");
            }
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
            //TODO param.Add("system_info", sysInfo.ToJSON());
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
    }
}
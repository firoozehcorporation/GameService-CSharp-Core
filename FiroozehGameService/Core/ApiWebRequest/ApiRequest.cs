using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Buckets;
using FiroozehGameService.Models.BasicApi.TResponse;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using Newtonsoft.Json;
using EditUserProfile = FiroozehGameService.Models.BasicApi.EditUserProfile;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal static class ApiRequest
    {
        private static string Pt => GameService.PlayToken;
        private static string Ut => GameService.UserToken;
        private static GameServiceClientConfiguration Configuration => GameService.Configuration;

        internal static async Task<AssetInfo> GetAssetInfo(string gameId, string tag)
        {
            var url = Api.BaseUrl2 + "/game/" + gameId + "/datapack/?tag=" + tag;
            var body = JsonConvert.SerializeObject(CreateDataPackDictionary());
            var response = await GsWebRequest.Post(url, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (!response.IsSuccessStatusCode)
                    throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                        .Message);
                var info = JsonConvert.DeserializeObject<AssetInfo>(await reader.ReadToEndAsync());
                return info;
            }
        }


        internal static async Task<Login> Authorize()
        {
            var body = JsonConvert.SerializeObject(CreateAuthorizationDictionary(Ut));
            var response = await GsWebRequest.Post(Api.Start, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Login> LoginAsGuest()
        {
            var body = JsonConvert.SerializeObject(CreateLoginDictionary(null, null, null, true));
            var response = await GsWebRequest.Post(Api.LoginUser, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Login> Login(string email, string password)
        {
            var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, null, false));
            var response = await GsWebRequest.Post(Api.LoginUser, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Login> LoginWithGoogle(string idToken)
        {
            var body = JsonConvert.SerializeObject(CreateGoogleLoginDictionary(idToken));
            var response = await GsWebRequest.Post(Api.LoginWithGoogle, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Login> LoginWithPhoneNumber(string name, string phoneNumber, string code)
        {
            var body = JsonConvert.SerializeObject(CreatePhoneLoginDictionary(name, code, phoneNumber));
            var response = await GsWebRequest.Post(Api.LoginWithPhoneNumber + "/callback", body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<bool> SendLoginCodeWithSms(string phoneNumber)
        {
            var body = JsonConvert.SerializeObject(CreateSendSmsDictionary(phoneNumber));
            var response = await GsWebRequest.Post(Api.LoginWithPhoneNumber, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Status;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Login> SignUp(string nickName, string email, string password)
        {
            var body = JsonConvert.SerializeObject(CreateLoginDictionary(email, password, nickName, false));
            var response = await GsWebRequest.Post(Api.LoginUser, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<List<LeaderBoard>> GetLeaderBoard()
        {
            var response = await GsWebRequest.Get(Api.LeaderBoard, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TLeaderBoard>(await reader.ReadToEndAsync())
                        .LeaderBoards;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<List<Achievement>> GetAchievements()
        {
            var response = await GsWebRequest.Get(Api.Achievements, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TAchievement>(await reader.ReadToEndAsync())
                        .Achievements;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<T> GetSaveGame<T>()
        {
            const string url = Api.SaveGame;
            var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Member> GetCurrentPlayer()
        {
            var response = await GsWebRequest.Get(Api.GetMemberData, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TMember>(await reader.ReadToEndAsync())
                        .Member;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Score> GetCurrentPlayerScore(string leaderBoardId)
        {
            var response = await GsWebRequest.Get(Api.LeaderBoard + leaderBoardId + "/me", CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Score>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<User> GetUserData(string userId)
        {
            var response = await GsWebRequest.Get(Api.GetUserData + userId, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<User>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Member> GetMemberData(string memberId)
        {
            var response = await GsWebRequest.Get(Api.GetMemberData + memberId, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Member>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<MemberInfo> GetLastLoginMemberInfo()
        {
            var body = JsonConvert.SerializeObject(CreateLastLoginDictionary());
            var response = await GsWebRequest.Post(Api.GetLastLoginInfo, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<MemberInfo>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<MemberInfo> EditCurrentPlayer(EditUserProfile editUserProfile)
        {
            if (editUserProfile.Logo != null)
            {
                var result = await ImageUtil.UploadProfileImage(editUserProfile.Logo);
                LogUtil.Log(null, result.Url);
            }

            var body = JsonConvert.SerializeObject(new Models.Internal.EditUserProfile
            {
                NickName = editUserProfile.NickName,
                PhoneNumber = editUserProfile.PhoneNumber,
                Email = editUserProfile.Email
            });

            var response = await GsWebRequest.Put(Api.GetMemberData, body, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<MemberInfo>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId, BucketOption[] options)
        {
            var url = UrlUtil.ParseBucketUrl(bucketId, options);
            var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<TBucket>>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<TBucket> GetBucketItem<TBucket>(string bucketId, string itemId)
        {
            var url = Api.Bucket + bucketId + '/' + itemId;
            var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<BucketT<TBucket>>(await reader.ReadToEndAsync()).BucketData;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardKey, int scoreLimit = 10)
        {
            var url = Api.LeaderBoard + leaderBoardKey + "?limit=" + scoreLimit;
            var response = await GsWebRequest.Get(url, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<LeaderBoardDetails>(await reader.ReadToEndAsync());
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<SaveDetails> SaveGame(string saveGameName, object saveGameObject)
        {
            var body = JsonConvert.SerializeObject(CreateSaveGameDictionary(saveGameName
                , JsonConvert.SerializeObject(saveGameObject, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})));

            var response = await GsWebRequest.Post(Api.SaveGame, body, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync()).SaveDetails;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<SubmitScoreResponse> SubmitScore(string leaderBoardKey, int scoreValue)
        {
            var url = Api.LeaderBoard + leaderBoardKey;
            var body = JsonConvert.SerializeObject(CreateSubmitScoreDictionary(scoreValue));

            var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TSubmitScore>(await reader.ReadToEndAsync())
                        .SubmitScoreResponse;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<Achievement> UnlockAchievement(string achievementKey)
        {
            var url = Api.Achievements + achievementKey;
            var response = await GsWebRequest.Post(url, headers: CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TUnlockAchievement>(await reader.ReadToEndAsync())
                        .Achievement;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<TBucket> UpdateBucketItem<TBucket>(string bucketId, string itemId,
            TBucket editedBucket)
        {
            var url = Api.Bucket + bucketId + '/' + itemId;
            var body = JsonConvert.SerializeObject(editedBucket, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            var response = await GsWebRequest.Put(url, body, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<BucketT<TBucket>>(await reader.ReadToEndAsync()).BucketData;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }

        internal static async Task<TBucket> AddBucketItem<TBucket>(string bucketId, TBucket newBucket)
        {
            var url = Api.Bucket + bucketId;
            var body = JsonConvert.SerializeObject(newBucket, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = await GsWebRequest.Post(url, body, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<BucketT<TBucket>>(await reader.ReadToEndAsync()).BucketData;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<bool> RemoveLastSave()
        {
            var response = await GsWebRequest.Delete(Api.SaveGame, CreatePlayTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TSave>(await reader.ReadToEndAsync())
                        .Status;
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }

        internal static async Task<bool> DeleteBucketItems(string bucketId)
        {
            var url = Api.Bucket + bucketId;

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

        internal static async Task<bool> DeleteBucketItem(string bucketId, string itemId)
        {
            var url = Api.Bucket + bucketId + '/' + itemId;

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
            const string url = Api.UserProfileLogo;

            var response = await GsWebRequest.DoMultiPartPost(url, imageBuffer, CreateUserTokenHeader());

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var data = await reader.ReadToEndAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ImageUploadResult>(data);
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(data).Message);
            }
        }

        internal static async Task<string> GetCurrentServerTime()
        {
            var response = await GsWebRequest.Get(Api.CurrentTime);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return await reader.ReadToEndAsync();
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<string> ExecuteCloudFunction(string functionId,
            object functionParameters, bool isPublic)
        {
            var body = JsonConvert.SerializeObject(functionParameters, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            HttpResponseMessage response;
            if (isPublic)
                response = await GsWebRequest.Post(Api.FaaS + functionId, body);
            else
                response = await GsWebRequest.Post(Api.FaaS + functionId, body, CreatePlayTokenHeader());


            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                if (response.IsSuccessStatusCode)
                    return await reader.ReadToEndAsync();
                throw new GameServiceException(JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync())
                    .Message);
            }
        }


        internal static async Task<bool> CheckPhoneLoginStatus()
        {
            var body = JsonConvert.SerializeObject(CreateSendSmsDictionary());
            var response = await GsWebRequest.Put(Api.LoginWithPhoneNumber, body);

            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            { 
                return JsonConvert.DeserializeObject<Error>(await reader.ReadToEndAsync()).Status;
            }
        }


        private static Dictionary<string, object> CreateLoginDictionary(string email, string password, string nickname,
            bool isGuest)
        {
            var param = new Dictionary<string, object>();

            if (isGuest)
            {
                param.Add("mode", "guest");
            }
            else if (nickname == null)
            {
                param.Add("mode", "login");
                param.Add("email", email);
                param.Add("password", password);
            }
            else
            {
                param.Add("name", nickname);
                param.Add("email", email);
                param.Add("password", password);
                param.Add("mode", "register");
            }

            param.Add("device_id", Configuration.SystemInfo.DeviceUniqueId);
            param.Add("client_id", Configuration.ClientId);
            return param;
        }


        private static Dictionary<string, object> CreateGoogleLoginDictionary(string idToken)
        {
            return new Dictionary<string, object>
                {{"token", idToken}, {"device_id", Configuration.SystemInfo.DeviceUniqueId}};
        }


        private static Dictionary<string, object> CreateSendSmsDictionary(string phoneNumber = null)
        {
            return new Dictionary<string, object>
            {
                {"game", Configuration.ClientId},
                {"secret", Configuration.ClientSecret},
                {"phone_number", phoneNumber}
            };
        }


        private static Dictionary<string, object> CreateLastLoginDictionary()
        {
            return new Dictionary<string, object>
            {
                {"game", Configuration.ClientId},
                {"secret", Configuration.ClientSecret},
                {"device_id", Configuration.SystemInfo.DeviceUniqueId}
            };
        }


        private static Dictionary<string, object> CreatePhoneLoginDictionary(string name, string code,
            string phoneNumber)
        {
            return new Dictionary<string, object>
            {
                {"device_id", Configuration.SystemInfo.DeviceUniqueId},
                {"phone_number", phoneNumber},
                {"code", code},
                {"name", name}
            };
        }


        private static Dictionary<string, object> CreateDataPackDictionary()
        {
            return new Dictionary<string, object>
            {
                {"secret", Configuration.ClientSecret}
            };
        }

        private static Dictionary<string, object> CreateAuthorizationDictionary(string userToken)
        {
            var param = new Dictionary<string, object>
            {
                {"token", userToken},
                {"game", Configuration.ClientId},
                {"secret", Configuration.ClientSecret},
                {"system_info", JsonConvert.SerializeObject(Configuration.SystemInfo)}
            };


            return param;
        }

        private static Dictionary<string, object> CreateSaveGameDictionary(string name, string data)
        {
            return new Dictionary<string, object>
            {
                {"name", name},
                {"data", data}
            };
        }

        private static Dictionary<string, object> CreateSubmitScoreDictionary(int score)
        {
            return new Dictionary<string, object>
            {
                {"value", score}
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
                {"x-access-token", Ut}, {"client-id", Configuration.ClientId}
            };
        }
    }
}
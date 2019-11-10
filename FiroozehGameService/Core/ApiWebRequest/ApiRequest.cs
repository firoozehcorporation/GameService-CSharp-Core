using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.ApiWebRequest
{
    public class ApiRequest
    {
        private static GsWebRequest _webRequest = new GsWebRequest();
       
        public static async Task<Download> GetDataPackInfo(string gameId , string tag , string playToken)
        {
            try
            {
                var url = Models.Consts.Api.BaseUrl + "/game/" + gameId + "/datapack/?tag=" + tag;
                var response = await _webRequest.Get(url, new Dictionary<string, string>
                {
                    {"x-access-token", playToken}
                });

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Download>(await reader.ReadToEndAsync());          
            }
            catch (Exception e)
            {
                if (!(e is WebException ee)) throw new GameServiceException(e.Message);
                if (ee.Status != WebExceptionStatus.ProtocolError) throw new GameServiceException(e.Message);
                using (var reader = new StreamReader(ee.Response.GetResponseStream()))
                    throw new GameServiceException(await reader.ReadToEndAsync());
            }
                
        }
        
        public static async Task<Login> Auth(GameServiceClientConfiguration configuration, string userToken , bool isGuest)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateAuthDictionary(configuration,userToken,isGuest));
                var response = await _webRequest.Post(Models.Consts.Api.Start, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());          
            }
            catch (Exception e)
            {
                if (!(e is WebException ee)) throw new GameServiceException(e.Message);
                if (ee.Status != WebExceptionStatus.ProtocolError) throw new GameServiceException(e.Message);
                using (var reader = new StreamReader(ee.Response.GetResponseStream()))
                    throw new GameServiceException(await reader.ReadToEndAsync());
            }
        }

        public static async Task<Login> Login(string email , string password)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email,password,null));
                var response = await _webRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());       
            }
            catch (Exception e)
            {
                if (!(e is WebException ee)) throw new GameServiceException(e.Message);
                if (ee.Status != WebExceptionStatus.ProtocolError) throw new GameServiceException(e.Message);
                using (var reader = new StreamReader(ee.Response.GetResponseStream()))
                    throw new GameServiceException(await reader.ReadToEndAsync());
            }
       }

        public static async Task<Login> SignUp(string nickName,string email , string password)
        {
            try
            {
                var body = JsonConvert.SerializeObject(CreateLoginDictionary(email,password,nickName));
                var response = await _webRequest.Post(Models.Consts.Api.LoginUser, body);

                using (var reader = new StreamReader(response.GetResponseStream()))
                    return JsonConvert.DeserializeObject<Login>(await reader.ReadToEndAsync());        
            }
            catch (Exception e)
            {
                if (!(e is WebException ee)) throw new GameServiceException(e.Message);
                if (ee.Status != WebExceptionStatus.ProtocolError) throw new GameServiceException(e.Message);
                using (var reader = new StreamReader(ee.Response.GetResponseStream()))
                    throw new GameServiceException(await reader.ReadToEndAsync());
            }
              
        }

       
        
        private static Dictionary<string, string> CreateLoginDictionary(string email,string password ,string nickname)
        {
            var param = new Dictionary<string,string>();
           
            if (nickname == null) {
                param.Add("mode", "login");
            } else {
                param.Add("name", nickname);
                param.Add("mode", "register");
            }

            param.Add("email", email);
            param.Add("password", password);
            //TODO param.Add("system_info", sysInfo.ToJSON());
            return param;
        }

        private static Dictionary<string, string> CreateAuthDictionary(GameServiceClientConfiguration configuration,string userToken,bool isGuest)
        {
            var param = new Dictionary<string,string>();
           
            if (isGuest) {
                param.Add("token",NetworkUtil.GetMacAddress());
                param.Add("mode", "guest");
            } else {
                param.Add("token",userToken);
                param.Add("mode", "normal");
            }

            param.Add("game", configuration.ClientId);
            param.Add("secret", configuration.ClientSecret);
           //TODO param.Add("system_info", sysInfo.ToJSON());
            return param;
        }
        
    }
}
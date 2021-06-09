using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class CommandInfo
    {
        [JsonProperty("cipher")] public string Cipher;
        [JsonProperty("encryption")] public string Encryption;
        [JsonProperty("ip")] public string Ip;
        [JsonProperty("port")] public int Port;
        [JsonProperty("protocol")] public string Protocol;
    }
}
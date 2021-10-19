using System;
using System.Text.Json.Serialization;

namespace Server
{
    class UserInfo
    {
        [JsonPropertyName("Username: ")]
        public string clientName { get; set; }
        [JsonPropertyName("OS: ")]
        public string clientOS { get; set; }
        [JsonPropertyName("Time: ")]
        public string clientTime { get; set; }
        public UserInfo()
        {
            this.clientName = Environment.UserName;
            this.clientOS = Environment.OSVersion.ToString();
            this.clientTime = DateTime.Now.ToString();
        }
        public override string ToString()
        {
            return $"Username: {this.clientName}\nOS: {this.clientOS}\nTime: {this.clientTime}";
        }
    }
}

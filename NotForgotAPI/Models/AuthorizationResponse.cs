using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotForgotAPI.Models
{
    public class AuthorizationResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("user")] public UserModel User { get; set; }

        public AuthorizationResponse(string accessToken, UserModel user)
        {
            AccessToken = accessToken;
            User = user;
        }
    }
}
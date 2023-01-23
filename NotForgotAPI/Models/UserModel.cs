using Newtonsoft.Json;
using NotForgotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotForgotAPI.Models
{
    public class UserModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("password")] public string Password { get; set; }

        public UserModel(User user) 
        { 
            Id = user.Id;
            Name = user.Name;
            Login = user.Login;
            Password = user.Password;
        }
        public UserModel()
        {

        }
    }
}
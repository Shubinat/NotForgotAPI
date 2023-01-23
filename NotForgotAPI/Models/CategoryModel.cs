using Newtonsoft.Json;
using NotForgotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotForgotAPI.Models
{
    public class CategoryModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        public CategoryModel()
        {

        }
        public CategoryModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}
using Newtonsoft.Json;
using NotForgotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotForgotAPI.Models
{
    public class NoteModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("creation_date")] public String CreationDate { get; set; }
        [JsonProperty("completion_date")] public String CompletionDate { get; set; }
        [JsonProperty("is_completed")] public bool IsCompleted { get; set; }
        [JsonProperty("category_id")] public int? CategoryId { get; set; }
        [JsonProperty("priority")] public int Priority { get; set; }

        public NoteModel() { }

        public NoteModel(Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Description = note.Description;
            CreationDate = note.CreationDate.ToString("yyyy-MM-dd");
            CompletionDate = note.CompletionDate.ToString("yyyy-MM-dd");
            CategoryId = note.CategoryId;
            Priority = note.Priority;
            IsCompleted = note.IsCompleted;
        }

    }
}
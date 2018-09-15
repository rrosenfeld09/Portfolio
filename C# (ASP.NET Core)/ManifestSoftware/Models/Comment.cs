using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManifestSoftware.Models
{
    public class Comment
    {
        [Key]
        public int comment_id {get; set;}

        [Required]
        public string comment_content {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        public Load load {get; set;}

        public int load_id {get; set;}

        public Post post {get; set;}

        public int post_id {get; set;}

        public Comment()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}
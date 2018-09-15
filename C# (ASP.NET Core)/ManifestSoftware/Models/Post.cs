using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManifestSoftware.Models
{
    public class Post
    {
        [Key]
        public int post_id {get; set;}

        [Required(ErrorMessage = "Post content can't be blank...")]
        [MaxLength(500)]
        public string post_content {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Load load {get; set;}

        public int load_id {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        public List<Comment> comments {get; set;}

        public Post()
        {
            comments = new List<Comment>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}
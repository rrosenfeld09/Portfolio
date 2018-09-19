using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ManifestSoftware.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required(ErrorMessage = "Your first name can't be blank")]
        public string first_name {get; set;}

        [Required(ErrorMessage = "Your last name can't be blank")]
        public string last_name {get; set;}

        [Required(ErrorMessage = "Your email can't be blank")]
        [EmailAddress]
        public string email {get; set;}

        [Required(ErrorMessage = "Your password can't be blank")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string password {get; set;}

        [Required(ErrorMessage = "Your password confirmation can't be blank")]
        [NotMapped]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Manifest> manifests {get; set;}

        public List<Post> posts {get; set;}

        public List<Comment> comments {get; set;}

        public User()
        {
            manifests = new List<Manifest>();
            posts = new List<Post>();
            comments = new List<Comment>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }

    public class LoginUser
    {
        [Required(ErrorMessage = "Your email can't be blank")]
        public string email {get; set;}


        [Required(ErrorMessage = "Your password can't be blank")]
        public string password {get; set;}
    }
}
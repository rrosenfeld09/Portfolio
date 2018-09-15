using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ManifestSoftware.Models
{
    public class LoadHomePageViewModel
    {
        public User user {get; set;}

        public Load load {get; set;}

        public List<Manifest> manifests {get; set;}

        public Post post {get; set;}

        public List<Post> posts {get; set;}

        public Comment comment {get; set;}

        public List<Comment> comments {get; set;}
    }
}
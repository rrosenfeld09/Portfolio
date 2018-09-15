using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManifestSoftware.Models
{
    public class Load
    {
        [Key]
        public int load_id {get; set;}

        public int max_jumpers {get; set;}

        public int current_manifested {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Manifest> manifests {get; set;}

        public List<Post> posts {get; set;}

        public List<Comment> comments {get; set;}

        public Load()
        {
            manifests = new List<Manifest>();
            posts = new List<Post>();
            comments = new List<Comment>();
            current_manifested = 0;
            max_jumpers = 14;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }

    public class loadList
    {
        public IEnumerable<Load> loads {get; set;} 
    }
}
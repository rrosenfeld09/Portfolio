using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManifestSoftware.Models
{
    public class Manifest
    {
        [Key]
        public int manifest_id {get; set;}

        public int user_id {get; set;}

        public User user {get; set;}

        public int load_id {get; set;}

        public Load load {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Manifest(int _user_id, int _load_id)
        {
            user_id = _user_id;
            load_id = _load_id;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        public Manifest()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}
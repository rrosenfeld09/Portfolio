using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ManifestSoftware.Models;

namespace ManifestSoftware.Controllers
{
    public class PostController : Controller
    {
        public MyContext _context;

        public PostController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("create_post")]
        public IActionResult CreatePost(LoadHomePageViewModel submittedPost)
        {
            if(submittedPost.post.user_id != HttpContext.Session.GetInt32("loggedUser"))
            {
                return RedirectToAction("LoadHomePage", "Load", new {load_id = submittedPost.post.load_id});
            }

            Post postToAdd = submittedPost.post;

            _context.posts.Add(postToAdd);
            _context.SaveChanges();

            return RedirectToAction("LoadHomePage", "Load", new {load_id = submittedPost.post.load_id});
        }
    }
}
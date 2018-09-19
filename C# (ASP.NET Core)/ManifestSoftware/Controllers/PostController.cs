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
    public class PostController : BaseEntity
    {
        public MyContext _context;

        public PostController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("create_post")]
        public IActionResult CreatePost(LoadHomePageViewModel submittedPost)
        {
            if(submittedPost.post.user_id != LoggedUserId())
            {
                return RedirectToAction("LoadHomePage", "Load", new {load_id = submittedPost.post.load_id});
            }

            Post postToAdd = submittedPost.post;

            _context.posts.Add(postToAdd);
            _context.SaveChanges();

            return RedirectToAction("LoadHomePage", "Load", new {load_id = submittedPost.post.load_id});
        }

        [HttpGet("delete_post/{post_id}")]
        public IActionResult DeletePost(int post_id)
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }

            Post postToDelete = _context.posts
            .Where(p => p.post_id == post_id)
            .FirstOrDefault();

            if(postToDelete.user_id == LoggedUserId())
            {
                _context.posts.Remove(postToDelete);
                _context.SaveChanges();
                return RedirectToAction("LoadHomePage", "Load", new{load_id = postToDelete.load_id});
            }

            return RedirectToAction("LoadHomePage", "Load", new{load_id = postToDelete.load_id});
        }
    }
}
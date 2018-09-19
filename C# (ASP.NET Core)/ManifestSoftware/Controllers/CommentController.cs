using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ManifestSoftware.Models;

namespace ManifestSoftware.Controllers
{
    public class CommentController : BaseEntity
    {
        public MyContext _context;

        public CommentController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("create_comment")]
        public IActionResult CreateComment(LoadHomePageViewModel submittedComment)
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }

            int _load_id = submittedComment.comment.load_id;

            if(ModelState.IsValid)
            {
                
                _context.comments.Add(submittedComment.comment);
                _context.SaveChanges();
                return RedirectToAction("LoadHomePage", "Load", new {load_id = _load_id});
            }

            return RedirectToAction("LoadHomePage", "Load", new {load_id = _load_id});
        }

        [HttpGet("delete_comment/{comment_id}")]
        public IActionResult DeleteComment(int comment_id)
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }

            Comment commentToDelete = _context.comments
            .Where(p => p.comment_id == comment_id)
            .FirstOrDefault();

            if(commentToDelete.user_id == LoggedUserId())
            {
                _context.comments.Remove(commentToDelete);
                _context.SaveChanges();
                return RedirectToAction("LoadHomePage", "Load", new{load_id = commentToDelete.load_id});
            }

            return RedirectToAction("LoadHomePage", "Load", new{load_id = commentToDelete.load_id});
        }
    }
}
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
    public class CommentController : Controller
    {
        public MyContext _context;

        public CommentController(MyContext context)
        {
            _context = context;
        }

        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return false;
            }
            return true;
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
    }
}
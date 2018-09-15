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
    public class LoadController : Controller
    {
        public MyContext _context;

        public LoadController(MyContext context)
        {
            _context = context;
        }


        //HOW CAN I NOT DUPLICATE THIS METHOD?
        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return false;
            }
            return true;
        }
        

        [HttpGet("select_load")]
        public IActionResult SelectLoad()
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }
            loadList loadlist = new loadList();

            loadlist.loads = _context.loads
            .Where(p => p.current_manifested < 14)
            .ToList();

            return View(loadlist);
        }

        [HttpGet("CreateLoad")]
        public IActionResult CreateLoad()
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }
            Load newLoad = new Load();

            _context.loads.Add(newLoad);
            _context.SaveChanges();
            
            return RedirectToAction("SelectLoad");
        }

        [HttpGet("load/homepage/{load_id}")]
        public IActionResult LoadHomePage(int load_id)
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }

            LoadHomePageViewModel viewModel = new LoadHomePageViewModel();

            viewModel.user = _context.users
            .Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser"))
            .FirstOrDefault();

            viewModel.load = _context.loads
            .Where(p => p.load_id == load_id)
            .FirstOrDefault();

            viewModel.manifests = _context.manifests
            .Where(p => p.load_id == load_id)
            .Include(p => p.user)
            .ToList();

            viewModel.posts = _context.posts
            .Where(p => p.load_id == load_id)
            .Include(p => p.user)
            .OrderByDescending(p => p.created_at)
            .ToList();

            viewModel.comments = _context.comments
            .Where(p => p.load_id == load_id)
            .Include(p => p.user)
            .OrderByDescending(p => p.created_at)
            .ToList();

            return View(viewModel);
        }
    }
}

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
    public class ManifestController : BaseEntity
    {
        public MyContext _context;

        public ManifestController(MyContext context)
        {
            _context = context;
        }

        public void AdjustLoad(int load_id)
        {
            Load loadToAdjust = _context.loads
            .Where(p => p.load_id == load_id)
            .FirstOrDefault();

            if(loadToAdjust.current_manifested < 14)
            {
                loadToAdjust.current_manifested += 1;
            }

            _context.SaveChanges();
        }


        [HttpGet("join_load/{load_id}")]
        public IActionResult JoinLoad(int load_id)
        {
            if(IsUserInSession() == false)
            {
                return RedirectToAction("Index", "User");
            }

            Manifest alreadyRegisteredCheck = _context.manifests
            .Where(p => p.load_id == load_id)
            .Where(p => p.user_id == LoggedUserId())
            .FirstOrDefault();

            if(alreadyRegisteredCheck != null)
            {
                TempData["JoinError"] = "You've already joined this load!";
                return RedirectToAction("LoadHomePage", "Load", new {load_id = load_id});
            }

            User loggedUser = _context.users
            .Where(p => p.user_id == LoggedUserId())
            .FirstOrDefault();
            Manifest newManifest = new Manifest(loggedUser.user_id, load_id);

            _context.manifests.Add(newManifest);
            _context.SaveChanges();

            AdjustLoad(load_id);

            return RedirectToAction("LoadHomePage", "Load", new {load_id = load_id});
        }
    }
}
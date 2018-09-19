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
    public class BaseEntity : Controller
    {
        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return false;
            }
            return true;
        }

        public int LoggedUserId()
        {
            int loggedUserId = (int)HttpContext.Session.GetInt32("loggedUser");

            return loggedUserId;
        }
    }
}
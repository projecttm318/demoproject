﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NongSan.Models;

namespace NongSan.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        [Route("lien-he")]
        public ActionResult Index()
        {
            return View();
        }
 
    }
}
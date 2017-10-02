﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DebateAppDomainAPI.Models;
using DebateApp.Domain;
using System.Net.Http;

namespace DebateAppDomainAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class DebatesController : Controller
    {
        private DBHelper _dbh = new DBHelper();
        // GET api/values
        [HttpPost]
        public DebateModel CreateCasual([FromForm]CreateCasualModel cm)
        {
            var u = _dbh.DBGetUser(cm.UserID);
            var c = new Casual(u.UserLogic, cm.Topic, cm.Category, cm.Opener);
            var d = new DebateModel(c);
            var res = _dbh.DBCreateDebate(d);
            return res;
        }

        
    }
}
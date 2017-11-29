using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asi.Data;

namespace nMappers.Controllers
{
    public class asiAuthController : Controller
    {
        //
        // GET: /asiAuth/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IsValidAuth(ServerDetails LineDetails)
        {
            return View();
        }
    }
}
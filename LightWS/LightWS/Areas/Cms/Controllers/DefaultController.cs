
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Script.Serialization;
using LightWS;
using Utilities;
using LightWS.Models;
using Utilities.Security;

namespace LightWS.Areas.Cms.Controllers
{
    
    public class DefaultController : BaseController
    {
        MarketingOnlineDBEntities store = new MarketingOnlineDBEntities();
        public ActionResult Index()
        {
           
            return View();
        }

    }
}
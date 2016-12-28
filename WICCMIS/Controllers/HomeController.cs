using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WICCDAL;
using WICCDomain;

namespace WICCMIS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Dashboard dashboard = new Dashboard();
            DailyProductionRepository repo = new DailyProductionRepository();

            dashboard.DailyProductions = repo.GetDailyProduction();


            return View(dashboard);
        }
    }
}
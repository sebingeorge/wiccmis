using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WICCDAL;
using WICCDomain;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace WICCMIS.Controllers
{
    public class HomeController :Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Dashboard dashboard = new Dashboard();
            DailyProductionRepository repo = new DailyProductionRepository();

            dashboard.DailyProductions = repo.GetDailyProduction();
            dashboard.ExportSaleOrder  = repo.GetExportSaleOrder();
            dashboard.LocalSaleOrder = repo.GetLocaltSaleOrder();
            dashboard.PurchaseOrder = repo.GetPurchaseOrder();
            dashboard.LocalSale = repo.GetLocalSales();
            dashboard.ExportSale  = repo.GetExportSales();
            return View(dashboard);
        }
    }
}
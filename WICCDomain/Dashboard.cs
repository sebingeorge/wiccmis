using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WICCDomain
{
    public class Dashboard
    {
        public IEnumerable<DailyProduction> DailyProductions { get; set; }
        public IEnumerable<SaleOrder> ExportSaleOrder { get; set; }
        public IEnumerable<SaleOrder> LocalSaleOrder { get; set; }
       public IEnumerable<PurchaseOrder> PurchaseOrder { get; set; }
        public IEnumerable<Sales> LocalSale { get; set; }
        public IEnumerable<Sales> ExportSale { get; set; }
    }
}

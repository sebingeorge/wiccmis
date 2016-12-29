using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WICCDomain
{
    public class DailyProduction
    {
        public string ItemName { get; set; }
        public decimal Production { get; set; }
    }
    public class SaleOrder
    {
        public string tpoDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class PurchaseOrder
    {
        public string poDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class Sales
    {
        public string InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

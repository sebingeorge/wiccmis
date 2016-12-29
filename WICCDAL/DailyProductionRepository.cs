using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WICCDomain;
using Dapper;

namespace WICCDAL
{
    public class DailyProductionRepository : BaseRepository
    {
        static string dataConnection = GetConnectionString("wicc");
        public string ConnectionString()
        {
            return dataConnection;
        }

        public IEnumerable<DailyProduction> GetDailyProduction()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @" select itemName,packingNewQtyinkg 
                            into #Product
                            from trnPackingNewProduct_DT 
                            inner join mstitem on itemCode =packingNewItem order by itemName
                            select ItemName,sum(packingNewQtyinkg) Production from #Product  group by itemName";

                return connection.Query<DailyProduction>(sql);
            }
        }
      
        public IEnumerable<SaleOrder> GetExportSaleOrder()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @"select distinct DATEPART(month,tpoDate)monthcode,DATENAME(month, tpoDate) + ' ' + DATENAME(YEAR, tpoDate) tpoDate ,0 TotalAmount
                               into #EMonth
                             from trnSaleOrderBookingHd SH  group by tpoDate 
                             select DATEPART(month,tpoDate)monthcode,DATENAME(month, tpoDate) + ' ' + DATENAME(YEAR, tpoDate) EtpoDate, 
                             SUM(tpoNetAmt) TotalAmount
                             into #Export
                             from trnSaleOrderBookingHd SH where tpoSaleOrderType='EXPORT' group by tpoDate
                             update #EMonth set TotalAmount =(select isnull(SUM(TotalAmount),0) from #Export where tpoDate= EtpoDate)
                             select tpoDate,TotalAmount from #EMonth";

                return connection.Query<SaleOrder>(sql);
            }
        }
        public IEnumerable<SaleOrder> GetLocaltSaleOrder()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @"select distinct DATEPART(month,tpoDate)monthcode,DATENAME(month, tpoDate) + ' ' + DATENAME(YEAR, tpoDate) tpoDate,0 TotalAmount
                               into #LMonth
                             from trnSaleOrderBookingHd SH  group by tpoDate 
                             select DATEPART(month,tpoDate)monthcode,DATENAME(month, tpoDate) + ' ' + DATENAME(YEAR, tpoDate) LtpoDate, 
                             SUM(tpoNetAmt) TotalAmount
                             into #Local
                             from trnSaleOrderBookingHd SH where tpoSaleOrderType='Local' group by tpoDate
                             update #LMonth set TotalAmount =(select isnull(SUM(TotalAmount),0) from #Local where tpoDate= LtpoDate)
                             select tpoDate,TotalAmount from #LMonth";

                return connection.Query<SaleOrder>(sql);
            }
        }
        public IEnumerable<PurchaseOrder> GetPurchaseOrder()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @" select DATEPART(month,poDate)monthcode,DATENAME(month, poDate) + ' ' + DATENAME(YEAR, poDate) poDate, 
                             SUM(poNetAmount) TotalAmount
                             into #Purchase
                             from trnPurchaseOrderHd    group by poDate
                             select poDate, SUM(TotalAmount)  TotalAmount from #Purchase group by poDate, monthcode";

                return connection.Query<PurchaseOrder>(sql);
            }
        }

        public IEnumerable<Sales> GetExportSales ()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @"select distinct DATEPART(month,FiDate)monthcode,DATENAME(month, FiDate) + ' ' + DATENAME(YEAR, FiDate) InvoiceDate,0 TotalAmount
                                into #FMonth
                                from trnFinalInvoiceHd   group by FiDate 
                                union all	 
                                select distinct DATEPART(month,invInvoiceDate)monthcode,DATENAME(month, invInvoiceDate) + ' ' + DATENAME(YEAR, invInvoiceDate) 
                                InvoiceDate,0 TotalAmount  from trnInvoiceHd   group by invInvoiceDate 
                                select distinct * into #Month from #FMonth 
                                
                                select DATEPART(month,FiDate)monthcode,DATENAME(month, FiDate) + ' ' + DATENAME(YEAR, FiDate) FinalDate, 
                                SUM(FiNettAmt) TotalAmount
                                into #Final
                                from trnFinalInvoiceHd SH group by FiDate
                                update #Month set TotalAmount =(select isnull(SUM(TotalAmount),0) from #Final where InvoiceDate= FinalDate)
                                select InvoiceDate,TotalAmount from #Month  order by InvoiceDate desc";

                return connection.Query<Sales>(sql);
            }
        }
        public IEnumerable<Sales> GetLocalSales()
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @"select distinct DATEPART(month,FiDate)monthcode,DATENAME(month, FiDate) + ' ' + DATENAME(YEAR, FiDate) InvoiceDate,0 TotalAmount
                                into #FMonth
                                from trnFinalInvoiceHd   group by FiDate 
                                union all	 
                                select distinct DATEPART(month,invInvoiceDate)monthcode,DATENAME(month, invInvoiceDate) + ' ' + DATENAME(YEAR, invInvoiceDate) 
                                InvoiceDate,0 TotalAmount  from trnInvoiceHd   group by invInvoiceDate
                                select distinct * into #LMonth from #FMonth
                                select DATEPART(month,invInvoiceDate)monthcode,DATENAME(month, invInvoiceDate) + ' ' + DATENAME(YEAR, invInvoiceDate) LocalInvoiceDate, 
                                SUM(invNetAmount) TotalAmount
                                into #Local
                                from trnInvoiceHd SH group by invInvoiceDate
                                update #LMonth set TotalAmount =(select isnull(SUM(TotalAmount),0) from #Local where InvoiceDate= LocalInvoiceDate)
                                select InvoiceDate,TotalAmount from #LMonth  order by InvoiceDate desc";

                return connection.Query<Sales>(sql);
            }
        }
    }
}

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

        public IEnumerable<DailyProduction> GetDailyProduction(int OrganizationId)
        {
            using (IDbConnection connection = OpenConnection(dataConnection))
            {
                string sql = @"@SELECT 
      ,[itemName], 0  Production

        FROM[WICC].[dbo].[mstItem]";

                return connection.Query<DailyProduction>(sql);
            }
        }

    }
}

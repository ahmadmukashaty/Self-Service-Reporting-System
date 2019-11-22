using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.DbLayer
{
    public class Helper
    {
        private SqlConnection _oracleConnection;

        public Helper()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_A4FAA3_SelfServiceRSEntities"].ConnectionString;

            var builder = new EntityConnectionStringBuilder(connectionString);
            var regularConnectionString = builder.ProviderConnectionString;

            _oracleConnection = new SqlConnection(regularConnectionString);
        }



        public DataTable ExcuteQuery(string sql)
        {
            if (sql == string.Empty)
                throw new NullReferenceException("this query not supported in oracle database !!");
            DataTable dt = new DataTable();
            SqlDataAdapter _adapter = new SqlDataAdapter(sql, _oracleConnection);
            try
            {
                _adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
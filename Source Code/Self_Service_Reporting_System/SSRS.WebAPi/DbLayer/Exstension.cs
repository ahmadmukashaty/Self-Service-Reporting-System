using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.DbLayer
{
    public static class Exstension
    {

        public static List<List<string>> Serialize(this DataTable dataTable)
        {

            if (dataTable.Rows.Count == 0)
                return null;

            var columns = dataTable.Columns;

            List<List<string>> res = new List<List<string>>();

            foreach (DataRow _row in dataTable.Rows)
            {
                List<string> _object = new List<string>();
                
                foreach (DataColumn col in columns)
                {
                    var currentRow = _row[col.ColumnName];



                    if (currentRow == DBNull.Value || currentRow == null)
                    {
                        _object.Add(DBNull.Value.ToString());
                    }
                    else
                        _object.Add(_row[col.ColumnName].ToString());
                }

                res.Add(_object);
            }

            return res;
        }

    }
}
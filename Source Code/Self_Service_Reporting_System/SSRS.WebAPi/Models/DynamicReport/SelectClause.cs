using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class SelectClause
    {
        public string Name { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }
    }
}
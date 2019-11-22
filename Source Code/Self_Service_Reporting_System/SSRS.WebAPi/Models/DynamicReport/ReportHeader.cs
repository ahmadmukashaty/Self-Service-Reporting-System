using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class ReportHeader
    {
        public ReportHeader(string dispalyName, string columnName, string columnType)
        {
            this.DisplayName = dispalyName;
            this.ColumnName = columnName;
            this.ColumnType = columnType;
        }

        public string ColumnType { get; set; }

        public string DisplayName { get; set; }

        public string ColumnName { get; set; }
    }
}
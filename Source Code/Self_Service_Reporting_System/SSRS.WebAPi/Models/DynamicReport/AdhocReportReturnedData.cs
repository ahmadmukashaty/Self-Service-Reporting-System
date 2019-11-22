using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class AdhocReportReturnedData
    {
        public List<List<string>> value_array { get; set; }

        public List<ReportHeader> H_List { get; set; }
    }
}
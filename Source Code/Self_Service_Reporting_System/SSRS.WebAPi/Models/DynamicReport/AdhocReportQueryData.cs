using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class AdhocReportQueryData
    {
        public string ClassificationName { get; set; }

        public string JoinType { get; set; }

        public List<SelectClause> SelectClauses { get; set; }

        public List<WhereClause> WhereClauses { get; set; }

    }
}
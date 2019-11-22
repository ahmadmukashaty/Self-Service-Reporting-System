using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class ReportLevelsModelView
    {
        public int Id { get; set; }

        public int LevelId { get; set; }

        public string TableName { get; set; }

        public int? Order { get; set; }

        public int? ParentId { get; set; }

        public string RelationName { get; set; }

        public string JunctionTable { get; set; }
    }
}
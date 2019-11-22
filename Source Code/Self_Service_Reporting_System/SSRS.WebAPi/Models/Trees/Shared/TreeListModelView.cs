using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.Shared
{
    public class TreeListModelView
    {
        public string label { get; set; }

        public int id { get; set; }

        public int parentId { get; set; }

        public string tableName { get; set; }

        public string columnName { get; set; }

        public string columnType { get; set; }

        public TreeListModelView()
        {
            this.label = "Attributes";
            this.id = 1;
            this.parentId = 0;
        }

        public TreeListModelView(string label, int id, int parentId, string tableName, string columnName, string columnType)
        {
            this.label = label;
            this.id = id;
            this.parentId = parentId;
            this.tableName = tableName;
            this.columnName = columnName;
            this.columnType = columnType;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews
{
    public class AttributeModelView
    {
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public string ColumnType { get; set; }

        public int? Order { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews
{
    public class LevelModelView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TableName { get; set; }

        public int? Order { get; set; }

        public int? ParentId { get; set; }

        public int LevelId { get; set; }
    }
}
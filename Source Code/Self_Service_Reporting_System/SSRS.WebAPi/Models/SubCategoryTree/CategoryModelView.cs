using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.SubCategoryTree
{
    public class CategoryModelView
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? ModuleID { get; set; }

        public int? Order { get; set; }
    }
}
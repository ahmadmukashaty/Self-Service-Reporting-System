using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.Shared
{
    public class TreeModel
    {
        public string label { get; set; }
        public object data { get; set; }
        public string icon { get; set; }

        public string expandedIcon { get; set; }
        public string collapsedIcon { get; set; }
        public List<TreeModel> children { get; set; }

        //required
        public bool leaf { get; set; }
        public bool expanded { get; set; }
        public string type { get; set; }

        //required
        public TreeModel parent { set; get; }
        public bool partialSelected { get; set; }
        public string styleClass { set; get; }
        public bool draggable { set; get; }
        public bool droppable { set; get; }

        //required
        public bool selectable { set; get; }

        public TreeModel(string label, object data, bool leaf)
        {
            this.label = label;
            this.data = data;
            this.leaf = leaf;
            this.selectable = true;
            this.parent = null;
            this.children = null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.AttributesTree
{
    public class AttributeNodeData
    {
        public int? Id { get; set; }
        public int? LevelId { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public AttributeNodeData(int Id, int LevelId, string TableName, string ColumnName, string ColumnType)
        {
            this.Id = Id;
            this.LevelId = LevelId;
            this.TableName = TableName;
            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
        }

        public AttributeNodeData()
        {
            this.TableName = null;
            this.ColumnName = null;
            this.ColumnType = null;
            this.LevelId = 0;
            this.Id = 0;
        }
    }
}
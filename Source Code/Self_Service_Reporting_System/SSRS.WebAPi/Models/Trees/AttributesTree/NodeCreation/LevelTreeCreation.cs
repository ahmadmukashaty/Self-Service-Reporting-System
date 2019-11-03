using Newtonsoft.Json;
using SSRS.WebAPi.Data;
using SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews;
using SSRS.WebAPi.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.AttributesTree.NodeCreation
{
    public class LevelTreeCreation
    {
        private DataLookup SQLHelper = new DataLookup();

        public TreeModel Tree { get; set; }

        public List<AttributeModelView> levelAtributes { get; set; }

        private TreeModel Parent { get; set; }

        public LevelTreeCreation(string levelName, string tableName, int levelId, int Id)
        {
            this.levelAtributes = SQLHelper.GetClassificationLevelAttributes(levelId);
            if (this.levelAtributes != null)
            {
                init(levelId, levelName, tableName, Id);
                GenerateAttributeTree(this.Tree.children[0]);
            }
        }

        public void init(int levelId, string levelName, string tableName, int Id)
        {
            AttributeNodeData rootAttributes = new AttributeNodeData(Id, levelId, tableName, null, null);
            this.Tree = new TreeModel(levelName, rootAttributes, false);
            var ParentSerialized = JsonConvert.SerializeObject(this.Tree);
            this.Parent = JsonConvert.DeserializeObject<TreeModel>(ParentSerialized);

            if (this.Tree.children == null)
                this.Tree.children = new List<TreeModel>();

            TreeModel attrTree = new TreeModel("Attributes", rootAttributes, false);
            this.Tree.children.Add(attrTree);

        }

        private void GenerateAttributeTree(TreeModel tree)
        {
            this.levelAtributes.Sort(delegate (AttributeModelView c1, AttributeModelView c2)
            {
                if (c1.Order == null)
                    return 1;
                if (c2.Order == null)
                    return -1;
                return ((int)c1.Order).CompareTo((int)c2.Order);
            });

            foreach (AttributeModelView attribute in levelAtributes)
            {
                AttributeNodeCreation attributeNodeTree = new AttributeNodeCreation(attribute.DisplayName, attribute.ColumnName, attribute.ColumnType);

                attributeNodeTree.Tree.parent = this.Parent;

                if (attributeNodeTree.Tree != null)
                {
                    if (tree.children == null)
                        tree.children = new List<TreeModel>();
                    tree.children.Add(attributeNodeTree.Tree);
                }

            }
        }
    }
}
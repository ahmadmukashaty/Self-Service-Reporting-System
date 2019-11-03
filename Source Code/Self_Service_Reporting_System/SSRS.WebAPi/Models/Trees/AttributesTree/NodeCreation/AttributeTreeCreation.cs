using SSRS.WebAPi.Data;
using SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews;
using SSRS.WebAPi.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Trees.AttributesTree.NodeCreation
{
    public class AttributeTreeCreation
    {
        private DataLookup SQLHelper = new DataLookup();

        public TreeModel Tree { get; set; }

        private List<LevelModelView> Levels { get; set; }

        public AttributeTreeCreation(string classificationName)
        {
            int classificationId = SQLHelper.GetClassificationID(classificationName);

            if (classificationId != 0)
            {
                this.Levels = SQLHelper.GetClassificationLevels(classificationId);
            }

            if (this.Levels != null)
            {
                init();
                GenerateLevelTree(null, this.Tree);
            }
        }

        public void init()
        {
            AttributeNodeData rootAttributes = new AttributeNodeData();
            this.Tree = new TreeModel("Attributes", rootAttributes, false);
            this.Tree.parent = null;
        }

        private void GenerateLevelTree(int? parent, TreeModel tree)
        {
            List<LevelModelView> levels = GetChildrenLevels(parent);
            if (levels != null)
            {
                foreach (LevelModelView level in levels)
                {
                    LevelTreeCreation levelTree = new LevelTreeCreation(level.Name, level.TableName, level.LevelId, level.Id);
                    if (levelTree.Tree != null)
                    {
                        if (tree.children == null)
                            tree.children = new List<TreeModel>();

                        tree.children.Add(levelTree.Tree);
                    }
                }

                foreach (TreeModel child in tree.children)
                {
                    if (child.leaf == false && child.label != "Attributes")
                    {
                        AttributeNodeData nodeAttribute = (AttributeNodeData)child.data;

                        GenerateLevelTree(nodeAttribute.Id, child);
                    }

                }
            }
        }

        private List<LevelModelView> GetChildrenLevels(int? parent)
        {
            List<LevelModelView> children = null;

            foreach (LevelModelView lmv in Levels)
            {
                if (lmv.ParentId == parent)
                {
                    if (children == null)
                        children = new List<LevelModelView>();
                    children.Add(lmv);
                }
            }
            if (children != null)
            {
                children.Sort(delegate (LevelModelView c1, LevelModelView c2)
                {
                    if (c1.Order == null)
                        return 1;
                    if (c2.Order == null)
                        return -1;
                    return ((int)c1.Order).CompareTo((int)c2.Order);
                });
            }


            return children;
        }
    }
}
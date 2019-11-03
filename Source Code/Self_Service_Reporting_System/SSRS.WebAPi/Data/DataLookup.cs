using SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Data
{
    public class DataLookup
    {
        DB_A4FAA3_SelfServiceRSEntities _context;
        public DataLookup()
        {
            _context = new DB_A4FAA3_SelfServiceRSEntities();
        }

        public int GetClassificationID(string classificationName)
        {
            return (int)_context.SSRS_CLASSIFICATION
                .Where(a => a.NAME.ToLower() == classificationName.ToLower())
                .Select(c => c.ID)
                .FirstOrDefault();
        }

        public int GetModuleID(string moduleName)
        {
            return (int)_context.SSRS_MODULE
                .Where(a => a.NAME.ToLower() == moduleName.ToLower())
                .Select(c => c.ID)
                .FirstOrDefault();
        }

        
        //Get Attribute Tree APIs
        public List<LevelModelView> GetClassificationLevels(int ClassificationId)
        {
            var levels = (from rc in _context.SSRS_CLASSIFICATION
                          join rcl in _context.SSRS_CLASSIFICATION_TABLE on rc.ID equals rcl.SSRS_CLASSIFICATION_ID
                          join rl in _context.SSRS_TABLE on rcl.SSRS_TABLE_ID equals rl.ID
                          where rcl.SSRS_CLASSIFICATION_ID == ClassificationId
                          select new LevelModelView()
                          {
                              Id = rcl.ID,
                              Name = rcl.DISPLAY_NAME,
                              Order = rcl.TABLE_ORDER,
                              TableName = rl.NAME,
                              ParentId = rcl.PARENT_ID,
                              LevelId = rl.ID
                          }).ToList();

            if (levels.Count == 0)
                return null;

            return levels;
        }

        public List<AttributeModelView> GetClassificationLevelAttributes(int LevelId)
        {
            var attributeModelView = _context.SSRS_ATTRIBUTE
                .Where(a => a.SSRS_TABLE_ID == LevelId)
                .Select(c => new AttributeModelView()
                {
                    ColumnName = c.NAME,
                    DisplayName = c.DISPLAY_NAME,
                    ColumnType = c.DATATYPE,
                    Order = c.ATTR_ORDER
                })
                .ToList();

            if (attributeModelView.Count == 0)
                return null;

            return attributeModelView;
        }
    }
}
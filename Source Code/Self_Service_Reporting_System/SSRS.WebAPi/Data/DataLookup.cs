using SSRS.WebAPi.Models.Trees.AttributesTree.ModelViews;
using Syriatel.OSS.API.DbLayer;
using Syriatel.OSS.API.Models.DynamicReport;
using Syriatel.OSS.API.Models.Trees.SubCategoryTree;
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

        
        
        //Adhoc Report
        public List<ReportLevelsModelView> GetCategoryLevelsAllData(int classificationId)
        {
            var levels = (from rl in _context.SSRS_TABLE
                          join rcl in _context.SSRS_CLASSIFICATION_TABLE on rl.ID equals rcl.SSRS_TABLE_ID
                          join rrt in _context.SSRS_RELATION_TYPE on rcl.SSRS_RELATION_TYPE_ID equals rrt.ID
                          where rcl.SSRS_CLASSIFICATION_ID == classificationId
                          select new ReportLevelsModelView()
                          {
                              Id = rcl.ID,
                              TableName = rl.NAME,
                              Order = rcl.TABLE_ORDER,
                              ParentId = rcl.PARENT_ID,
                              JunctionTable = rcl.INJUNCTION_TABLE,
                              RelationName = rrt.NAME,
                              LevelId = rl.ID

                          }).ToList();

            if (levels.Count == 0)
                return null;

            return levels;
        }

        public bool ColumnExistInTable(string tableName, string columnName)
        {
            var typeData = _context.Database
                .SqlQuery<string>(string.Format("Select COLUMN_NAME FROM user_tab_cols where table_name = '" + tableName + "'"))
                .ToList();

            if (typeData.Count == 0)
                return false;

            foreach (string value in typeData)
            {
                if (value == columnName)
                    return true;
            }

            return false;
        }

        public AdhocReportReturnedData ExecuteAdhocReportQuery(string query, List<SelectClause> selectClauses)
        {
            Helper _helper = new Helper();

            var queryResult = _helper.ExcuteQuery(query).Serialize();

            if (queryResult != null)
            {
                AdhocReportReturnedData reportData = new AdhocReportReturnedData();
                reportData.value_array = queryResult;
                reportData.H_List = new List<ReportHeader>();
                Dictionary<string, bool> RepeatedKeys = new Dictionary<string, bool>();
                foreach (SelectClause select in selectClauses)
                {
                    if (RepeatedKeys.ContainsKey(select.Name))
                    {
                        RepeatedKeys[select.Name] = true;
                    }
                    else
                    {
                        RepeatedKeys.Add(select.Name, false);
                    }
                }
                foreach (SelectClause select in selectClauses)
                {
                    if (RepeatedKeys[select.Name])
                    {
                        string CombinedName = select.TableName + ":" + select.Name;
                        ReportHeader reportHeader = new ReportHeader(CombinedName, select.ColumnName, select.ColumnType);
                        reportData.H_List.Add(reportHeader);
                    }
                    else
                    {
                        ReportHeader reportHeader = new ReportHeader(select.Name, select.ColumnName, select.ColumnType);
                        reportData.H_List.Add(reportHeader);
                    }
                }

                return reportData;
            }

            return null;
        }

        public int GetClassificationIsUnion(string classificationName)
        {
            var x = _context.SSRS_CLASSIFICATION
                .Where(a => a.NAME.ToLower() == classificationName.ToLower())
                .Select(c => c.IS_UNION)
                .FirstOrDefault();
            if (x == null)
                return 0;
            var y = (bool)x;

            if (!y)
                return 0;
            return 1;
        }

        public List<CategoryModelView> GetClassificationCategories(string classificationName)
        {
            var data = (from rc in _context.SSRS_CLASSIFICATION
                        join rcc in _context.SSRS_CLASSIFICATION_CATEGORY on rc.ID equals rcc.SSRS_CLASSIFICATION_ID
                        join rca in _context.SSRS_CATEGORY on rcc.SSRS_CATEGORY_ID equals rca.ID
                        join rm in _context.SSRS_MODULE on rca.SSRS_MODULE_ID equals rm.ID
                        where rc.NAME.ToLower() == classificationName.ToLower()
                        select new CategoryModelView()
                        {
                            ID = rca.ID,
                            Name = rca.NAME,
                            ModuleID = rca.SSRS_MODULE_ID,
                            Order = rca.CAT_ORDER
                        }).Distinct().ToList();

            if (data.Count == 0)
                return null;

            return data;
        }


    }
}
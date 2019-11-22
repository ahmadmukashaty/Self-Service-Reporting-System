using SSRS.WebAPi.Data;
using Syriatel.OSS.API.Models.Trees.SubCategoryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class AdhocUnionReportCreation
    {
        public AdhocReportReturnedData ReportData { get; set; }

        private List<CategoryModelView> Categories { get; set; }

        private List<ReportLevelsModelView> Levels { get; set; }

        private Dictionary<string, int> columnsList = new Dictionary<string, int>();


        private DataLookup _context = new DataLookup();


        private List<AdhocReportReturnedData> subReportData = new List<AdhocReportReturnedData>();


        public AdhocUnionReportCreation(AdhocReportQueryData response)
        {
            int classificationId = _context.GetClassificationID(response.ClassificationName);

            if (classificationId != 0)
            {
                this.Categories = _context.GetClassificationCategories(response.ClassificationName);

                if(this.Categories != null)
                {
                    GenerateAllQueryData(response);
                    ParsingReportData();
                }
            }
        }

        private void ParsingReportData()
        {
            
            this.ReportData = new AdhocReportReturnedData();
            this.ReportData.H_List = new List<ReportHeader>();
            this.ReportData.value_array = new List<List<string>>();

            int counter = 1;
            foreach(AdhocReportReturnedData subData in this.subReportData)
            {
                foreach(ReportHeader column in subData.H_List)
                {
                    if(!columnsList.ContainsKey(column.DisplayName))
                    {
                        columnsList.Add(column.DisplayName, counter++);
                        this.ReportData.H_List.Add(column);
                    }
                }
            }

            foreach (AdhocReportReturnedData subData in this.subReportData)
            {
                FillDataInReportData(subData);
            }
        }

        private void FillDataInReportData(AdhocReportReturnedData subData)
        {      
            foreach(List<string> row in subData.value_array)
            {
                List<string> Frow = initRowInReportData();
                for (int i=0; i< subData.H_List.Count; i++) 
                {
                    ReportHeader column = subData.H_List[i];
                    int index = this.columnsList[column.DisplayName] - 1;
                    Frow[index] = row[i];
                }
                ReportData.value_array.Add(Frow);
            }
        }

        private List<string> initRowInReportData()
        {
            List<string> row = new List<string>();

            foreach(ReportHeader column in ReportData.H_List)
            {
                row.Add("");
            }

            return row;
        }

        private void GenerateAllQueryData(AdhocReportQueryData response)
        {
            foreach (CategoryModelView cat in Categories)
            {
                int subClassificationId = _context.GetClassificationID(cat.Name);
                this.Levels = _context.GetCategoryLevelsAllData(subClassificationId);
                AdhocReportQueryData subResponse = new AdhocReportQueryData();
                subResponse.ClassificationName = cat.Name;
                subResponse.SelectClauses = GetSelectClauses(response.SelectClauses);
                subResponse.WhereClauses = GetWhereClauses(response.WhereClauses);
                AdhocCategoryReportCreation subData = null;
                if (subResponse.SelectClauses != null)
                {
                    subData = new AdhocCategoryReportCreation(subResponse);
                }
                if (subData != null)
                {
                    if (subData.ReportData != null)
                        subReportData.Add(subData.ReportData);
                }
            }
        }


        private List<SelectClause> GetSelectClauses(List<SelectClause> response)
        {
            List<SelectClause> selectClauses = new List<SelectClause>();
            foreach(SelectClause select in response)
            {
                if (SearchTableInLevels(select.TableName))
                    selectClauses.Add(select);
            }

            if (selectClauses.Count > 0)
                return selectClauses;

            return null;
        }

        private List<WhereClause> GetWhereClauses(List<WhereClause> response)
        {
            List<WhereClause> whereClauses = new List<WhereClause>();
            foreach (WhereClause where in response)
            {
                if (SearchTableInLevels(where.TableName))
                    whereClauses.Add(where);
            }

            if (whereClauses.Count > 0)
                return whereClauses;

            return null;
        }

        public bool SearchTableInLevels(string TableName)
        {
            foreach(ReportLevelsModelView level in Levels)
            {
                if (level.TableName == TableName)
                    return true;
            }
            return false;
        }
    }
}
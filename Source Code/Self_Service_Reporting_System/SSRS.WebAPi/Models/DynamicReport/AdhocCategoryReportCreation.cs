
using SSRS.WebAPi.Data;
using SSRS.WebAPi.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class AdhocCategoryReportCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        private List<ReportLevelsModelView> Levels { get; set; }

        private GraphTraversal graphTraversal { get; set; }

        public AdhocReportReturnedData ReportData { get; set; }

        public AdhocCategoryReportCreation(AdhocReportQueryData response)
        {
            int classificationId = OracleHelper.GetClassificationID(response.ClassificationName);
            int classificationUnionType = OracleHelper.GetClassificationIsUnion(response.ClassificationName);

            if (classificationId != 0)
            {
                if (classificationUnionType == 0)
                {
                    this.Levels = OracleHelper.GetCategoryLevelsAllData(classificationId);
                }
                else
                {
                    AdhocUnionReportCreation UnionReport = new AdhocUnionReportCreation(response);
                    this.ReportData = UnionReport.ReportData;
                }
            }

            if(this.Levels != null)
            {
                string JoinType = GetJoinType(response.JoinType);

                if (JoinType != null)
                {
                    this.graphTraversal = new GraphTraversal(this.Levels, JoinType);
                    AddEffectedTablesToGraph(response);
                    string query = GenerateReportQuery(response);
                    this.ReportData = OracleHelper.ExecuteAdhocReportQuery(query, response.SelectClauses);
                }
            }
        }

        private string GenerateReportQuery(AdhocReportQueryData response)
        {
            string join = "";
            List<string> junctionTables = this.graphTraversal.GenerateJoinClause(ref join);

            string query = "SELECT DISTINCT ";
            bool firstSelect = true;
            foreach (SelectClause select in response.SelectClauses)
            {
                if (firstSelect)
                {
                    query += (select.TableName + "." + select.ColumnName + " ");
                    firstSelect = false;
                }
                else
                    query += (" , " + select.TableName + "." + select.ColumnName + " ");

            }
            query += " FROM " + join;

            bool firstWhere = true;
            if (response.WhereClauses != null)
            {
                foreach (WhereClause where in response.WhereClauses)
                {
                    if(firstWhere == true)
                    {
                        query += (" WHERE " + where.TableName + "." + where.ColumnName + " IN ( ");
                        firstWhere = false;
                    }
                    else
                    {
                        query += (" AND " + where.TableName + "." + where.ColumnName + " IN ( ");
                    }

                    bool InValueFirstTime = true;
                    string InValues = "";
                    foreach (string value in where.Values)
                    {
                        

                        if(InValueFirstTime)
                        {
                            InValues +=( " '" + value + "' ");
                            InValueFirstTime = false;
                        }
                        else
                        {
                            InValues += ", '" + value + "'";
                        }
                    }

                    query += (InValues + "  ) ");
                }
            }

            if(junctionTables != null && junctionTables.Count > 0)
            {
                foreach (string table in junctionTables)
                {
                    if (firstWhere == true)
                    {
                        query += (" WHERE " + table + ".RETIRE_DATE IS NULL ");
                        firstWhere = false;
                    }
                    else
                        query += (" AND " + table + ".RETIRE_DATE IS NULL ");

                }
            }

            return query;
        }

        private void AddEffectedTablesToGraph(AdhocReportQueryData response)
        {
            List<string> tables = new List<string>();

            foreach (SelectClause selectedLevel in response.SelectClauses)
            {
                tables.Add(selectedLevel.TableName);
            }

            foreach (WhereClause filterLevel in response.WhereClauses)
            {
                tables.Add(filterLevel.TableName);
            }

            this.graphTraversal.AddTablesToGraph(tables);
        }

        private string GetJoinType(string join)
        {
            if (join == null)
                return Constants.JoinType.INNER_JOIN;

            string InnerJoin = Constants.JoinType.INNER_JOIN;
            string LeftOuterJoin = Constants.JoinType.LEFT_OUTER_JOIN;

            if (InnerJoin.Contains(join.ToUpper()) || Regex.Replace(InnerJoin, @"\s+", "").Contains(join.ToUpper()))
            {
                return Constants.JoinType.INNER_JOIN;
            }
            else if (LeftOuterJoin.Contains(join.ToUpper()) || Regex.Replace(LeftOuterJoin, @"\s+", "").Contains(join.ToUpper()))
            {
                return Constants.JoinType.LEFT_OUTER_JOIN;
            }

            return Constants.JoinType.INNER_JOIN;
        }


    }
}
using SSRS.WebAPi.Data;
using SSRS.WebAPi.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicReport
{
    public class GraphTraversal
    {
        private int sourceIndex { get; set; }

        private int InitialIndex { get; set; }

        private Dictionary<int, Dictionary<int, bool>> GraphLevels { get; set; }

        private DataLookup _context = new DataLookup();

        private List<string> junctionTables = new List<string>();

        private string JoinType { get; set; }

        public Dictionary<int,bool> SelectedLevels { get; set; }

        public List<ReportLevelsModelView> levels { get; set; }

        public GraphTraversal(List<ReportLevelsModelView> levels, string JoinType)
        {
            this.SelectedLevels = new Dictionary<int, bool>();
            this.GraphLevels = new Dictionary<int, Dictionary<int, bool>>();
            this.levels = levels;
            this.JoinType = JoinType;
            Init();
            //FillLevelsRelations(this.sourceIndex);
        }

        private void Init()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                for (int j = 0; j< levels.Count; j++)
                {
                    if (!GraphLevels.ContainsKey(i))
                        GraphLevels[i] = new Dictionary<int, bool>();

                    GraphLevels[i][j] = false;
                }

                if (levels[i].ParentId == null)
                {
                    this.InitialIndex = i;
                }

                this.SelectedLevels[i] = false;
            }
            FillLevelsRelations();
        }

        private void FillLevelsRelations()
        {
            for(int i=0; i< levels.Count; i++)
            {
                for(int j=0; j< levels.Count; j++)
                {
                    if (levels[i].ParentId == this.levels[j].Id)
                    {
                        Add_Edge(j, i);
                    }
                }
            }
        }

        private void Add_Edge(int source, int dest)
        {
            this.GraphLevels[source][dest] = true;
            this.GraphLevels[dest][source] = true;
        }

        public List<string> GenerateJoinClause(ref string join)
        {
            bool firstJoin = true;
            DFS(ref join, this.InitialIndex, ref firstJoin);

            if (this.junctionTables.Count > 0)
                return this.junctionTables;

            return null;
        }

        internal void AddTablesToGraph(List<string> tables)
        {
            this.sourceIndex = GetLevelIndex(tables[0]);
            this.SelectedLevels[this.sourceIndex] = true;

            foreach (string table in tables)
            {
                BFS(table);
            }
        }

        private int GetLevelIndex(string tableName)
        {
            for (int i = 0; i < levels.Count; i++)
                if (levels[i].TableName == tableName)
                    return i;

            return -1;
        }

        private int GetLevelIndex(int Id)
        {
            for (int i = 0; i < levels.Count; i++)
                if (levels[i].Id == Id)
                    return i;

            return -1;
        }

        public void BFS(string tableName)
        {
            Queue queue = new Queue();
            Dictionary<int, bool>  visited = new Dictionary<int, bool>();
            Dictionary<int, int>  pred = new Dictionary<int, int>();
            Dictionary<int, int>  dist = new Dictionary<int, int>();

            int destIndex = GetLevelIndex(tableName);

            if (destIndex == -1 || this.SelectedLevels[destIndex] == true)
                return;

            for (int i = 0; i < levels.Count; i++)
            {
                visited[i] = false; dist[i] = 10000; pred[i] = -1;
            }

            visited[this.sourceIndex] = true;
            dist[this.sourceIndex] = 0;
            queue.Enqueue(this.sourceIndex);

            while (queue.Count != 0)
            {
                int u = (int)queue.Dequeue();

                for (int i = 0; i < levels.Count; i++)
                {
                    if (GraphLevels[u][i] == true && visited[i] == false)
                    {
                        visited[i] = true;
                        dist[i] = dist[u] + 1;
                        pred[i] = u;
                        queue.Enqueue(i);

                        if (i == destIndex)
                        {
                            int crawl = destIndex;
                            this.SelectedLevels[crawl] = true;
                            while (pred[crawl] != -1)
                            {
                                this.SelectedLevels[pred[crawl]] = true;
                                crawl = pred[crawl];
                            }
                        }
                    }
                }
            }
        }

        private void DFS(ref string join,int levelIndex, ref bool firstJoin)
        {
            if (this.SelectedLevels[levelIndex] == true)
            {
                if (firstJoin)
                {
                    join = levels[levelIndex].TableName;
                    firstJoin = false;
                }
                else
                {
                    int parentIndex = GetLevelIndex((int)levels[levelIndex].ParentId);
                    string ParentTableName = levels[parentIndex].TableName;
                    string tableName = levels[levelIndex].TableName;

                    string relationName = (levels[parentIndex].ParentId == levels[levelIndex].Id) ? levels[parentIndex].RelationName : levels[levelIndex].RelationName;
                    string junctionTable = (levels[parentIndex].ParentId == levels[levelIndex].Id) ? levels[parentIndex].JunctionTable : levels[levelIndex].JunctionTable;

                    if (relationName == Constants.Relations.ONE_TO_ONE)
                    {
                        join += (this.JoinType + levels[levelIndex].TableName + " ON ");
                        join += (ParentTableName + ".ID = " + tableName + ".ID");
                    }


                    if (relationName == Constants.Relations.ONE_TO_MANY)
                    {
                        join += (this.JoinType + levels[levelIndex].TableName + " ON ");

                        if (_context.ColumnExistInTable(ParentTableName, tableName + "_ID"))
                        {
                            join += (tableName + ".ID = " + ParentTableName + "." + tableName + "_ID");
                        }

                        if (_context.ColumnExistInTable(tableName, ParentTableName + "_ID"))
                        {
                            join += (ParentTableName + ".ID = " + tableName + "." + ParentTableName + "_ID");
                        }
                    }

                    if (relationName == Constants.Relations.MANY_TO_MANY)
                    {
                        join += (this.JoinType + junctionTable + " ON ");
                        join += (ParentTableName + ".ID = " + junctionTable + "." + ParentTableName + "_ID");
                        join += (this.JoinType + tableName + " ON ");
                        join += (tableName + ".ID = " + junctionTable + "." + tableName + "_ID");

                        junctionTables.Add(junctionTable);
                    }
                }
            }

            for (int i=0; i<levels.Count; i++)
            {
                if (levels[i].ParentId == levels[levelIndex].Id)
                {
                    DFS(ref join, i, ref firstJoin);
                } 
            }
        }

    }
}
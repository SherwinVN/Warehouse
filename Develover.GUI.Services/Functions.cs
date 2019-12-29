﻿using Develover.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using static Develover.Utilities.DelTypeData;
using static Develover.Utilities.Enum;

namespace Develover.GUI.Services
{
    public class Functions 
    {
       public DataBase dataBase;
        public Functions()
        {
            dataBase = new DataBase();
        }
        //public List<Dictionary<string, string>> LoadListControlAndFile(iDeveloverControl, string Table)
        //{
        //    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        //    Dictionary<string, string> dictionary = new Dictionary<string, string>();

        //    return list;
        //}
        public string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public bool DeleteRowTable(string Table, string NameFieldKey, string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DELETE FROM [" + Table + "] WHERE [" + NameFieldKey + "] = '" + value + "'");

            string result = stringBuilder.ToString();
            return dataBase.ExecuteNonQuery(result) != -1;

        }

        public bool DeleteRowTable(string Table)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DELETE FROM [" + Table + "]");

            string result = stringBuilder.ToString();
            return dataBase.ExecuteNonQuery(result) != -1;

        }
        public bool UpdateTable(Dictionary<string, string> ListFilesValues, string Table, string NameFieldKey, string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE a SET ");
            foreach (string key in ListFilesValues.Keys)
            {
                stringBuilder.Append("[" + key + "] = N'" + ListFilesValues[key].Replace("'", "''") + "',");
            }

            string result = stringBuilder.ToString(0, stringBuilder.Length - 1) + " FROM  [" + Table + "] a  WHERE  [" + NameFieldKey + "] = N'" + value + "' ";
            return dataBase.ExecuteNonQuery(result) != -1;

        }
        public bool UpdateTable(Dictionary<string, string> ListFilesValues, string Table)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE a SET ");
            foreach (string key in ListFilesValues.Keys)
            {
                stringBuilder.Append("[" + key + "] = N'" + ListFilesValues[key].Replace("'", "''") + "',");
            }

            string result = stringBuilder.ToString(0, stringBuilder.Length - 1) + ")" + " FROM  [" + Table + "] a ";
            return dataBase.ExecuteNonQuery(result) != -1;

        }
        public bool InsertIntoTable(Dictionary<string, string> ListFilesValues, string Table, string NameFieldKey, string value)
        {
            StringBuilder stringBuilderField = new StringBuilder();
            StringBuilder stringBuilderValue = new StringBuilder();
            stringBuilderField.Append("INSERT INTO [" + Table + "] ([" + NameFieldKey + "],");
            stringBuilderValue.Append(" Values(N'" + value + "',");
            foreach (string key in ListFilesValues.Keys)
            {
                stringBuilderField.Append("[" + key + "],");
                stringBuilderValue.Append("N'" + ListFilesValues[key].Replace("'", "''") + "',");
            }
            string result = stringBuilderField.ToString(0, stringBuilderField.Length - 1) + ")    " + stringBuilderValue.ToString(0, stringBuilderValue.Length - 1) + ")";
            return dataBase.ExecuteNonQuery(result) != -1;

        }
        public bool InsertIntoTable(Dictionary<string, string> ListFilesValues, string Table)
        {
            StringBuilder stringBuilderField = new StringBuilder();
            StringBuilder stringBuilderValue = new StringBuilder();
            stringBuilderField.Append("INSERT INTO [" + Table + "] (");
            stringBuilderValue.Append("Values(");
            foreach (string key in ListFilesValues.Keys)
            {
                stringBuilderField.Append("[" + key + "],");
                stringBuilderValue.Append("N'" + ListFilesValues[key].Replace("'", "''") + "',");
            }
            string result = stringBuilderField.ToString(0, stringBuilderField.Length - 1) + ")    " + stringBuilderValue.ToString(0, stringBuilderValue.Length - 1) + ")";
            return dataBase.ExecuteNonQuery(result) != -1;

        }
        public bool CheckExistsValueInTable(string TableName,string ColumnName,string value,string NameFieldCodePrimary, string CodePrimary) {
           return dataBase.GetDataTable("SELECT top 1 [" + ColumnName + "] FROM [" + TableName + "] WHERE [" + ColumnName + "] = N'" + value + "' AND ["+ NameFieldCodePrimary + "] <> N'"+ CodePrimary + "'").Rows.Count >0;
        }
        public bool CheckExistsValueInTable(string TableName, string Where, string NameFieldCodePrimary, string CodePrimary)
        {
            return dataBase.GetDataTable("SELECT top 1 [" + NameFieldCodePrimary + "] FROM [" + TableName + "] WHERE " + Where + " AND [" + NameFieldCodePrimary + "] <> N'" + CodePrimary + "'").Rows.Count > 0;
        }
        public bool CheckExistsColumnInTableOrView(string TypeObject,string ObjectName,string ColumnName)
        {
            return dataBase.GetDataTable("SELECT top 1 id FROM sysobjects WHERE [xtype] = '"+ TypeObject+"'  and  [name] = N'"+ ObjectName+"' and [id] IN (SELECT [id] FROM syscolumns WHERE [name] =  N'"+ ColumnName+"' )").Rows.Count > 0;
        }
        public bool CheckExistsObject(string TypeObject, string ObjectName)
        {
            return dataBase.GetDataTable("SELECT top 1 id FROM sysobjects WHERE [xtype] = '" + TypeObject + "'  and  [name] = N'" + ObjectName + "' ").Rows.Count > 0;
        }

        public class GetObjects {

            public DataBase dataBase;

            public GetObjects()
            {
                dataBase = new DataBase();
            }

            public List<TypeColumns> GetSysDelGridcolumns(string Model)
            {
                List<TypeColumns> typeColumns = new List<TypeColumns>();
                TypeColumns typeColumns_;
                using (DataTable data = dataBase.GetDataTable("SELECT * FROM sysDELGridColumns WHERE Model = '" + Model + "' ORDER BY OrderNo"))
                {
                    foreach (DataRow dr in data.Rows)
                    {
                        typeColumns_ = new TypeColumns();
                        typeColumns_.Caption = dr["Caption"]?.ToString();
                        typeColumns_.Name = dr["Name"]?.ToString();
                        typeColumns_.FieldName = dr["Name"]?.ToString();
                        typeColumns_.Visible = false;
                        bool.TryParse(dr["Visible"]?.ToString(), out typeColumns_.Visible);
                        typeColumns_.AllowFocus = false;
                        bool.TryParse(dr["AllowFocus"]?.ToString(), out typeColumns_.AllowFocus);
                        typeColumns_.AllowEdit = false;
                        typeColumns_.Width = 10;
                        int.TryParse(dr["Width"].ToString(), out typeColumns_.Width);
                        typeColumns_.Index = 10;
                        int.TryParse(dr["OrderNo"].ToString(), out typeColumns_.Index);

                        typeColumns_.SumaryType = GetSumaryType(dr["SumaryType"]?.ToString());
                        typeColumns_.StringFormat = dr["StringFormat"]?.ToString();

                        typeColumns_.TypeColumn = GetTypeColumn(dr["Type"]?.ToString());
                        bool.TryParse(dr["AllowEdit"]?.ToString(), out typeColumns_.AllowEdit);


                        typeColumns_.ChildModel = dr["ChildModel"]?.ToString();
                        typeColumns_.SQLData = dr["DataSource"]?.ToString();
                        typeColumns_.KeyMember = dr["KeyMember"]?.ToString();
                        typeColumns_.DisplayMember = dr["DisplayMember"]?.ToString();
                        typeColumns_.ValueMember = dr["ValueMember"]?.ToString();
                        typeColumns_.NullText = dr["NullText"]?.ToString();

                        typeColumns_.Model = typeColumns_.ChildModel;
                        typeColumns.Add(typeColumns_);
                    }
                }
                return typeColumns;
            }

            public List<TypeColumns> GetSysDelGridcolumnsView(string Model)
            {
                List<TypeColumns> typeColumns = new List<TypeColumns>();
                TypeColumns typeColumns_;
                using (DataTable data = dataBase.GetDataTable("SELECT * FROM sysDELGridColumns WHERE Model = '" + Model + "' ORDER BY OrderNo"))
                {
                    foreach (DataRow dr in data.Rows)
                    {
                        typeColumns_ = new TypeColumns();
                        typeColumns_.Caption = dr["Caption"]?.ToString();
                        typeColumns_.Name = dr["Name"]?.ToString();
                        typeColumns_.FieldName = dr["Name"]?.ToString();
                        typeColumns_.Visible = false;
                        bool.TryParse(dr["Visible"]?.ToString(), out typeColumns_.Visible);
                        typeColumns_.AllowFocus = false;
                        bool.TryParse(dr["AllowFocus"]?.ToString(), out typeColumns_.AllowFocus);
                        typeColumns_.AllowEdit = false;
                        typeColumns_.Width = 10;
                        int.TryParse(dr["Width"].ToString(), out typeColumns_.Width);
                        typeColumns_.Index = 10;
                        int.TryParse(dr["OrderNo"].ToString(), out typeColumns_.Index);

                        typeColumns_.SumaryType = GetSumaryType(dr["SumaryType"]?.ToString());
                        typeColumns_.StringFormat = dr["StringFormat"]?.ToString();

                        typeColumns_.TypeColumn = GetTypeColumn("Text");


                        typeColumns_.ChildModel = dr["ChildModel"]?.ToString();
                        typeColumns_.SQLData = dr["DataSource"]?.ToString();
                        typeColumns_.KeyMember = dr["KeyMember"]?.ToString();
                        typeColumns_.DisplayMember = dr["DisplayMember"]?.ToString();
                        typeColumns_.ValueMember = dr["ValueMember"]?.ToString();
                        typeColumns_.NullText = dr["NullText"]?.ToString();

                        //typeColumns_.TypeColumnGridLookup = GetSysDelGridcolumnsView(typeColumns_.ChildModel);
                        typeColumns.Add(typeColumns_);
                    }
                }
                return typeColumns;
            }

        }


        public class DataBase
        {
            public SqlDataProvider sqlDataProvider;

            public DataBase()
            {
                sqlDataProvider = new SqlDataProvider();
            }

            public string GetSQLConnectionString()
            {

                return sqlDataProvider.GetSQLConnectionString();
            }

            public bool CheckLogin()
            {
                return sqlDataProvider.CheckLogin();
            }

            public bool OpenConection()
            {
                return sqlDataProvider.OpenConection();
            }

            public bool CloseConection()
            {
                return sqlDataProvider.CloseConection();
            }

            public bool ChangeDataBase()
            {
                return sqlDataProvider.ChangeDataBase();

            }

            public DataTable GetDataTable(string query)
            {
                return sqlDataProvider.GetDataTable(query);
            }

            public async Task<DataTable> GetDataTableAsync(string query)
            {
                return await sqlDataProvider.GetDataTableAsync(query);
            }

            public DataTable GetDataTableParallel(string query)
            {
                return sqlDataProvider.GetDataTableParallel(query);
            }

            public async Task<DataTable> GetDataTableParallelAsync(string query)
            {
                return await sqlDataProvider.GetDataTableParallelAsync(query);
            }

            public DataSet GetDataSet(string query)
            {
                return sqlDataProvider.GetDataSet(query);
            }

            public DataSet GetDataSet(string query, string tableName)
            {
                return sqlDataProvider.GetDataSet(query, tableName);
            }


            public int ExecuteNonQuery(string query)
            {
                return sqlDataProvider.ExecuteNonQuery(query);
            }

            public async Task<int> ExecuteNonQueryAsync(string query)
            {
                return await sqlDataProvider.ExecuteNonQueryAsync(query);
            }

            public object ExecuteScalar(string query)
            {
                return sqlDataProvider.ExecuteScalar(query);
            }

            public async Task<object> ExecuteScalarAsync(string query)
            {
                return await sqlDataProvider.ExecuteScalarAsync(query);
            }

            public void ExecuteProcedure(string procedureName, SqlParameter[] paramsIn)
            {
                ExecuteProcedure(procedureName, paramsIn);
            }

            public async Task ExecuteProcedureAsync(string procedureName, SqlParameter[] paramsIn)
            {
                await ExecuteProcedureAsync(procedureName, paramsIn);
            }

            public object ExecuteProcedureOut(string procedureName, SqlParameter[] paramsIn, SqlParameter paramOut)
            {

                return ExecuteProcedureOut(procedureName, paramsIn, paramOut);
            }

            public async Task<object> ExecuteProcedureOutAsync(string procedureName, SqlParameter[] paramsIn, SqlParameter paramOut)
            {
                return await ExecuteProcedureOutAsync(procedureName, paramsIn, paramOut);
            }
        }

    }
}
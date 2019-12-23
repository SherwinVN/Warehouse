﻿using Develover.Core;
using DevExpress.Data.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.Data;
using static Develover.Utilities.DelTypeData;
using static Develover.Utilities.Enum;

namespace Develover.GUI.Controls
{
    public partial class DeveloverGridControl : GridControl, IDeveloverControl
    {
        SqlDataProvider sqlDataProvider = new SqlDataProvider();
        protected override BaseView CreateDefaultView()
        {
            return CreateView("DeveloverGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new DeveloverGridViewInfoRegistrator());
        }

        public void BuildGridControls(string SQLData, List<TypeColumns> typeColumns)
        {
            DataSource = sqlDataProvider.GetDataTable(SQLData);
            ((DeveloverGridView)this.DefaultView).BuidColumns(typeColumns);
        }
        public void BuildGridControls(string SQLData, string Model)
        {
            List<TypeColumns> typeColumns = GetSysDelGridcolumns(Model, true);
            ((DeveloverGridView)this.DefaultView).BuidColumns(typeColumns);
           DataSource = sqlDataProvider.GetDataTable(SQLData);
        }

        private List<TypeColumns> GetSysDelGridcolumns(string Model, bool RunOne)
        {
            List<TypeColumns> typeColumns = new List<TypeColumns>();
            TypeColumns typeColumns_;
            using (DataTable data = sqlDataProvider.GetDataTable("SELECT * FROM sysDELGridColumns WHERE Model = '" + Model + "'"))
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
                    bool.TryParse(dr["AllowEdit"]?.ToString(), out typeColumns_.AllowEdit);
                    typeColumns_.Width = 10;
                    int.TryParse(dr["Width"].ToString(), out typeColumns_.Width);
                    typeColumns_.Index = 10;
                    int.TryParse(dr["OrderNo"].ToString(), out typeColumns_.Index);

                    typeColumns_.TypeColumn = GetTypeColumn(dr["Type"]?.ToString());

                    typeColumns_.ChildModel = RunOne ? dr["ChildModel"]?.ToString():"";
                    typeColumns_.SQLData = dr["DataSource"]?.ToString();
                    typeColumns_.KeyMember = dr["KeyMember"]?.ToString();
                    typeColumns_.DisplayMember = dr["DisplayMember"]?.ToString();
                    typeColumns_.ValueMember = dr["ValueMember"]?.ToString();
                    typeColumns_.NullText = dr["NullText"]?.ToString();

                    if(RunOne && dr["ChildModel"]?.ToString() != string.Empty)
                    typeColumns_.TypeColumnGridLookup = GetSysDelGridcolumns(typeColumns_.ChildModel, false);
                    typeColumns.Add(typeColumns_);
                }
            }
            return typeColumns;
        }

        private static EnumTypeColumns GetTypeColumn(string model)
        {
            EnumTypeColumns enumTypeColumns = new EnumTypeColumns();
            switch (model)
            {
                case "Number":
                    enumTypeColumns = EnumTypeColumns.Number;
                    break;

                case "Check":
                    enumTypeColumns = EnumTypeColumns.Check;
                    break;
                case "Combobox":
                    enumTypeColumns = EnumTypeColumns.Combobox;
                    break;
                case "Date":
                    enumTypeColumns = EnumTypeColumns.Date;
                    break;
                case "Gridlookup":
                    enumTypeColumns = EnumTypeColumns.Gridlookup;
                    break;
                case "Text":
                    enumTypeColumns = EnumTypeColumns.Text;
                    break;
                case "Time":
                    enumTypeColumns = EnumTypeColumns.Time;
                    break;
            }
            return enumTypeColumns;
        }
    }
}

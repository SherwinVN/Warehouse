﻿using Develover.GUI.Controls;
using Develover.GUI.Services;
using DevExpress.Data.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using static Develover.GUI.Services.Functions;
using static Develover.Utilities.DelTypeData;

namespace Develover.GUI.RepositoryItems
{
    [UserRepositoryItem("RegisterDeveloverGridLookUpEdit")]
    public class DeveloverRepositoryItemGridLookUpEdit : RepositoryItemGridLookUpEdit
    {

        Functions functions = new Functions();
        GetObjects getObjects = new GetObjects();
        string sqlData_; string model_;
        public const string DeveloverGridLookUpEditName = "DeveloverGridLookUpEdit";

        public override string EditorTypeName { get { return DeveloverGridLookUpEditName; } }
        static DeveloverRepositoryItemGridLookUpEdit() { RegisterDeveloverGridLookUpEdit(); }

        private GridLookUpEdit edt;
        public DeveloverRepositoryItemGridLookUpEdit() : base()
        {
            PopupView = new DeveloverGridView();
            ShowPopupShadow = true;
            AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            ValidateOnEnterKey = true;
            Popup += DeveloverRepositoryItemGridLookUpEdit_Popup; ;
            Closed += DeveloverRepositoryItemGridLookUpEdit_Closed;

            this.Buttons.Add(new EditorButton(ButtonPredefines.Combo));
            EditorButton btn = new EditorButton(ButtonPredefines.Plus, "Add");
            btn.Click += delegate { System.Windows.Forms.MessageBox.Show("Test"); };
            this.Buttons.Add(btn);
        }

        protected override ColumnView CreateViewInstance()
        {
            switch (ViewType)
            {
                case GridLookUpViewType.BandedView:
                    return new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
                case GridLookUpViewType.AdvBandedView:
                    return new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            }
            return new DeveloverGridView();
        }

        public static void RegisterDeveloverGridLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(DeveloverGridLookUpEditName,
              typeof(DeveloverGridLookUpEdit), typeof(DeveloverRepositoryItemGridLookUpEdit),
              typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
        }
        protected override DevExpress.XtraGrid.GridControl CreateGrid()
        {
            return new DeveloverGridControl();
        }


        private void DeveloverRepositoryItemGridLookUpEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            edt = ((GridLookUpEdit)sender);
            edt.Properties.View.DoubleClick -= View_DoubleClick;
            edt.Properties.View.Click -= View_DoubleClick;
            edt = null;
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (edt != null)
            {
                edt.EditValue = ((GridView)sender).GetFocusedDataRow()[this.ValueMember];
                edt.ClosePopup();
            }
        }

        private void DeveloverRepositoryItemGridLookUpEdit_Popup(object sender, EventArgs e)
        {
            edt = ((GridLookUpEdit)sender);
            edt.Properties.View.DoubleClick += View_DoubleClick;
            edt.Properties.View.Click += View_DoubleClick;
        }

        public void LoadData(DataTable data, string model, string keyMember = "", string displayMember = "", string valueMember = "", string nullText = "")
        {
            DisplayMember = displayMember;
            ValueMember = valueMember;
            KeyMember = keyMember;
            NullText = nullText;
            model_ = model;

            ((DeveloverGridView)PopupView).BuidColumnsView(getObjects.GetSysDelGridcolumnsView(model_));
            DataSource = data;
        }
        public void LoadData(string sqlData, string model, string keyMember = "", string valueMember = "", string displayMember = "", string nullText = "")
        {
            sqlData_ = sqlData;
            KeyMember = keyMember;
            ValueMember = valueMember;
            DisplayMember = displayMember;
            NullText = nullText;
            model_ = model;

            LoadData();
        }

        public void LoadData()
        {
            ((DeveloverGridView)PopupView).BuidColumnsView(getObjects.GetSysDelGridcolumnsView(model_));
            DataSource = functions.dataBase.GetDataTable(sqlData_);
        }
    }
}

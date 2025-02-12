﻿using DevExpress.XtraEditors;
using System.Windows.Forms;
using static Develover.Utilities.Enum;

namespace Develover.GUI.Controls
{
    public class DeveloverTextEditAutoComplete : DeveloverTextEdit, IDeveloverControl
    {
        AutoCompleteStringCollection collection;
        public DeveloverTextEditAutoComplete(){
            Name = "TextEditAutoComplete";
            collection = new AutoCompleteStringCollection();
            MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MaskBox.AutoCompleteCustomSource = collection;
            TypeFieldColumns = EnumTypeColumns.Text;
        }
        public void init(string[] AutoCompleteSource, bool RetainTheOld) {
            if (RetainTheOld)
            { collection.AddRange(AutoCompleteSource); }
            else {
                collection.Clear();
                collection.AddRange(AutoCompleteSource);
            }
        }
        public void Reload() {
            collection.Clear();
        }
    }
}

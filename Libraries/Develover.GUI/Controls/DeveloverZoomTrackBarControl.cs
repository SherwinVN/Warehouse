﻿using Develover.Utilities;
using DevExpress.XtraEditors;
using static Develover.Utilities.Enum;

namespace Develover.GUI.Controls
{
    public class DevloverZoomTrackBarControl : ZoomTrackBarControl, IDeveloverControl
    {
        string fieldBinding;
        EnumTypeColumns typeFieldColumns;
        public string FieldBinding { get => fieldBinding; set => fieldBinding = value; }
        public EnumTypeColumns TypeFieldColumns { get => typeFieldColumns; set => typeFieldColumns = value; }
    }
}

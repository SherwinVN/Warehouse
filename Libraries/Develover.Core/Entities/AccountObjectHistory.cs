//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Develover.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountObjectHistory
    {
        public System.Guid UserID { get; set; }
        public System.Guid LocationID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Content { get; set; }
        public string Machine { get; set; }
        public string LocalIP { get; set; }
        public string PublicIP { get; set; }
    }
}

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
    
    public partial class SchedulerTaskDependency
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> DependentId { get; set; }
        public int Type { get; set; }
    }
}

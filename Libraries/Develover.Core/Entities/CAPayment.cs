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
    
    public partial class CAPayment
    {
        public System.Guid VoucherID { get; set; }
        public System.DateTime VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<System.Guid> BranchID { get; set; }
        public System.Guid AccountObjectID { get; set; }
        public string AccountObjectName { get; set; }
        public string AccountObjectAddress { get; set; }
        public string AccountObjectContactName { get; set; }
        public string noted { get; set; }
        public string CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountForeignCurrency { get; set; }
    }
}
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
    
    public partial class PUVoucher
    {
        public System.Guid VoucherID { get; set; }
        public System.Guid BranchID { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public System.Guid AccountObjectID { get; set; }
        public string AccountObjectName { get; set; }
        public string AccountObjectAddress { get; set; }
        public string AccountObjectBankAccount { get; set; }
        public string AccountObjectBankName { get; set; }
        public string AccountObjectContactName { get; set; }
        public string Receiver { get; set; }
        public string Noted { get; set; }
        public string CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public decimal TotalAmountForeignCurrency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVATAmountForeignCurrency { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalDiscountAmountForeignCurrency { get; set; }
        public decimal TotalDiscountAmount { get; set; }
    }
}
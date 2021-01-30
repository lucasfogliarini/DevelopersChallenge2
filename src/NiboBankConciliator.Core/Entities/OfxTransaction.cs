using System;

namespace NiboBankConciliator.Core.Entities
{
    public struct OfxTransaction
    {
        public TransType TransType { get; set; }
        public decimal TransAmount { get; set; }
        public DateTime DatePosted { get; set; }
        public string Memo { get; set; }
    }

    public enum TransType
    {
        Debit, 
        Credit
    }
}

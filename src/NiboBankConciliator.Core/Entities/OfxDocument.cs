using System.Collections.Generic;

namespace NiboBankConciliator.Core.Entities
{
    public class OfxDocument
    {
        public List<OfxTransaction> Transactions { get; set; }
        public string AccountType { get; set; }
        public string AccountID { get; set; }
        public string BankID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string OfxHeader { get; set; }
        public string Data { get; set; }
        public string Version { get; set; }
        public string Security { get; set; }
        public string Encoding { get; set; }
        public string Charset { get; set; }
        public string Compression { get; set; }
        public string OldFileUID { get; set; }
        public string NewFileUID { get; set; }
    }
}

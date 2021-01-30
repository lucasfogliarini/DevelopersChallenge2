using NiboBankConciliator.Core.Entities;
using System.Collections.Generic;

namespace NiboBankConciliator.Core.Services
{
    public interface IBankReconciliationService
    {
        BankAccount Reconcile(IEnumerable<OfxDocument> ofxDocuments);
        BankAccount ReconcileAndAddTransactions(IEnumerable<OfxDocument> ofxDocuments);
        BankAccount GetBankAccount(int bankAccountId);
        IEnumerable<BankAccount> GetBankAccounts();
    }
}

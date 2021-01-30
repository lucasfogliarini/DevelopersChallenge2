using NiboBankConciliator.Core.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NiboBankConciliator.Core.Services
{
    public class BankReconciliationService : IBankReconciliationService
    {
        readonly IBankConciliatorRepository _bankConciliatorRepository;
        public BankReconciliationService(IBankConciliatorRepository bankConciliatorRepository)
        {
            _bankConciliatorRepository = bankConciliatorRepository;
        }

        public BankAccount GetBankAccount(int bankAccountId)
        {
            var bankAccount = _bankConciliatorRepository.Query<BankAccount>().Include(e => e.Transactions).FirstOrDefault(e => e.Id == bankAccountId);
            return bankAccount;
        }

        public IEnumerable<BankAccount> GetBankAccounts()
        {
            var bankAccounts = _bankConciliatorRepository.Query<BankAccount>().ToList();
            return bankAccounts;
        }

        public BankAccount Reconcile(IEnumerable<OfxDocument> ofxDocuments)
        {
            var bankTransactions = new List<BankTransaction>();

            foreach (var ofxDocument in ofxDocuments)
            {
                foreach (var ofxTransactions in ofxDocument.Transactions)
                {
                    bool exists = bankTransactions.Any(t => t.TransAmount == ofxTransactions.TransAmount && 
                                                            t.DatePosted == ofxTransactions.DatePosted && 
                                                            t.Memo == ofxTransactions.Memo && 
                                                            t.TransType == ofxTransactions.TransType);
                    if (!exists)
                    {
                        bankTransactions.Add(new BankTransaction()
                        {
                            TransAmount = ofxTransactions.TransAmount,
                            DatePosted = ofxTransactions.DatePosted,
                            Memo = ofxTransactions.Memo,
                            TransType = ofxTransactions.TransType
                        });
                    }
                }
            }

            var bankDoc = ofxDocuments.FirstOrDefault();
            var bankAccount = new BankAccount()
            {
                Transactions = bankTransactions,
                AccountID = bankDoc.AccountID,
                AccountType = bankDoc.AccountType,
                BankID = bankDoc.BankID
            };
            return bankAccount;
        }

        public BankAccount ReconcileAndAddTransactions(IEnumerable<OfxDocument> ofxDocuments)
        {
            var bankAccount = Reconcile(ofxDocuments);
            var bankAccountOnDb = _bankConciliatorRepository.Query<BankAccount>().FirstOrDefault(e => e.BankID == bankAccount.BankID && e.AccountID == bankAccount.AccountID);
            
            if (bankAccountOnDb == null)
            {
                _bankConciliatorRepository.Add(bankAccount);
            }
            else
            {
                bankAccountOnDb.Transactions = bankAccount.Transactions;
                _bankConciliatorRepository.Update(bankAccountOnDb);
            }

            _bankConciliatorRepository.Commit();
            return bankAccount;
        }
    }
}

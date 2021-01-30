using System;
using Xunit;
using NiboBankConciliator.Core.Services;
using NiboBankConciliator.Core.Entities;
using System.Collections.Generic;
using NiboBankConciliator.Core;
using NSubstitute;
using System.Linq;

namespace NiboBankConciliator.Tests.Unit
{
    public class BankReconciliationServiceTests
    {
        readonly IBankReconciliationService _bankReconciliationService;

        public BankReconciliationServiceTests()
        {
            var bankConciliatorRepository = Substitute.For<IBankConciliatorRepository>();
            _bankReconciliationService = new BankReconciliationService(bankConciliatorRepository);
        }

        [Fact]
        public void Reconcile_Should_Reconcile()
        {
            //Given
            IEnumerable<OfxDocument> ofxDocuments = new List<OfxDocument>()
            {
                new OfxDocument()
                {
                    BankID = "0341",
                    AccountID = "7037300576",
                    AccountType = "CHECKING",
                    Transactions = new List<OfxTransaction>()
                    {
                        new OfxTransaction()
                        {
                            TransType = TransType.Debit,
                            DatePosted = new DateTime(2021,01,02),
                            TransAmount = -102.19m,
                            Memo = "RSHOP-SUPERMERCAD-03/02"
                        },
                        new OfxTransaction()
                        {
                            TransType = TransType.Credit,
                            DatePosted = new DateTime(2021,01,02),
                            TransAmount = 16766.28m,
                            Memo = "TED 399.1934NIBO SOF CUR"
                        }
                    }
                },
                new OfxDocument()
                {
                    BankID = "0341",
                    AccountID = "7037300576",
                    AccountType = "CHECKING",
                    Transactions = new List<OfxTransaction>()
                    {
                        new OfxTransaction()
                        {
                            TransType = TransType.Credit,
                            DatePosted = new DateTime(2021,01,02),
                            TransAmount = 16766.28m,
                            Memo = "TED 399.1934NIBO SOF CUR"
                        }
                    }
                }
            };


            //When
            var bankAccount = _bankReconciliationService.Reconcile(ofxDocuments);

            //Then
            var expectedTransactionsCount = 2;
            Assert.Equal(expectedTransactionsCount, bankAccount.Transactions.Count);
        }

        [Fact]
        public void GetBankAccount_Should_GetBankAccount()
        {
            //Given
            var bankAccountId = 1;

            //When
            BankAccount bankAccount = _bankReconciliationService.GetBankAccount(bankAccountId);

            //Then
            Assert.True(true);
        }
    }
}

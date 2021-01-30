using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiboBankConciliator.Core.Services;

namespace NiboBankConciliator.Mvc.Controllers
{
    public class BankAccountsController : Controller
    {
        readonly IBankReconciliationService _bankReconciliationService;
        public BankAccountsController(IBankReconciliationService bankReconciliationService)
        {
            _bankReconciliationService = bankReconciliationService;
        }

        // GET: BankAccountsController
        public ActionResult Index()
        {
            var bankAccounts = _bankReconciliationService.GetBankAccounts();
            return View(bankAccounts);
        }

        // GET: BankAccountsController/Details/5
        public ActionResult Details(int id)
        {
            var bankAccount = _bankReconciliationService.GetBankAccount(id);
            return View(bankAccount);
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiboBankConciliator.Core;
using NiboBankConciliator.Core.Entities;
using NiboBankConciliator.Core.Services;

namespace NiboBankConciliator.Mvc
{
    public class BankConciliationsController : Controller
    {
        readonly IBankReconciliationService _bankReconciliationService;
        public BankConciliationsController(IBankReconciliationService bankReconciliationService)
        {
            _bankReconciliationService = bankReconciliationService;
        }

        // GET: BankConciliationsController/Reconcile
        public ActionResult Reconcile()
        {
            return View();
        }

        // POST: BankConciliationsController/Reconcile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reconcile(IFormCollection formCollection)
        {
            try
            {
                //Should be tested. IFormFileCollection should be in Core Framework?
                var ofxDocuments = new List<OfxDocument>();
                foreach (var file in formCollection.Files)
                {
                    var stream = file.OpenReadStream();
                    var ofxDocument = OfxDocumentParser.Parse(stream);
                    ofxDocuments.Add(ofxDocument);
                }
                var bankAccount = _bankReconciliationService.ReconcileAndAddTransactions(ofxDocuments);

                return View(nameof(Reconcile), bankAccount);
            }
            catch
            {
                return View();
            }
        }
    }
}

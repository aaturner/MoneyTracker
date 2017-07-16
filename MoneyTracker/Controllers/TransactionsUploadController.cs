using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LumenWorks.Framework.IO.Csv;
using MoneyTracker.DAL;
using MoneyTracker.AppModels;

namespace MoneyTracker.Controllers
{
    [Authorize]
    public class TransactionsUploadController : Controller
    {
        private PrimaryContext db = new PrimaryContext();
        public ActionResult Upload()
        {
            AppModels.TransactionsUpload transactionUpload = new AppModels.TransactionsUpload();
            return View(transactionUpload);
        }

        public ActionResult Index()
        {
            TransactionsUpload transactionsUpload  = new TransactionsUpload();
            transactionsUpload.TransactionTempsCollection = db.TransactionTemps.ToList();
            return View(transactionsUpload);
        }

        [HttpPost]
        public ActionResult Index(TransactionsUpload model, string command)
        {
            switch (command)
            {
                case "Commit":
                    Utilities.TransactionUtils.CommitTransactions(model);
                    break;
                case "Save":
                    db.SaveChanges();
                    break;
                default:
                    throw new NotImplementedException();
/*
                    break;
*/
            }
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload, [Bind(Include = "DataTable,Account,AccountId,PreTransactions")] AppModels.TransactionsUpload transactionsUpload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();

                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }

                        transactionsUpload.DataTable = csvTable;

                        //TODO Need to make all amounts have cents

                        transactionsUpload.TransactionTempsCollection = Utilities.TransactionUtils.GetTransactions(csvTable, (int)transactionsUpload.AccountId);
                        
                        foreach (var transact in transactionsUpload.TransactionTempsCollection)
                        {
                            db.TransactionTemps.Add(transact);
                        }
                        db.SaveChanges();
                            
                        //ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transactionsUpload.AccountId);
                        //ViewBag.AllocationBaseId = new SelectList(db.Allocations, "Id", "Name", transactionsUpload.AllocationId);

                        return View("Index");
                        

                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

    }
}
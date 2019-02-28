using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;

namespace WebSecurityAssignment.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public IActionResult Index()
        {
            TransactionRepo transactionRepo = new TransactionRepo(_context);
            return View(transactionRepo.GetAllTransactions());
        }

        // GET: Transactions/Details/5
        public IActionResult Details(int transactionID)
        {
            TransactionRepo transactionRepo = new TransactionRepo(_context);
            Transaction transaction = transactionRepo.GetTransaction(transactionID);
            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["employeeID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["jobID"] = new SelectList(_context.Jobs, "jobID", "jobID");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            var transactions = _context.Transactions;
            if (ModelState.IsValid)
            {
                TransactionRepo transactionRepo = new TransactionRepo(_context);
                var success = transactionRepo.CreateTransaction(transaction.transactionID, transaction.employeeID, transaction.jobID, transaction.paymentToEmployee, transaction.paymentToProvider, 
                    transaction.date);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while creating this role. Please try again.";
            return View();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.transactionID == id);
        }
    }
}

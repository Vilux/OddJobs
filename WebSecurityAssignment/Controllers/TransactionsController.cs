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
        public IActionResult Details(int transactionID, int jobID)
        {
            TransactionRepo transactionRepo = new TransactionRepo(_context);
            Transaction transaction = transactionRepo.GetTransaction(transactionID);
            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create(int id)
        {
            ViewData["jobID"] = id;

            var job = _context.Jobs.Where(j => j.jobID == id).FirstOrDefault();
            string employee = job.employeeID;

            var emp = _context.Users.Where(u => u.Id == employee).FirstOrDefault();
            ViewBag.name = emp.FirstName + " " + emp.LastName;

            ViewData["amount"] = job.amount;

            var transactions = _context.Transactions;

            if (ModelState.IsValid)
            {
                TransactionRepo transactionRepo = new TransactionRepo(_context);
                Transaction transaction = new Transaction() {
                    employeeID = job.employeeID,
                    jobID = job.jobID,
                    paymentToEmployee = job.amount * 0.85f,
                    paymentToProvider = job.amount * 0.15f,
                    date = DateTime.Now
                };

                try
                {
                    var success = transactionRepo.CreateTransaction(transaction.transactionID, transaction.employeeID, transaction.jobID, transaction.paymentToEmployee, transaction.paymentToProvider,
                        transaction.date);

                    if (success)
                    {
                        return View(transaction);
                    }
                }
                catch
                {
                    return View(transaction);
                }
            }
            ViewBag.Error = "An error occurred while creating this role. Please try again.";
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction, int id)
        {
            
            return View();
        }

        public IActionResult FinishShopping(int transactionID, int jobID)
        {
            Transaction transaction = _context.Transactions.Where(t => t.transactionID == transactionID).FirstOrDefault();

            foreach (Job job in _context.Jobs)
            {
                if (job.jobID == jobID)
                {
                    _context.Jobs.Remove(job);
                }
            }

            _context.SaveChanges();
            return View(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.transactionID == id);
        }
    }
}

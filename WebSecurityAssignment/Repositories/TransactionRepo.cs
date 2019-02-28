using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Repositories
{
    public class TransactionRepo
    {
        ApplicationDbContext _context;

        public TransactionRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Transaction> GetAllTransactions()
        {
            var transactions = _context.Transactions;
            List<Transaction> transactionList = new List<Transaction>();

            foreach (var item in transactions)
            {
                transactionList.Add(new Transaction() {
                    transactionID = item.transactionID,
                    employeeID = item.employeeID,
                    jobID = item.jobID,
                    paymentToEmployee = item.paymentToEmployee,
                    paymentToProvider = item.paymentToProvider,
                    date = item.date
                });
            }
            return transactionList;
        }

        public Transaction GetTransaction(int transactionID)
        {
            var transaction = _context.Transactions.Where(t => t.transactionID == transactionID).FirstOrDefault();
            if (transaction != null)
            {
                return new Transaction() { transactionID = transaction.transactionID };
            }
            return null;
        }
    }
}

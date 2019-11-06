using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Data_Layer;
using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.View_Models;
using Data_Layer.Errors;

namespace Web_Interface.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IRepository _repo;

        public AccountsController(IRepository repo)
        {
            _repo = repo;
        }

        //GET: Accounts
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Customers");
        }

        //GET: Accounts/Details/5
        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Account account = null;
                try
                {
                    string guid = GetUserGuID();
                    int customerID = _repo.GetCustomer(guid).ID;
                    account = _repo.GetAccountInformation(customerID, id.Value);
                   
                    if (account == null)
                    {
                        return NotFound();
                    }
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return NotFound();
                }

                return View(account);
            }

            return NotFound();
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["AccountTypeID"] = new SelectList(_repo.GetAllAccountTypes(), "ID", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,AccountTypeID,CustomerID,AccountBalance,MaturityDate,InterestRate,IsActive,IsOpen")] Account account)
        {
            if (ModelState.IsValid)
            {
                // Check if valid starting balance was entered.
                if (account.AccountBalance >= 0.0)
                {
                    string guid = GetUserGuID();
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    account = _repo.OpenAccount(currentCustomer.ID, account.AccountTypeID, account.AccountBalance);
                    return RedirectToAction(nameof(Details), new { id = account.ID });
                }
            }
            ViewData["AccountTypeID"] = new SelectList(_repo.GetAllAccountTypes(), "ID", "Name");
            return View(account);
        }

        //GET: Accounts/Edit/5
        public IActionResult Deposit(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return NotFound();
            }


            AccountTransactionVM account = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);

                if (currentAccount != null)
                {
                    account = new AccountTransactionVM()
                    {
                        Account = currentAccount,
                        Amount = 0.0,
                    };
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return NotFound();
            }
           
            if (account == null)
            {
                return NotFound();
            }
           
            ViewData["AccountType"] = _repo.GetAccountTypeName(account.Account.AccountTypeID);
            //ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
            //ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(int id, [Bind("Account,Amount")] AccountTransactionVM accountPost)
        {
            if (id != accountPost.Account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string guid = GetUserGuID();
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    _repo.Deposit(currentCustomer.ID, id, accountPost.Amount);
                }
                catch (UnauthorizedAccessException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidAccountException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException WTF)
                {
                    Console.WriteLine(WTF);
                    //if (!AccountExists(account.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                    //return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
            //ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
            return View(accountPost);
        }

        //GET: Accounts/Edit/5
        public IActionResult Withdraw(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return NotFound();
            }
            
            AccountTransactionVM account = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);

                if (currentAccount != null)
                {
                    // Check if current account is withdrawable.
                    if (_repo.IsAccountWithdrawable(currentAccount))
                    {
                        account = new AccountTransactionVM()
                        {
                            Account = currentAccount,
                            Amount = 0.0,
                        };
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return NotFound();
            }

            if (account == null)
            {
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(account.Account.AccountTypeID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(int id, [Bind("Account,Amount")] AccountTransactionVM accountPost)
        {
            if (id != accountPost.Account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string guid = GetUserGuID();
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    _repo.Withdraw(currentCustomer.ID, id, accountPost.Amount);
                }
                catch (UnauthorizedAccessException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidAccountException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (OverdraftProtectionException WTF)
                {
                    Console.WriteLine(WTF);
                    ViewData["ErrorMessage"] = "Overdraft Protection! Withdrawal amount exceeded current balance!";
                    return View(accountPost);
                }
                catch (DbUpdateConcurrencyException WTF)
                {
                    Console.WriteLine(WTF);
                    //if (!AccountExists(account.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                    //return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(accountPost);
        }


        //GET: Accounts/Edit/5
        public IActionResult Installment(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return NotFound();
            }


            AccountTransactionVM account = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);

                if (currentAccount != null)
                {
                    // Check if valid loan account.
                    if (_repo.IsAccountLoanPayable(currentAccount))
                    {
                        account = new AccountTransactionVM()
                        {
                            Account = currentAccount,
                            Amount = 0.0,
                        };
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            if (account == null)
            {
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(account.Account.AccountTypeID);
            //ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
            //ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Installment(int id, [Bind("Account,Amount")] AccountTransactionVM accountPost)
        {
            if (id != accountPost.Account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string guid = GetUserGuID();
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    _repo.Deposit(currentCustomer.ID, id, accountPost.Amount);
                }
                catch (InvalidAccountException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidAmountException WTF)
                {
                    Console.WriteLine(WTF);
                    accountPost.Amount = 0;
                    ViewData["ErrorMessage"] = "Installment Amount Exceeded Remaining Balance!";
                    return View(accountPost);
                }
                catch (UnauthorizedAccessException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException WTF)
                {
                    Console.WriteLine(WTF);
                    //if (!AccountExists(account.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
            //ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
            return View(accountPost);
        }


        // GET: Accounts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _repo.Accounts
        //        .Include(a => a.AccountType)
        //        .Include(a => a.Customer)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(account);
        //}

        // POST: Accounts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var account = await _repo.Accounts.FindAsync(id);
        //    _repo.Accounts.Remove(account);
        //    await _repo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AccountExists(int id)
        //{
        //    return _repo.Accounts.Any(e => e.ID == id);
        //}

        private string GetUserGuID()
        {
            string userEmail = User.Identity.Name;
            var userData = User.Claims.ToList()[0];
            string guid = userData.Value;
            return guid;
        }
    }
}

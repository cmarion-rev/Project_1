﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.View_Models;
using Data_Layer.Errors;
using Microsoft.AspNetCore.Authorization;

namespace Web_Interface.Controllers
{
    [Authorize]
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
                        return RedirectToAction(nameof(Index));
                    }

                    ViewData["AccountType"] = _repo.GetAccountTypeName(account.AccountTypeID);
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index), "Customers");
                }

                return View(account);
            }

            return RedirectToAction(nameof(Index));
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
                else
                {
                    ViewData["ErrorMessage"] = "Starting Balance needs to be a positive value.";
                    account.AccountBalance = 0.0;
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
                return RedirectToAction(nameof(Index), "Customers");
            }

            AccountTransactionVM account = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);

                if (currentAccount != null)
                {
                    if (_repo.IsAccountDepositable(currentAccount))
                    {
                        account = new AccountTransactionVM()
                        {
                            Account = currentAccount,
                            Amount = 0.0,
                        };
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), "Customers");
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index), "Customers");
            }

            if (account == null)
            {
                return RedirectToAction(nameof(Index), "Customers");
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(account.Account.AccountTypeID);
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
                return RedirectToAction(nameof(Index));
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index), "Customers");
            }

            return View(accountPost);
        }

        //GET: Accounts/Edit/5
        public IActionResult Withdraw(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }

            if (account == null)
            {
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                    accountPost.Amount = 0;
                    return View(accountPost);
                }
                catch (DbUpdateConcurrencyException WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
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

                return RedirectToAction(nameof(Index), "Customers");
            }
            return View(accountPost);
        }

        //GET: Accounts/Edit/5
        public IActionResult Installment(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
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
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
            }

            if (account == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(account.Account.AccountTypeID);
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index), "Customers");
            }
            return View(accountPost);
        }

        public IActionResult Transactions(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Check if customer is registered.
            string guid = GetUserGuID();
            if (_repo.IsCustomerPresent(guid))
            {
                // Display Main Account page.
                try
                {
                    Customer currentCustomer = _repo.GetCustomer(guid);

                    if (currentCustomer != null)
                    {
                        Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);
                        CustomerAccountTransactionsVM customerTransactions = _repo.GetAllTransactions(currentCustomer.ID, id.Value);
                        customerTransactions.StartDate = DateTime.Now;
                        customerTransactions.EndDate = DateTime.Now;
                        customerTransactions.Limit = 0;
                        customerTransactions.AccountTransactionStates = _repo.GetTransactionStates();

                        List<TransactionLimitVM> tempList = new List<TransactionLimitVM>()
                        {
                           new TransactionLimitVM(){ ID = 0, Name = "All" },
                           new TransactionLimitVM(){ ID = 1, Name = "10" },
                           new TransactionLimitVM(){ ID = 2, Name = "25" },
                           new TransactionLimitVM(){ ID = 3, Name = "50" },
                        };

                        ViewData["Limit"] = new SelectList(tempList, "ID", "Name", customerTransactions.Limit);

                        return View(customerTransactions);
                    }
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index), "Customers");
            }
            else
            {
                // Redirect to create customer information page.
                return RedirectToAction(nameof(Create), "Customers");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transactions(int? id, [Bind("Customer,Account,AccountTransaction,StartDate,EndDate,Limit,AccountTransactionStates")] CustomerAccountTransactionsVM accountPost)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Check if customer is registered.
            string guid = GetUserGuID();
            if (_repo.IsCustomerPresent(guid))
            {
                // Display Main Account page.
                try
                {
                    Customer currentCustomer = _repo.GetCustomer(guid);

                    if (currentCustomer != null)
                    {
                        Account currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);
                        CustomerAccountTransactionsVM customerTransactions = null;

                        // Check limit settings.
                        int limit = -1;
                        switch (accountPost.Limit)
                        {
                            case 3:
                                // Limit 10.
                                limit = 50;
                                break;

                            case 2:
                                // Limit 10.
                                limit = 25;
                                break;

                            case 1:
                                // Limit 10.
                                limit = 10;
                                break;

                            case 0:
                            default:
                                // All transactions.
                                limit = -1;
                                break;
                        }

                        // Check if date ranges are not the same.
                        if (accountPost.StartDate.Subtract(accountPost.EndDate).TotalDays < 0)
                        {
                            // Check if limit was set.
                            if (limit > 0)
                            {
                                customerTransactions = _repo.GetAllTransactions(currentCustomer.ID, currentAccount.ID, accountPost.StartDate, accountPost.EndDate, limit);
                            }
                            else
                            {
                                customerTransactions = _repo.GetAllTransactions(currentCustomer.ID, currentAccount.ID, accountPost.StartDate, accountPost.EndDate);
                            }
                        }
                        else
                        {
                            // Check if limit was set.
                            if (limit > 0)
                            {
                                customerTransactions = _repo.GetAllTransactions(currentCustomer.ID, currentAccount.ID, limit);
                            }
                            else
                            {
                                customerTransactions = _repo.GetAllTransactions(currentCustomer.ID, currentAccount.ID);
                            }

                            accountPost.StartDate = DateTime.Now;
                            accountPost.EndDate = accountPost.StartDate;
                        }

                        // Restore old values.
                        customerTransactions.StartDate = accountPost.StartDate;
                        customerTransactions.EndDate = accountPost.EndDate;
                        customerTransactions.Limit = accountPost.Limit;
                        customerTransactions.AccountTransactionStates = _repo.GetTransactionStates();

                        List<TransactionLimitVM> tempList = new List<TransactionLimitVM>()
                        {
                           new TransactionLimitVM(){ ID = 0, Name = "All" },
                           new TransactionLimitVM(){ ID = 1, Name = "10" },
                           new TransactionLimitVM(){ ID = 2, Name = "25" },
                           new TransactionLimitVM(){ ID = 3, Name = "50" },
                        };
                        ViewData["Limit"] = new SelectList(tempList, "ID", "Name", customerTransactions.Limit);

                        return View(customerTransactions);
                    }
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Redirect to create customer information page.
                return RedirectToAction(nameof(Create), "Customers");
            }
        }

        //GET: Accounts/Edit/5
        public IActionResult Close(int? id)
        {
            // Check if valid id was presented.
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            Account currentAccount = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                currentAccount = _repo.GetAccountInformation(currentCustomer.ID, id.Value);
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
            }

            if (currentAccount == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(currentAccount.AccountTypeID);
            return View(currentAccount);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id, [Bind("ID,AccountTypeID,CustomerID,AccountBalance,MaturityDate,InterestRate,IsActive,IsOpen")] Account accountPost)
        {
            if (id != accountPost.ID)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string guid = GetUserGuID();
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    _repo.CloseAccount(currentCustomer.ID, id);
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
                }
                return RedirectToAction(nameof(Index), "Customers");
            }

            ViewData["AccountType"] = _repo.GetAccountTypeName(accountPost.AccountTypeID);
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

        public IActionResult Transfer()
        {
            AccountTransferVM transfer = null;
            try
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);

                if (_repo.CanTransferBalance(currentCustomer.ID))
                {
                    transfer = new AccountTransferVM
                    {
                        Amount = 0,
                    };

                    // Get all withdrawable accounts.
                    transfer.SourceAccounts = _repo.GetWithdrawAccounts(currentCustomer.ID);
                    transfer.SourceID = transfer.SourceAccounts[0].ID;

                    // Get all depositable accounts.
                    transfer.DestinationAccounts = _repo.GetDepositAccounts(currentCustomer.ID);
                    transfer.DestinationID = transfer.DestinationAccounts[0].ID;
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
            }

            if (transfer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(transfer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer([Bind("SourceID,DestinationID,Amount,SourceAccounts,DestinationAccounts")] AccountTransferVM transferPost)
        {
            AccountTransferVM transfer = null;

            try
            {
                // Check if user is valid for each account.
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);

                transfer = new AccountTransferVM
                {
                    Amount = 0,
                };

                // Get all withdrawable accounts.
                transfer.SourceAccounts = _repo.GetWithdrawAccounts(currentCustomer.ID);
                transfer.SourceID = transfer.SourceAccounts[0].ID;

                // Get all depositable accounts.
                transfer.DestinationAccounts = _repo.GetDepositAccounts(currentCustomer.ID);
                transfer.DestinationID = transfer.DestinationAccounts[0].ID;

                Account sourceAccount = _repo.GetAccountInformation(currentCustomer.ID, transferPost.SourceID);
                Account destinationAccount = _repo.GetAccountInformation(currentCustomer.ID, transferPost.DestinationID);

                // Check if source account and destination account are not the same.
                if (sourceAccount.ID != destinationAccount.ID)
                {
                    // Check if withdraw amount is valid.
                    sourceAccount = _repo.Withdraw(currentCustomer.ID, sourceAccount.ID, transferPost.Amount);

                    // Deposit amount to destination account.
                    destinationAccount = _repo.Deposit(currentCustomer.ID, destinationAccount.ID, transferPost.Amount);

                    return RedirectToAction(nameof(Index), "Customers");
                }
            }
            catch (OverdraftProtectionException WTF)
            {
                Console.WriteLine(WTF);
                ViewData["ErrorMessage"] = "Overdraft Protection! Withdrawal amount exceeded current balance of source account!";

                if (transfer == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(transfer);
            }
            catch(UnauthorizedAccessException WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ErrorMessage"] = "Same account can NOT be selected for both source and destination of a transfer!";
            return View(transfer);
        }

        private string GetUserGuID()
        {
            string userEmail = User.Identity.Name;
            var userData = User.Claims.ToList()[0];
            string guid = userData.Value;
            return guid;
        }
    }
}
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
            return RedirectToAction(nameof(Index), nameof(CustomersController));
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
        public IActionResult Create([Bind("ID,AccountTypeID,CustomerID,AccountBalance,LastTransactionState,MaturityDate,InterestRate,IsActive,IsOpen")] Account account)
        {
            if (ModelState.IsValid)
            {
                string guid = GetUserGuID();
                Customer currentCustomer = _repo.GetCustomer(guid);
                _repo.OpenAccount(currentCustomer.ID, account.AccountTypeID, account.AccountBalance);

                return RedirectToAction(nameof(Details), account.ID);
            }
            ViewData["AccountTypeID"] = new SelectList(_repo.GetAllAccountTypes(), "ID", "Name");
            return View(account);
        }

        // GET: Accounts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _repo.Accounts.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
        //    ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
        //    return View(account);
        //}

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,AccountTypeID,CustomerID,AccountBalance,LastTransactionState,MaturityDate,InterestRate,IsActive,IsOpen")] Account account)
        //{
        //    if (id != account.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _repo.Update(account);
        //            await _repo.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AccountExists(account.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AccountTypeID"] = new SelectList(_repo.AccountTypes, "ID", "ID", account.AccountTypeID);
        //    ViewData["CustomerID"] = new SelectList(_repo.Customers, "ID", "FirstName", account.CustomerID);
        //    return View(account);
        //}

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

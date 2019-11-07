using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Data_Layer;
using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.View_Models;

namespace Web_Interface.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IRepository _repo;

        public CustomersController(IRepository newRepo)
        {
            _repo = newRepo;
        }

        // GET: Customers
        public IActionResult Index()
        {
            // Check if customer is registered.
            string guid = GetUserGuID();
            if (_repo.IsCustomerPresent(guid))
            {
                // Display Main Account page.
                try
                {
                    Customer currentCustomer = _repo.GetCustomer(guid);
                    CustomerAccountsVM customerAccounts = _repo.GetCustomerAccounts(currentCustomer.ID);

                    ViewData["State"] = _repo.GetStates().FirstOrDefault(s => s.ID == currentCustomer.StateID).Name;
                    ViewData["Account Types"] = _repo.GetAllAccountTypes();
                    return View(customerAccounts);
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    throw;
                }
            }
            else
            {
                // Redirect to create customer information page.
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            Customer currentCustomer = null;

            // Get current customer data.
            string guid = GetUserGuID();

            // Check if customer exits.
            bool result = _repo.IsCustomerPresent(guid);
            if (result)
            {
                // Get current customer info.
                try
                {
                    currentCustomer = _repo.GetCustomer(guid);
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    throw;
                }
            }
            else
            {
                // Create new customer.
                try
                {
                    return RedirectToAction(nameof(Create));
                }
                catch (Exception WTF)
                {
                    Console.WriteLine(WTF);
                    throw;
                }
            }

            if (currentCustomer != null)
            {
                ViewData["State"] = _repo.GetStates().FirstOrDefault(s => s.ID == currentCustomer.StateID).Name;
                return View(currentCustomer);
            }
            else
            {
                return NotFound();
            }
        }

        //GET: Customers/Create
        public IActionResult Create()
        {
            string guid = GetUserGuID();

            // Check if customer infomation has already been created.
            if (_repo.IsCustomerPresent(guid))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["StateID"] = new SelectList(_repo.GetStates(), "ID", "Abbreviation");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,FirstName,LastName,Address,City,StateID,ZipCode,PhoneNumber,UserIdentity")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                string guid = GetUserGuID();
                Customer newCustomer = _repo.CreateNewCustomer(guid, customer);
                return RedirectToAction(nameof(Details));
            }
            ViewData["StateID"] = new SelectList(_repo.GetStates(), "ID", "Abbreviation");
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            Customer customer = null;

            string guid = GetUserGuID();
            if (_repo.IsCustomerPresent(guid))
            {
                customer = _repo.GetCustomer(guid);
            }
            else
            {
                return RedirectToAction(nameof(Create));
            }

            if (customer == null)
            {
                return NotFound();
            }

            ViewData["StateID"] = new SelectList(_repo.GetStates(), "ID", "Abbreviation");
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,FirstName,LastName,Address,City,StateID,ZipCode,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    customer.UserIdentity = GetUserGuID();
                    bool isValidCustomerID = _repo.IsCustomerIdValid(id, customer.UserIdentity);

                    if (isValidCustomerID)
                    {
                        _repo.UpdateCustomer(customer);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                catch (DbUpdateConcurrencyException WTF)
                {
                    Console.WriteLine(WTF);
                    if (!_repo.IsCustomerPresent(customer.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                catch(Exception WTF)
                {
                    Console.WriteLine(WTF);
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["StateID"] = new SelectList(_repo.GetStates(), "ID", "Abbreviation");
     
            return View(customer);
        }

        //// GET: Customers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _context.Customers
        //        .Include(c => c.State)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        //// POST: Customers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);
        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CustomerExists(int id)
        //{
        //    return _context.Customers.Any(e => e.ID == id);
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
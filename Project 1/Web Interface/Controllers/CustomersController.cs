﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data_Layer;
using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web_Interface.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly Repository _repo;

        public CustomersController(Repository newRepo)
        {
            _repo = newRepo;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            //var mainDbContext = _repo.Customers.Include(c => c.State);
            //return View(await mainDbContext.ToListAsync());

            return RedirectToAction(nameof(Details));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Customer currentCustomer = null;

            if (id == null)
            {
                // Get current customer data.
                string userEmail = User.Identity.Name;
                var userData = User.Claims.ToList()[0];
                string guid = userData.Value;

                // Check if customer exits.
                if (await _repo.IsCustomerPresent(guid))
                {
                    // Get current customer info.
                    try
                    {
                        currentCustomer = await _repo.GetCustomer(guid);
                    }
                    catch (Exception WTF)
                    {
                        Console.WriteLine(WTF);
                        throw;
                    }
                    finally
                    {

                    }
                }
                else
                {
                    // Create new customer.
                    try
                    {
                        currentCustomer = await _repo.CreateNewCustomer(guid);
                    }
                    catch (Exception WTF)
                    {
                        Console.WriteLine(WTF);
                        throw;
                    }
                }
            }
            else
            {
                return NotFound();
            }
            /* GET USER ID
             * 
            string X = User.Identity.Name;
            var Y = User.Claims.ToList()[0];
            string Z = Y.Value;
             *
             */

            //var customer = await _context.Customers
            //    .Include(c => c.State)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (customer == null)
            //{
            //    return NotFound();
            //}

            return View(currentCustomer);
        }

        // GET: Customers/Create
        //public IActionResult Create()
        //{
        //    ViewData["StateID"] = new SelectList(_context.States, "ID", "Abbreviation");
        //    return View();
        //}

        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Address,City,StateID,ZipCode,PhoneNumber,UserIdentity")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(customer);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StateID"] = new SelectList(_context.States, "ID", "Abbreviation", customer.StateID);
        //    return View(customer);
        //}

        //// GET: Customers/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["StateID"] = new SelectList(_context.States, "ID", "Abbreviation", customer.StateID);
        //    return View(customer);
        //}

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Address,City,StateID,ZipCode,PhoneNumber,UserIdentity")] Customer customer)
        //{
        //    if (id != customer.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(customer);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CustomerExists(customer.ID))
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
        //    ViewData["StateID"] = new SelectList(_context.States, "ID", "Abbreviation", customer.StateID);
        //    return View(customer);
        //}

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
    }
}
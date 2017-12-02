using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consulting.Models;
using Microsoft.AspNetCore.Http;

namespace Consulting.Controllers
{
    public class WorkSessionsController : Controller
    {
        private readonly ConsultingContext _context;

        public WorkSessionsController(ConsultingContext context)
        {
            _context = context;
        }

        // GET: WorkSessions
        public async Task<IActionResult> Index(int? contractId)
        {
            //Store to session
            if (contractId != null)
            {
                HttpContext.Session.SetInt32("_contractId", Convert.ToInt32(contractId));
                TempData["message"] = "Stored to session: Contract ID#" + contractId;

            }
            else if ((contractId == null) && (HttpContext.Session.GetString("_contractId") != null))
            {
                contractId = HttpContext.Session.GetInt32("_contractId");
                //TempData["message"] = "Got from session: Contract ID#" + contractId;
            }
            else if (HttpContext.Session.GetInt32("_contractId") == null)
            {
                TempData["message"] = "Please select a Contract first";
                return RedirectToAction("Index", "Contracts");
            }





            var consultingContext = _context.WorkSession
                            .Include(w => w.Consultant)
                            .Include(w => w.Contract)
                            .Where(c => c.ContractId == contractId)
                            .OrderBy(c => c.Consultant.LastName)
                            .OrderByDescending(d => d.DateWorked)
                          ;
            //Get contract Name from DB
           
                            
            ViewBag.ContractId = contractId;
            ViewBag.ContractName = _context.Contract
                                .Where(c => c.ContractId == contractId)
                                .SingleOrDefault()
                                .Name; 

            return View(await consultingContext.ToListAsync());
        }

        // GET: WorkSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSession = await _context.WorkSession
                .Include(w => w.Consultant)
                .Include(w => w.Contract)
                .SingleOrDefaultAsync(m => m.WorkSessionId == id);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // GET: WorkSessions/Create
        public IActionResult Create(int? contractId)
        {
            ViewData["ConsultantId"] = new SelectList(_context.Consultant.OrderBy(x => x.LastName), "ConsultantId", "LastName");
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name");

            ViewBag.Contractid = contractId;
            ViewBag.Contractname = _context.Contract
                                        .Where(x => x.ContractId == contractId)
                                        .SingleOrDefault()
                                        .Name;
            return View();
        }

        // POST: WorkSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkSessionId,ContractId,DateWorked,ConsultantId,HoursWorked,WorkDescription,HourlyRate,ProvincialTax,TotalChargeBeforeTax")] WorkSession workSession)
        {
            

            if (ModelState.IsValid)
            {
                var consultantRate = _context.Consultant.Where(x => x.ConsultantId == workSession.ConsultantId).SingleOrDefault().HourlyRate;
                workSession.HourlyRate = workSession.HoursWorked * consultantRate;
                workSession.TotalChargeBeforeTax += workSession.HourlyRate;
                
                //To add total spent to contract
                //_context.Contract.Where(x => x.ContractId == workSession.ContractId).SingleOrDefault().TotalChargedToDate += workSession.TotalChargeBeforeTax;

                _context.Add(workSession);
                await _context.SaveChangesAsync();
                @TempData["message"] = "Save to database... Success";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);




            return View(workSession);
        }

        // GET: WorkSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSession = await _context.WorkSession.SingleOrDefaultAsync(m => m.WorkSessionId == id);
            if (workSession == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);
            return View(workSession);
        }

        // POST: WorkSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkSessionId,ContractId,DateWorked,ConsultantId,HoursWorked,WorkDescription,HourlyRate,ProvincialTax,TotalChargeBeforeTax")] WorkSession workSession)
        {
            if (id != workSession.WorkSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkSessionExists(workSession.WorkSessionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);
            return View(workSession);
        }

        // GET: WorkSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSession = await _context.WorkSession
                .Include(w => w.Consultant)
                .Include(w => w.Contract)
                .SingleOrDefaultAsync(m => m.WorkSessionId == id);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // POST: WorkSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workSession = await _context.WorkSession.SingleOrDefaultAsync(m => m.WorkSessionId == id);
            _context.WorkSession.Remove(workSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkSessionExists(int id)
        {
            return _context.WorkSession.Any(e => e.WorkSessionId == id);
        }
    }
}

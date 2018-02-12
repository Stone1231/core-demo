using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coreDemo.Entity;

namespace coreDemo.Controllers
{
    public class ClassController : Controller
    {
        private readonly TestContext _context;

        public ClassController(TestContext context)
        {
            _context = context;
        }

        // GET: Class
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassMs.ToListAsync());
        }

        // GET: Class/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classM = await _context.ClassMs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classM == null)
            {
                return NotFound();
            }

            return View(classM);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ClassM classM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classM);
        }

        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classM = await _context.ClassMs.SingleOrDefaultAsync(m => m.Id == id);
            if (classM == null)
            {
                return NotFound();
            }
            return View(classM);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] ClassM classM)
        {
            if (id != classM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassMExists(classM.Id))
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
            return View(classM);
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classM = await _context.ClassMs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classM == null)
            {
                return NotFound();
            }

            return View(classM);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var classM = await _context.ClassMs.SingleOrDefaultAsync(m => m.Id == id);
            _context.ClassMs.Remove(classM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassMExists(string id)
        {
            return _context.ClassMs.Any(e => e.Id == id);
        }
    }
}

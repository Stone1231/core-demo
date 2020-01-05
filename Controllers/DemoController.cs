using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coreDemo.Entity;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace coreDemo.Controllers
{
    public class DemoController : Controller
    {
        private readonly TestContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DemoController(TestContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        private String Query
        {
            get
            {
                return TempData["query"] == null ? "" : TempData["query"].ToString();
            }
            set
            {
                TempData["query"] = value;
            }
        }

        private RedirectToActionResult IndexAction()
        {
            RouteValueDictionary rv = new RouteValueDictionary();
            var qs = Query.Replace("?", "").Split('&');
            foreach (var q in qs)
            {
                var strs = q.Split('=');
                if (strs.Length == 2)
                {
                    rv.Add(strs[0], strs[1]);
                }
            }

            return RedirectToAction(nameof(Index), rv);
        }

        private void BackupQuery()
        {
            Query = Query;
        }


        // GET: Demo
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Name")
        {
            var q = _context.Students
                .AsNoTracking()
                .Include(s => s.ClassM)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                q = q.Where(p => EF.Functions.Like(p.Name, "%" + filter + "%"));
            }

            var model = await PagingList.CreateAsync(q, 10, page, sortExpression, "Name");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            Query = Request.QueryString.ToString();

            return View(model);
        }


        // GET: Demo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var r = Request;

            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.ClassM)
                .SingleOrDefaultAsync(m => m.Sn == id);
            if (student == null)
            {
                return NotFound();
            }

            BackupQuery();

            return View(student);
        }

        // GET: Demo/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassMs, "Id", "Name");
            BackupQuery();
            return View();
        }

        // POST: Demo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sn,Name,Hight,Weight,Birthday,Photo,Memo,ClassId")] Student student, IFormFile PhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (PhotoFile != null)
                {
                    var folder = _hostingEnvironment.WebRootPath + "/Files";
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var filePath = Path.Combine(folder, PhotoFile.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //await file.CopyToAsync(fileStream);
                        PhotoFile.CopyTo(fileStream);
                    }

                    student.Photo = PhotoFile.FileName;
                }

                _context.Add(student);
                await _context.SaveChangesAsync();

                return IndexAction();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassMs, "Id", "Name", student.ClassId);
            return View(student);
        }

        // GET: Demo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.Sn == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassMs, "Id", "Name", student.ClassId);


            BackupQuery();

            return View(student);
        }

        // POST: Demo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sn,Name,Hight,Weight,Birthday,Photo,Memo,ClassId")] Student student, IFormFile PhotoFile)
        {
            if (id != student.Sn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PhotoFile != null)
                    {
                        var folder = _hostingEnvironment.WebRootPath + "/Files";
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        var filePath = Path.Combine(folder, PhotoFile.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            //await file.CopyToAsync(fileStream);
                            PhotoFile.CopyTo(fileStream);
                        }

                        student.Photo = PhotoFile.FileName;
                    }

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Sn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return IndexAction();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassMs, "Id", "Name", student.ClassId);
            return View(student);
        }

        // GET: Demo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.ClassM)
                .SingleOrDefaultAsync(m => m.Sn == id);
            if (student == null)
            {
                return NotFound();
            }

            BackupQuery();

            return View(student);
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Sn == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return IndexAction();
            //return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Sn == id);
        }

        public async Task<IActionResult> Back()
        {

            return IndexAction();
        }
    }




}

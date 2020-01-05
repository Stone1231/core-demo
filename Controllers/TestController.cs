using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coreDemo.Entity;
using coreDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace coreDemo.Controllers
{
    public class TestController : Controller
    {
        private readonly TestContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TestController(TestContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var model = new TestModel();

            bind(model);

            model.Text4 = DateTime.Today;

            return View(model);
        }


        [HttpPost]
        public IActionResult Index(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                //return Index();
                bind(model);
                return View(model);
            }

            bind(model);

            var folder = _hostingEnvironment.WebRootPath + "/files";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (model.File != null)
            {
                var filePath = Path.Combine(folder, model.File.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    //await file.CopyToAsync(fileStream);
                    model.File.CopyTo(fileStream);
                }
            }

            //if (model.File != null)
            //{
            //    byte[] uploadedFile = new byte[model.File.InputStream.Length];
            //    model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            //}

            if (model.Files != null)
            {
                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        var filePath = Path.Combine(folder, file.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                }
            }
            return View(model);
        }

        public IActionResult ClassStud(string id)
        {
            //var defItem = new SelectListItem { Text = "請選擇", Value = "" };

            var list = _context.Students
                .Where(m => m.ClassId == id)
                .Select(s => new SelectListItem
                {
                    Value = s.Sn.ToString(),
                    Text = s.Name
                }).ToList();

            return Json(list);
            //return Json(list, JsonRequestBehavior.AllowGet);
        }

        private void bind(TestModel model)
        {
            model.Select1 = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Sn.ToString(),
                Text = s.Name
            });

            model.Select2 = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Sn.ToString(),
                Text = s.Name
            });

            //班級
            model.ClassSelect = _context.ClassMs.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });

            if (!string.IsNullOrWhiteSpace(model.ClassId))
            {
                var list = _context.Students
                    .Where(m => m.ClassId == model.ClassId)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Sn.ToString(),
                        Text = s.Name
                    });

                model.StudentSelect = list;
            }
            else
            {
                model.StudentSelect = new List<SelectListItem>();
            }
        }
    }
}
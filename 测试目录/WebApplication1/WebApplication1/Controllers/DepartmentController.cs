using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DepartmentController:Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new Exception();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var department = await  _departmentService.GetAll();
            return View();
        }

        public IActionResult add()
        {
            ViewBag.Title = "add Department";
            return View(new Department());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.add(model);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

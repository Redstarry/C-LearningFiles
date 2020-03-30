using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> Index(int departmentId)
        {
            var department = await _departmentService.GetById(departmentId);
            ViewBag.Title = $"Employee of {department.Name}";
            ViewBag.Department = department;

            var employee = await _employeeService.GetByDepartmentId(departmentId);

            return View(employee);
        }
        public  IActionResult add(int departmentId)
        {
            ViewBag.Title = "add Employee";
            return View(new Employee
            {
                DepartmentId = departmentId
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Add(model);
            }
            return RedirectToAction(nameof(Index), new { departmentId = model.DepartmentId });
        }

        public async Task<IActionResult> Fire(int employeeId)
        {
            var employee = await _employeeService.Fire(employeeId);
            return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
        }
    }
}
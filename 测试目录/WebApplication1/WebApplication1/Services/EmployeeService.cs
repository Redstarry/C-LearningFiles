using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employee = new List<Employee>();
        public EmployeeService()
        {
            _employee.Add(new Employee { 
                Id = 1,
                DepartmentId = 1,
                FirstName = "Nick",
                LastName = "Carter",
                Gender = Gender.男
            });

            _employee.Add(new Employee { 
                Id = 2,
                DepartmentId = 1,
                FirstName = "Michael",
                LastName = "Jackson",
                Gender = Gender.男
            });
            _employee.Add(new Employee { 
            
                Id = 3,
                DepartmentId = 1,
                FirstName = "Mariah",
                LastName = "Carey",
                Gender = Gender.女
            });

            _employee.Add(new Employee { 
                Id = 4,
                DepartmentId = 2,
                FirstName = "Axl",
                LastName = "Rose",
                Gender = Gender.男
            });
            _employee.Add(new Employee { 
                Id = 5,
                DepartmentId = 2,
                FirstName = "Kate",
                LastName = "Winslet",
                Gender = Gender.女
            });
            _employee.Add(new Employee { 
                Id = 6,
                DepartmentId = 3,
                FirstName = "Rob",
                LastName = "Thomas",
                Gender = Gender.男
            });
            _employee.Add(new Employee { 
                Id = 7,
                DepartmentId = 3,
                FirstName = "Avril",
                LastName = "Lavigne",
                Gender = Gender.女
            });
            _employee.Add(new Employee { 
                Id = 8,
                DepartmentId = 3,
                FirstName = "Katy",
                LastName = "Perry",
                Gender = Gender.女
            });
            _employee.Add(new Employee { 
                Id = 9,
                DepartmentId = 3,
                FirstName = "Michelle",
                LastName = "Monaghan",
                Gender = Gender.女
            });
        }
        public Task Add(Employee emloyee)
        {
            emloyee.Id = _employee.Max(x => x.Id) + 1;
            _employee.Add(emloyee);
            return Task.CompletedTask;
        }

        public Task<Employee> Fire(int id)
        {
            return Task.Run(() =>
            {

                var employee = _employee.FirstOrDefault(x=>x.Id == id);
                if (employee != null)
                {
                    employee.Fired = true;
                    return employee;
                }
                return null;
            });
        }

        public Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId)
        {
            return Task.Run(()=> _employee.Where(x=>x.DepartmentId == departmentId));
        }

        //public Task<Employee> GetById(int id)
        //{
        //    return Task.Run(()=>_employee.FirstOrDefault(x=>x.Id == id));
        //}
    }
}

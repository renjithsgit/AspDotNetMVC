using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository dataRepository = new MockEmployeeRepository();

        // GET: Employee
        [HttpGet]
        public IHttpActionResult Index()
        {
            var employees = dataRepository.GetEmployees();

            if (!employees.Any())
            {
                return NotFound();
            }
            return Ok(employees);
        }

        //post http://localhost:55642/api/employee/
        [HttpPost]
        public Employee Post(Employee employee)
        {
            return dataRepository.AddEmployee(employee);
        }

        //get  http://localhost:55642/api/employee/2
        [HttpGet]
        public Employee Details(int id)
        {
            return dataRepository.GetEmployee(id);
        }

        public IHttpActionResult Put(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            var updatedEmployee = dataRepository.UpdateEmployee(employee);
            if(updatedEmployee == null)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            dataRepository.DeleteEmployee(id);
            return Ok();
        }

    }
}

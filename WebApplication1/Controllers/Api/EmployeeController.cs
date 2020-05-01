using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class EmployeeController : ApiController
    {
        //added cooment to test when in branch : feature/testBranch
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
        public HttpResponseMessage Post(Employee employee)
        {
            try
            {
                var addedEmployee = dataRepository.AddEmployee(employee);
                var message = Request.CreateResponse(HttpStatusCode.Created, addedEmployee);
                message.Headers.Location = new Uri(Request.RequestUri + addedEmployee.Id.ToString());

                return message;
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        //get  http://localhost:55642/api/employee/2
        [HttpGet]
        public HttpResponseMessage Details(int id)
        {
            var employee = dataRepository.GetEmployee(id);
            if(employee != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with id = {id.ToString()} not found");
            }
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

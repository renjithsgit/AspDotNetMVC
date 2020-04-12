using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<EmployeeViewModel> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55642/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Employee");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeViewModel>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    employees = Enumerable.Empty<EmployeeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55642/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmployeeViewModel>("employee", employee);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(employee);
        }

        public ActionResult Edit(int Id)
        {
            EmployeeViewModel employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55642/api/");
                //HTTP GET
                var responseTask = client.GetAsync("employee?id=" + Id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55642/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<EmployeeViewModel>("employee", employee);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
    }

}
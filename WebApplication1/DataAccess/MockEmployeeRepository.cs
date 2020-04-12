using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class MockEmployeeRepository:IEmployeeRepository
    {
        private IList<Employee> employees = new List<Employee>();

        public MockEmployeeRepository()
        {
            employees.Add(new Employee() { Id = 1, FirstName = "First", LastName = "firstlast" });
            employees.Add(new Employee() { Id = 2, FirstName = "Second", LastName = "ssss" });
            employees.Add(new Employee() { Id = 3, FirstName = "Third", LastName = "tttt" });
        }


        public Employee AddEmployee(Employee employee)
        {
            employee.Id = employees.Max(e => e.Id) + 1;
            employees.Add(employee);
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public IList<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var existingEmployee = employees.Where(s => s.Id == employee.Id).FirstOrDefault<Employee>();
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Gender = employee.Gender;

            }
            return existingEmployee;
        }

        public void DeleteEmployee(int id)
        {
            var existingEmployee = employees.Where(s => s.Id == id).FirstOrDefault<Employee>();
            employees.Remove(existingEmployee);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetEmployees();
        Employee AddEmployee(Employee employee);
        Employee GetEmployee(int id);
    }
}

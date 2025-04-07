using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.DataTransferObjects.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public interface IEmployeeService
    {
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployee();
       EmployeeDetialsDto? GetEmployeeById(int id);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
    }
}

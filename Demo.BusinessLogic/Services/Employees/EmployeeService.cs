using Demo.BusinessLogic.DataTransferObjects.Employees;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
          var employee= _employeeRepository.GetAll();
            return employee.Select(e=>e.ToEmployeeDto());
        }
        public EmployeeDetialsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : employee.ToEmployeeDetialsDto(); 
        }
        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee =employeeDto.ToEntity();
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
           var employee = employeeDto.ToEntity();
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee= _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                 var result =_employeeRepository.Remove(employee);
                if(result >0 ) return true; 
                else return false;
            }
        }
    }
}

using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.Employees;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService(IEmployeeRepository _employeeRepository ,IMapper _mapper ) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
          var employee= _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<Employee>,IEnumerable< EmployeeDto>>(employee);
        }
        public EmployeeDetialsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetialsDto>(employee); 
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee =_mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
           var employee = _mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto);
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

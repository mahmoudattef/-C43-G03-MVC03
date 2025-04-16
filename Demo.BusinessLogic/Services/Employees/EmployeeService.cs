using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.Employees;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService(IUnitOfWork _unitOfWork ,IMapper _mapper ,IAttachmentService _attachmentService ) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName)) 
                employees = _unitOfWork.employeeRepository.GetAll();
            else
            employees = _unitOfWork.employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));


            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }
        public EmployeeDetialsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetialsDto>(employee); 
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee =_mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);
            if (employeeDto.Image is not null) { 
               employee.ImageName= _attachmentService.Uploud(employeeDto.Image,"Images");
            }
             _unitOfWork.employeeRepository.Add(employee);
            return _unitOfWork.SaveChange();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
           var employee = _mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto);
            if (employeeDto.Image is not null )
            {
                employee.ImageName = _attachmentService.Uploud(employeeDto.Image, "Images");
            }
          
            _unitOfWork.employeeRepository.Update(employee);
            return _unitOfWork.SaveChange();

        }
        public bool DeleteEmployee(int id)
        {
            var employee= _unitOfWork.employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                 _unitOfWork.employeeRepository.Remove(employee);
                var result = _unitOfWork.SaveChange();

                if (result >0 ) return true; 
                else return false;
            }
        }
    }
}

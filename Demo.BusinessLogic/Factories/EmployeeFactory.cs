using Demo.BusinessLogic.DataTransferObjects.Employees;

using Demo.DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
     static class EmployeeFactory
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
           return  new EmployeeDto()
            {
                Age = employee.Age,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                Id = employee.Id,
                IsAction = employee.IsAction,
                Name = employee.Name,
                Salary = employee.Salary,
                
            };
        }
        public static EmployeeDetialsDto ToEmployeeDetialsDto(this Employee employee)
        {
            return new EmployeeDetialsDto()
            {
                Address = employee.Address,
                Age = employee.Age,
                CreatedBy = employee.CreatedBy,
                CreatedOn = DateOnly.FromDateTime( employee.CreatedOn),
                Email = employee.Email,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Id = employee.Id,
                IsAction= employee.IsAction,
                IsDeleted = employee.IsDeleted,
                LastModifiedBy = employee.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(employee.LastModifiedOn),   
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                Salary  =employee.Salary
            };
        }
        public static Employee ToEntity(this CreatedEmployeeDto createdEmployeeDto)
        {
            return new Employee()
            {
                Address= createdEmployeeDto.Address,
                Age= createdEmployeeDto.Age,
                Email = createdEmployeeDto.Email,
                EmployeeType = createdEmployeeDto.EmployeeType,
                Gender = createdEmployeeDto.Gender,
                IsAction = createdEmployeeDto.IsAction,
                Name = createdEmployeeDto.Name, 
                PhoneNumber= createdEmployeeDto.PhoneNumber,
                Salary=createdEmployeeDto.Salary
            };

        }
        public static Employee ToEntity(this UpdatedEmployeeDto E)
        {
            return new Employee()
            {
                Address = E.Address,
                Age = E.Age,
                Email = E.Email,
                EmployeeType = E.EmployeeType,
                Gender = E.Gender,
                IsAction = E.IsAction,
                Name = E.Name,
                PhoneNumber = E.PhoneNumber,
                Salary = E.Salary
            };

        }

    }
}

using Demo.BusinessLogic.DataTransferObjects;
using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                DeptId = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }

        public static DepartmentDetialsDto ToDepartmentDetialstDTo(this Department D)
        {
            return new DepartmentDetialsDto()
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                CreatedOn = DateOnly.FromDateTime(D.CreatedOn),
                CreatedBy = D.CreatedBy,
                LastModifiedBy = D.CreatedBy,
                LastModifiedOn = DateOnly.FromDateTime(D.CreatedOn),
                IsDeleted = D.IsDeleted,
                    
            };
        }   

        public static Department ToEntity(this CreatedDepartmentDto dto)
        {
            return new Department()
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto dto) {

            return new Department()
            {
                Id= dto.Id, 
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}


using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.Employees
{
    public class CreatedEmployeeDto
    {
        [Required]
        public string Name { get; set; } = null!;
    
        public int Age { get; set; }

        public string? Address { get; set; }
        public decimal Salary { get; set; }

        public bool IsAction { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
     
        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public  int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}

﻿using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class EmployeeViewModel
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
        public int? DepartmentId { get; set; }
        public  IFormFile? Image { get; set; }
    }
}

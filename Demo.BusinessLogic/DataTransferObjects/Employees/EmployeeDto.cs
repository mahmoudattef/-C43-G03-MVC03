using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsAction { get; set; }
        public string? Email { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}

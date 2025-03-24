using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Departments
{
    public class DepartmentService(IDepartmentRepositories _departmentRepositories) : IDepartmentService
    {
        //Get All Department
        public IEnumerable<DepartmentDto> GetAllDepartment()
        {
            var department = _departmentRepositories.GetAll();
            return department.Select(D => D.ToDepartmentDto())
 ;
        }

        //Get Department By Id
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var departmet = _departmentRepositories.GetById(id);

            return departmet is null ? null : departmet.ToDepartmentDetialstDTo();

        }


        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepositories.Add(department);
        }

        public int UpdateDeparment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepositories.Update(departmentDto.ToEntity());
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepositories.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepositories.Remove(Department);
                if (Result > 0) return true;
                else return false;
            }
        }


    }
}

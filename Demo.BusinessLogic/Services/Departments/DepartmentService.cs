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
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //Get All Department
        public IEnumerable<DepartmentDto> GetAllDepartment()
        {
            var department = _unitOfWork.departmentRepository.GetAll();
            return department.Select(D => D.ToDepartmentDto())
 ;
        }

        //Get Department By Id
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var departmet = _unitOfWork.departmentRepository.GetById(id);

            return departmet is null ? null : departmet.ToDepartmentDetialstDTo();

        }


        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.departmentRepository.Add(department);
            return _unitOfWork.SaveChange();
        }

        public int UpdateDeparment(UpdatedDepartmentDto departmentDto)
        {
             _unitOfWork.departmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChange();

        }

        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                 _unitOfWork.departmentRepository.Remove(Department);
                int Result = _unitOfWork.SaveChange();

                if (Result > 0) return true;
                else return false;
            }
        }


    }
}

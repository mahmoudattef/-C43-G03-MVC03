using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.Department;

namespace Demo.BusinessLogic.Services.Departments
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartment();
        DepartmentDetialsDto? GetDepartmentById(int id);
        int UpdateDeparment(UpdatedDepartmentDto departmentDto);
    }
}
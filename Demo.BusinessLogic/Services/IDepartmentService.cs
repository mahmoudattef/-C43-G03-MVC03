using Demo.BusinessLogic.DataTransferObjects;

namespace Demo.BusinessLogic.Services
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
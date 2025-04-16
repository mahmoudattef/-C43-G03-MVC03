using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public  IEmployeeRepository employeeRepository {  get; }    
        public  IDepartmentRepositories departmentRepository { get; }
        int SaveChange(); 

    }
}

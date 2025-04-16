using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IDepartmentRepositories> _departmentRepositories;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public UnitOfWork( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _departmentRepositories = new Lazy<IDepartmentRepositories>(() => new DepartmentRepositories(dbContext));
            _employeeRepository =new Lazy<IEmployeeRepository>(() => new EmployeeRepositories(dbContext));  
        }
        public IEmployeeRepository employeeRepository => _employeeRepository.Value;

        public IDepartmentRepositories departmentRepository => _departmentRepositories.Value;


        public int SaveChange()
        {
           return _dbContext.SaveChanges();
        }
    }
}

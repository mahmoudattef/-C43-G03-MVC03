using Demo.DataAccess.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories
{
    public class DepartmentRepositories(ApplicationDbContext dbContext) : IDepartmentRepositories
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }

        // find ده بتدور الاول علي ال id local لو ملقتهوش تبدا تبعت request to Db
        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        public int Update(Department department)
        {
            _dbContext.Update(department);
            return _dbContext.SaveChanges();
        }

        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }


    }
}

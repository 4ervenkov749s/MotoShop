using MotoShop.DL.InMemoryDb;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.DL.Repositories
{
    public class EmployeeInMemoryRepository : IEmployeeRepository
    {
        public EmployeeInMemoryRepository()
        { }

        public Employee Create(Employee employee)
        {
            EmployeeInMemoryCollection.EmployeeDb.Add(employee);

            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = EmployeeInMemoryCollection.EmployeeDb.FirstOrDefault(employee => employee.Id == id);

            EmployeeInMemoryCollection.EmployeeDb.Remove(employee);

            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return EmployeeInMemoryCollection.EmployeeDb;
        }

        public Employee GetById(int id)
        {
            return EmployeeInMemoryCollection.EmployeeDb.FirstOrDefault(x => x.Id == id);
        }

        public Employee Update(Employee employee)
        {
            var result = EmployeeInMemoryCollection.EmployeeDb.FirstOrDefault(x => x.Id == employee.Id);

            result.Name = employee.Name;

            return result;
        }
    }
}

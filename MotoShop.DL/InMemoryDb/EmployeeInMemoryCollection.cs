using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.InMemoryDb
{
    public static class EmployeeInMemoryCollection
    {
        public static List<Employee> EmployeeDb = new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                Name = "Genadii",
                Competence = Models.Enums.EmployeeCompetence.Engines,
                Salary = 2000
            },
            new Employee()
            {
                Id=2,
                Name = "Simo",
                Competence = Models.Enums.EmployeeCompetence.Paint,
                Salary = 1800
            },
            new Employee()
            {
                Id = 3,
                Name = "Dimitar",
                Competence = Models.Enums.EmployeeCompetence.Electronic,
                Salary = 3000
            },
            new Employee()
            {
                Id = 4,
                Name = "Velin",
                Competence = Models.Enums.EmployeeCompetence.Tuning,
                Salary = 3500
            }
        };
    }
}

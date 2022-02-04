using MotoShop.Models.Enums;

namespace MotoShop.Models.DTO
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeeCompetence Competence { get; set; }
        public double Salary { get; set; }


    }
}

using Domain.Common;

namespace Domain
{
    public class Company : EntityObject
    {
        public string Name { get; set; }

        public IEnumerable<Employee> Employees { get; private set; } = new List<Employee>();

        public Company(string name,
                       IEnumerable<Employee> employees)
        {
            Name = name;
            Employees = employees;
        }

        protected Company()
        {
        }

        public Company(string name)
        {
            Name = name;
        }
    }
}
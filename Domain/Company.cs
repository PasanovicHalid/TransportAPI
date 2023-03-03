using Domain.Common;

namespace Domain
{
    public class Company : EntityObject
    {
        public string Name { get; private set; }

        public IEnumerable<Employee> Employees { get; private set; } = new List<Employee>();

        protected Company(ulong id,
                       bool deleted) : base(id, deleted)
        {
        }

        public Company(ulong id,
                       bool deleted,
                       string name,
                       IEnumerable<Employee> employees) : base(id, deleted)
        {
            Name = name;
            Employees = employees;
        }

    }
}
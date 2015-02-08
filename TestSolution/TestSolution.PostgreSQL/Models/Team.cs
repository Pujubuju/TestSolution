using System.Collections.Generic;

namespace TestSolution.PostgreSQL.Models
{
    public class Team : Entity
    {
        public virtual IList<Person> Members { get; set; }
    }
}

using FluentNHibernate.Mapping;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL.Mappings
{
    public class PersonMapping : ClassMap<Person>
    {
        public PersonMapping()
        {
            Id(p => p.Id).GeneratedBy.Guid();
            Map(x => x.Name).Length(20).Not.Nullable();
            Map(x => x.Age).Not.Not.Nullable();
        }
    }
}

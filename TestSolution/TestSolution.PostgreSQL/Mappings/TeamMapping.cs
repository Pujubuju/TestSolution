using FluentNHibernate.Mapping;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL.Mappings
{
    public class TeamMapping : ClassMap<Team>
    {
        public TeamMapping()
        {
            Id(p => p.Id).GeneratedBy.Guid();
            Map(x => x.Members);
            References<Person>(x => x.Members);
        }
    }
}

using FluentNHibernate.Mapping;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL.Mappings
{
    public class ModelMapping : ClassMap<Model>
    {
        public ModelMapping()
        {
            Id(p => p.Id).GeneratedBy.Guid();
            Map(p => p.Name);
            Map(p => p.Description);
        }
    }
}

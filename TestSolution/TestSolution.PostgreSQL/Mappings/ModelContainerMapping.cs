using FluentNHibernate.Mapping;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL.Mappings
{
    public class ModelContainerMapping : ClassMap<ModelContainer>
    {
        public ModelContainerMapping()
        {
            Id(p => p.Id).GeneratedBy.Guid();
            Map(p => p.Name);
            References(p => p.Model);
        }
    }
}

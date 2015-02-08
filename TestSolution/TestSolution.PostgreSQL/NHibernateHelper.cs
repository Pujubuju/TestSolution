using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL
{
    public static class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static void ResetSessionFactory()
        {
            _sessionFactory = null;
        }

        //This method will create/recreate our database
	    //This method should be called only once when
	    //we want to create our database
        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }

        //This method build our session factory -
        //the most important part of our ORM application
        private static void BuildSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .ConnectionString(c => c
                        .Host("localhost")
                        .Port(5432)
                        .Database("testdb")
                        .Username("postgres")
                        .Password("pujubuju89")))
                .Mappings(m => m
                    .AutoMappings.Add(CreateMappings))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        //This method will create our auto-mappings model
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap.AssemblyOf<Entity>()
                .Where(t => t.Namespace == "TestSolution.PostgreSQL.Mappings" ||
                            t.Namespace == "TestSolution.PostgreSQL.Models");
        }

    }
}

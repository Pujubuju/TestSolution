using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TestSolution.PostgreSQL.Models;

namespace TestSolution.PostgreSQL
{
    public class PostgreSQLDatabaseConnector
    {

        public void Save(Entity entity)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public string GetModelName(Guid modelId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var model = session.Load<Model>(modelId);
                    transaction.Commit();
                    return model.Name;
                }
            }
        }

        public string GetModelDescription(Guid modelId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var model = session.Load<Model>(modelId);
                    transaction.Commit();
                    return model.Description;
                }
            }
        }

        public string GetModelContainerName(Guid modelContainerId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var model = session.Load<ModelContainer>(modelContainerId);
                    transaction.Commit();
                    return model.Name;
                }
            }
        }

        public Model GetModelContainerModel(Guid modelContainerId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var model = session.Load<ModelContainer>(modelContainerId);
                    transaction.Commit();
                    return model.Model;
                }
            }
        }

        public string GetPersonName(Guid personId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var person = session.Load<Person>(personId);
                    return person.Name;
                }
            }
        }

        public int GetPersonAge(Guid personId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var person = session.Load<Person>(personId);
                    return person.Age;
                }
            }
        }

        public Person GetPerson(Guid personId)
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var person = session.Load<Person>(personId);
                    return new Person(){Id = person.Id, Name = person.Name, Age = person.Age};
                }
            }
        }

        public IList<Person> GetAllPersons()
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var persons = session.QueryOver<Person>().List();
                    return persons;
                }
            }
        }

        public IList<Team> GetAllTeams()
        {
            using (ISession session = NHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teams = session.QueryOver<Team>().List();
                    teams.ForEach(x => x.Members = x.Members.ToList());
                    return teams;
                }
            }
        }

    }
}
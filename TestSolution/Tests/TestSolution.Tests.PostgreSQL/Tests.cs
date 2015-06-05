using System;
using System.Collections.Generic;
using NUnit.Framework;
using TestSolution.Common;
using TestSolution.PostgreSQL;
using TestSolution.PostgreSQL.Models;
using TestSolution.Tests.Common;

namespace TestSolution.Tests.PostgreSQL
{
    [TestFixture]
    public class Tests : BaseTest
    {
        public override void TestFixtureSetUp()
        {
            NHibernateHelper.ResetSessionFactory();
        }

        public override void TestFixtureTearDown()
        {
            NHibernateHelper.ResetSessionFactory();
        }

        public override void SetUp()
        {
            NHibernateHelper.ResetSessionFactory();
        }

        public override void TearDown()
        {
            NHibernateHelper.ResetSessionFactory();
        }

        private static void AssertPersonsAreEqual(Person person1, Person person2)
        {
            Assert.AreEqual(person1.Id, person2.Id);
            Assert.AreEqual(person1.Name, person2.Name);
            Assert.AreEqual(person1.Age, person2.Age);
        }

        private static void AssertPersonsListsAreSame(IList<Person> expectedPersons, IList<Person> persons)
        {
            Assert.AreEqual(expectedPersons.Count, persons.Count);
            for (var index = 0; index < persons.Count; index++)
            {
                var person1 = expectedPersons[index];
                var person2 = persons[index];
                AssertPersonsAreEqual(person1, person2);
            }
        }

        [Test]
        public void Should_save_and_load_model_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var model = new Model()
            {
                Id = Guid.NewGuid(), 
                Name = "name", 
                Description = "description"
            };
            connector.Save(model);
            var name = connector.GetModelName(model.Id);
            var description = connector.GetModelDescription(model.Id);

            Assert.AreEqual(model.Name, name);
            Assert.AreEqual(model.Description, description);
        }

        [Test]
        public void Should_save_and_load_model_container_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var modelContainer = new ModelContainer()
            {
                Id = Guid.NewGuid(),
                Name = "containerName",
                Model = new Model() { Id = Guid.NewGuid(), Name = "modelName", Description = "modelDescription"}
            };
            connector.Save(modelContainer.Model);
            connector.Save(modelContainer);
            var modelContainerName = connector.GetModelContainerName(modelContainer.Id);
            var model = connector.GetModelContainerModel(modelContainer.Id);
            var name = connector.GetModelName(model.Id);
            var description = connector.GetModelDescription(model.Id);

            Assert.AreEqual(modelContainer.Name, modelContainerName);

            Assert.AreEqual(modelContainer.Model.Name, name);
            Assert.AreEqual(modelContainer.Model.Description, description);
        }

        [Test]
        public void Should_save_and_load_person_propertis_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var model = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Age = 19,
            };
            connector.Save(model);

            var name = connector.GetPersonName(model.Id);
            var age = connector.GetPersonAge(model.Id);

            Assert.AreEqual(model.Name, name);
            Assert.AreEqual(model.Age, age);
        }

        [Test]
        public void Should_save_and_load_person_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var model = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Age = 19,
            };
            connector.Save(model);

            var person = connector.GetPerson(model.Id);

            AssertPersonsAreEqual(model, person);
        }

        [Test]
        public void Should_save_and_load_all_persons_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var randomGenerator = new RandomGenerator(82447248);
            var expectedPersons = new List<Person>(100);
            for (var i = 0; i < 100; i++)
            {
                var model = new Person()
                {
                    Id = Guid.NewGuid(),
                    Name = randomGenerator.RandomAlphanumericString(18),
                    Age = randomGenerator.Next(1, 95),
                };
                expectedPersons.Add(model);
                connector.Save(model); 
            }

            var persons = connector.GetAllPersons();
            Assert.AreEqual(expectedPersons.Count, persons.Count);
            for (var index = 0; index < persons.Count; index++)
            {
                var person1 = expectedPersons[index];
                var person2 = persons[index];
                AssertPersonsAreEqual(person1, person2);
            }
        }

        [Test]
        public void Should_save_and_load_all_teams_from_database()
        {
            var connector = new PostgreSQLDatabaseConnector();

            var randomGenerator = new RandomGenerator(82447248);
            var expectedPersons = new List<Person>(100);
            var expectedPersonsCopy = new List<Person>(100);
            for (var i = 0; i < 100; i++)
            {
                var model = new Person()
                {
                    Id = Guid.NewGuid(),
                    Name = randomGenerator.RandomAlphanumericString(18),
                    Age = randomGenerator.Next(1, 95),
                };
                expectedPersons.Add(model);
                expectedPersonsCopy.Add(model);
                connector.Save(model);
            }

            var persons = connector.GetAllPersons();
            AssertPersonsListsAreSame(expectedPersons, persons);

            var expectedTeams = new List<Team>(10);
            for (var i = 0; i < 10; i++)
            {
                var members = randomGenerator.GetRandomElements(expectedPersonsCopy, 5);
                foreach (var member in members)
                {
                    expectedPersonsCopy.Remove(member);
                }
                var model = new Team()
                {
                    Id = Guid.NewGuid(),
                    Members = members,
                };
                expectedTeams.Add(model);
                connector.Save(model);
            }

            var teams = connector.GetAllTeams();
            Assert.AreEqual(expectedTeams.Count, teams.Count);
            for (var index = 0; index < expectedTeams.Count; index++)
            {
                var team1 = expectedTeams[index];
                var team2 = teams[index];
                Assert.AreEqual(team1.Id, team2.Id);
                AssertPersonsListsAreSame(team1.Members, team2.Members);
            }
        }

    }
}

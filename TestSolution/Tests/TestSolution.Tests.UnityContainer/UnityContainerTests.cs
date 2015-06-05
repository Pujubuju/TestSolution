using Microsoft.Practices.Unity;
using NUnit.Framework;
using TestSolution.Tests.Common;
using TestSolution.Tests.Unity.Classes;

namespace TestSolution.Tests.Unity
{
    public class UnityContainerTests : BaseTest
    {

        [Test]
        public void Resolve_of_register_type_gets_same_instance_every_time()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceA, ServiceA>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceB, ServiceB>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceA>();
            var serviceInstance2 = container.Resolve<IServiceA>();

            serviceInstance1.Count = 24;

            Assert.AreEqual(serviceInstance1, serviceInstance2);
            Assert.AreEqual(serviceInstance1.Count, serviceInstance2.Count);
        }

        [Test]
        public void Resolve_of_register_instance_gets_same_instance_every_time()
        {
            var container = new UnityContainer();

            container.RegisterInstance(new ServiceA(), new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceB, ServiceB>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<ServiceA>();
            var serviceInstance2 = container.Resolve<ServiceA>();

            serviceInstance1.Count = 24;

            Assert.AreEqual(serviceInstance1, serviceInstance2);
            Assert.AreEqual(serviceInstance1.Count, serviceInstance2.Count);
        }

        [Test]
        public void Resolve_of_register_type_gets_same_instance_every_time_if_inharitance_is_involved()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceA, ServiceA>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceAChild, ServiceAChild>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceA>();
            var serviceInstance2 = container.Resolve<IServiceA>();

            serviceInstance1.Count = 24;

            Assert.AreEqual(serviceInstance1, serviceInstance2);
            Assert.AreEqual(serviceInstance1.Count, serviceInstance2.Count);
        }

        [Test]
        public void Resolve_of_register_type_gets_same_instance_every_time_if_inharitance_is_involved_without_interfaces()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceA, ServiceA>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceAChild, ServiceAChild>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceA>();
            var serviceInstance2 = container.Resolve<IServiceAChild>();

            serviceInstance1.Count = 24;

            Assert.AreNotEqual(serviceInstance1, serviceInstance2);
        }

        [Test]
        public void Resolve_of_register_type_gets_same_instance_every_time_if_inharitance_is_involved_with_interfaces()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceB, ServiceB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceBChild, ServiceBChild>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceB>();
            var serviceInstance2 = container.Resolve<IServiceBChild>();

            serviceInstance1.Count = 24;

            Assert.AreNotEqual(serviceInstance1, serviceInstance2);
            Assert.AreNotEqual(serviceInstance1.Count, serviceInstance2.Count);
        }

        [Test, ExpectedException]
        public void Cant_get_base_interface_from_child()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceAChild, ServiceAChild>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceA>();
            var serviceInstance2 = container.Resolve<IServiceAChild>();

            serviceInstance1.Count = 24;

            Assert.AreNotEqual(serviceInstance1, serviceInstance2);
        }

        [Test]
        public void Gets_same_instance_for_two_interfaces_if_it_was_registered_that_way()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceAChild, ServiceAChild>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceA, ServiceAChild>(new ContainerControlledLifetimeManager());

            var serviceInstance1 = container.Resolve<IServiceA>();
            var serviceInstance2 = container.Resolve<IServiceAChild>();

            serviceInstance1.Count = 24;

            Assert.AreEqual(serviceInstance1, serviceInstance2);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CollectionsPerformanceComparison.Actions;
using CollectionsPerformanceComparison.Generators;
using NUnit.Framework;

namespace CollectionsPerformanceComparison.Tests
{
    [TestFixture]
    public class ContainsItemSpeedTest
    {

        [Test]
        public void HashSet()
        {
            var action = new ContainsAction<HashSet<string>, string>(TestsSettings.OPERATIONS_COUNT, TestsSettings.ITEMS_COUNT, new StringGenerator());
            var measurement = new ActionTimeMeasurement<HashSet<string>, string>(action);
            TimeSpan measure = measurement.Measure();
            Console.WriteLine(measure);
        }

        [Test]
        public void List()
        {
            var action = new ContainsAction<List<string>, string>(TestsSettings.OPERATIONS_COUNT, TestsSettings.ITEMS_COUNT, new StringGenerator());
            var measurement = new ActionTimeMeasurement<List<string>, string>(action);
            TimeSpan measure = measurement.Measure();
            Console.WriteLine(measure);
        }

        [Test]
        public void Collection()
        {
            var action = new ContainsAction<Collection<string>, string>(TestsSettings.OPERATIONS_COUNT, TestsSettings.ITEMS_COUNT, new StringGenerator());
            var measurement = new ActionTimeMeasurement<Collection<string>, string>(action);
            TimeSpan measure = measurement.Measure();
            Console.WriteLine(measure);
        }

    }
}
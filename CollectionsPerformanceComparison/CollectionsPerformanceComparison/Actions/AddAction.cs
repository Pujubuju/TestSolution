using System.Collections.Generic;
using CollectionsPerformanceComparison.Actions.Abstraction;
using CollectionsPerformanceComparison.Generators;

namespace CollectionsPerformanceComparison.Actions
{
    public class AddAction<Tc, Ti> : ExecutableAction<Tc, Ti> where Tc : ICollection<Ti>, new()
    {

        public AddAction(int times, long itemsCount, Generator<Ti> generator)
            : base(times, itemsCount, generator)
        {
        }

        protected override void ExecuteSingleOperation(int i)
        {
            TestCollection.Add(TestItems[i]);
        }

    }
}
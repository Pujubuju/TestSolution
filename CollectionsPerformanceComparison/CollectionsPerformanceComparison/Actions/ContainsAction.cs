using System.Collections.Generic;
using CollectionsPerformanceComparison.Actions.Abstraction;
using CollectionsPerformanceComparison.Generators;

namespace CollectionsPerformanceComparison.Actions
{
    public class ContainsAction<Tc, Ti> : ExecutableAction<Tc, Ti> where Tc : ICollection<Ti>, new()
    {

        public ContainsAction(int times, long itemsCount, Generator<Ti> generator)
            : base(times, itemsCount, generator)
        {
        }

        protected override void ExecuteSingleOperation(int i)
        {
            TestCollection.Contains(TestItems[i]);
        }

    }
}
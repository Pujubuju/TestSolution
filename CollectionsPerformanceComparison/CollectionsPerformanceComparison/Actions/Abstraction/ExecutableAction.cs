using System.Collections.Generic;
using CollectionsPerformanceComparison.Generators;

namespace CollectionsPerformanceComparison.Actions.Abstraction
{
    public abstract class ExecutableAction<Tc, Ti> where Tc : ICollection<Ti>, new()
    {
        private readonly Generator<Ti> _generator;
        private readonly int _times;
        private readonly long _itemsCount;
        private readonly Tc _testCollection;
        private Ti[] _testItems;

        protected Ti[] TestItems
        {
            get
            {
                return _testItems;
            }
        }

        protected Tc TestCollection
        {
            get
            {
                return _testCollection;
            }
        }

        protected ExecutableAction(
            int times, 
            long itemsCount, 
            Generator<Ti> generator)
        {
            _times = times;
            _itemsCount = itemsCount;
            _generator = generator;
            _testCollection = new Tc();
        }

        private void CreateTestItems(int testItemsCount)
        {
            _testItems = new Ti[testItemsCount];
            for (int i = 0; i < testItemsCount; i++)
            {
                _testItems[i] = _generator.Next();
            }
        }

        private void CreateItems(long itemsCount)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                _testCollection.Add(_generator.Next());
            }
        }

        protected abstract void ExecuteSingleOperation(int i);

        public void Execute()
        {
            for (int i = 0; i < _times; i++)
            {
                ExecuteSingleOperation(i);
            }
        }

        public void Prepare()
        {
            CreateTestItems(_times);
            CreateItems(_itemsCount);
        }
    }
}
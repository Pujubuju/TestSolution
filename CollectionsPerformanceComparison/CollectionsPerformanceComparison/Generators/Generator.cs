using CollectionsPerformanceComparison.Utils;

namespace CollectionsPerformanceComparison.Generators
{
    public abstract class Generator<T>
    {
        protected RandomGenerator RandomGenerator { get; private set; }

        protected Generator()
        {
            RandomGenerator = new RandomGenerator();
        }

        public abstract T Next();
    }
}

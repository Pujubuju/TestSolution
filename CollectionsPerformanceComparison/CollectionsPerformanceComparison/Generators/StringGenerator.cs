namespace CollectionsPerformanceComparison.Generators
{
    public class StringGenerator : Generator<string>
    {
        public override string Next()
        {
            return RandomGenerator.RandomString(20);
        }
    }
}
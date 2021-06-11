using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.All)]

namespace TestNamespace
{
    public static class Config
    {
        public static Parameters Parameters { get; set; } = new Parameters();
    }

    public class Parameters
    {
        public int Integer { get; set; } = 1;
    }


    public class Tests
    {
        [TestCase(1, Category = "LongRunning", Explicit = true)]
        [TestCase(2, Category = "LongRunning", Explicit = true)]
        [TestCase(3, Category = "LongRunning", Explicit = true)]
        [TestCase(4, Category = "LongRunning", Explicit = true)]
        [TestCase(5, Category = "LongRunning", Explicit = true)]
        [TestCase(6, Category = "LongRunning", Explicit = true)]
        [TestCase(7, Category = "LongRunning", Explicit = true)]
        [TestCase(8, Category = "LongRunning", Explicit = true)]
        public void BestMove_Mate_in_3(int n)
        {
            TestContext.WriteLine($"Value: ${Config.Parameters.Integer}");  // Seeing values different to 1 here, and therefore test cases failing
            Validate(1);
        }

        [NonParallelizable]
        [TestCase(1, Category = "LongRunning", Explicit = true)]
        [TestCase(2, Category = "LongRunning", Explicit = true)]
        [TestCase(3, Category = "LongRunning", Explicit = true)]
        [TestCase(4, Category = "LongRunning", Explicit = true)]
        [TestCase(5, Category = "LongRunning", Explicit = true)]
        [TestCase(6, Category = "LongRunning", Explicit = true)]
        [TestCase(7, Category = "LongRunning", Explicit = true)]
        [TestCase(8, Category = "LongRunning", Explicit = true)]
        public void BestMove_Quiescence(int n)
        {
            int oldValue = Config.Parameters.Integer;

            try
            {
                TestContext.WriteLine($"Changing value to ${n}");
                Config.Parameters.Integer = n;

                Validate(n);
            }
            finally
            {
                TestContext.WriteLine($"Restoring value to ${oldValue}");
                Config.Parameters.Integer = oldValue;
            }
        }

        public static void Validate(int a)
        {
            for (int i = 0; i < 1_000; ++i)
            {
                Assert.AreEqual(a, Config.Parameters.Integer);
            }
        }
    }
}
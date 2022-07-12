using Recognizer;

namespace ImageTests
{
    internal class SobelFilterTests
    {
        public class SobelFilterTest
        {
            public double[,] Image;
            public double[,] SX;

            public double[,] Expected;
        }

        [TestCaseSource(nameof(SobelFilterCases))]
        public void TestSoberFilter(SobelFilterTest test)
        {
            var actual = SobelFilterTask.SobelFilter(test.Image, test.SX);
            for (int y = 0 ; y < actual.GetLength(0) ; y++)
                for (int x = 0 ; x < actual.GetLength(1) ; x++)
                    Assert.That(actual[ y, x ], Is.EqualTo(test.Expected[ y, x ]).Within(0.0001));
        }

        private static SobelFilterTest[] SobelFilterCases =
        {
            new SobelFilterTest()
            {
                Image = new [,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 0 },
                    { 1, 1, 0, 0 },
                    { 1, 0, 0, 0.0 },
                },
                SX = new [,]
                {
                    { 1, 2, 1 },
                    { 0, 0, 0 },
                    {-1, -2, -1.0},
                },
                Expected = new [,]
                {
                    { 0, 0,                0,                0 },
                    { 0, Math.Sqrt(2),     3 * Math.Sqrt(2), 0 },
                    { 0, 3 * Math.Sqrt(2), 3 * Math.Sqrt(2), 0 },
                    { 0, 0,                0,                0 }
                }
            }
        };
    }
}

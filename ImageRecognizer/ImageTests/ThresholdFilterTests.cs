using Recognizer;

namespace ImageTests
{
    internal class ThresholdFilterTests
    {
        public class ThresholdTest
        {
            public double[,] Pixels;
            public double WhitePixelFraction;

            public double[,] Expected;
        }

        [TestCaseSource(nameof(ThresholdFilterCases))]
        public void TestThresholdFilter(ThresholdTest test)
        {
            var actual = ThresholdFilterTask.ThresholdFilter(test.Pixels, test.WhitePixelFraction);
            Assert.That(actual, Is.EqualTo(test.Expected));
        }

        static ThresholdTest[] ThresholdFilterCases =
        {
            new ThresholdTest
            {
                Pixels = new [,] { { 123.0 } },
                Expected = new[,] { { 0.0 } },
                WhitePixelFraction = 0
            },
            new ThresholdTest
            {
                Pixels = new [,] { { 123.0 } },
                Expected = new[,] { { 1.0 } },
                WhitePixelFraction = 1
            },
            new ThresholdTest
            {
                Pixels = new [,] { { 123.0 } },
                Expected = new[,] { { 0.0 } },
                WhitePixelFraction = 0.5
            },
            new ThresholdTest
            {
                Pixels = new[,] { { 1.0, 0.0 } },
                Expected = new[,] { {1.0, 0.0 } },
                WhitePixelFraction = 0.5
            },
            new ThresholdTest
            {
                Pixels = new [,] { { 1.0, 2, 2, 3 } },
                Expected = new[,] { { 0.0, 1, 1, 1 } },
                WhitePixelFraction = 0.5
            },
            new ThresholdTest
            {
                Pixels = new[,]
                {
                    { 0.4, 0.6 },
                    { 0.1, 0 },
                    { 0.6, 0.9 }
                },
                Expected = new[,]
                {
                    { 0.0, 0.0 },
                    { 0.0, 0.0 },
                    { 0.0, 0.0 }
                },
                WhitePixelFraction = 0.1
            }
        };
    }
}

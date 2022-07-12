using Recognizer;

namespace ImageTests
{
    public class MedianFilterTests
    {
        public class MedianFilterTest
        {
            public double[,] Image;

            public double[,] Expected;
        }

        [TestCaseSource(nameof(MedianFilterCases))]
        public void TestMedianFilter(MedianFilterTest test)
        {
            var actual = MedianFilterTask.MedianFilter(test.Image);
            Assert.That(actual, Is.EqualTo(test.Expected));
        }

        private static MedianFilterTest[] MedianFilterCases =
        {
            new MedianFilterTest
            {
                Image = new [,]
                {
                  { 0, 0.3, 0.7, 0.5 },
                  { 0, 0.4, 0.9, 0.7 }
                },
                Expected = new[,]
                {
                    { 0.15, 0.35, 0.6, 0.7 },
                    { 0.15, 0.35, 0.6, 0.7 }
                }
            }
        };
    }
}
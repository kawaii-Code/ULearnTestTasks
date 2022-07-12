namespace Recognizer
{
	public static class GrayscaleTask
	{
		private static double GetBrightness(Pixel original)
        {
			return ( 0.299 * original.R 
				   + 0.587 * original.G 
				   + 0.114 * original.B ) / 255.0;
        }

		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var lenVertical = original.GetLength(0);
			var lenHorizontal = original.GetLength(1);
			double[,] result = new double[lenVertical, lenHorizontal];

			for (int y = 0 ; y < lenVertical ; y++)
				for	(int x = 0 ; x < lenHorizontal ; x++)
                    result[y, x] = GetBrightness(original[y, x]);

			return result;
		}
	}
}
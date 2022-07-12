using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
	{		
		private static double GetT( double[] arr, double whitePixelsFraction)
        {
			int whitePixelsCount = (int) (arr.Length * whitePixelsFraction);

			if (whitePixelsCount <= 0) 
				return double.MaxValue;
			return arr[whitePixelsCount - 1];
        }

		private static double[] ToOneDimensional( double[,] twoDim, int lenVertical, int lenHorizontal)
        {
			double[] result = new double[lenVertical * lenHorizontal];

			for (int i = 0 ; i < lenVertical ; i++)
				for (int j = 0 ; j < lenHorizontal ; j++)
					result[ i * lenHorizontal + j ] = twoDim[ i, j ];
			return result;
        }

		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var lenV = original.GetLength(0);
			var lenH = original.GetLength(1);
			double[] pixels = ToOneDimensional(original, lenV, lenH).OrderByDescending(x=>x).ToArray();
			double t = GetT(pixels, whitePixelsFraction);

			for (int i = 0 ; i < lenV ; i++)
				for (int j = 0 ; j < lenH ; j++)
					original[ i, j ] = original[ i, j ] >= t ? 1.0 : 0.0;
			return original;
		}
	}
}
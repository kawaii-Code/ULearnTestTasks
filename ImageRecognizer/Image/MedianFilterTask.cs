using System.Collections.Generic;

namespace Recognizer
{    
	public static class MedianFilterTask
	{
		private const int NeighborWindowSize = 1;

		private static List<double> CreateNeighborList( double[,] array, int yIndex, int xIndex)
        {
			List<double> result = new List<double>();
			
			int neighborY = yIndex - NeighborWindowSize;
			if (neighborY < 0) 
				neighborY += NeighborWindowSize;

			int yWindowLen = yIndex + 3 - NeighborWindowSize;
			int xWindowLen = xIndex + 3 - NeighborWindowSize;
			
			if (yWindowLen > array.GetLength(0))
				yWindowLen--;
			if (xWindowLen > array.GetLength(1))
				xWindowLen--;

			for ( ; neighborY < yWindowLen ; neighborY++)
            {
				int neighborX = xIndex - NeighborWindowSize;
				if (neighborX < 0) 
					neighborX += NeighborWindowSize;

				for (; neighborX < xWindowLen ; neighborX++)
					result.Add(array[ neighborY, neighborX ]);
            }
			return result;
        }

		private static double Median(List<double> pixels)
        {
			pixels.Sort();
			if (pixels.Count % 2 == 1)
				return pixels[ pixels.Count / 2 ];
			else
				return (pixels[ pixels.Count / 2 - 1 ] + pixels[ pixels.Count / 2 ]) / 2;
        }

		public static double[,] MedianFilter(double[,] original)
        {
			var lenVertical = original.GetLength(0);
			var lenHorizontal = original.GetLength(1);

			double[,] result = new double[ lenVertical, lenHorizontal ];

			for (int y = 0 ; y < lenVertical ; y++)
				for (int x = 0 ; x < lenHorizontal ; x++)                
					result[ y, x ] = Median(CreateNeighborList(original, y, x));   
			return result;
        }
	}
}
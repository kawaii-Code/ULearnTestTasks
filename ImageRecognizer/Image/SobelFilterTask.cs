using System;

namespace Recognizer
{
    public static class SobelFilterTask
    {
        private static double GetGX(double[,] g, double[,] sx, int sxLen, int x, int y)
        {
            double result = 0;
            for (int i = x - sxLen, k = 0 ; i <= x + sxLen ; i++, k++)
                for (int j = y - sxLen, m = 0 ; j <= y + sxLen ; j++, m++)
                {
                    result += g[ i, j ] * sx[k, m];
                }
            return result;
        }

        private static double GetGY( double[,] g, double[,] sx, int sxLen, int x, int y)
        {
            return GetGX(g, Transpose(sx), sxLen, x, y);
        }

        private static double[,] Transpose( double[,] matrix)
        {
            int size = matrix.GetLength(0);
            double[,] result = new double[ size, size ];

            for(int i = 0 ; i < size ; i++)
                for (int j = 0 ; j < size ; j++)                
                    result[ i, j ] = matrix[ j, i ];

            return result;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var sxLen = sx.GetLength(0) / 2;
            var result = new double[width, height];

            for (int x = sxLen ; x < width - sxLen; x++)
                for (int y = sxLen; y < height - sxLen; y++)
                {
                    var gx = GetGX(g, sx, sxLen, x, y);
                    var gy = GetGY(g, sx, sxLen, x, y);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
    }
}
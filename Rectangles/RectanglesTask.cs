using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
        private static bool TopLeftCornerInsideTheOther( Rectangle r1, Rectangle r2 )
        {
			return r2.Top <= r1.Top && r2.Top + r2.Height >= r1.Top &&
				   r2.Left <= r1.Left && r2.Left + r2.Width >= r1.Left;
        }
        private static bool PlusTypeIntersection( Rectangle r1, Rectangle r2 )
        {
			return r1.Left + r1.Width >= r2.Left &&
				   r1.Left <= r2.Left &&
				   r2.Top + r2.Height >= r1.Top &&
				   r2.Top <= r1.Top;
        }
		public static bool AreIntersected( Rectangle r1, Rectangle r2 )
		{
			return TopLeftCornerInsideTheOther(r1, r2) ||
				   PlusTypeIntersection(r1, r2) ||
				   TopLeftCornerInsideTheOther(r2, r1) ||
				   PlusTypeIntersection(r2, r1);
		}

        public static int IntersectionSquare( Rectangle r1, Rectangle r2 )
		{
			if (!AreIntersected(r1, r2)) return 0;

			if (TopLeftCornerInsideTheOther(r1, r2))
				return RectInsideOtherIntersectionSquare(r1, r2);
			else if (TopLeftCornerInsideTheOther(r2, r1))
				return RectInsideOtherIntersectionSquare(r2, r1);
			else if (PlusTypeIntersection(r1, r2))
				return PlusTypeIntersectionSquare(r1, r2);
			else
				return PlusTypeIntersectionSquare(r2, r1);
		}

		private static int PlusTypeIntersectionSquare( Rectangle r1, Rectangle r2 )
        {
			int intrsctWidth;
			int intrsctHeight;

			if (r2.Left + r2.Width < r1.Left + r1.Width)
				intrsctWidth = r2.Width;
			else
				intrsctWidth = r1.Width - (r2.Left - r1.Left);

			if (r2.Top + r2.Height < r1.Top + r1.Height)
				intrsctHeight = r2.Height - (r1.Top - r2.Top); 
			else
				intrsctHeight = r1.Height;

			return intrsctWidth * intrsctHeight;
        }

        private static int RectInsideOtherIntersectionSquare( Rectangle r1, Rectangle r2 )
        {
			int intrsctWidth = r2.Left + r2.Width - r1.Left;
			int intrsctHeight = r2.Top + r2.Height - r1.Top;

			if (r1.Left + r1.Width <= r2.Width + r2.Left)
				intrsctWidth = r1.Width;
			if (r1.Top + r1.Height <= r2.Top + r2.Height)
				intrsctHeight = r1.Height;

            return intrsctWidth * intrsctHeight;
        }

        public static int IndexOfInnerRectangle( Rectangle r1, Rectangle r2 )
		{
			if ( TopLeftCornerInsideTheOther(r1, r2) &&
				 r1.Top + r1.Height <= r2.Top + r2.Height &&
				 r1.Left + r1.Width <= r2.Left + r2.Width )
				return 0;
			if ( TopLeftCornerInsideTheOther(r2, r1) &&
				 r2.Top + r2.Height <= r1.Top + r1.Height &&
				 r2.Left + r2.Width <= r1.Left + r1.Width )
				return 1;			
			return -1;
		}
	}
}
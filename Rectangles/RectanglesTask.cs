namespace Rectangles
{
	public static class RectangleExtensions
    {
		public static int Right(this Rectangle r) 
        {
			return r.Left + r.Width;
        }
		public static int Bottom(this Rectangle r)
        {
			return r.Top + r.Height;
        }
    }

	public static class RectanglesTask
	{
        #region Intersection checks
		//It's important to note that since we are working on an image, (0,0) is in top-left corner and Y axis is pointing down.
		//That means that outer.Top <= inner.Top is true when outer.Top is higher than inner.Top on an image.
        private static bool IsTopLeftCornerInsideTheOther( Rectangle inner, Rectangle outer )
        {
			return outer.Top <= inner.Top && outer.Bottom >= inner.Top &&
				   outer.Left <= inner.Left && outer.Right >= inner.Left;
        }
        private static bool IsRectInsideTheOther( Rectangle inner, Rectangle outer )
        {
			return IsTopLeftCornerInsideTheOther(inner, outer)
				&& inner.Bottom <= outer.Bottom
				&& inner.Right <= outer.Right;
        }
        private static bool IsPlusTypeIntersection( Rectangle horizontal, Rectangle vertical )
        {
			return horizontal.Right >= vertical.Left &&
				   horizontal.Left <= vertical.Left &&
				   vertical.Bottom >= horizontal.Top &&
				   vertical.Top <= horizontal.Top;
        }
		public static bool AreIntersected( Rectangle r1, Rectangle r2 )
		{
			return IsTopLeftCornerInsideTheOther(r1, r2) ||
				   IsPlusTypeIntersection(r1, r2) ||
				   IsTopLeftCornerInsideTheOther(r2, r1) ||
				   IsPlusTypeIntersection(r2, r1);
		}
        #endregion

        #region Intersection square
        public static int IntersectionSquare( Rectangle r1, Rectangle r2 )
		{
			if (IsTopLeftCornerInsideTheOther(r1, r2))
				return RectInsideOtherIntersectionSquare(r1, r2);

			else if (IsTopLeftCornerInsideTheOther(r2, r1))
				return RectInsideOtherIntersectionSquare(r2, r1);

			else if (IsPlusTypeIntersection(r1, r2))
				return PlusTypeIntersectionSquare(r1, r2);

			else if (IsPlusTypeIntersection(r2, r1))
				return PlusTypeIntersectionSquare(r2, r1);

			else
				return 0;
		}
		private static int PlusTypeIntersectionSquare( Rectangle r1, Rectangle r2 )
        {
			int intrsctWidth;
			int intrsctHeight;

			if (r2.Right < r1.Right)
				intrsctWidth = r2.Width;
			else
				intrsctWidth = r1.Width - (r2.Left - r1.Left);
			
			if (r2.Bottom < r1.Bottom)
				intrsctHeight = r2.Height - (r1.Top - r2.Top); 
			else
				intrsctHeight = r1.Height;

			return intrsctWidth * intrsctHeight;
        }
        private static int RectInsideOtherIntersectionSquare( Rectangle inner, Rectangle outer )
        {
			int intrsctWidth = outer.Right - inner.Left;
			int intrsctHeight = outer.Bottom - inner.Top;

			if (inner.Right <= outer.Right)
				intrsctWidth = inner.Width;
			if (inner.Bottom <= outer.Bottom)
				intrsctHeight = inner.Height;

            return intrsctWidth * intrsctHeight;
        }
        #endregion

        #region Inner index
        public static int IndexOfInnerRectangle( Rectangle r1, Rectangle r2 )
		{
			if (IsRectInsideTheOther(r1, r2))
				return 0;
			else if (IsRectInsideTheOther(r2, r1))
				return 1;			
			else 
				return -1;
		}
        #endregion
    }
}
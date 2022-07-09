namespace Mazes
{
	public static class DiagonalMazeTask
	{
		private const int SizeDelta = 3;
        private static void MoveRobotByAmount( Robot robot, int stepCount, Direction direction )
        {
			for(int i = 0 ; i < stepCount ; i++)            
				robot.MoveTo(direction);            
        }

		private static void MoveDependingOnDimension
			( Robot robot, 
			  int largerDim, Direction largerDimDirection, 
			  int lesserDim, Direction lesserDimDirection )
        {
			int stepCount = ( largerDim - 1 ) / lesserDim;
			
			MoveRobotByAmount(robot, stepCount, largerDimDirection);
			while(!robot.Finished)
            {				
				MoveRobotByAmount(robot, 1, lesserDimDirection);
				MoveRobotByAmount(robot, stepCount, largerDimDirection);
            }
        }

        public static void MoveOut( Robot robot, int width, int height )
		{
			if (height >= width)
				MoveDependingOnDimension
					( robot, 
					  height - SizeDelta, Direction.Down, 
					  width - SizeDelta, Direction.Right );
			else
				MoveDependingOnDimension
					( robot, 
					  width - SizeDelta, Direction.Right, 
					  height - SizeDelta, Direction.Down );
		}
	}
}
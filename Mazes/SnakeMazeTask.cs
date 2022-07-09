namespace Mazes
{
	public static class SnakeMazeTask
	{
		private const int SizeError = 3;
		private const int VerticalCorridorLen = 2;
		private static void MoveRobotByAmount(Robot robot, int stepCount, Direction direction)
        {
			for(int i = 0 ; i < stepCount ; i++)            
				robot.MoveTo(direction);            
        }
		
		private static void MoveIntoStartPosition(Robot robot, int horizontalCorridorLen)
        {
			MoveRobotByAmount(robot, horizontalCorridorLen, Direction.Right);
			MoveRobotByAmount(robot, VerticalCorridorLen, Direction.Down);
			MoveRobotByAmount(robot, horizontalCorridorLen, Direction.Left);
        }

		private static void StartMoveLoop(Robot robot, int horizontalCorridorLen)
        {
			while(!robot.Finished)
            {
				MoveRobotByAmount(robot, VerticalCorridorLen, Direction.Down);
				MoveRobotByAmount(robot, horizontalCorridorLen, Direction.Right);
				MoveRobotByAmount(robot, VerticalCorridorLen, Direction.Down);
				MoveRobotByAmount(robot, horizontalCorridorLen, Direction.Left);
            }
        }

		public static void MoveOut(Robot robot, int width, int height)
		{
			MoveIntoStartPosition(robot, width - SizeError);
			StartMoveLoop(robot, width - SizeError);
		}
	}
}
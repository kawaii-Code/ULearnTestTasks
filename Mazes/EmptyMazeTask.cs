namespace Mazes
{
	public static class EmptyMazeTask
	{
		private const int SizeDelta = 3;
		private static void MoveRobotByAmount(Robot robot, int stepCount, Direction direction)
        {
			for(int i = 0 ; i < stepCount ; i++)            
				robot.MoveTo(direction);            
        }

		public static void MoveOut(Robot robot, int width, int height)
		{
			MoveRobotByAmount(robot, height - SizeDelta, Direction.Down);
			MoveRobotByAmount(robot, width - SizeDelta, Direction.Right);
		}
	}
}
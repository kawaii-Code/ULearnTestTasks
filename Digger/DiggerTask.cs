using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Digger
{
    public static class PointExtensions
    {
        public static bool OutOfBounds(this Point p) => p.X >= Game.MapWidth || p.Y >= Game.MapHeight || p.X < 0 || p.Y < 0;

        public static int XDifferenceWith(this Point p1, Point p2) => Math.Abs(p1.X - p2.X);
        public static int YDifferenceWith(this Point p1, Point p2) => Math.Abs(p1.Y - p2.Y);

        public static bool IsEmptyFixed(this Point p) => p.X < 0 || p.Y < 0;
        public static Point EmptyFixed() => new Point { X = -1, Y = -1 };
    }

    public class Living 
    {
        private readonly Type[] killers = new[] { typeof(Sack), typeof(Monster) };
        public virtual bool DeadInConflict(ICreature conflictedObject) => killers.Contains(conflictedObject.GetType());
    }
    
    public class Monster : Living, ICreature
    {
        private Point GetDiggerPosition()
        {
            for (int x = 0; x < Game.MapWidth; x++)
            for (int y = 0; y < Game.MapHeight; y++)
                {
                    if (Game.Map[x, y] is Player)
                        return new Point { X = x, Y = y };
                }
            return PointExtensions.EmptyFixed();
        }

        private readonly Type[] blockerTypes = new[] 
        { 
            typeof(Terrain), typeof(Sack), typeof(Monster) 
        };

        private bool IsPointFree(Point point)
        {
            if (point.OutOfBounds()) return false;

            foreach (Type type in blockerTypes)
                if (Game.Map[point.X, point.Y]?.GetType() == type) return false;

            return true;
        }

        private Point[] GetFreePointsAround(Point p)
        {
            Point[] validMoveLocations = new[]
            {
                new Point { X = p.X + 1, Y = p.Y },
                new Point { X = p.X - 1, Y = p.Y },
                new Point { X = p.X, Y = p.Y + 1},
                new Point { X = p.X, Y = p.Y - 1}
            };

            return validMoveLocations.Where(x => IsPointFree(x)).ToArray();
        }

        private Point GetPathToPlayer(Point monsterLocation, Point playerPosition)
        {
            Point[] freePoints = GetFreePointsAround(monsterLocation);
            if (freePoints.Length == 0) return PointExtensions.EmptyFixed();

            List<Point> bestPoints = freePoints.Where(nextPoint =>
                    nextPoint.XDifferenceWith(playerPosition) < monsterLocation.XDifferenceWith(playerPosition) ||
                    nextPoint.YDifferenceWith(playerPosition) < monsterLocation.YDifferenceWith(playerPosition)).ToList();

            return bestPoints.Count == 0 ? PointExtensions.EmptyFixed() : bestPoints[new Random().Next(0, bestPoints.Count)];
        }

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();

            Point diggerPosition = GetDiggerPosition();
            if (diggerPosition.IsEmptyFixed()) return result;

            Point next = GetPathToPlayer(new Point { X = x, Y = y }, diggerPosition);

            if (next.IsEmptyFixed()) return result;

            result.DeltaX = next.X - x;
            result.DeltaY = next.Y - y;

            return result;
        }

        public int GetDrawingPriority() => 2;

        public string GetImageFileName() => "Monster.png";
    }

    public class Player : Living, ICreature
    {
        private bool WillCollideWith(Type type, Point nextPlayerPosition)
        {
            return Game.Map[nextPlayerPosition.X, nextPlayerPosition.Y]?.GetType() == type;
        }

        private void CancelCommand(CreatureCommand command)
        {
            command.DeltaX = 0;
            command.DeltaY = 0;
            command.TransformTo = null;
        }

        private void ProcessInput(CreatureCommand command)
        {
            switch (Game.KeyPressed)
            {
                case (Keys.Up): command.DeltaY = -1; break;
                case (Keys.Down): command.DeltaY = 1; break;
                case (Keys.Right): command.DeltaX = 1; break;
                case (Keys.Left): command.DeltaX = -1; break;
            }
        }

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();
            
            ProcessInput(result);

            Point nextPosition = new Point { X = x + result.DeltaX, Y = y + result.DeltaY };

            if (nextPosition.OutOfBounds() || WillCollideWith(typeof(Sack), nextPosition))            
                CancelCommand(result);           

            return result;
        }

        public int GetDrawingPriority() => 3;

        public string GetImageFileName() => "Digger.png";
    }

    public class Sack : ICreature
    {
        private int cellsFallen = 0;

        private bool CanFall(Point sackFallPos)
        {
            return !sackFallPos.OutOfBounds() &&
                (Game.Map[sackFallPos.X, sackFallPos.Y] is null ||
                (Game.Map[sackFallPos.X, sackFallPos.Y] is Living && cellsFallen >= 1));
        }

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();

            if (CanFall(new Point { X = x, Y = y + 1 }))
            {
                result.DeltaY = 1;
                cellsFallen++;
            }
            else
            {
                if (cellsFallen > 1)
                    result.TransformTo = new Gold();
                cellsFallen = 0;
            }

            return result;
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Sack.png";
    }

    public class Inactive
    {
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public int GetDrawingPriority() => 5;
    }

    public class Gold : Inactive, ICreature
    {
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player) Game.Scores += 10;
            return conflictedObject is Living;
        }

        public string GetImageFileName() => "Gold.png";
    }

    public class Terrain : Inactive, ICreature
    {
        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Player;

        public string GetImageFileName() => "Terrain.png";
    }
}
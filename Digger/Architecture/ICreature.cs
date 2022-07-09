namespace Digger
{
    public interface ICreature
    {
        int GetDrawingPriority();
        CreatureCommand Act(int x, int y);
        bool DeadInConflict(ICreature conflictedObject);
        string GetImageFileName();
    }
}
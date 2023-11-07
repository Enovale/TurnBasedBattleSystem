namespace TurnBasedBattleSystem;

public abstract class Status
{
    public Unit Unit { get; set; }

    public Status(Unit owner)
    {
        Unit = owner;
    }
    
    public abstract void Remove(int stacks);
}
using TurnBasedBattleSystem;

namespace BattleTests;

public abstract class HackmonStatus : Status
{
    public int RemainingTurns { get; set; }
    
    public HackmonStatus(Unit owner, int turns) : base(owner)
    {
        RemainingTurns = turns;
    }

    public abstract override void Remove(int stacks);
}
using TurnBasedBattleSystem;

namespace BattleTests;

public class TestStatus : HackmonStatus
{
    
    public TestStatus(Unit owner, int turns) : base(owner, turns)
    {
    }

    public override void Remove(int stacks)
    {
        throw new NotImplementedException();
    }
}
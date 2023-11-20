using BattleTests.Units;
using TurnBasedBattleSystem;

namespace BattleTests;

public class TestStatus : HackmonStatus
{
    public TestStatus(Unit owner, int turns) : base(owner, turns)
    {
        BattleManager.OnTurnEnd += OnTurnEnd;
        Name = "Test Status";
    }

    [Status("TestStatus")]
    public static TestStatus Init(Unit owner, int turns)
    {
        return new TestStatus(owner, turns);
    }

    public override void Remove(int stacks)
    {
        RemainingTurns -= stacks;
        if (RemainingTurns <= 0)
        {
            BattleManager.OnTurnEnd -= OnTurnEnd;
        }
    }

    private void OnTurnEnd(object data)
    {
        Remove(1);
        Console.WriteLine($"Test Status on unit {((HackmonUnit)Unit).Name} ticked once, turns remaining: ${RemainingTurns}");
    }
}
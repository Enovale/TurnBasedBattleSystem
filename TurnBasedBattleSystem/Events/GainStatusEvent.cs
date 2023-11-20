namespace TurnBasedBattleSystem.Events;

public class GainStatusEvent : BattleEvent
{
    public Unit Unit { get; set; }
    public Status Status { get; set; }
    public int Stacks { get; set; }

    public GainStatusEvent(Unit u, Status s, int n)
    {
        Unit = u;
        Status = s;
        Stacks = n;
    }
}
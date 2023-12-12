namespace TurnBasedBattleSystem.Events;

public class GainStatusEvent : BattleEvent
{
    public IUnit Unit { get; set; }
    public IStatus Status { get; set; }
    public int Stacks { get; set; }

    public GainStatusEvent(IUnit u, IStatus s, int n)
    {
        Unit = u;
        Status = s;
        Stacks = n;
    }
}
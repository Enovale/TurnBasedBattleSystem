namespace TurnBasedBattleSystem.Events;

public class DeathEvent : BattleEvent
{
    public DeathEvent(IUnit deadUnit)
    {
        Unit = deadUnit;
    }
    public IUnit Unit { get; set; }
}
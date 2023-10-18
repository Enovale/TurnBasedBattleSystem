namespace HackmonBattleSystem.Events;

public class DeathEvent : BattleEvent
{
    public DeathEvent(Unit deadUnit)
    {
        Unit = deadUnit;
    }
    public Unit Unit { get; set; }
}
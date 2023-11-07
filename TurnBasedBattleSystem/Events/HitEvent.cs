namespace TurnBasedBattleSystem.Events;

public class HitEvent : BattleEvent
{
    public HitEvent(Unit atkr, Unit tgt, Attack atk, int dmg)
    {
        Attacker = atkr;
        Target = tgt;
        Attack = atk;
        Damage = dmg;
    }
    public Unit Attacker { get; set; }
    public Unit Target { get; set; }
    public Attack Attack { get; set; }
    public int Damage { get; set; }
}
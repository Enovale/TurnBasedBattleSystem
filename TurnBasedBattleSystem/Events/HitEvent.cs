namespace TurnBasedBattleSystem.Events;

public class HitEvent : BattleEvent
{
    public HitEvent(IUnit atkr, IUnit tgt, IAttack atk, int dmg)
    {
        Attacker = atkr;
        Target = tgt;
        Attack = atk;
        Damage = dmg;
    }
    public IUnit Attacker { get; set; }
    public IUnit Target { get; set; }
    public IAttack Attack { get; set; }
    public int Damage { get; set; }
}
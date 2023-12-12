namespace TurnBasedBattleSystem.Actions;

public class AttackAction : BattleAction
{
    public AttackAction(IUnit attacker, IUnit target, IAttack atk)
    {
        Attacker = attacker;
        Target = target;
        Attack = atk;
    }
    
    public IUnit Attacker { get; set; }  
    public IUnit Target { get; set; }
    public IAttack Attack { get; set; }
    public int Priority => Attacker.Speed;
}
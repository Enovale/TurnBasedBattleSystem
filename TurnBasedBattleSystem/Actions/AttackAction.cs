namespace HackmonBattleSystem.Actions;

public class AttackAction : BattleAction
{
    public AttackAction(Unit attacker, Unit target, Attack atk)
    {
        Attacker = attacker;
        Target = target;
        Attack = atk;
    }
    
    public Unit Attacker { get; set; }  
    public Unit Target { get; set; }
    public Attack Attack { get; set; }
    public int Priority => Attacker.Speed;
}
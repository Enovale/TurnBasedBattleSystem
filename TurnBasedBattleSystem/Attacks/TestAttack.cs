using System.Data.Common;
using TurnBasedBattleSystem.Events;

namespace TurnBasedBattleSystem.Attacks;

public class TestAttack : Attack
{
    public int Damage { get; set; } = 5;
    public IEnumerable<BattleEvent> Resolve(Unit attacker, Unit target)
    {
        attacker.Health -= Damage;

        HitEvent hit = new(attacker, target, this, Damage);
        yield return hit;
    }
}
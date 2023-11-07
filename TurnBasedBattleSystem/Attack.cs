using System.Collections;
using TurnBasedBattleSystem.Events;

namespace TurnBasedBattleSystem;

public interface Attack 
{
    public IEnumerable<BattleEvent> Resolve(Unit attacker, Unit target);
}
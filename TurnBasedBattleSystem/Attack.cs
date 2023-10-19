using System.Collections;
using HackmonBattleSystem.Events;

namespace HackmonBattleSystem;

public interface Attack 
{
    public int Damage { get; set; }
    public IEnumerable<BattleEvent> Resolve(Unit attacker, Unit target);
}
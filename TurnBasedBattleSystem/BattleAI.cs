using TurnBasedBattleSystem.Actions;

namespace TurnBasedBattleSystem;

public interface BattleAI
{
    public BattleAction DoAction(IUnit actor);
}
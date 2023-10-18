using HackmonBattleSystem.Actions;

namespace HackmonBattleSystem;

public interface BattleAI
{
    public BattleAction DoAction(Unit actor);
}
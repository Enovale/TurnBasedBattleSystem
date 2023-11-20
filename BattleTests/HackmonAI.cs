using BattleTests.Attacks;
using BattleTests.Units;
using TurnBasedBattleSystem;
using TurnBasedBattleSystem.Actions;

namespace BattleTests;

public class HackmonAI : BattleAI
{
    public BattleAction DoAction(Unit actor)
    {
        if (actor is HackmonUnit unit)
        {
            var moveId = unit.KnownMoveIDs[0];
            var move = HackmonManager.HackmonMoves[moveId];
            var target = BattleManager.PlayerTeam[0];

            var action = new AttackAction(unit, target, new HackmonAttack(move));

            return action;
        }

        throw new Exception("Used outside of intended context.");
    }
}
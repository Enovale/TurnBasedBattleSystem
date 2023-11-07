using System.Reflection.Metadata;
using BattleTests.Units;
using TurnBasedBattleSystem;
using TurnBasedBattleSystem.Events;

namespace BattleTests.Attacks;

public class HackmonAttack : Attack
{
    private HackmonMove dataBacking { get; set; }

    public HackmonAttack(HackmonMove atkData)
    {
        dataBacking = atkData;
    }
    
    public IEnumerable<BattleEvent> Resolve(Unit attacker, Unit target)
    {
        //calc damage
        if (attacker is not HackmonUnit || target is not HackmonUnit)
        {
            throw new Exception("Use in unsupported scenario.");
        }
    
        HackmonUnit moveUser = (HackmonUnit)attacker;
        HackmonUnit moveTarget = (HackmonUnit)target;
        

        if (dataBacking.Damage != 0)
        {
            var atk = 0;
            var def = 0;
            var stab = (moveUser.PrimaryType == dataBacking.MoveType) ? 1.25f : 1f;

            switch (dataBacking.AttackType)
            {
                case AttackType.None:
                    throw new Exception(
                        "If move does damage, AttackType must be one of either 'Physical' or 'Special'");
                    break;
                case AttackType.Physical:
                    atk = moveUser.Attack;
                    def = moveTarget.Defense;
                    break;
                case AttackType.Special:
                    atk = moveUser.SpAttack;
                    def = moveUser.SpDefense;
                    break;
            }

            int damage = (int)((dataBacking.Damage * atk + moveUser.Level) / def * stab);

            moveTarget.Health -= damage;
            HitEvent damageEvent = new HitEvent(moveUser, moveTarget, this, damage);
            yield return damageEvent;
        }

        if (dataBacking.TargetStatuses.Count > 0)
        {
            
        }

        if (dataBacking.UserStatuses.Count > 0)
        {
            
        }
    }
}
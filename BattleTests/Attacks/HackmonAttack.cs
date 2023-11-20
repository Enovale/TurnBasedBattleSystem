using System.Reflection.Metadata;
using BattleTests.Units;
using TurnBasedBattleSystem;
using TurnBasedBattleSystem.Events;

namespace BattleTests.Attacks;

public class HackmonAttack : Attack
{
    public HackmonMove AttackData { get; set; }

    public HackmonAttack(HackmonMove atkAttackData)
    {
        AttackData = atkAttackData;
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

        if (AttackData.Damage != 0)
        {
            var atk = 0;
            var def = 0;
            var stab = (moveUser.PrimaryType == AttackData.MoveType) ? 1.25f : 1f;

            switch (AttackData.AttackType)
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

            int damage = (int)((AttackData.Damage * atk + moveUser.Level) / def * stab);

            moveTarget.Health -= damage;
            HitEvent damageEvent = new HitEvent(moveUser, moveTarget, this, damage);
            yield return damageEvent;
        }

        if (AttackData.TargetStatuses.Count > 0)
        {
            foreach (string statusName in AttackData.TargetStatuses)
            {
                var status = HackmonManager.InstanceStatus(statusName, (HackmonUnit)target, 1);
                var sEvent = new GainStatusEvent(target, status, 1);

                yield return sEvent;
            }
        }

        if (AttackData.UserStatuses.Count > 0)
        {
            
        }
    }
}
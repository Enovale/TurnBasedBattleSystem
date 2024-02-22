using TurnBasedBattleSystem.Actions;
using TurnBasedBattleSystem.Events;

namespace TurnBasedBattleSystem;

public static class BattleManager
{
    public static List<IUnit> PlayerTeam { get; private set; } = new();
    public static List<IUnit> AITeam { get; private set; } = new();
    public static BattleAI EnemyAI = new TestAI();
    public static bool BattleInProgress = false;
    public static List<BattleAction> CurrentEnemyActions = new();

    public delegate void OnTurnStartListener(StartTurnEvent data);
    public static event OnTurnStartListener OnTurnStart = null!;

    public delegate void OnHitListener(HitEvent data);
    public static event OnHitListener OnHit = null!;

    public delegate void OnDeathListener(DeathEvent data);
    public static event OnDeathListener OnDeath = null!;

    public delegate void OnGainStatusListener(GainStatusEvent data);
    public static event OnGainStatusListener OnGainStatus = null!;

    public delegate void OnTurnEndListener(EndTurnEvent data);
    public static event OnTurnEndListener OnTurnEnd = null!;
    
    public static void StartBattle(List<IUnit> playerUnits, List<IUnit> enemyUnits)
    {
        BattleInProgress = true;
        PlayerTeam = playerUnits;
        AITeam = enemyUnits;
        
        OnTurnStart?.Invoke(new StartTurnEvent());

        foreach (IUnit enemy in AITeam)
        {
            CurrentEnemyActions.Add(EnemyAI.DoAction(enemy));
        }
    }

    public static void Cleanup()
    {
        BattleInProgress = false;
        OnTurnStart = null!;
        OnTurnEnd = null!;
        OnHit = null!;
        OnDeath = null!;
    }
    
    /*
     * Start Battle
     * Queue Enemy Actions
     * Init battle/turn specific
     * loop:wait for player input
     * resolve player input
     * resolve turn end
     * resolve turn begin
     * queue enemy actions
     * jmp loop
     *
     * Event registry 
     */

    public static void HandlePlayerInput(List<BattleAction> actions)
    {
        if (!BattleInProgress) throw new Exception();
        List<BattleAction> combinedActions = CurrentEnemyActions.Concat(actions).ToList();
        //TODO: tiebreaking for priority
        combinedActions.Sort(ActionSort);

        foreach (BattleAction action in combinedActions)
        {
            Resolve(action);
        }
        
        OnTurnEnd?.Invoke(new EndTurnEvent());
    }

    private static int ActionSort(BattleAction a, BattleAction b)
    {
        return a.Priority - b.Priority;
    }

    static void Resolve(BattleAction action)
    {
        if (action is AttackAction atk)
        {
            Resolve(atk.Attacker, atk.Target, atk.Attack);
        }
    }
    
    static void Resolve(IUnit attacker, IUnit target, IAttack attack)
    {
        /*
         *
         * So long story short
         *
         * each attack has a function resolve that returns an Enumerator<BattleEvent>
         *
         * loop through that, emit each event created.
         */

        foreach (BattleEvent e in attack.Resolve(attacker, target))
        {
            // Err: Need defined order of operations for certain handlers.
            // do we tho?    
            if (e is HitEvent h)
            {
                OnHit?.Invoke(h);
            }
            else if (e is DeathEvent d)
            {
                OnDeath?.Invoke(d);
            }
            else if (e is GainStatusEvent s)
            {
               OnGainStatus?.Invoke(s); 
            }
        }
        
        //death check
        if (target.Health <= 0)
        {
            //push death event.
            DeathEvent ded = new(target);
            OnDeath?.Invoke(ded);
        }
    }
}
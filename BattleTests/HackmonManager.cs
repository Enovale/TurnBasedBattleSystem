using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using BattleTests.Attacks;
using BattleTests.Units;
using TurnBasedBattleSystem;
using TurnBasedBattleSystem.Actions;
using TurnBasedBattleSystem.Events;

namespace BattleTests;

public class HackmonManager
{
    private delegate HackmonStatus statusInitializer(HackmonUnit unit, int numStacks);
    private static Dictionary<string, statusInitializer> statusMap = new();
    private static bool endBattle = false;

    //public static Dictionary<int, HackmonData> HackmonRegistry { get; set; } = new();
    public static List<HackmonData> HackmonList { get; set; } = new();
    public static Dictionary<int, HackmonMove> HackmonMoves { get; set; } = new();
    
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
    
    public static void LoadAllData()
    {
        var hackmon = LoadData<HackmonData>("Hackmon");
        HackmonList = hackmon;
        var moves = LoadData<HackmonMove>("Moves");
        foreach (var move in moves)
        {
            HackmonMoves.Add(move.ID, move);
        }
    }
    
    private static List<T> LoadData<T>(string dir)
    {
        var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{dir}");
        List<T> loadedData = new();
    
        foreach (var file in Directory.EnumerateFiles(dataPath))
        {
            var json = File.ReadAllText(file);
    
            try
            {
                var parsedItem = JsonSerializer.Deserialize<T>(json, _jsonOpts);
                if (parsedItem != null) loadedData.Add(parsedItem);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error when parsing {file}. Skipped.");
            }
        }
    
        return loadedData;
    }

    public static void LoadStaticStatuses(Assembly a)
    {
        foreach (var type in a.GetTypes())
        {
            /*
            if (type.IsSealed == false) continue;
            if (type.IsClass == false) continue;
            */
            
            foreach (var method in type.GetRuntimeMethods())
            {
                if(method.IsStatic == false) continue;

                var attr = method.GetCustomAttribute<StatusAttribute>();

                if (attr == null) continue;
                
                statusInitializer del;
                try
                {
                    del = (statusInitializer)method.CreateDelegate(typeof(statusInitializer));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load status {attr.Name}, incorrect method signature.");
                    continue;
                }
                
                statusMap[attr.Name] = del;
                Console.WriteLine($"Found and loaded status with name {attr.Name}");
            }
        }
    }

    public static HackmonStatus InstanceStatus(string statusName, HackmonUnit t, int numTurns)
    {
        return statusMap[statusName](t, numTurns);
    }

    public static void StartBattle(List<HackmonUnit> playerTeam, List<HackmonUnit> enemyTeam)
    {
        BattleManager.OnDeath += BattleEndCheck;
        BattleManager.OnHit += hitLogger;
        BattleManager.OnGainStatus += LogStatus;
        BattleManager.OnTurnStart += LogTurnBoundary;
        BattleManager.OnTurnEnd += LogTurnBoundary; 
        BattleManager.EnemyAI = new HackmonAI();
        BattleManager.StartBattle(playerTeam.Cast<Unit>().ToList(), enemyTeam.Cast<Unit>().ToList());
    }

    public static void HandleInput(List<BattleAction> a)
    {
        BattleManager.HandlePlayerInput(a);
    }

    private static void LogStatus(GainStatusEvent s)
    {
        var unit = (HackmonUnit)s.Unit;
        var status = (HackmonStatus)s.Status;
        
        Console.WriteLine($"{unit.Name} gained {s.Stacks} stacks of {status.Name}");
    }

    private static void LogTurnBoundary(BattleEvent b)
    {
        if (b is StartTurnEvent)
        {
            Console.WriteLine("Start of turn");
        }
        else if (b is EndTurnEvent)
        {
            Console.WriteLine("End of turn");
            if (endBattle)
            {
                endBattle = false;
                BattleManager.Cleanup();
            }
        }
    }
    
    private static void hitLogger(HitEvent e)
    {
        HackmonUnit attacker = (HackmonUnit)e.Attacker;
        HackmonUnit target = (HackmonUnit)e.Target;
        HackmonAttack atk = (HackmonAttack)e.Attack;
        
        Console.WriteLine($"{attacker.Name} uses {atk.AttackData.Name} on {target.Name}\nDamage: {e.Damage}. {target.Name} HP Remaining: {target.Health}");
    }
    
    private static void BattleEndCheck(DeathEvent data)
    {
        // log death
        HackmonUnit deadUnit = (HackmonUnit)data.Unit;
        Console.WriteLine($"{deadUnit.Name} has fainted!");
        
        bool playerAlive = false;
        foreach (HackmonUnit u in BattleManager.PlayerTeam)
        {
            if (u.Health > 0)
            {
                playerAlive = true;
                break;
            }
        }

        if (!playerAlive)
        {
            Console.WriteLine("Battle ends in player loss.");
            endBattle = true;
            return;
        }

        bool enemyAlive = false;
        foreach (HackmonUnit u in BattleManager.AITeam)
        {
            if (u.Health > 0)
            {
                enemyAlive = true;
                break;
            }
        }

        if (!enemyAlive)
        {
            Console.WriteLine("Battle ends in player victory.");
            endBattle = true;
        }
    }
}
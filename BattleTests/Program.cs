using System.Reflection;
using BattleTests.Attacks;
using BattleTests.Units;
using TurnBasedBattleSystem;
using TurnBasedBattleSystem.Actions;

namespace BattleTests;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        
        HackmonManager.LoadStaticStatuses(Assembly.GetExecutingAssembly());
        HackmonManager.LoadAllData();

        var playerMon = new HackmonUnit(HackmonManager.HackmonList[0], 1);
        var enemyMon = new HackmonUnit(HackmonManager.HackmonList[1], 1);

        var playerTeam = new List<HackmonUnit>() {playerMon};
        var enemyTeam = new List<HackmonUnit>() {enemyMon};
        
        HackmonManager.StartBattle(playerTeam, enemyTeam);

        var playerAction = new AttackAction(
            playerMon,
            enemyMon,
            new HackmonAttack(HackmonManager.HackmonMoves[playerMon.KnownMoveIDs[0]])
        );
        
        HackmonManager.HandleInput(new() {playerAction});
    }
}
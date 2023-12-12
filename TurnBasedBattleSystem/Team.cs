namespace TurnBasedBattleSystem;

public class Team
{
    public List<IUnit> Members { get; set; } = new();
    public bool hasAI { get; set; }
}
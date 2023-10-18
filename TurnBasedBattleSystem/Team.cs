namespace HackmonBattleSystem;

public class Team
{
    public List<Unit> Members { get; set; } = new();
    public bool hasAI { get; set; }
}
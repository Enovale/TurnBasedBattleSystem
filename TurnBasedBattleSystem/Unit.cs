namespace TurnBasedBattleSystem;

public interface Unit
{
    public int Health { get; set; }
    public int Speed { get; set; }

    public List<Status> Statuses { get; set; }
}
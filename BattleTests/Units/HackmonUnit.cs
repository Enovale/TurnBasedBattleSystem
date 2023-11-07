using TurnBasedBattleSystem;

namespace BattleTests.Units;

public class HackmonUnit : Unit
{
    private HackmonData DataBacking { get; set; }
    
    public int Level { get; private set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Attack => DataBacking.Attack.Value;
    public int Defense => DataBacking.Defense.Value;
    public int SpAttack => DataBacking.SpAttack.Value;
    public int SpDefense => DataBacking.SpDefense.Value;
    public HackmonType PrimaryType => DataBacking.PrimaryType;
    
    public List<Status> Statuses { get; set; } = new();

    public HackmonUnit(HackmonData unitData, int unitLevel)
    {
        DataBacking = unitData;
        Level = unitLevel;

        DataBacking.InitValuesForLevel(unitLevel);
        Health = DataBacking.MaxHp.Value;
    }
}
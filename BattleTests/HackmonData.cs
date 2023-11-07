using System.Text.Json;
using System.Text.Json.Serialization;

namespace BattleTests;

public class HackmonData
{
    public HackmonType PrimaryType { get; set; }
    public HackmonType? SecondaryType { get; set; }
    public HackmonFamily Family { get; set; }
    public Stat MaxHp { get; set; }
    public Stat Attack { get; set; }
    public Stat SpAttack { get; set; }
    public Stat Defense { get; set; }
    public Stat SpDefense { get; set; }
    public List<int> LearnableMoves { get; set; }

    public void InitValuesForLevel(int lvl)
    {
        Attack.Value = Attack.BaseValueForLevel(lvl);
        SpAttack.Value = SpAttack.BaseValueForLevel(lvl);
        Defense.Value = Defense.BaseValueForLevel(lvl);
        SpDefense.Value = SpDefense.BaseValueForLevel(lvl);
        MaxHp.Value = MaxHp.BaseValueForLevel(lvl);
    }
}
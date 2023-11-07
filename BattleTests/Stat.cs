using System.Text.Json.Serialization;

namespace BattleTests;

public class Stat
{
   [JsonIgnore]
   public int Value { get; set; }
   
   public float GrowthPerLevel { get; set; }
   public int BaseValue { get; set; }
   public int BaseValueForLevel(int level) => BaseValue + (int)(GrowthPerLevel * level);
}
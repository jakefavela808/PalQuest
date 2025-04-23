namespace AdventureS25;

public class Pal
{
    public int HP { get; private set; }
    public int ATK { get; private set; }
    public int DEF { get; private set; }
    public List<string> Moves { get; private set; }
    public string AsciiArt { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsAcquirable { get; private set; }
    public bool HasBeenAcquired { get; private set; }

    public Pal(string name, string description, string initialDescription, bool isAcquirable = true, string asciiArt = "", int hp = 30, int atk = 10, int def = 8, List<string> moves = null)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsAcquirable = isAcquirable;
        AsciiArt = asciiArt;
        HP = hp;
        ATK = atk;
        DEF = def;
        Moves = moves ?? new List<string> { "Tackle", "Growl" };
    }

    // Returns stats string for battle: HP current/max, ATK, DEF
    public string GetBattleStatsString(int currentHP)
    {
        return $"HP {currentHP}/{HP}, ATK {ATK}, DEF {DEF}";
    }

    public string GetMovesString()
    {
        return string.Join(", ", Moves);
    }

    public void Acquire()
    {
        HasBeenAcquired = true;
    }

    public string GetWorldDescription()
    {
        if (HasBeenAcquired)
        {
            return $"You have acquired {Name}.";
        }
        return Description;
    }
}

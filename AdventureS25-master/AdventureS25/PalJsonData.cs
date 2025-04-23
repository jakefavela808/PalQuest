namespace AdventureS25;

public class PalJsonData
{
    public int HP { get; set; } = 30;
    public int ATK { get; set; } = 10;
    public int DEF { get; set; } = 8;
    public List<string> Moves { get; set; } = new List<string> { "Tackle", "Growl" };

    public string Name { get; set; }
    public string Description { get; set; }
    public string InitialDescription { get; set; }
    public bool IsAcquirable { get; set; }
    public string Location { get; set; }
    public string AsciiArt { get; set; }
}

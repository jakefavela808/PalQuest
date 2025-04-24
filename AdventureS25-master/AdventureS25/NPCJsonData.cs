namespace AdventureS25;

public class NPCJsonData
{
    public string Dialogue { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string InitialDescription { get; set; }
    public bool IsInteractable { get; set; }
    public string Location { get; set; }
    public string AsciiArt { get; set; }
    public List<string> Quests { get; set; }
    public bool IsTrainer { get; set; } = false;
    public List<string> Pals { get; set; } = new List<string>();
}

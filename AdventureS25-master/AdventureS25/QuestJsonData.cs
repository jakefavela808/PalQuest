namespace AdventureS25;

public class QuestJsonData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string InitialDescription { get; set; }
    public bool IsRepeatable { get; set; }
    public string Reward { get; set; }
    public List<string> Objectives { get; set; }
    public string Location { get; set; }
}

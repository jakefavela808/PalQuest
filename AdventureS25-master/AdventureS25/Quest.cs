namespace AdventureS25;

public class Quest
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsRepeatable { get; private set; }
    public string Reward { get; private set; }
    public List<string> Objectives { get; private set; }
    public string Location { get; private set; }

    public Quest(string name, string description, string initialDescription, bool isRepeatable, string reward, List<string> objectives, string location)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsRepeatable = isRepeatable;
        Reward = reward;
        Objectives = objectives;
        Location = location;
    }
}

namespace AdventureS25;

public class NPC
{
    public string AsciiArt { get; private set; }
    public string Dialogue { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsInteractable { get; private set; }
    public bool HasBeenInteracted { get; private set; }
    public List<Quest> Quests { get; set; } = new List<Quest>();

    public NPC(string name, string description, string initialDescription, bool isInteractable = true, string dialogue = "", string asciiArt = "", List<Quest> quests = null)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsInteractable = isInteractable;
        Dialogue = dialogue;
        AsciiArt = asciiArt;
        if (quests != null)
            Quests = quests;
    }

    public void OfferQuests()
    {
        foreach (var quest in Quests)
        {
            Player.OfferQuest(quest);
        }
    }

    public void Interact()
    {
        HasBeenInteracted = true;
    }

    public string GetWorldDescription()
    {
        if (HasBeenInteracted)
        {
            return $"You have interacted with {Name}.";
        }
        return InitialDescription;
    }
}

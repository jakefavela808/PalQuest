namespace AdventureS25;

public class NPC
{
    public bool IsDefeated => IsTrainer && Player.DefeatedTrainers.Contains(Name);
    public string AsciiArt { get; private set; }
    public string Dialogue { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsInteractable { get; private set; }
    public bool HasBeenInteracted { get; private set; }
    public List<Quest> Quests { get; set; } = new List<Quest>();
    public bool IsTrainer { get; private set; } = false;
    public List<Pal> Pals { get; private set; } = new List<Pal>();

    public NPC(string name, string description, string initialDescription, bool isInteractable = true, string dialogue = "", string asciiArt = "", List<Quest> quests = null, bool isTrainer = false, List<Pal> pals = null)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsInteractable = isInteractable;
        Dialogue = dialogue;
        AsciiArt = asciiArt;
        IsTrainer = isTrainer;
        if (quests != null)
            Quests = quests;
        if (pals != null)
            Pals = pals;
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
        if (IsTrainer)
        {
            return $"Trainer {Name} is here! {InitialDescription}";
        }
        return InitialDescription;
    }
}

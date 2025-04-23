namespace AdventureS25;

public class NPC
{
    public string Dialogue { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsInteractable { get; private set; }
    public bool HasBeenInteracted { get; private set; }

    public NPC(string name, string description, string initialDescription, bool isInteractable = true, string dialogue = "")
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsInteractable = isInteractable;
        Dialogue = dialogue;
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

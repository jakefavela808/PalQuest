namespace AdventureS25;

public class Pal
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsAcquirable { get; private set; }
    public bool HasBeenAcquired { get; private set; }

    public Pal(string name, string description, string initialDescription, bool isAcquirable = true)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsAcquirable = isAcquirable;
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
        return InitialDescription;
    }
}

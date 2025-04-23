namespace AdventureS25;

public class Pal
{
    public string AsciiArt { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string InitialDescription { get; private set; }
    public bool IsAcquirable { get; private set; }
    public bool HasBeenAcquired { get; private set; }

    public Pal(string name, string description, string initialDescription, bool isAcquirable = true, string asciiArt = "")
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsAcquirable = isAcquirable;
        AsciiArt = asciiArt;
    }

    public void Acquire()
    {
        HasBeenAcquired = true;
    }

    public string GetWorldDescription()
    {
        string art = string.IsNullOrEmpty(AsciiArt) ? "" : $"\n{AsciiArt}\n";
        if (HasBeenAcquired)
        {
            return $"{art}You have acquired {Name}.";
        }
        return art + InitialDescription;
    }
}

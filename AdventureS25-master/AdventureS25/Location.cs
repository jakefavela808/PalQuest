namespace AdventureS25;

public class Location
{
    // Holds a quest that was just accepted in this location, to trigger display after exits
    public Quest NewQuestAccepted = null; // Set by Player.ReceiveQuest
    private string name;
    public string Description;
    public string AsciiArt = null; // Optional ASCII art for this location
    
    public Dictionary<string, Location> Connections;
    public List<Item> Items = new List<Item>();
    public List<Pal> Pals = new List<Pal>();
    public List<NPC> NPCs = new List<NPC>();
    
    public Location(string nameInput, string descriptionInput, string asciiArt = null)
    {
        name = nameInput;
        Description = descriptionInput;
        Connections = new Dictionary<string, Location>();
        AsciiArt = asciiArt;
    }

    public void AddConnection(string direction, Location location)
    {
        Connections.Add(direction, location);
    }

    public bool CanMoveInDirection(Command command)
    {
        if (Connections.ContainsKey(command.Noun))
        {
            return true;
        }
        return false;
    }

    public Location GetLocationInDirection(Command command)
    {
        return Connections[command.Noun];
    }

    private string GetAsciiArtResolved()
    {
        if (!string.IsNullOrWhiteSpace(AsciiArt) && AsciiArt.StartsWith("AsciiArt."))
        {
            var artName = AsciiArt.Substring("AsciiArt.".Length);
            var artType = typeof(AsciiArt);
            var artProp = artType.GetProperty(artName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
            if (artProp != null)
            {
                return artProp.GetValue(null)?.ToString() ?? "";
            }
            var artField = artType.GetField(artName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
            if (artField != null)
            {
                return artField.GetValue(null)?.ToString() ?? "";
            }
        }
        return AsciiArt ?? "";
    }

    public string GetDescription()
    {
        string fullDescription = LocationNameFormatter.Decorate(name);
        string asciiArtToDisplay = GetAsciiArtResolved();
        if (!string.IsNullOrWhiteSpace(asciiArtToDisplay))
        {
            fullDescription += "\n" + asciiArtToDisplay;
        }
        fullDescription += "\n" + Description + "\n";

        foreach (Item item in Items)
        {
            fullDescription += "\n" + item.GetLocationDescription() + "\n";
        }
        // Only show wild/unacquired Pals
        foreach (Pal pal in Pals)
        {
            if (!Player.CaughtPals.Any(c => c.Name == pal.Name))
            {
                fullDescription += "\n" + pal.GetWorldDescription() + "\n";
            }
        }
        foreach (NPC npc in NPCs)
        {
            fullDescription += "\n" + npc.GetWorldDescription() + "\n";
        }
        // Append exits/directions in single line style, no label, no extra newline
        if (Connections != null && Connections.Count > 0)
        {
            List<string> exitParts = new List<string>();
            foreach (var kvp in Connections)
            {
                string dir = char.ToUpper(kvp.Key[0]) + kvp.Key.Substring(1).ToLower();
                string locName = kvp.Value != null ? kvp.Value.name : "Unknown";
                exitParts.Add($"{dir} ({locName})");
            }
            fullDescription += "\n| " + string.Join(" | ", exitParts) + " |";
        }
        // Show quest acceptance message if a new quest was just accepted here
        if (NewQuestAccepted != null)
        {
            fullDescription += $"\n\n=== New Quest Accepted! ===\nQuest: {NewQuestAccepted.Name}\n{NewQuestAccepted.Description}";
            NewQuestAccepted = null; // Only show once
        }
        return fullDescription;
    }

    public void AddItem(Item item)
    {
        Debugger.Write("Adding item "+ item.Name + "to " + name);
        Items.Add(item);
    }

    public void AddPal(Pal pal)
    {
        Pals.Add(pal);
    }

    public void AddNPC(NPC npc)
    {
        NPCs.Add(npc);
    }

    public bool HasItem(Item itemLookingFor)
    {
        foreach (Item item in Items)
        {
            if (item.Name == itemLookingFor.Name)
            {
                return true;
            }
        }
        
        return false;
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void RemovePal(Pal pal)
    {
        Pals.Remove(pal);
    }

    public void RemoveNPC(NPC npc)
    {
        NPCs.Remove(npc);
    }

    public void RemoveConnection(string direction)
    {
        if (Connections.ContainsKey(direction))
        {
            Connections.Remove(direction);
        }
    }
}
namespace AdventureS25;

public class Location
{
    private string name;
    public string Description;
    
    public Dictionary<string, Location> Connections;
    public List<Item> Items = new List<Item>();
    public List<Pal> Pals = new List<Pal>();
    public List<NPC> NPCs = new List<NPC>();
    
    public Location(string nameInput, string descriptionInput)
    {
        name = nameInput;
        Description = descriptionInput;
        Connections = new Dictionary<string, Location>();
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

    public string GetDescription()
    {
        string fullDescription = name + "\n" + Description;

        foreach (Item item in Items)
        {
            fullDescription += "\n" + item.GetLocationDescription();
        }
        foreach (Pal pal in Pals)
        {
            fullDescription += "\n" + pal.GetWorldDescription();
        }
        foreach (NPC npc in NPCs)
        {
            fullDescription += "\n" + npc.GetWorldDescription();
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
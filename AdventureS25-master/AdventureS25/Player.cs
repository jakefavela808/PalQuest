namespace AdventureS25;

public static class Player
{
    public static Location CurrentLocation;
    public static List<Item> Inventory;

    public static void Initialize()
    {
        Inventory = new List<Item>();
        CurrentLocation = Map.StartLocation;
    }

    public static void Move(Command command)
    {
        // Check if player is trying to leave Home without reading Jon's note
        if (CurrentLocation.Name == "Home" && !Conditions.IsTrue(ConditionTypes.ReadJonNote))
        {
            TextDisplay.TypeLine("You feel like you should check if there's anything important on the note before you leave.");
            return;
        }
        
        if (CurrentLocation.CanMoveInDirection(command))
        {
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            TextDisplay.TypeLine("You can't move " + command.Noun + ".");
        }
    }

    public static string GetLocationDescription()
    {
        return CurrentLocation.GetDescription();
    }

    public static void Take(Command command)
    {
        // figure out which item to take: turn the noun into an item
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            TextDisplay.TypeLine("I don't know what " + command.Noun + " is.");
        }
        else if (!CurrentLocation.HasItem(item))
        {
            TextDisplay.TypeLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            TextDisplay.TypeLine("The " + command.Noun + " can't be taked.");
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            TextDisplay.TypeLine("You take the " + command.Noun + ".");
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            TextDisplay.TypeLine("You are empty-handed.");
        }
        else
        {
            TextDisplay.TypeLine("You are carrying:");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                TextDisplay.TypeLine(" " + article + " " + item.Name);
            }
        }
    }

    public static void Look()
    {
        Console.WriteLine(CurrentLocation.GetDescription());
    }

    public static void Drop(Command command)
    {       
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            string article = SemanticTools.CreateArticle(command.Noun);
            TextDisplay.TypeLine("I don't know what " + article + " " + command.Noun + " is.");
        }
        else if (!Inventory.Contains(item))
        {
            TextDisplay.TypeLine("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            TextDisplay.TypeLine("You drop the " + command.Noun + ".");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            TextDisplay.TypeLine("Drinking...");
            Conditions.ChangeCondition(ConditionTypes.IsDrunk, true);
        }
        else
        {
            TextDisplay.TypeLine("Can't drink that.");
        }
    }
    
    public static void Talk(Command command)
    {
        string npcName = command.Noun;
        
        // If no NPC name provided, check for any NPCs in the current location
        if (string.IsNullOrEmpty(npcName))
        {
            if (CurrentLocation.NPCs.Count == 0)
            {
                TextDisplay.TypeLine("There is no one here to talk to.");
                return;
            }
            else if (CurrentLocation.NPCs.Count == 1)
            {
                // If there's only one NPC, automatically talk to them
                NPC npc = CurrentLocation.NPCs[0];
                StartConversationWith(npc);
                return;
            }
            else
            {
                // If multiple NPCs, list them
                TextDisplay.TypeLine("Who would you like to talk to? Available NPCs:");
                foreach (NPC npc in CurrentLocation.NPCs)
                {
                    TextDisplay.TypeLine("- " + npc.Name);
                }
                return;
            }
        }
        
        // Try to find the NPC in the current location
        NPC? targetNPC = null;
        foreach (NPC npc in CurrentLocation.NPCs)
        {
            if (npc.Name.ToLower().Contains(npcName.ToLower()))
            {
                targetNPC = npc;
                break;
            }
        }
        
        if (targetNPC == null)
        {
            TextDisplay.TypeLine("There is no one by that name here.");
            return;
        }
        
        StartConversationWith(targetNPC);
    }
    
    private static void StartConversationWith(NPC npc)
    {
        TextDisplay.TypeLine("You approach " + npc.Name + ".");
        States.ChangeState(StateTypes.Talking);
        ConversationCommandHandler.StartConversation(npc);
    }

    public static void AddItemToInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);

        if (item == null)
        {
            return;
        }
        
        Inventory.Add(item);
    }

    public static void RemoveItemFromInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);
        if (item == null)
        {
            return;
        }
        Inventory.Remove(item);
    }
    
    public static bool HasItemInInventory(Item item)
    {
        return Inventory.Contains(item);
    }

    public static void Use(Command command)
    {
        string itemName = command.Noun;
        Item item = Items.GetItemByName(itemName);
        
        // Check if the item exists
        if (item == null)
        {
            TextDisplay.TypeLine("I don't see a " + itemName + ".");
            return;
        }
        
        // Check if the item is in the player's inventory
        if (!HasItemInInventory(item))
        {
            TextDisplay.TypeLine("You don't have a " + itemName + " in your inventory.");
            return;
        }
        
        // Handle different items
        if (itemName == "note")
        {
            TextDisplay.TypeLine("You read the note from Professor Jon. It says:");
            TextDisplay.TypeLine("Dear Adventurer,\n\nI hope this note finds you well. I've heard you're interested in becoming a Pal Tamer, and I want to help you get started. I've been studying a remarkable new Pal specimen that I believe would be perfect for a beginner.\n\nPlease visit me at the Fusion Lab as soon as possible.\n\nRegards,\nProfessor Jon");
            
            // Set the ReadJonNote condition
            Conditions.ChangeCondition(ConditionTypes.ReadJonNote, true);
            
            // Remove the note from inventory after reading it
            RemoveItemFromInventory(itemName);
            TextDisplay.TypeLine("The note crumbles to dust after you read it.");
        }
        else
        {
            TextDisplay.TypeLine("You can't figure out how to use the " + itemName + ".");
        }
    }

    public static void MoveToLocation(string locationName)
    {
        // look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        // if there's no location with that name
        if (newLocation == null)
        {
            TextDisplay.TypeLine("Trying to move to unknown location: " + locationName + ".");
            return;
        }
            
        // set our current location to the new location
        CurrentLocation = newLocation;
        
        // print out a description of the location
        Look();
    }
}
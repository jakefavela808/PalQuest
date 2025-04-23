namespace AdventureS25;

public static class Player
{
    // ...
    public static void ShowCaughtPals()
    {
        if (CaughtPals.Count == 0)
        {
            Console.WriteLine("You haven't caught any Pals yet.");
            return;
        }
        Console.WriteLine("Your caught Pals:");
        foreach (var pal in CaughtPals)
        {
            Console.WriteLine($"\n=== {pal.Name} ===");
            if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                Console.WriteLine(pal.AsciiArt);
            Console.WriteLine($"Description: {pal.Description}");
            Console.WriteLine($"Stats: HP 30, ATK 10, DEF 8 (example)");
            Console.WriteLine($"Moves: Tackle, Growl (example)");
        }
    }
    public static List<Pal> CaughtPals = new List<Pal>();
    public static Location CurrentLocation;
    public static List<Item> Inventory;

    public static void Initialize()
    {
        Inventory = new List<Item>();
        CurrentLocation = Map.StartLocation;
    }

    public static void Move(Command command)
    {
        if (CurrentLocation.CanMoveInDirection(command))
        {
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            Console.WriteLine("You can't move " + command.Noun + ".");
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
            Console.WriteLine("I don't know what " + command.Noun + " is.");
        }
        else if (!CurrentLocation.HasItem(item))
        {
            Console.WriteLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            Console.WriteLine("The " + command.Noun + " can't be taked.");
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            Console.WriteLine("You take the " + command.Noun + ".");
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("You are empty-handed.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Console.WriteLine(" " + article + " " + item.Name);
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
            Console.WriteLine("I don't know what " + article + " " + command.Noun + " is.");
        }
        else if (!Inventory.Contains(item))
        {
            Console.WriteLine("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            Console.WriteLine("You drop the " + command.Noun + ".");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            Console.WriteLine("** drinking beer");
            Conditions.ChangeCondition(ConditionTypes.IsDrunk, true);
            RemoveItemFromInventory("beer");
            AddItemToInventory("beer-bottle");
        }
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

    public static void Talk(Command command)
    {
        if (CurrentLocation.NPCs != null && CurrentLocation.NPCs.Count > 0)
        {
            NPC npc = CurrentLocation.NPCs[0];
            npc.Interact();
            Console.WriteLine($"You talk to {npc.Name}.");
            Console.WriteLine(npc.Description);
            if (!string.IsNullOrWhiteSpace(npc.Dialogue))
            {
                Console.WriteLine(npc.Dialogue);
            }
            States.ChangeState(StateTypes.Talking);
        }
        else
        {
            Console.WriteLine("There is no one here to talk to.");
        }
    }

    public static void Battle(Command command)
    {
        if (CurrentLocation.Pals != null && CurrentLocation.Pals.Count > 0)
        {
            Pal pal = CurrentLocation.Pals[0];
            Console.WriteLine($"A wild {pal.Name} appears! You are pulled into battle.");
            if (!string.IsNullOrWhiteSpace(pal.Description))
            {
                Console.WriteLine(pal.Description);
            }
            States.ChangeState(StateTypes.Fighting);

            // Start the battle
            var battle = new Battle(pal);
            battle.StartBattle();
            // Set the current battle context for the handler
            CombatCommandHandler.CurrentBattle = battle;
            while (battle.State != BattleState.Won && battle.State != BattleState.Lost)
            {
                if (battle.State == BattleState.PlayerTurn)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine()?.Trim().ToLower();
                    Command combatCommand = new Command();
                    combatCommand.Verb = input;
                    if (CombatCommandValidator.IsValid(combatCommand))
                    {
                        CombatCommandHandler.Handle(combatCommand);
                        // Tame can win instantly, so check for win
                        if (battle.State == BattleState.Won)
                        {
                            Console.WriteLine($"You tamed {pal.Name}! {pal.Name} is now your Pal.");
                            CurrentLocation.Pals.Remove(pal);
                            States.ChangeState(StateTypes.Exploring);
                            return;
                        }
                        // Run can escape, so check for exploring state
                        if (States.CurrentStateType == StateTypes.Exploring)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Valid commands are: basic, special, defend, potion, tame, run.");
                    }
                }
                else if (battle.State == BattleState.PalTurn)
                {
                    battle.PalAttack();
                }
            }
            if (battle.State == BattleState.Won)
            {
                Console.WriteLine($"You caught {pal.Name}! {pal.Name} is now your Pal.");
                if (!CaughtPals.Contains(pal))
                {
                    CaughtPals.Add(pal);
                    if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                    {
                        Console.WriteLine($"\n{pal.AsciiArt}\n");
                    }
                    Console.WriteLine(pal.Description);
                }
                CurrentLocation.Pals.Remove(pal);
            }
            States.ChangeState(StateTypes.Exploring);
        }
        else
        {
            Console.WriteLine("There are no Pals here to battle!");
        }
    }

    public static void MoveToLocation(string locationName)
    {
        // look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        // if there's no location with that name
        if (newLocation == null)
        {
            Console.WriteLine("Trying to move to unknown location: " + locationName + ".");
            return;
        }
            
        // set our current location to the new location
        CurrentLocation = newLocation;
        
        // print out a description of the location
        Look();
    }
}
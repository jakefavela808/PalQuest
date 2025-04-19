namespace AdventureS25;

public static class ExplorationCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"eat", Eat},
            {"go", Move},
            {"tron", Tron},
            {"troff", Troff},
            {"take", Take},
            {"inventory", ShowInventory},
            {"look", Look},
            {"drop", Drop},
            {"nouns", Nouns},
            {"verbs", Verbs},
            {"fight", FightWildPal},
            {"tame", TameWildPal},
            {"explore", ChangeToExploreState},
            {"talk", Talk},
            {"drink", Drink},
            {"use", Use},
            {"pet", Pet},
            {"pals", ShowPals},
            {"heal", HealPals},
            {"beerme", SpawnBeerInInventory},
            {"unbeerme", UnSpawnBeerInInventory},
            {"puke", Puke},
            {"tidyup", TidyUp},
            {"teleport", Teleport},
            {"connect", Connect},
            {"disconnect", Disconnect},
            {"help", ShowHelp}
        };

    private static void Disconnect(Command obj)
    {
        Conditions.ChangeCondition(ConditionTypes.IsRemovedConnection, true);
    }

    private static void Connect(Command obj)
    {
        Conditions.ChangeCondition(ConditionTypes.IsCreatedConnection, true);
    }

    private static void Teleport(Command obj)
    {
        Conditions.ChangeCondition(ConditionTypes.IsTeleported, true);
    }   

    private static void TidyUp(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsTidiedUp, true);
    }

    private static void Puke(Command obj)
    {
        Conditions.ChangeCondition(ConditionTypes.IsHungover, true);
    }

    private static void UnSpawnBeerInInventory(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsBeerMed, false);

    }

    private static void SpawnBeerInInventory(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsBeerMed, true);
    }

    private static void Drink(Command command)
    {
        Player.Drink(command);
    }

    private static void Talk(Command command)
    {
        Player.Talk(command);
    }
    
    // Fight with a wild Pal
    private static void FightWildPal(Command command)
    {
        // Check if there's a wild Pal in the current location
        string currentLocationName = Player.CurrentLocation.Name;
        bool success = WildPalManager.FightWildPalInLocation(currentLocationName);
        
        if (!success)
        {
            TextDisplay.TypeLine("There are no wild Pals here to fight with.");
        }
    }
    
    // Attempt to tame a wild Pal
    private static void TameWildPal(Command command)
    {
        // Check if there's a wild Pal in the current location
        string currentLocationName = Player.CurrentLocation.Name;
        bool success = WildPalManager.TameWildPalInLocation(currentLocationName);
        
        if (!success)
        {
            TextDisplay.TypeLine("Make sure you have Pal treats in your inventory. They can be obtained from Professor Jon.");
        }
    }
    
    private static void ChangeToFightState(Command obj)
    {
        States.ChangeState(StateTypes.Fighting);
    }
    
    private static void ChangeToExploreState(Command obj)
    {
        States.ChangeState(StateTypes.Exploring);
    }

    private static void Verbs(Command command)
    {
        List<string> verbs = ExplorationCommandValidator.GetVerbs();
        foreach (string verb in verbs)
        {
            TextDisplay.TypeLine(verb);
        }
    }

    private static void Nouns(Command command)
    {
        List<string> nouns = ExplorationCommandValidator.GetNouns();
        foreach (string noun in nouns)
        {
            TextDisplay.TypeLine(noun);
        }
    }

    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> method = commandMap[command.Verb];
            method.Invoke(command);
        }
        else
        {
            TextDisplay.TypeLine("I don't know how to do that.");
        }
    }
    
    private static void Drop(Command command)
    {
        Player.Drop(command);
    }
    
    private static void Look(Command command)
    {
        Player.Look();
    }

    private static void ShowInventory(Command command)
    {
        Player.ShowInventory();
    }
    
    private static void Take(Command command)
    {
        Player.Take(command);
    }

    private static void Troff(Command command)
    {
        Debugger.Troff();
    }

    private static void Tron(Command command)
    {
        Debugger.Tron();
    }

    public static void Eat(Command command)
    {
        TextDisplay.TypeLine("Eating..." + command.Noun);
    }

    public static void Move(Command command)
    {
        Player.Move(command);
    }
    
    private static void Use(Command command)
    {
        Player.Use(command);
    }
    
    private static void Pet(Command command)
    {
        Player.Pet(command);
    }
    
    // Shows available commands for the current state
    private static void ShowHelp(Command command)
    {
        TextDisplay.TypeLine("Here are the available commands for your current state:");
        States.ShowAvailableCommands();
    }
    
    // Show the player's Pals and their stats
    private static void ShowPals(Command command)
    {
        List<Pal> playerPals = Pals.GetPlayerPals();
        
        if (playerPals.Count == 0)
        {
            TextDisplay.TypeLine("You don't have any Pals yet!");
            return;
        }
        
        TextDisplay.TypeLine("\n===== YOUR PALS =====\n");
        
        foreach (Pal pal in playerPals)
        {
            string status = Pals.IsPalFainted(pal.Name) ? "[FAINTED]" : "[HEALTHY]"; 
            TextDisplay.TypeLine($"{pal.Name} - {status} - Level: {pal.Level} - Health: {pal.Health}/{pal.MaxHealth}");
            TextDisplay.TypeLine($"Attack Power: {pal.AttackPower} | Special Power: {pal.SpecialPower}");
            TextDisplay.TypeLine($"XP: {pal.XP}/{pal.XPToNextLevel}");
            TextDisplay.TypeLine(pal.Description);
            Console.WriteLine(pal.AsciiArt);
            TextDisplay.TypeLine("--------------------");
        }
        
        // Show message about healing if player has fainted Pals
        List<Pal> faintedPals = Pals.GetFaintedPlayerPals();
        if (faintedPals.Count > 0)
        {
            TextDisplay.TypeLine($"\nYou have {faintedPals.Count} fainted Pal(s) that need healing.");
            TextDisplay.TypeLine("Visit the Pal Center and use the 'heal' command to restore them.");
        }
    }
    
    // Heal Pals at a Pal Center
    private static void HealPals(Command command)
    {
        // Check if player is at a Pal Center
        if (Player.CurrentLocation.Name != "Pal Center")
        {
            TextDisplay.TypeLine("You need to be at a Pal Center to heal your Pals!");
            return;
        }
        
        // Get all fainted Pals
        List<Pal> faintedPals = Pals.GetFaintedPlayerPals();
        
        if (faintedPals.Count == 0)
        {
            TextDisplay.TypeLine("All your Pals are already healthy!");
            return;
        }
        
        // Heal all fainted Pals
        TextDisplay.TypeLine("\nWelcome to the Pal Center!");
        TextDisplay.TypeLine("We'll take care of your Pals and restore them to full health.\n");
        
        // Dramatic pause for effect
        System.Threading.Thread.Sleep(1000);
        TextDisplay.TypeLine("*healing music plays*");
        System.Threading.Thread.Sleep(1500);
        
        // Heal all fainted Pals
        Pals.HealAllFaintedPals();
        
        TextDisplay.TypeLine("\nAll your Pals are now fully healed and ready for battle again!");
    }
}
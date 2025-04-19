namespace AdventureS25;

public static class CombatCommandHandler
{
    // Mapping all possible battle commands to handlers
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            // Battle commands - text only
            {"attack", Attack},
            {"defend", Defend},
            {"special", Special},
            {"tame", Tame},    // Added tame command
            {"run", Run},
            {"pals", ShowPals},
            {"help", ShowHelp}
        };
    
    public static void Handle(Command command)
    {
        // Convert to lowercase for case-insensitive matching
        string verb = command.Verb.ToLower();
        
        if (commandMap.ContainsKey(verb))
        {
            commandMap[verb].Invoke(command);
        }
        else
        {
            if (PalBattle.IsBattleAgainstWildPal)
            {
                TextDisplay.TypeLine("Valid battle commands are: attack, defend, special, tame, or run.");
            }
            else
            {
                if (Player.CurrentLocation.Name == "Verdant Grasslands")
            {
                TextDisplay.TypeLine("Valid battle commands are: attack, defend, special, tame, or run.");
            }
            else
            {
                TextDisplay.TypeLine("Valid battle commands are: attack, defend, special, or run.");
            }
            }
        }
    }

    // Individual command methods for better reusability
    private static void Attack(Command command)
    {
        if (PalBattle.IsBattleActive)
        {
            // Create a standardized command for the battle system
            Command attackCommand = new Command { Verb = "attack", Noun = "" };
            PalBattle.HandleBattleCommand(attackCommand);
        }
        else
        {
            TextDisplay.TypeLine("There's no active battle right now.");
            States.ChangeState(StateTypes.Exploring);
        }
    }
    
    private static void Defend(Command command)
    {
        if (PalBattle.IsBattleActive)
        {
            Command defendCommand = new Command { Verb = "defend", Noun = "" };
            PalBattle.HandleBattleCommand(defendCommand);
        }
        else
        {
            TextDisplay.TypeLine("There's no active battle right now.");
            States.ChangeState(StateTypes.Exploring);
        }
    }
    
    private static void Special(Command command)
    {
        if (PalBattle.IsBattleActive)
        {
            Command specialCommand = new Command { Verb = "special", Noun = "" };
            PalBattle.HandleBattleCommand(specialCommand);
        }
        else
        {
            TextDisplay.TypeLine("There's no active battle right now.");
            States.ChangeState(StateTypes.Exploring);
        }
    }
    
    private static void Run(Command command)
    {
        if (PalBattle.IsBattleActive)
        {
            Command runCommand = new Command { Verb = "run", Noun = "" };
            PalBattle.HandleBattleCommand(runCommand);
        }
        else
        {
            TextDisplay.TypeLine("There's no active battle right now.");
            States.ChangeState(StateTypes.Exploring);
        }
    }
    
    private static void Tame(Command command)
    {
        if (PalBattle.IsBattleActive)
        {
            // Always check if we have treats first
            Item? palTreat = Items.GetItemByName("pal-treats");
            if (palTreat != null && Player.HasItemInInventory(palTreat))
            {
                // Always allow taming in the Verdant Grasslands and for any non-Morty Pal
                if (Player.CurrentLocation.Name == "Verdant Grasslands" && PalBattle.EnemyPal.Name != "Morty")
                {
                    // Remove one treat from inventory
                    Player.RemoveItemFromInventory("pal-treats");
                    
                    TextDisplay.TypeLine($"You offered a treat to the wild {PalBattle.EnemyPal.Name}.");
                    
                    // 70% chance of success, with increased chances for damaged Pals
                    Random random = new Random();
                    int baseChance = 70;
                    
                    // If enemy Pal is damaged, taming is easier (up to 90% at low health)
                    int healthPercentage = (PalBattle.EnemyPal.Health * 100) / PalBattle.EnemyPal.MaxHealth;
                    int bonusChance = (100 - healthPercentage) / 5; // Up to 20% bonus for low health
                    
                    int tameChance = Math.Min(90, baseChance + bonusChance);
                    int roll = random.Next(0, 100);
                    
                    if (roll < tameChance)
                    {
                        // Success!
                        TextDisplay.TypeLine($"The {PalBattle.EnemyPal.Name} happily accepted your offering!");
                        TextDisplay.TypeLine($"You successfully tamed {PalBattle.EnemyPal.Name}!");
                        
                        // Add the Pal to the player's collection
                        Pals.GivePalToPlayer(PalBattle.EnemyPal.Name);
                        
                        // End battle via standard flow
                        // Use a delegate method to end the battle properly
                        Command endBattleCommand = new Command { Verb = "end-battle-tamed", Noun = "" };
                        PalBattle.HandleBattleCommand(endBattleCommand);
                        
                        // Clean up and return to exploring
                        WildPalManager.ClearWildPalFromLocation(Player.CurrentLocation.Name);
                        States.ChangeState(StateTypes.Exploring);
                    }
                    else
                    {
                        // Failed taming attempt
                        TextDisplay.TypeLine($"The {PalBattle.EnemyPal.Name} sniffed at the treat but wasn't interested.");
                        // Player's turn is over, pass turn to enemy
                        Command endTurnCommand = new Command { Verb = "end-turn", Noun = "" };
                        PalBattle.HandleBattleCommand(endTurnCommand);
                    }
                }
                else
                {
                    TextDisplay.TypeLine("You can only tame wild Pals!");
                }
            }
            else
            {
                TextDisplay.TypeLine("You don't have any Pal treats! You need treats to tame wild Pals.");
                TextDisplay.TypeLine("You can get treats from Professor Jon after your first battle.");
            }
        }
        else
        {
            TextDisplay.TypeLine("There's no active battle right now.");
            States.ChangeState(StateTypes.Exploring);
        }
    }
    
    // Shows available commands for battle
    private static void ShowHelp(Command command)
    {
        TextDisplay.TypeLine("Here are the available commands for battle:");
        States.ShowAvailableCommands();
    }
    
    // Show the player's Pals and their stats during battle
    private static void ShowPals(Command command)
    {
        List<Pal> playerPals = Pals.GetPlayerPals();
        
        if (playerPals.Count == 0)
        {
            TextDisplay.TypeLine("You don't have any Pals yet!");
            return;
        }
        
        TextDisplay.TypeLine("\n===== YOUR PALS =====\n");
        
        // First show the active battle Pal
        if (PalBattle.IsBattleActive && PalBattle.PlayerPal != null)
        {
            TextDisplay.TypeLine("ACTIVE BATTLE PAL:");
            TextDisplay.TypeLine($"{PalBattle.PlayerPal.Name} - Health: {PalBattle.PlayerPal.Health}/{PalBattle.PlayerPal.MaxHealth}");
            TextDisplay.TypeLine($"Currently fighting {PalBattle.EnemyPal?.Name ?? "an unknown opponent"}");
            TextDisplay.TypeLine("--------------------");
        }
        
        // Then show all other Pals
        foreach (Pal pal in playerPals)
        {
            // Skip detailed display of the active battle pal since we already showed it
            if (PalBattle.IsBattleActive && PalBattle.PlayerPal != null && pal.Name == PalBattle.PlayerPal.Name)
            {
                continue;
            }
            
            TextDisplay.TypeLine($"{pal.Name} - Health: {pal.Health}/{pal.MaxHealth}");
            TextDisplay.TypeLine(pal.Description);
            TextDisplay.TypeLine("--------------------");
        }
    }
}
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
            TextDisplay.TypeLine("Valid battle commands are: attack, defend, special, or run.");
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
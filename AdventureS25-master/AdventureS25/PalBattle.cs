using System;
using System.Collections.Generic;

namespace AdventureS25;

public class Pal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AsciiArt { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public List<string> Moves { get; set; }
    public bool IsWild { get; set; } = false; // Flag to indicate if this is a wild Pal
    
    // New properties for leveling system
    public int Level { get; set; }
    public int XP { get; set; }
    public int XPToNextLevel { get; set; }
    
    // New properties for energy system
    public int AttackEnergy { get; set; }
    public int MaxAttackEnergy { get; set; }
    public int SpecialEnergy { get; set; }
    public int MaxSpecialEnergy { get; set; }
    
    // New properties for damage scaling
    public int AttackPower { get; set; }
    public int SpecialPower { get; set; }

    public Pal(string name, string description, string asciiArt, int health)
    {
        Name = name;
        Description = description;
        AsciiArt = asciiArt;
        Health = health;
        MaxHealth = health;
        Moves = new List<string>() { "Attack", "Defend", "Special" };
        
        // Initialize level and XP
        Level = 1;
        XP = 0;
        XPToNextLevel = 100; // Base XP required for level 2
        
        // Initialize energy
        MaxAttackEnergy = 15;
        AttackEnergy = MaxAttackEnergy;
        MaxSpecialEnergy = 5;
        SpecialEnergy = MaxSpecialEnergy;
        
        // Initialize attack power
        AttackPower = 5; // Base attack power
        SpecialPower = 10; // Base special attack power
    }
    
    // Method to add XP and level up if necessary
    public void AddXP(int amount)
    {
        XP += amount;
        TextDisplay.TypeLine($"{Name} gained {amount} XP!");
        
        // Check if leveled up
        if (XP >= XPToNextLevel)
        {
            LevelUp();
        }
    }
    
    // Level up method
    private void LevelUp()
    {
        Level++;
        XP -= XPToNextLevel;
        XPToNextLevel = 100 * Level; // Each level requires more XP
        
        // Increase stats based on level
        int healthIncrease = 5 * Level;
        MaxHealth += healthIncrease;
        Health = MaxHealth; // Heal to full when leveling up
        
        // Energy increases with level
        MaxAttackEnergy += 2;
        AttackEnergy = MaxAttackEnergy;
        MaxSpecialEnergy += 1;
        SpecialEnergy = MaxSpecialEnergy;
        
        // Increase attack and special power
        int attackIncrease = 2;
        int specialIncrease = 3;
        AttackPower += attackIncrease;
        SpecialPower += specialIncrease;
        
        TextDisplay.TypeLine($"\n==============================\n{Name} LEVELED UP to level {Level}!\n==============================");
        TextDisplay.TypeLine($"Max Health increased by {healthIncrease} to {MaxHealth}!");
        TextDisplay.TypeLine($"Attack Energy increased to {MaxAttackEnergy}!");
        TextDisplay.TypeLine($"Special Energy increased to {MaxSpecialEnergy}!");
        TextDisplay.TypeLine($"Attack Power increased by {attackIncrease} to {AttackPower}!");
        TextDisplay.TypeLine($"Special Power increased by {specialIncrease} to {SpecialPower}!");
    }
    
    // Method to use attack energy
    public bool UseAttackEnergy(int amount = 1)
    {
        if (AttackEnergy >= amount)
        {
            AttackEnergy -= amount;
            return true;
        }
        return false; // Not enough energy
    }
    
    // Method to use special energy
    public bool UseSpecialEnergy(int amount = 1)
    {
        if (SpecialEnergy >= amount)
        {
            SpecialEnergy -= amount;
            return true;
        }
        return false; // Not enough energy
    }
    
    // Method to restore energy
    public void RestoreEnergy()
    {
        AttackEnergy = MaxAttackEnergy;
        SpecialEnergy = MaxSpecialEnergy;
        TextDisplay.TypeLine($"{Name}'s energy has been fully restored!");
    }

    public void DisplayInfo()
    {
        Console.WriteLine(AsciiArt);
        TextDisplay.TypeLine($"{Name} - Level: {Level} - Health: {Health}/{MaxHealth}");
        TextDisplay.TypeLine($"Attack Energy: {AttackEnergy}/{MaxAttackEnergy} | Special Energy: {SpecialEnergy}/{MaxSpecialEnergy}");
        TextDisplay.TypeLine($"XP: {XP}/{XPToNextLevel}");
        TextDisplay.TypeLine(Description);
    }
}

public static class PalBattle
{
    public static Pal PlayerPal { get; private set; }
    public static Pal EnemyPal { get; private set; }
    public static bool IsBattleActive { get; private set; } = false;
    public static bool IsPlayerTurn { get; private set; } = true;
    public static int TurnCount { get; private set; } = 0;
    public static StateTypes PreviousStateType { get; private set; } = StateTypes.Exploring;
    public static string LastOpponent { get; private set; } = string.Empty;
    public static bool IsBattleAgainstWildPal { get; private set; } = false;

    private static readonly Random random = new Random();

    public static void StartBattle()
    {
        // Initialize battle with default Pals
        StartBattle("Sandie", "Morty");
    }
    
    public static void StartBattle(string playerPalName, string enemyPalName)
    {
        // Store the current state before battle (will be either Exploring or Talking)
        PreviousStateType = States.CurrentStateType;
        
        // Get Pals from the Pals system
        PlayerPal = Pals.GetPalByName(playerPalName) ?? 
            throw new ArgumentException($"Player Pal '{playerPalName}' not found!");
            
        EnemyPal = Pals.GetPalByName(enemyPalName) ?? 
            throw new ArgumentException($"Enemy Pal '{enemyPalName}' not found!");
            
        // Store the enemy's name to identify post-battle context
        LastOpponent = EnemyPal.Name;
        
        // Always consider these Pals as wild Pals
        string[] wildPalPrefixes = new string[] {
            "Leafy", "Vine", "Bloom", "Moss", "Seed", "LeafyWhip", "VineSnare", 
            "BloomBud", "MossBack", "SeedShooter"
        };
        
        // Set the wild Pal flag based on the battle type
        bool isWildPal = false;
        foreach (string prefix in wildPalPrefixes)
        {
            if (enemyPalName.Contains(prefix))
            {
                isWildPal = true;
                break;
            }
        }
        
        // Debug output to help diagnose issues
        if (isWildPal)
        {
            IsBattleAgainstWildPal = true;
            EnemyPal.IsWild = true;
            Console.WriteLine($"DEBUG: {enemyPalName} is a wild Pal, taming should be enabled");
        }
        else
        {
            IsBattleAgainstWildPal = false;
            EnemyPal.IsWild = false;
            Console.WriteLine($"DEBUG: {enemyPalName} is NOT a wild Pal, taming is disabled");
        }
        
        // Mark the Sandie Pal as owned by the player if it's in the battle
        if (playerPalName == "Sandie")
        {
            Pals.GivePalToPlayer("Sandie");
        }
        
        IsBattleActive = true;
        IsPlayerTurn = true;
        TurnCount = 0;
        
        // Change state to Fighting
        States.ChangeState(StateTypes.Fighting);
        
        // Display battle intro
        TextDisplay.TypeLine($"\nA battle begins between {PlayerPal.Name} and {EnemyPal.Name}!");
        Console.WriteLine(@"
+======================================================================+

▀█████████▄     ▄████████     ███         ███      ▄█          ▄████████ 
  ███    ███   ███    ███ ▀█████████▄ ▀█████████▄ ███         ███    ███ 
  ███    ███   ███    ███    ▀███▀▀██    ▀███▀▀██ ███         ███    █▀  
 ▄███▄▄▄██▀    ███    ███     ███   ▀     ███   ▀ ███        ▄███▄▄▄     
▀▀███▀▀▀██▄  ▀███████████     ███         ███     ███       ▀▀███▀▀▀     
  ███    ██▄   ███    ███     ███         ███     ███         ███    █▄  
  ███    ███   ███    ███     ███         ███     ███▌    ▄   ███    ███ 
▄█████████▀    ███    █▀     ▄████▀      ▄████▀   █████▄▄██   ██████████ 
                                                  ▀                      
+======================================================================+");
        
        // Show both Pals
        EnemyPal.DisplayInfo();
        TextDisplay.TypeLine(@" 
██▒   █▓  ██████ 
▓██░   █▒▒██    ▒ 
 ▓██  █▒░░ ▓██▄   
  ▒██ █░░  ▒   ██▒
   ▒▀█░  ▒██████▒▒
   ░ ▐░  ▒ ▒▓▒ ▒ ░
   ░ ░░  ░ ░▒  ░ ░
     ░░  ░  ░  ░  
      ░        ░  
     ░");
        PlayerPal.DisplayInfo();
        
        // Note: PreviousStateType is already set at the beginning of the method
        // No need to set it again here
        
        // Ensure we're in fighting state
        States.ChangeState(StateTypes.Fighting);
        
        // Show battle commands
        ShowBattleCommands();
    }

    private static void ShowBattleCommands()
    {
        TextDisplay.TypeLine("\nWhat will " + PlayerPal.Name + " do?");
        
        // Allow tame command in Verdant Grasslands or if it's a wild Pal battle
        if (IsBattleAgainstWildPal || Player.CurrentLocation.Name == "Verdant Grasslands")
        {
            TextDisplay.TypeLine("Commands: attack, defend, special, tame, run");
        }
        else
        {
            TextDisplay.TypeLine("Commands: attack, defend, special, run");
        }
    }

    public static void HandleBattleCommand(Command command)
    {
        if (!IsBattleActive)
        {
            return;
        }

        if (!IsPlayerTurn)
        {
            TextDisplay.TypeLine("Wait for your turn!");
            return;
        }

        string action = command.Verb.ToLower();

        switch (action)
        {
            case "attack":
                PlayerAttack();
                break;
            case "defend":
                PlayerDefend();
                break;
            case "special":
                PlayerSpecial();
                break;
            case "tame":
                // Set the battle as a wild Pal battle if in Verdant Grasslands
                if (Player.CurrentLocation.Name == "Verdant Grasslands" && EnemyPal.Name != "Morty")
                {
                    // Force-enable taming in this location
                    IsBattleAgainstWildPal = true;
                    EnemyPal.IsWild = true;
                }
                
                // Now try to tame the Pal
                AttemptToTame();
                break;
            
        case "end-battle-tamed":
            // Special case for when a Pal is successfully tamed
            TextDisplay.TypeLine("Ending battle after successful taming.");
            IsBattleActive = false;
            // No need to reset other state variables as they'll be initialized on next battle
            break;
            case "run":
                AttemptToFlee();
                break;
            case "pals":
                // Show player's Pal stats during battle
                TextDisplay.TypeLine("\nYour Pal's stats:");
                TextDisplay.TypeLine($"{PlayerPal.Name} - Level: {PlayerPal.Level} - Health: {PlayerPal.Health}/{PlayerPal.MaxHealth}");
                TextDisplay.TypeLine($"Attack Energy: {PlayerPal.AttackEnergy}/{PlayerPal.MaxAttackEnergy} | Special Energy: {PlayerPal.SpecialEnergy}/{PlayerPal.MaxSpecialEnergy}");
                TextDisplay.TypeLine($"XP: {PlayerPal.XP}/{PlayerPal.XPToNextLevel}");
                TextDisplay.TypeLine($"Attack Power: {PlayerPal.AttackPower} | Special Power: {PlayerPal.SpecialPower}");
                return; // Don't end turn when checking pals
            case "help":
                States.ShowAvailableCommands();
                return; // Don't end turn when checking help
            default:
            // Show available commands based on location or wild Pal status
            if (IsBattleAgainstWildPal || Player.CurrentLocation.Name == "Verdant Grasslands")
            {
                TextDisplay.TypeLine("Valid commands are: attack, defend, special, tame, run, pals, help");
            }
            else
            {
                TextDisplay.TypeLine("Valid commands are: attack, defend, special, run, pals, help");
            }
            return;
        }

        // After player turn, check if battle should continue
        if (IsBattleActive && !IsPlayerTurn)
        {
            // Enemy turn
            TextDisplay.TypeLine($"\n{EnemyPal.Name}'s turn!");
            EnemyTurn();
        }
    }

    private static void PlayerAttack()
    {
        // Check if player has enough energy
        if (!PlayerPal.UseAttackEnergy())
        {
            TextDisplay.TypeLine($"\n{PlayerPal.Name} doesn't have enough attack energy!");
            ShowBattleCommands(); // Show commands again
            return;
        }
        
        // Calculate damage using the Pal's attack power plus randomization
        Random random = new Random();
        int variationRange = 3; // Random variation of +/- 3 damage
        int randomVariation = random.Next(-variationRange, variationRange + 1);
        int damage = PlayerPal.AttackPower + randomVariation;
        damage = Math.Max(1, damage); // Ensure at least 1 damage
        
        // Apply damage to enemy Pal
        EnemyPal.Health -= damage;
        EnemyPal.Health = Math.Max(0, EnemyPal.Health); // Ensure health doesn't go below 0
        
        // Display results
        TextDisplay.TypeLine($"\n{PlayerPal.Name} attacks {EnemyPal.Name} for {damage} damage!");
        TextDisplay.TypeLine($"Attack Energy: {PlayerPal.AttackEnergy}/{PlayerPal.MaxAttackEnergy}");
        
        // Check if enemy defeated
        if (EnemyPal.Health <= 0)
        {
            EndBattle(true); // Player won
        }
        else
        {
            IsPlayerTurn = false; // Set to enemy turn
        }
    }

    private static void PlayerDefend()
    {
        TextDisplay.TypeLine($"\n{PlayerPal.Name} takes a defensive stance!");
        TextDisplay.TypeLine($"{(PlayerPal.Name.EndsWith("s") ? PlayerPal.Name : PlayerPal.Name + "'s")} defense increases temporarily.");
        
        // Implementing a simple defense buff
        PlayerPal.Health += 2;
        if (PlayerPal.Health > PlayerPal.MaxHealth)
        {
            PlayerPal.Health = PlayerPal.MaxHealth;
        }
        
        TextDisplay.TypeLine($"{PlayerPal.Name} recovered 2 health points!");
        
        IsPlayerTurn = false;
    }

    private static void PlayerSpecial()
    {
        // Check if player has enough special energy
        if (!PlayerPal.UseSpecialEnergy())
        {
            TextDisplay.TypeLine($"\n{PlayerPal.Name} doesn't have enough special energy!");
            ShowBattleCommands(); // Show commands again
            return;
        }
        
        // Special attacks do more damage but have a chance to miss
        Random random = new Random();
        
        // 20% chance to miss
        if (random.Next(1, 11) <= 2)
        {
            TextDisplay.TypeLine($"\n{PlayerPal.Name} tried to use a special attack but missed!");
            TextDisplay.TypeLine($"Special Energy: {PlayerPal.SpecialEnergy}/{PlayerPal.MaxSpecialEnergy}");
            IsPlayerTurn = false; // Set to enemy turn
            return;
        }
        
        // Calculate damage using the Pal's special power plus randomization
        int variationRange = 5; // Random variation of +/- 5 damage for special attacks
        int randomVariation = random.Next(-variationRange, variationRange + 1);
        int damage = PlayerPal.SpecialPower + randomVariation;
        damage = Math.Max(1, damage); // Ensure at least 1 damage
        
        // Apply damage to enemy Pal
        EnemyPal.Health -= damage;
        EnemyPal.Health = Math.Max(0, EnemyPal.Health); // Ensure health doesn't go below 0
        
        // Display results
        TextDisplay.TypeLine($"\n{PlayerPal.Name} uses a special attack on {EnemyPal.Name} for {damage} damage!");
        TextDisplay.TypeLine($"Special Energy: {PlayerPal.SpecialEnergy}/{PlayerPal.MaxSpecialEnergy}");
        
        // Check if enemy defeated
        if (EnemyPal.Health <= 0)
        {
            EndBattle(true); // Player won
        }
        else
        {
            IsPlayerTurn = false; // Set to enemy turn
        }
    }

    private static void AttemptToTame()
    {
        // Check if this is a wild Pal first
        if (!IsBattleAgainstWildPal)
        {
            TextDisplay.TypeLine("You can only tame wild Pals!");
            return;
        }
        
        // Get Pal treats from inventory
        Item? palTreat = Items.GetItemByName("pal-treats");
        if (palTreat == null || !Player.HasItemInInventory(palTreat))
        {
            TextDisplay.TypeLine("You don't have any Pal treats! You need treats to tame wild Pals.");
            TextDisplay.TypeLine("You can get treats from Professor Jon after your first battle.");
            // Don't end turn for usability - let the player try another action
            return;
        }
        
        // Remove one treat from inventory
        Player.RemoveItemFromInventory("pal-treats");
        TextDisplay.TypeLine($"You offered a treat to the wild {EnemyPal.Name}...");
        
        // Calculate tame chance - base 60% + up to 30% more for damaged Pals
        int baseChance = 60;
        
        // If enemy Pal is damaged, taming is easier
        int healthPercentage = (EnemyPal.Health * 100) / EnemyPal.MaxHealth;
        int bonusChance = (100 - healthPercentage) / 3; // Up to 33% bonus for low health
        
        int tameChance = Math.Min(90, baseChance + bonusChance);
        int roll = new Random().Next(1, 101); // Roll 1-100
        
        if (roll <= tameChance)
        {
            // Success!
            TextDisplay.TypeLine($"Success! You tamed {EnemyPal.Name}!");
            
            // Add to player's Pals collection
            Pals.GivePalToPlayer(EnemyPal.Name);
            
            // End battle with success and let the system know this was a tame
            EndBattle(true, true);
        }
        else
        {
            // Failed attempt
            TextDisplay.TypeLine($"The {EnemyPal.Name} wasn't interested in your treat.");
            IsPlayerTurn = false; // End player's turn
        }
    }

    private static void AttemptToFlee()
    {
        int escapeChance = random.Next(1, 101); // 1-100
        
        if (escapeChance > 50)
        {
            TextDisplay.TypeLine($"\n{PlayerPal.Name} tries to run away...");
            TextDisplay.TypeLine($"But {EnemyPal.Name} blocks the exit!");
            TextDisplay.TypeLine($"{EnemyPal.Name}: \"Where do you think you're going?! This isn't *burp* over!\"");
            
            IsPlayerTurn = false;
        }
        else
        {
            TextDisplay.TypeLine($"\n{PlayerPal.Name} tries to run away...");
            TextDisplay.TypeLine($"{EnemyPal.Name}: \"Running away? That's the smartest *burp* move you've made today!\"");
            TextDisplay.TypeLine("You've escaped the battle!");
            
            EndBattle(false);
        }
    }

    private static void EnemyTurn()
    {
        // Simple AI for enemy turn - universally works for any enemy Pal
        int choice = random.Next(0, 3); // 0-2 for different actions
        
        // Base attack and special power for wild Pals, scaling with level
        int enemyAttackPower = 3 + EnemyPal.Level; // Base attack power
        int enemySpecialPower = 6 + (EnemyPal.Level * 2); // Base special power
        
        switch (choice)
        {
            case 0:
                // Basic attack with power scaling
                TextDisplay.TypeLine($"{EnemyPal.Name} uses a basic attack!");
                int variationRange = 2; // Random variation of +/- 2 damage
                int randomVariation = random.Next(-variationRange, variationRange + 1);
                int damage = enemyAttackPower + randomVariation;
                damage = Math.Max(1, damage); // Ensure at least 1 damage
                
                PlayerPal.Health -= damage;
                TextDisplay.TypeLine($"{PlayerPal.Name} took {damage} damage!");
                break;
            case 1:
                // Special attack with power scaling
                string specialMove = "special attack";
                
                // If the Pal has moves defined, use one of them randomly
                if (EnemyPal.Moves != null && EnemyPal.Moves.Count > 0)
                {
                    // Pick a random move from the Pal's move list
                    int moveIndex = random.Next(0, EnemyPal.Moves.Count);
                    specialMove = EnemyPal.Moves[moveIndex];
                }
                
                int specialVariationRange = 3; // Random variation of +/- 3 damage
                int specialRandomVariation = random.Next(-specialVariationRange, specialVariationRange + 1);
                int specialDamage = enemySpecialPower + specialRandomVariation;
                specialDamage = Math.Max(1, specialDamage); // Ensure at least 1 damage
                
                TextDisplay.TypeLine($"{EnemyPal.Name} executes a {specialMove}!");
                PlayerPal.Health -= specialDamage;
                TextDisplay.TypeLine($"{PlayerPal.Name} took {specialDamage} damage!");
                break;
            case 2:
                // Heal - scales with level
                TextDisplay.TypeLine($"{EnemyPal.Name} defends!");
                int healBase = 2 + EnemyPal.Level;
                int heal = random.Next(healBase, healBase + 5); // Healing scales with level
                EnemyPal.Health += heal;
                if (EnemyPal.Health > EnemyPal.MaxHealth)
                {
                    EnemyPal.Health = EnemyPal.MaxHealth;
                }
                TextDisplay.TypeLine($"{EnemyPal.Name} recovered {heal} health points!");
                break;
        }
        
        // Check if player lost
        if (PlayerPal.Health <= 0)
        {
            PlayerPal.Health = 0;
            EndBattle(false);
        }
        else
        {
            TurnCount++;
            IsPlayerTurn = true;
            
            // Display health status - dynamically use Pal names
            TextDisplay.TypeLine($"\n{PlayerPal.Name}'s Health: {PlayerPal.Health}/{PlayerPal.MaxHealth}");
            TextDisplay.TypeLine($"{EnemyPal.Name}'s Health: {EnemyPal.Health}/{EnemyPal.MaxHealth}");
            
            // Show battle commands for next player turn
            ShowBattleCommands();
        }
    }

    private static void EndBattle(bool playerWon, bool tamedPal = false)
    {
        IsBattleActive = false;
        
        if (playerWon)
        {
            if (tamedPal)
            {
                // Pal was tamed successfully - handled in the AttemptToTame method
                // Just clear the wild Pal from the location
                WildPalManager.ClearWildPalFromLocation(Player.CurrentLocation.Name);
            }
            else
            {
                // Player won the battle by defeating the wild Pal
                TextDisplay.TypeLine($"\n{EnemyPal.Name} has been defeated!");
                
                // Award XP based on enemy's level and a base amount
                int xpGained = 50 + (EnemyPal.Level * 10);
                PlayerPal.AddXP(xpGained);
                
                // Generic victory message
                TextDisplay.TypeLine($"You and {PlayerPal.Name} have won the battle!");
                TextDisplay.TypeLine($"Your bond with {PlayerPal.Name} grows stronger.");
                
                // Clear the wild Pal from the location
                WildPalManager.ClearWildPalFromLocation(Player.CurrentLocation.Name);
            }
        }
        else
        {
            if (PlayerPal.Health <= 0)
            {
                // Player's Pal was defeated
                TextDisplay.TypeLine($"\n{PlayerPal.Name} has been defeated!");
                
                // Mark the Pal as fainted but don't remove it from player's collection
                string faintedPalName = PlayerPal.Name;
                Pals.MarkPalAsFainted(faintedPalName);
                
                // Check if player has any healthy Pals left
                var healthyPals = Pals.GetHealthyPlayerPals();
                if (healthyPals.Count == 0)
                {
                    // Game over - player has no healthy Pals left
                    TextDisplay.TypeLine("\n===================================");
                    TextDisplay.TypeLine("All your Pals have fainted!");
                    TextDisplay.TypeLine("You need to visit a Pal Center to heal them.");
                    TextDisplay.TypeLine("Game Over - You need to restart the adventure.");
                    TextDisplay.TypeLine("===================================");
                    
                    // Wait for player to acknowledge game over
                    TextDisplay.TypeLine("\nPress any key to exit...");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }
                else
                {
                    TextDisplay.TypeLine($"Your Pal {faintedPalName} has fainted! You need to visit a Pal Center to heal it.");
                    TextDisplay.TypeLine($"You still have {healthyPals.Count} healthy Pal(s) that can battle.");
                    
                    // Award a small amount of XP to remaining healthy Pals
                    int xpGained = 10 + (EnemyPal.Level * 2);
                    foreach (var pal in healthyPals)
                    {
                        pal.AddXP(xpGained);
                    }
                }
                
                // Do not clear the wild Pal - give player chance to try again with other Pals
            }
            else
            {
                // Player ran away
                TextDisplay.TypeLine($"You and {PlayerPal.Name} retreat from the battle.");
                TextDisplay.TypeLine("Sometimes the wisest move is a tactical retreat.");
                
                // Do not clear the wild Pal - give player chance to try again
            }
        }
        
        Console.WriteLine(@"
+======================================================================================+

 ▄▄▄▄    ▄▄▄      ▄▄▄█████▓▄▄▄█████▓ ██▓    ▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
▓█████▄ ▒████▄    ▓  ██▒ ▓▒▓  ██▒ ▓▒▓██▒    ▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██▒ ▄██▒██  ▀█▄  ▒ ▓██░ ▒░▒ ▓██░ ▒░▒██░    ▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
▒██░█▀  ░██▄▄▄▄██ ░ ▓██▓ ░ ░ ▓██▓ ░ ▒██░    ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▓█  ▀█▓ ▓█   ▓██▒  ▒██▒ ░   ▒██▒ ░ ░██████▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
░▒▓███▀▒ ▒▒   ▓▒█░  ▒ ░░     ▒ ░░   ░ ▒░▓  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
▒░▒   ░   ▒   ▒▒ ░    ░        ░    ░ ░ ▒  ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
 ░    ░   ░   ▒     ░        ░        ░ ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
 ░            ░  ░                      ░  ░   ░  ░       ░ ░        ░     ░  ░   ░     
      ░                                                             ░                   
+======================================================================================+");
        
        // If the opponent was Morty (Professor Jon's Pal) and player won, return to conversation state
        if (LastOpponent == "Morty" && playerWon)
        {
            // Return to talking with Professor Jon
            States.ChangeState(StateTypes.Talking);
            ConversationCommandHandler.PostBattleWithJon();
        }
        else
        {
            // Otherwise, return to the previous state
            States.ChangeState(PreviousStateType);
        }
    }
}

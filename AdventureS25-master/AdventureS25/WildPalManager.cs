using System;
using System.Collections.Generic;

namespace AdventureS25;

public static class WildPalManager
{
    // Dictionary to map location names to potential wild Pals
    private static Dictionary<string, List<string>> locationToPalNames = new Dictionary<string, List<string>>();
    
    // Track the current wild Pal in each location (if any)
    private static Dictionary<string, string> locationToCurrentPal = new Dictionary<string, string>();
    
    // Random number generator for Pal spawning
    private static Random random = new Random();
    
    // Initialize the wild Pal system
    public static void Initialize()
    {
        // Set up grass-type Pals that can appear in Verdant Grasslands
        RegisterWildPalLocation("Verdant Grasslands", "LeafyWhip");
        RegisterWildPalLocation("Verdant Grasslands", "VineSnare");
        RegisterWildPalLocation("Verdant Grasslands", "BloomBud");
        RegisterWildPalLocation("Verdant Grasslands", "MossBack");
        RegisterWildPalLocation("Verdant Grasslands", "SeedShooter");
    }
    
    // Register a wild Pal that can appear in a specific location
    public static void RegisterWildPalLocation(string locationName, string palName)
    {
        if (!locationToPalNames.ContainsKey(locationName))
        {
            locationToPalNames[locationName] = new List<string>();
        }
        
        if (!locationToPalNames[locationName].Contains(palName))
        {
            locationToPalNames[locationName].Add(palName);
        }
    }
    
    // List of building/indoor locations where wild Pals should not spawn
    private static readonly List<string> indoorLocations = new List<string>
    {
        "Player's Home",
        "Professor Jon's Lab",
        "Pal Center", 
        "Pal Mart",
        "Town Hall"
    };
    
    // Spawn a wild Pal in a location
    public static void SpawnWildPalInLocation(string locationName)
    {
        // Clear any existing Pal first
        if (locationToCurrentPal.ContainsKey(locationName))
        {
            locationToCurrentPal.Remove(locationName);
        }
        
        // Check if this is an indoor location where wild Pals shouldn't spawn
        if (indoorLocations.Contains(locationName))
        {
            return; // No wild Pals in buildings/indoor locations
        }
        
        // Check if we have registered Pals for this location
        if (!locationToPalNames.ContainsKey(locationName) || locationToPalNames[locationName].Count == 0)
        {
            return; // No wild Pals for this location
        }
        
        // Always spawn a wild Pal in wildlife areas (100% chance)
        // Pick a random Pal from the available options
        string randomPalName = locationToPalNames[locationName][random.Next(locationToPalNames[locationName].Count)];
        locationToCurrentPal[locationName] = randomPalName;
        
        // Get the actual Pal object
        Pal? wildPal = Pals.GetPalByName(randomPalName);
        
        if (wildPal != null)
        {
            // Mark this as a wild Pal
            wildPal.IsWild = true;
            
            TextDisplay.TypeLine($"\nA wild {wildPal.Name} appears in the tall grass!");
        }
    }
    
    // Get the current wild Pal in a location (if any)
    public static Pal? GetWildPalInLocation(string locationName)
    {
        if (locationToCurrentPal.ContainsKey(locationName))
        {
            string palName = locationToCurrentPal[locationName];
            return Pals.GetPalByName(palName);
        }
        
        return null;
    }
    
    // Clear the wild Pal from a location (after catching or defeating it)
    public static void ClearWildPalFromLocation(string locationName)
    {
        if (locationToCurrentPal.ContainsKey(locationName))
        {
            locationToCurrentPal.Remove(locationName);
        }
    }
    
    // Start a battle with the wild Pal in a location
    public static bool FightWildPalInLocation(string locationName)
    {
        Pal? wildPal = GetWildPalInLocation(locationName);
        
        // Check if there's a wild Pal in this location
        if (wildPal != null)
        {
            // Get player's active healthy Pals (only Pals that haven't fainted)
            var healthyPlayerPals = Pals.GetHealthyPlayerPals();
            
            // Check if player has any healthy Pals to fight with
            if (healthyPlayerPals.Count == 0)
            {
                TextDisplay.TypeLine("You don't have any healthy Pals to fight with!");
                TextDisplay.TypeLine("Visit a Pal Center to heal your fainted Pals.");
                // Don't clear the wild Pal, so it remains for future interactions
                return false;
            }
            
            // If player has multiple healthy Pals, let them choose which one to use
            Pal playerPal;
            if (healthyPlayerPals.Count > 1)
            {
                playerPal = SelectPalForBattle(healthyPlayerPals);
            }
            else
            {
                // Only one healthy Pal, use it
                playerPal = healthyPlayerPals[0];
                TextDisplay.TypeLine($"\nYou'll use {playerPal.Name} for this battle!");
            }
            
            // Start a battle with the wild Pal
            PalBattle.StartBattle(playerPal.Name, wildPal.Name);
            
            // Wild Pal is cleared from location in EndBattle method if player wins
            // Otherwise, we keep it around for another try
            
            return true;
        }
        
        // If we get here, there's no wild Pal in this location
        TextDisplay.TypeLine("There are no wild Pals here to fight.");
        return false;
    }
    
    // Let player select which Pal to use in battle
    private static Pal SelectPalForBattle(List<Pal> playerPals)
    {
        TextDisplay.TypeLine("\nWhich Pal would you like to use for this battle?");
        
        // Display available Pals
        for (int i = 0; i < playerPals.Count; i++)
        {
            TextDisplay.TypeLine($"{i+1}. {playerPals[i].Name} (Level {playerPals[i].Level}) - Health: {playerPals[i].Health}/{playerPals[i].MaxHealth}");
        }
        
        // Get player choice
        int selection = 0;
        bool validSelection = false;
        
        while (!validSelection)
        {
            TextDisplay.TypeLine("\nEnter the number of the Pal you want to use:");
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out selection) && selection >= 1 && selection <= playerPals.Count)
            {
                validSelection = true;
            }
            else
            {
                TextDisplay.TypeLine("Invalid selection. Please try again.");
            }
        }
        
        // Return selected Pal (adjust for 0-based index)
        TextDisplay.TypeLine($"\nYou chose {playerPals[selection-1].Name}!");
        return playerPals[selection-1];
    }
    
    // Attempt to tame the wild Pal in a location
    public static bool TameWildPalInLocation(string locationName)
    {
        Pal? wildPal = GetWildPalInLocation(locationName);
        
        // Check if there's a wild Pal in this location
        if (wildPal != null)
        {
            // Check if player has Pal treats
            Item? palTreat = Items.GetItemByName("pal-treats");
            if (palTreat != null && Player.HasItemInInventory(palTreat))
            {
                // Remove one treat from inventory
                Player.RemoveItemFromInventory("pal-treats");
                
                // 70% chance of success
                if (random.Next(0, 100) < 70)
                {
                    TextDisplay.TypeLine($"You offered a treat to the wild {wildPal.Name}.");
                    TextDisplay.TypeLine($"The {wildPal.Name} happily accepted your offering!");
                    TextDisplay.TypeLine($"You successfully tamed {wildPal.Name}!");
                    
                    // Add the Pal to the player's collection
                    Pals.GivePalToPlayer(wildPal.Name);
                    
                    // Clear the Pal from the location
                    ClearWildPalFromLocation(locationName);
                }
                else
                {
                    TextDisplay.TypeLine($"You offered a treat to the wild {wildPal.Name}.");
                    TextDisplay.TypeLine($"The {wildPal.Name} sniffed at it, but ran away!");
                    
                    // Clear the Pal from the location
                    ClearWildPalFromLocation(locationName);
                }
                
                return true;
            }
            else
            {
                TextDisplay.TypeLine($"The wild {wildPal.Name} looks at you expectantly. You need Pal treats to tame wild Pals.");
                TextDisplay.TypeLine("You can get Pal treats from Professor Jon after your first battle.");
                // Don't clear the wild Pal, so it remains for future interactions
                return true; // Return true because there is a wild Pal, just can't tame it yet
            }
        }
        
        TextDisplay.TypeLine("There are no wild Pals here to tame.");
        return false;
    }
}

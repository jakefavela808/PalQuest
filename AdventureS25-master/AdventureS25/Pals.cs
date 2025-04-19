using System;
using System.Collections.Generic;

namespace AdventureS25;

public static class Pals
{
    // Dictionary to store all Pals by name
    private static Dictionary<string, Pal> nameToPal = new Dictionary<string, Pal>();
    
    // List of player-owned Pals (useful for inventory management)
    private static List<string> playerPals = new List<string>();
    
    // List to track which player Pals have fainted (0 HP) and need healing
    private static List<string> faintedPals = new List<string>();
    
    public static void Initialize()
    {
        // Create and register Sandie Pal
        Pal sandie = new Pal(
            "Sandie",
            "A rare corgi-type Pal with quantum-computing capabilities. She's extremely affectionate and loyal.",
            @"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⢟⣛⣛⣛⣛⣛⣛⣛⣛⣛⡻⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⣛⣫⣥⠶⢛⣩⣭⣭⣭⣵⣶⣤⣍⠻⣿⣟⡻⢿⣶⣝⠻⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢟⣋⣭⠴⠶⠾⠿⣛⣭⣴⣾⠟⣫⣭⣶⣾⣿⣿⣿⣿⣷⣙⣿⣿⣷⣌⠻⣷⡜⢿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⣛⣩⣶⠟⣩⣶⣾⢿⣿⣿⣿⣿⣿⢗⣹⣿⡿⢛⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠿⠳⡘⣿⣎⢿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⠿⣛⣫⣥⡶⠿⠟⠋⠰⣶⣬⣍⠻⢷⣎⣹⣿⢿⣷⣿⣿⣏⣴⠋⠀⠀⠀⠈⠋⢻⠁⣏⢿⣷⡉⠀⠀⠈⣿⢈⣿⣿⣿⣿
⣿⣿⣿⣿⡿⢡⡾⠛⣩⣴⣦⣤⣀⠀⠐⠻⣯⣉⣭⡛⡟⢻⡏⢸⣿⣿⣿⡿⢰⣤⣀⣀⣀⣀⣀⠀⢰⣿⣦⡻⣷⡀⠀⠀⣿⠈⣿⣿⣿⣿
⣿⣿⣿⣿⣇⢨⣧⣀⠉⠻⡿⢿⠟⣛⣷⣿⣦⣀⣘⣧⣴⡞⢀⣿⣿⣿⣿⣷⣾⣿⣿⡿⠯⠚⣣⣴⡿⣿⣿⣿⣿⣿⣦⣀⢿⣧⣝⠻⣿⣿
⣿⣿⣿⣿⣿⣶⣭⡛⠿⣶⣦⣀⠀⢸⣿⣿⣿⣋⣭⣾⣿⠃⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣭⣾⣿⣿⣿⣿⣿⡿⠟⠓⢉⠻⣷⣎⢻
⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣍⡛⢿⣶⡬⢙⣿⠛⢍⢻⣷⣾⣿⣿⣿⣿⣿⣿⢟⣵⣾⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠉⠀⠀⠀⠀⠁⠈⣿⡎
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣡⣿⢋⣴⣿⣿⣴⣾⣿⣾⣿⣿⣿⣿⣿⣿⣿⣼⣿⡇⠐⢿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⢠⣿⢃
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣱⣿⢏⣾⣿⣿⣿⣿⣾⣿⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣤⣤⡙⢿⣿⣿⣿⣿⣿⣿⡿⠆⠀⠀⠀⣠⡾⢃⣾
⣿⣿⣿⣿⣿⣿⣿⣿⢟⣴⣿⢯⣾⣿⣿⣿⣿⣿⣿⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣉⠛⠛⠟⠛⠋⠀⠀⠀⠀⣼⡟⣤⣾⣿
⣿⣿⣿⣿⣿⣿⠟⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⣵⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣾⣶⣤⣄⣴⡇⣿⡆⣿⣿⣿
⣿⣿⣿⣿⣿⢏⣼⡿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⢼⢹⣱⠏⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⡇⣿⣿⣿
⣿⣿⣿⣿⡏⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⠸⡏⣷⢣⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣽⡇⢹⣿⣿
⣿⣿⣿⡟⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣥⣟⢁⣻⣧⢻⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⢻⣿⣿⣿⢈⣿⠃⣿⣿⣿
⣿⣿⡿⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣍⣏⡌⠁⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡹⣿⣷⡹⣿⢰⣿⢰⣿⣿⣿
⣿⡟⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣷⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⡟⣾⡿⢼⣿⣿⣿
⣿⢀⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢏⣾⡇⣸⣿⣿⣿
⣿⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠡⣿⢇⣿⣿⣿⣿
⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⢨⣿⢠⣿⣿⣿⣿
⣿⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣹⣿⢸⣿⣿⣿⣿
⢃⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⢼⣿⢸⣿⣿⣿⣿
⠸⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢫⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⢼⣿⢸⣿⣿⣿⣿
⣧⣙⣻⣿⣿⣿⣛⣛⣛⣛⣛⣟⣛⣿⣻⣻⣿⣿⣿⣿⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣛⣻⣟⣻⣿⣛⣛⣛⣻⣟⣃⣾⣿⣿⣿⣿
",
            30);
        RegisterPal(sandie);
        
        
        // Create and register Morty (Professor Jon's Pal)
        Pal morty = new Pal(
            "Morty",
            "Professor Jon's prized digital-type Pal. It appears to be made of pure energy and code, displaying complex algorithms in its movements.",
            @"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⣛⣅⣒⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢇⣚⠉
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠟⠛⠛⣛⡛⠉⣑⠒⠘⠛⠻⠭⠟⠿⠟⠫⠉⠻⠛⠓⠉⠀⣁⣈⣤⣤⣄⣠⣀⣤⣤⣤
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠟⠛⠛⣛⡛⠉⣑⠒⠘⠛⠻⠭⠟⠿⠟⠫⠉⠻⠛⠓⠉⠀⣁⣈⣤⣤⣄⣠⣀⣤⣤⣤
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠯⡀⠀⣠⣾⣿⡿⠁⣰⠂⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡂⣵⣾⣿⡿⠿⠿⠻⠛⠛⠙⠛⠿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣾⣿⣿⣶⡙⠿⢟⠁⢺⠋⠰⡀⠀⠂⠄⠀⠀⠀⠐⣲⣸⣷⡀⠘⡸⣿⣿⠋⠀⠉⠁⠀⣤⠀⠀⠀⠀
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣸⣿⣿⣿⣿⡇⠞⠁⠀⣰⣮⣄⣀⣀⣀⢔⠁⢀⣴⣾⣿⣿⣿⣷⡄⠀⢞⢻⠃⠀⠀⡀⠈⠉⠂⠀⠀⠀
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢼⣿⣿⣿⣿⣧⠀⢀⡐⠩⠽⠋⠝⠛⢃⣠⣴⣾⣿⣿⣿⣿⣿⣿⣿⣦⠄⠈⠀⠀⠀⠐⡶⣶⣶⣶⣶⠆
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⣸⣿⣿⣿⣿⣿⢠⣣⣿⣿⣿⣿⢋⡴⡿⠿⠫⢿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣮⣤⣠⡀⠀⡀⠈⠉⢉⣁⣈
⣿⣿⣿⣿⣿⣿⣿⠿⣿⢿⠿⠟⣸⣽⣿⣿⣿⣿⢸⣿⣿⣿⠟⣁⣿⠋⠀⠀⠀⠀⠉⠉⠉⠉⠛⠿⠿⢿⣿⣿⣿⣯⣃⠂⠒⠒⠂⠀⢸⣿
⠉⣦⣧⣖⡎⡍⣦⣷⣿⣶⣗⠇⠸⣿⣿⣿⣿⣯⢈⢿⣯⣷⣾⣿⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣽⣽⣦⠀⠈⠀⣀⡀⢿
⢸⣿⣿⣽⣿⣅⢸⣿⣿⣿⣿⣿⠊⣿⣿⣿⣿⣿⢸⢇⣽⣿⡿⣿⣣⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⡯⣀⠀⠀⢽⣾
⢸⣟⡿⡻⢿⢿⡊⡫⡿⡿⣟⡟⡁⢏⣿⣿⣿⣿⠄⢸⣿⣿⣧⢱⠿⡿⣆⠀⠀⠀⠀⠀⠀⠀⢀⣤⣶⣿⣿⣿⣿⣿⣿⣿⣿⠋⠀⠀⢸⣿
⣼⡗⢾⣿⣿⡹⡿⠿⣿⣿⣿⣿⣿⣷⣶⣭⣝⢿⠅⣿⣿⣟⣿⡿⣤⢡⢯⣤⣢⣀⣀⣀⣀⡀⠈⣻⠛⣿⣿⣿⣿⣿⣮⡍⠙⠀⠀⠀⣸⣿
⣿⣷⣎⣛⠿⠾⠮⠯⣫⡻⣷⡾⡝⣿⣿⣿⣿⣿⣷⡄⢹⣿⣿⣙⣻⣏⠁⠀⠘⣿⣿⣿⢾⣿⣿⡏⠈⠙⠛⠿⠿⠿⠋⠀⠀⠀⠀⢠⣿⣿
⣿⣿⣿⣿⣵⣵⣠⣤⣤⠄⠈⠸⣜⣾⣿⣿⣿⣿⣿⣟⢼⣿⣿⣿⣾⣿⣶⣤⣀⣈⠉⠁⠈⠉⠁⣀⣤⣾⣿⣿⣷⣀⣀⣠⣤⣴⣾⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣯⡇⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇
",
            25);
        RegisterPal(morty);
        
        // Only Morty and Sandie Pals are defined in this file
        // Wild Pals are defined in Pals_Wild.cs
    }
    
    // Add a Pal to the system
    public static void RegisterPal(Pal pal)
    {
        nameToPal[pal.Name] = pal;
    }
    
    // Add a Pal to the player's collection
    public static void GivePalToPlayer(string palName)
    {
        if (nameToPal.ContainsKey(palName) && !playerPals.Contains(palName))
        {
            playerPals.Add(palName);
            // You could trigger other events here, like updating the UI
        }
    }
    
    // Check if player has a specific Pal
    public static bool PlayerHasPal(string palName)
    {
        return playerPals.Contains(palName);
    }
    
    // Get a Pal by name - returns null if not found
    public static Pal? GetPalByName(string palName)
    {
        if (nameToPal.ContainsKey(palName))
        {
            return nameToPal[palName];
        }
        return null;
    }
    
    // Get all Pals owned by the player
    public static List<Pal> GetPlayerPals()
    {
        List<Pal> pals = new List<Pal>();
        foreach (string palName in playerPals)
        {
            if (nameToPal.ContainsKey(palName))
            {
                pals.Add(nameToPal[palName]);
            }
        }
        return pals;
    }
    
    // Get all available Pals in the game
    public static List<Pal> GetAllPals()
    {
        return new List<Pal>(nameToPal.Values);
    }
    
    // Mark a Pal as fainted (used when a Pal is defeated in battle)
    public static void MarkPalAsFainted(string palName)
    {
        if (playerPals.Contains(palName) && !faintedPals.Contains(palName))
        {
            // Add to fainted list but don't remove from player ownership
            faintedPals.Add(palName);
            
            // Set health to 0 to mark as fainted
            Pal? pal = GetPalByName(palName);
            if (pal != null)
            {
                pal.Health = 0;
            }
        }
    }
    
    // Heal a fainted Pal (used at Pal Center)
    public static void HealFaintedPal(string palName)
    {
        if (faintedPals.Contains(palName))
        {
            faintedPals.Remove(palName);
            
            // Restore the Pal's health to full
            Pal? pal = GetPalByName(palName);
            if (pal != null)
            {
                pal.Health = pal.MaxHealth;
                TextDisplay.TypeLine($"{pal.Name} has been fully healed and can battle again!");
            }
        }
    }
    
    // Heal all fainted Pals (used at Pal Center)
    public static void HealAllFaintedPals()
    {
        // Make a copy of the list to avoid modification during iteration
        List<string> faintedPalsCopy = new List<string>(faintedPals);
        
        foreach (string palName in faintedPalsCopy)
        {
            HealFaintedPal(palName);
        }
        
        TextDisplay.TypeLine("All your Pals have been fully healed!");
    }
    
    // Check if a Pal is fainted
    public static bool IsPalFainted(string palName)
    {
        return faintedPals.Contains(palName);
    }
    
    // Get all player's Pals that are not fainted and can battle
    public static List<Pal> GetHealthyPlayerPals()
    {
        List<Pal> healthyPals = new List<Pal>();
        foreach (string palName in playerPals)
        {
            if (!faintedPals.Contains(palName) && nameToPal.ContainsKey(palName))
            {
                healthyPals.Add(nameToPal[palName]);
            }
        }
        return healthyPals;
    }
    
    // Get all player's Pals that are fainted
    public static List<Pal> GetFaintedPlayerPals()
    {
        List<Pal> allFaintedPals = new List<Pal>();
        foreach (string palName in faintedPals)
        {
            if (nameToPal.ContainsKey(palName))
            {
                allFaintedPals.Add(nameToPal[palName]);
            }
        }
        return allFaintedPals;
    }
}

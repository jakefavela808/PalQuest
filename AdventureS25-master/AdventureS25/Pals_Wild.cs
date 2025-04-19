using System;
using System.Collections.Generic;

namespace AdventureS25;

// This file contains the wild Pals for the game
public static class Pals_Wild
{
    // Initialize wild Pals
    public static void Initialize()
    {
        // 1. LeafyWhip (grass-type)
        Pal leafyWhip = new Pal(
            "BirdMan",
            "A grass-type Pal with sharp scratch attacks. Often found in grasslands and forests. Very friendly once tamed.",
            @"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣤⡤⠄⢀⣠
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢁⡀⠀⠀⠈⠁⠀⠀⠰⣿⠿⠛⢉⣤⣶⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⣴⣿⣧⡀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⣡⣴⣿⣿⣿⣿⣧⡀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢁⣴⣿⣿⣿⣿⣿⣿⣿⣇⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠀⠉⠀⠀⠉⠙⠛⠻⠿⠟⠛⠂⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠀⠀⠀⠀⠀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢁⠄⠀⠀⠀⠀⣠⣾⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢡⠔⠃⠀⠀⠀⣠⣾⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠁⠀⣀⣀⣴⣾⣿⣿⣿⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⢀⣼⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⢀⣾⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⣠⣾⣿⡿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠀⠀⠀⠀⠀⠀⡼⠟⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⠿⠃⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣾⣷⣶⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⢠⡄⢶⣶⣶⣶⣆⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⡟⠁⠀⠀⠀⠀⠀⣠⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡈⣇⠸⣿⣿⣿⣿⠀⣆⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⠏⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⡀⢿⣿⣿⣿⡆⢹⡀⣿⠟⣛⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⡇⠀⢀⡄⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⣧⠘⣉⣉⠛⠛⣀⣁⣠⣾⣿⣷⣄⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣇⣀⣾⣧⣤⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠉⠉⣭⣭⣥⣴⣾⣿⣿⣷⡀⠻⣿⣿⣿⣿⡿⠿⡗⢀⠉⠻⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⣦⣤⣾⣿⣿⣿⣿⣿⣿⣿⠛⢇⡀⡈⠛⠿⣿⣧⣤⣰⠚⠉⣠⠤⠄⠉⢻⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢡⣾⣿⣿⣿⣿⣿⣿⣿⣿⣅⣀⡦⠼⠉⢠⠤⡀⢠⡙⢻⣿⣷⣯⣀⣤⣴⣾⡄⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢦⣭⣛⣛⡛⠛⠻⠿⣿⣿⣿⣿⣧⣤⡏⠉⣤⣿⣿⡿⠀⢻⣿⣿⡿⠿⠛⡫⢀⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣤⣙⣛⡛⠛⠻⠿⠷⢶⠬⢙⡛⠻⠿⠿⠿⠿⠟⢋⣥⠂⠀⠒⢒⣒⣈⣭⣴⣿⣿⣿⣿⣿⣿⣿  
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣤⣄⡁⠐⠘⠓⠒⠛⢉⣩⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
",
            25);
        Pals.RegisterPal(leafyWhip);
        
        // 2. VineSnare (grass-type)
        Pal vineSnare = new Pal(
            "VineSnare",
            "A grass-type Pal that uses its extending vines to trap opponents. Its body resembles a small bush with eyes.",
            @"
     _____
    /     \
   /       \
  /  O   O  \
 |     w     |
 |           |
  \~~~~~~~~/
   \      /
    \___/
     ||||
     ||||
     ||||
            ",
            24);
        Pals.RegisterPal(vineSnare);
        
        // 3. BloomBud (grass-type)
        Pal bloomBud = new Pal(
            "BloomBud",
            "A grass-type Pal with a flower bud on its head. The bud blooms when it's happy, releasing sweet-smelling pollen.",
            @"
     @@@@
    @@@@@@
    @@  @@
     \\//
      ||
     _||_
    / .. \
   |  ..  |
   |      |
    \____/
      ||
     /  \
    /    \
            ",
            22);
        Pals.RegisterPal(bloomBud);
        
        // 4. MossBack (grass-type)
        Pal mossBack = new Pal(
            "MossBack",
            "A grass-type Pal with a thick layer of moss growing on its back. It's very slow but extremely resilient to damage.",
            @"
    _______
   /       \
  /         \
 |  -     -  |
 |     >     |
 |           |
  \_________/
  /MMMMMMMMM\
 |MMMMMMMMMMM|
 |MMMMMMMMMMM|
  \MMMMMMMMM/
     || ||
     || ||
     || ||
            ",
            28);
        Pals.RegisterPal(mossBack);
        
        // 5. SeedShooter (grass-type)
        Pal seedShooter = new Pal(
            "SeedShooter",
            "A grass-type Pal that can fire seeds at high velocity. These seeds can grow into plants that trap opponents.",
            @"
      ____
     /    \
    / ^  ^ \
   |  ----  |
  /|        |\
 / |        | \
/==|        |==\
    \______/
      |  |
     /|  |\
    / |  | \
      |  |
      |  |
      \__/
            ",
            23);
        Pals.RegisterPal(seedShooter);
    }
}

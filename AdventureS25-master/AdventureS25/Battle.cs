using System;
using System.Collections.Generic;

namespace AdventureS25
{
    public enum BattleState { Start, PlayerTurn, PalTurn, Won, Lost }

    public class Battle
    {
        public Pal Opponent { get; set; }
        public int OpponentHP { get; set; }
        public int PlayerHP { get; set; }
        public BattleState State { get; set; }
        private bool defendActive = false;
        private int potions = 1;

        private bool easierToTameAnnounced = false;
        private double catchChance = 0.35;
        private int maxOpponentHP = 30;

        private Pal playerPal; // The selected Pal for this battle

        public Battle(Pal opponent)
        {
            Opponent = opponent;
            OpponentHP = maxOpponentHP;
            PlayerHP = 30;
            State = BattleState.Start;
        }

        public void StartBattle()
        {
            Console.Clear();
            State = BattleState.PlayerTurn;
            Console.WriteLine("\n============================");
            Console.WriteLine("A battle is about to begin!");

            // Pal selection logic
            if (Player.CaughtPals != null && Player.CaughtPals.Count > 1)
            {
                Console.WriteLine("Choose which Pal to use for this battle:");
                for (int i = 0; i < Player.CaughtPals.Count; i++)
                {
                    var pal = Player.CaughtPals[i];
                    Console.WriteLine($"[{i + 1}] {pal.Name} - {pal.Description}");
                }
                int choice = 0;
                while (true)
                {
                    string input = CommandProcessor.GetInput();
                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= Player.CaughtPals.Count)
                    {
                        playerPal = Player.CaughtPals[choice - 1];
                        break;
                    }
                    Typewriter.Print("Invalid choice. Please enter a valid number.\n");
                }
            }
            else if (Player.CaughtPals != null && Player.CaughtPals.Count == 1)
            {
                playerPal = Player.CaughtPals[0];
                Console.WriteLine($"Using your only Pal: {playerPal.Name} - {playerPal.Description}");
            }
            else
            {
                playerPal = null;
                Console.WriteLine("You have no Pals to use in battle!");
            }

            string playerName = playerPal != null ? playerPal.Name : "You";
            string playerArt = playerPal != null && !string.IsNullOrWhiteSpace(playerPal.AsciiArt) ? playerPal.AsciiArt : "";
            string playerDesc = playerPal != null ? playerPal.Description : "A brave trainer ready for battle!";
            string playerStats = playerPal != null ? playerPal.GetBattleStatsString(PlayerHP) : $"HP {PlayerHP}";

            // Opponent info
            string oppName = Opponent.Name;
            string oppArt = !string.IsNullOrWhiteSpace(Opponent.AsciiArt) ? Opponent.AsciiArt : "";
            string oppDesc = !string.IsNullOrWhiteSpace(Opponent.Description) ? Opponent.Description : "A wild opponent appears!";
            string oppStats = Opponent.GetBattleStatsString(OpponentHP);

            // Print player Pal info
            Console.WriteLine($"{playerName}");
            if (!string.IsNullOrWhiteSpace(playerArt))
                Console.WriteLine(playerArt);
            Console.WriteLine($"{playerDesc}");
            Console.WriteLine($"{playerStats}");
            Console.WriteLine("\n==================== VS ====================\n");
            // Print opponent Pal info
            Console.WriteLine($"{oppName}");
            if (!string.IsNullOrWhiteSpace(oppArt))
                Console.WriteLine(oppArt);
            Console.WriteLine($"{oppDesc}");
            Console.WriteLine($"{oppStats}");
            Console.WriteLine("\n============================================\n");

        }

        public void PlayerAttack()
        {
            string moveName = playerPal != null && playerPal.Moves.Count > 0 ? playerPal.Moves[0] : "Tackle";
            int atk = playerPal != null ? playerPal.ATK : 10;
            int damage = new Random().Next(atk - 2, atk + 3); // Use Pal's ATK for variability
            if (damage < 1) damage = 1;
            OpponentHP -= damage;
            string palName = playerPal != null ? playerPal.Name : "Your Pal";
            Console.WriteLine($"{palName} used {moveName} on {Opponent.Name} for {damage} damage!");
            CheckEasierToTame();
            if (OpponentHP <= 0)
            {
                State = BattleState.Won;
                return;
            }
            State = BattleState.PalTurn;
        }

        public void PlayerSpecialAttack()
        {
            string moveName = playerPal != null && playerPal.Moves.Count > 1 ? playerPal.Moves[1] : "Special Attack";
            int atk = playerPal != null ? playerPal.ATK : 10;
            int damage = new Random().Next(atk + 3, atk + 8); // Special attack is stronger
            OpponentHP -= damage;
            string palName = playerPal != null ? playerPal.Name : "Your Pal";
            Console.WriteLine($"{palName} used {moveName} on {Opponent.Name} for {damage} damage!");
            CheckEasierToTame();
            if (OpponentHP <= 0)
            {
                State = BattleState.Won;
                return;
            }
            State = BattleState.PalTurn;
        }

        public void PlayerDefend()
        {
            defendActive = true;
            Console.WriteLine("You brace yourself for the next attack. Incoming damage will be reduced.");
            State = BattleState.PalTurn;
        }

        public void Playerpotion()
        {
            if (potions > 0)
            {
                int heal = new Random().Next(12, 20);
                PlayerHP += heal;
                potions--;
                Console.WriteLine($"You use a potion and restore {heal} HP! (potions left: {potions})");
            }
            else
            {
                Console.WriteLine("You have no potions left!");
            }
            State = BattleState.PalTurn;
        }

        public void PlayerTame()
        {
            // Variable chance to tame, increased if OpponentHP < 50% and message shown
            Random rng = new Random();
            if (rng.NextDouble() < catchChance)
            {
                State = BattleState.Won;
                Console.WriteLine($"You tamed {Opponent.Name}!");
                Player.AddPal(Opponent); // Ensure the tamed Pal is added to your collection
            }
            else
            {
                Console.WriteLine($"You tried to tame {Opponent.Name}, but it resisted!");
                State = BattleState.PalTurn;
            }
        }

        private void CheckEasierToTame()
        {
            // Only trigger if below 50% and not already announced
            if (!easierToTameAnnounced && OpponentHP < (maxOpponentHP / 2))
            {
                catchChance = 0.7; // Set to 70%
                easierToTameAnnounced = true;
                Console.WriteLine($"{Opponent.Name} has weakened and is easier to tame!");
            }
            // If OpponentHP goes above 50% again, reset flag and restore base chance
            else if (easierToTameAnnounced && OpponentHP >= (maxOpponentHP / 2))
            {
                easierToTameAnnounced = false;
                catchChance = 0.35; // Reset to base chance
            }
        }

        public void PalAttack()
        {
            string moveName = Opponent.Moves != null && Opponent.Moves.Count > 0 ? Opponent.Moves[0] : "Tackle";
            int atk = Opponent.ATK;
            int damage = new Random().Next(atk - 2, atk + 3);
            if (damage < 1) damage = 1;
            if (defendActive)
            {
                damage /= 2;
                defendActive = false;
                Console.WriteLine("Your defense lessened the damage!");
            }
            PlayerHP -= damage;
            Console.WriteLine($"{Opponent.Name} used {moveName} on you for {damage} damage!");
            if (PlayerHP <= 0)
            {
                State = BattleState.Lost;
                Console.WriteLine("You were defeated!");
                return;
            }
            State = BattleState.PlayerTurn;
        }
    }
}

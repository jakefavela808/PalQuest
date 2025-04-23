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
        private double catchChance = 0.3;
        private int maxOpponentHP = 30;

        public Battle(Pal opponent)
        {
            Opponent = opponent;
            OpponentHP = maxOpponentHP;
            PlayerHP = 30;
            State = BattleState.Start;
        }

        public void StartBattle()
        {
            State = BattleState.PlayerTurn;
            Console.WriteLine("\n============================");
            Console.WriteLine("A battle is about to begin!");

            // Get player's active Pal (first in CaughtPals) or fallback to 'You'
            var playerPal = Player.CaughtPals != null && Player.CaughtPals.Count > 0 ? Player.CaughtPals[0] : null;
            string playerName = playerPal != null ? playerPal.Name : "You";
            string playerArt = playerPal != null && !string.IsNullOrWhiteSpace(playerPal.AsciiArt) ? playerPal.AsciiArt : "";
            string playerDesc = playerPal != null ? playerPal.Description : "A brave trainer ready for battle!";
            string playerStats = $"HP {PlayerHP}  ATK 10  DEF 8 (example)";

            // Opponent info
            string oppName = Opponent.Name;
            string oppArt = !string.IsNullOrWhiteSpace(Opponent.AsciiArt) ? Opponent.AsciiArt : "";
            string oppDesc = !string.IsNullOrWhiteSpace(Opponent.Description) ? Opponent.Description : "A wild opponent appears!";
            string oppStats = $"HP {OpponentHP}  ATK 10  DEF 8 (example)";

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
            Console.WriteLine("Type 'basic', 'special', 'defend', 'potion', 'tame', or 'run'.");
        }

        public void PlayerAttack()
        {
            int damage = new Random().Next(6, 12);
            OpponentHP -= damage;
            Console.WriteLine($"You attack {Opponent.Name} for {damage} damage!");
            CheckEasierToTame();
            if (OpponentHP <= 0)
            {
                State = BattleState.Won;
                Console.WriteLine($"You defeated {Opponent.Name}!");
                return;
            }
            State = BattleState.PalTurn;
        }

        public void PlayerSpecialAttack()
        {
            int damage = new Random().Next(10, 18);
            OpponentHP -= damage;
            Console.WriteLine($"You use a special attack on {Opponent.Name} for {damage} damage!");
            CheckEasierToTame();
            if (OpponentHP <= 0)
            {
                State = BattleState.Won;
                Console.WriteLine($"You defeated {Opponent.Name} with a special move!");
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

        public void PlayerPotion()
        {
            if (potions > 0)
            {
                int heal = new Random().Next(12, 20);
                PlayerHP += heal;
                potions--;
                Console.WriteLine($"You use a Potion and restore {heal} HP! (Potions left: {potions})");
            }
            else
            {
                Console.WriteLine("You have no Potions left!");
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
                catchChance += 0.25; // Increase by 25%
                easierToTameAnnounced = true;
                Console.WriteLine($"{Opponent.Name} has weakened and is easier to tame!");
            }
            // If OpponentHP goes above 50% again, reset flag so message can show again if it drops again
            else if (easierToTameAnnounced && OpponentHP >= (maxOpponentHP / 2))
            {
                easierToTameAnnounced = false;
            }
        }

        public void PalAttack()
        {
            int damage = new Random().Next(4, 10);
            if (defendActive)
            {
                damage /= 2;
                defendActive = false;
                Console.WriteLine("Your defense lessened the damage!");
            }
            PlayerHP -= damage;
            Console.WriteLine($"{Opponent.Name} attacks you for {damage} damage!");
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

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

        public Battle(Pal opponent)
        {
            Opponent = opponent;
            OpponentHP = 30;
            PlayerHP = 30;
            State = BattleState.Start;
        }

        public void StartBattle()
        {
            State = BattleState.PlayerTurn;
            Console.WriteLine($"You face off against {Opponent.Name}!");
            if (!string.IsNullOrWhiteSpace(Opponent.AsciiArt))
                Console.WriteLine(Opponent.AsciiArt);
            Console.WriteLine($"{Opponent.Name} HP: {OpponentHP}");
            Console.WriteLine($"Your HP: {PlayerHP}");
            Console.WriteLine("Type 'basic', 'special', 'defend', 'potion', 'tame', or 'run'.");
        }

        public void PlayerAttack()
        {
            int damage = new Random().Next(6, 12);
            OpponentHP -= damage;
            Console.WriteLine($"You attack {Opponent.Name} for {damage} damage!");
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
            // 30% chance to tame instantly
            Random rng = new Random();
            if (rng.NextDouble() < 0.3)
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

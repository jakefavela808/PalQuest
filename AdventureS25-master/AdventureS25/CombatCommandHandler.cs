namespace AdventureS25;

public static class CombatCommandHandler
{
    public static Battle CurrentBattle { get; set; }

    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"basic", BasicAttack},
            {"special", SpecialAttack},
            {"defend", Defend},
            {"potion", potion},
            {"tame", Tame},
            {"run", Run},
        };
    
    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> action = commandMap[command.Verb];
            action.Invoke(command);
        }
    }

    private static void BasicAttack(Command command)
    {
        if (CurrentBattle != null)
            CurrentBattle.PlayerAttack();
    }
    
    private static void SpecialAttack(Command command)
    {
        if (CurrentBattle != null)
            CurrentBattle.PlayerSpecialAttack();
    }
    
    private static void Defend(Command command)
    {
        if (CurrentBattle != null)
            CurrentBattle.PlayerDefend();
    }

    private static void potion(Command command)
    {
        if (CurrentBattle != null)
            CurrentBattle.Playerpotion();
    }

    private static void Tame(Command command)
    {
        if (CurrentBattle != null)
            CurrentBattle.PlayerTame();
    }
    
    private static void Run(Command command)
    {
        // 45% chance to escape
        if (CurrentBattle != null)
        {
            Random rng = new Random();
            if (rng.NextDouble() < 0.45)
            {
                Console.WriteLine("You successfully escaped!");
                States.ChangeState(StateTypes.Exploring);
            }
            else
            {
                Console.WriteLine("You tried to run, but couldn't escape!");
                CurrentBattle.State = BattleState.PalTurn;
            }
        }
    }
}
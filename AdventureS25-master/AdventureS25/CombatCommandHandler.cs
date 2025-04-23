namespace AdventureS25;

public static class CombatCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"basic", BasicAttack},
            {"special", SpecialAttack},
            {"defend", Defend},
            {"potion", Potion},
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
        Console.WriteLine("You use a basic attack");
    }
    
    private static void SpecialAttack(Command command)
    {
        Console.WriteLine("You use a special attack");
    }
    
    private static void Defend(Command command)
    {
        Console.WriteLine("You defend it in the face parts");
    }

    private static void Potion(Command command)
    {
        Console.WriteLine("You quaff the potion parts");
    }

    private static void Tame(Command command)
    {
        Console.WriteLine("You tame the Pal");
    }
    
    private static void Run(Command command)
    {
        Console.WriteLine("You flee");
        States.ChangeState(StateTypes.Exploring);
    }
}
namespace AdventureS25;

public static class ConversationCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"talk", Player.Talk},
            {"yes", Yes},
            {"no", No},

            {"completed", _ => Player.ShowCompletedQuests()},
            {"leave", Leave},
        };
    
    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> action = commandMap[command.Verb];
            action.Invoke(command);
        }
    }

    private static void Yes(Command command)
    {

        if (Player.PendingQuestOffer != null)
        {
            Player.AcceptQuest();
        }
        else
        {
            Console.WriteLine("You agreed");
        }
    }
    
    private static void No(Command command)
    {
        if (Player.PendingQuestOffer != null)
        {
            Player.DeclineQuest();
        }
        else
        {
            Console.WriteLine("You are disagreed");
        }
    }

    private static void Leave(Command command)
    {
        Console.Clear();
        States.ChangeState(StateTypes.Exploring);
        Player.Look();
    }
}
namespace AdventureS25;

public static class Game
{
    public static void PlayGame()
    {
        Initialize();
        Console.Clear();
        Console.WriteLine(Player.GetLocationDescription());

        bool isPlaying = true;
        
        while (isPlaying == true)
        {
            Command command = CommandProcessor.Process();
            
            if (command.IsValid)
            {
                if (command.Verb == "exit")
                {
                    Console.WriteLine("Game Over!");
                    isPlaying = false;
                }
                else
                {
                    CommandHandler.Handle(command);
                }
            }
        }
    }

    private static void Initialize()
    {
        Quests.Initialize(); // Ensure quests are loaded first
        Conditions.Initialize();
        States.Initialize();
        Map.Initialize();
        NPCs.Initialize();
        Pals.Initialize();
        Items.Initialize();
        Player.Initialize();
    }
}
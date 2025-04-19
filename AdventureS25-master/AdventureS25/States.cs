namespace AdventureS25;

public static class States
{
    public static State CurrentState { get; set; }
    
    public static StateTypes CurrentStateType { get; set; }
    
    private static Dictionary<StateTypes, State> states = 
        new Dictionary<StateTypes, State>();

    public static void Initialize()
    {
        Add(StateTypes.Exploring);
        Add(StateTypes.Fighting);
        Add(StateTypes.Talking);
        
        ChangeState(StateTypes.Exploring);
    }
    
    public static void Add(StateTypes stateType)
    {
        State newState = new State(stateType);
        states.Add(stateType, newState);
    }

    public static void ChangeState(StateTypes stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            return;
        }
        CurrentState = states[stateType];
        CurrentStateType = stateType;
        
        // Always show available commands when state changes
        ShowAvailableCommands();
    }
    
    // Changes state without displaying available commands
    // This prevents duplicate command display in certain situations
    public static void ChangeStateQuiet(StateTypes stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            return;
        }
        CurrentState = states[stateType];
        CurrentStateType = stateType;
    }
    
    // Shows available commands based on current state
    public static void ShowAvailableCommands()
    {
        Console.WriteLine("\n----- Available Commands -----");
        
        switch (CurrentStateType)
        {
            case StateTypes.Exploring:
                Console.WriteLine("look - Look around your current location");
                Console.WriteLine("go [direction] - Move in a direction (north, south, east, west)");
                Console.WriteLine("talk [npc] - Talk to a character");
                Console.WriteLine("take [item] - Pick up an item");
                Console.WriteLine("pet [pal] - Pet your Pal");
                Console.WriteLine("inventory - Check your inventory");
                Console.WriteLine("pals - Check your Pals");
                Console.WriteLine("help - Show these commands");
                break;
                
            case StateTypes.Talking:
                Console.WriteLine("yes - Accept an offer or respond positively");
                Console.WriteLine("no - Decline an offer or respond negatively");
                Console.WriteLine("help - Show these commands");
                break;
                
            case StateTypes.Fighting:
                Console.WriteLine("attack - Attack with your Pal");
                Console.WriteLine("defend - Take a defensive stance");
                Console.WriteLine("special - Use a special move");
                Console.WriteLine("run - Try to escape from battle");
                Console.WriteLine("pals - Check your Pals' stats");
                Console.WriteLine("help - Show these commands");
                break;
        }
        
        Console.WriteLine("-----------------------------");
    }
}
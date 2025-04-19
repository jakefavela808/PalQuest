namespace AdventureS25;

public static class CombatCommandValidator
{
    // List of valid combat commands
    private static readonly string[] validCommands = new string[]
    {
        "attack", 
        "defend", 
        "special",
        "tame",    // Added tame command
        "run",
        "pals",
        "help"
    };
    
    public static bool IsValid(Command command)
    {
        // Check if the command is in our list of valid commands
        return validCommands.Contains(command.Verb.ToLower());
    }
}
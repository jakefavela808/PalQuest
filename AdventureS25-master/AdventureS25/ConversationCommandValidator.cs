namespace AdventureS25;

public static class ConversationCommandValidator
{
    public static bool IsValid(Command command)
    {
        if (command.Verb == "yes" || command.Verb == "no" || command.Verb == "help")
        {
            return true;
        }
        TextDisplay.TypeLine("Valid options are: yes or no. Type 'help' to see available commands.");
        return false;
    }
}
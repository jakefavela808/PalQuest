namespace AdventureS25;

public static class CombatCommandValidator
{
    public static bool IsValid(Command command)
    {
        if (command.Verb == "basic" || command.Verb == "special" ||
            command.Verb == "defend" || command.Verb == "potion" ||
            command.Verb == "tame" || command.Verb == "run")
        {
            return true;
        }
        return false;
    }
}
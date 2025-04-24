namespace AdventureS25;

public static class LocationNameFormatter
{
    public static string Decorate(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "";
        return $"============ {name} ============";
    }
}

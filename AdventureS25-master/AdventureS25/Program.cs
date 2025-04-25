namespace AdventureS25;

class Program
{
    public static void Main(string[] args)
    {
        // Ensure Unicode/ASCII art displays correctly
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Game.PlayGame();
    }
}
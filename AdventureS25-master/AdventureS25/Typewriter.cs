using System;
using System.Collections.Generic;
using System.Threading;

public static class Typewriter
{
    // Easily editable delay values (in milliseconds) for each punctuation or character type
    public static Dictionary<char, int> DelayMap = new Dictionary<char, int>
    {
        { '.', 300 },   // Period
        { '!', 300 },   // Exclamation
        { '?', 300 },   // Question
        { ',', 200 },   // Comma
        { ';', 200 },   // Semicolon
        { ':', 200 },   // Colon
        { ' ', 15 },    // Space
        // Add more as desired
    };

    public static int DefaultDelay = 20; // Default delay for normal characters

    public static void Print(string text)
    {
        bool skipRequested = false;
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            Console.Write(c);

            if (!skipRequested)
            {
                int delay = DefaultDelay;
                if (DelayMap.TryGetValue(c, out int foundDelay))
                    delay = foundDelay;

                int waited = 0;
                int checkInterval = 10; // ms granularity for key checking
                while (waited < delay)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            skipRequested = true;
                            break;
                        }
                    }
                    Thread.Sleep(checkInterval);
                    waited += checkInterval;
                }
            }
        }
        // Ensure cursor is at the end of the printed text (not in the middle)
        if (text.Length > 0 && text[text.Length - 1] != '\n' && text[text.Length - 1] != '\r')
        {
            // Move cursor to the end of the line
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }
    }
}

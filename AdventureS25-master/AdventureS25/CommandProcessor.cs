namespace AdventureS25;

public static class CommandProcessor
{
    public static Command Process()
    {
        string rawInput = null;
        do
        {
            rawInput = GetInput();
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                // Move cursor up to overwrite the blank line
                int cursorTop = Console.CursorTop;
                if (cursorTop > 0)
                {
                    Console.SetCursorPosition(0, cursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, cursorTop - 1);
                }
                continue;
            }
            break;
        } while (true);

        Command command = Parser.Parse(rawInput);

        Debugger.Write("Verb: [" + command.Verb + "]");
        Debugger.Write("Noun: [" + command.Noun + "]");
        
        bool isValid = CommandValidator.IsValid(command);
        command.IsValid = isValid;

        Debugger.Write("isValid = " + isValid);

        return command;
    }
    
    public static string GetInput(string prompt = "> ")
    {
        while (true)
        {
            Typewriter.Print(prompt);
            // Ensure cursor is placed after prompt
            int promptLength = prompt.Length;
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            // If the prompt did not end with a newline, move cursor to end of prompt
            if (!(prompt.EndsWith("\n") || prompt.EndsWith("\r")))
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
            }
            string input = "";
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine(); // Only print new line after valid input
                        return input;
                    }
                    // If input is blank, reprint prompt on same line
                    Console.SetCursorPosition(prompt.Length, Console.CursorTop); // after prompt
                    input = "";
                    continue;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        int backspaceCursorLeft = Console.CursorLeft;
                        // Prevent erasing the prompt text
                        if (Console.CursorLeft > promptLength)
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }
    }
}
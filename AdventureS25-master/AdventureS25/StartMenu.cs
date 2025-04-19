using System;

namespace AdventureS25
{
    public class StartMenu
    {
        String titleAndLogo = @"
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣠⣶⣤⣶⣿⣿⣷⣶⣦⣤⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⠀⢀⣴⡿⢿⣿⣿⠿⠻⠿⢿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠰⠟⠋⣴⣦⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠟⠛⠛⢿⡟⠛⠿⢦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⣧⠀⠀⠀⠀⠀⣠⡖⠀⠀⢀⣸⡿⠁⠀⠘⠿⣿⣶⣤⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠲⢔⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⣿⡀⠀⠀⠀⢸⡇⠀⢀⣴⣿⣿⠃⠀⠀⠀⠀⢀⣼⣿⣿⣿⣉⠻⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠪⣛⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⡇⠀⠀⠀⠊⠀⣶⣿⣿⣿⣿⠀⠀⠀⠀⣴⣿⣿⣿⡿⠿⠿⢿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⡝⢷⣄⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⣴⣿⠟⠉⠉⢿⠀⠀⠀⣀⣼⣿⣿⣿⣿⣿⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣶⣦⣀⡙⠿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢦⠙⣷⣄⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⣼⠟⠁⠀⠀⠀⠈⣧⢀⣾⣿⣿⣿⣿⣿⣿⣿⡀⢀⣾⣿⣿⣿⣿⠖⠀⠀⠉⠉⠛⠛⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢳⡈⢿⣦⠀⠀⠀
                ⠀⠀⠀⢀⡾⠁⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣼⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠈⢿⣧⠀⠀
                ⠀⠀⣰⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣮⣻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⠘⣿⣇⠀
                ⠀⠀⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠳⢯⣛⣛⣥⣿⣿⣿⣿⣿⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢷⠀⢹⣿⡄
                ⠀⢰⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣹⣿⣿⣿⣿⣿⡟⠁⠀⠀⠉⠂⢄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠸⣿⡇
                ⠀⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣾⣿⠿⠿⠿⣿⣿⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⣿⣿
                ⢸⡇⠀⠀⠀⢠⠞⠓⢄⠀⠀⢀⣴⣿⡟⢱⢆⠀⠀⢀⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠀⠀⣿⣿
                ⣸⡇⠀⢀⡴⠁⠀⠀⢀⣷⣿⣿⣿⣿⡀⠃⠈⠀⢀⢚⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡇⠀⠀⣿⣿
                ⣿⡇⢠⠎⠀⠀⠀⠀⠸⠏⠘⡿⠋⠟⠃⠀⠀⠐⠃⢸⣿⣿⣿⣿⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠀⠀⢸⣿⡏
                ⣿⡇⠎⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢠⠁⠀⠀⠀⠀⠈⡿⣿⣿⣿⣿⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⠀⢀⣿⣿⠃
                ⢹⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⠀⠀⠀⠀⠀⠀⢇⢻⣿⣿⣿⣿⣿⣿⣿⣆⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⠁⠀⢀⣾⣿⠏⠀
                ⠈⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠃⠀⠀⠀⠀⠀⠘⡌⢿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⢠⣿⣿⣿⣿⡃⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⠾⠋⠀⠀⣠⣿⣿⡟⠀⠀
                ⠀⠈⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣦⡙⢿⣿⣿⣿⣿⣿⣿⣷⣤⣄⡀⠉⠙⠻⠿⣷⣤⣀⣀⣀⣤⣤⠶⠞⠋⠁⠀⠀⢀⣴⣿⣿⠏⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣶⣍⡛⠿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⡿⠃⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⣮⣝⠻⢿⣿⣿⡿⢿⡿⠦⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣾⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠻⢿⣿⣷⣦⣽⣿⣷⣤⣤⣦⣤⣤⣤⣤⣤⣶⣾⣿⣿⣿⠿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠛⠛⠿⠿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠛⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀

+===================================================================================================+

   ▄███████▄    ▄████████  ▄█            ████████▄   ███    █▄     ▄████████    ▄████████     ███     
  ███    ███   ███    ███ ███            ███    ███  ███    ███   ███    ███   ███    ███ ▀█████████▄ 
  ███    ███   ███    ███ ███            ███    ███  ███    ███   ███    █▀    ███    █▀     ▀███▀▀██ 
  ███    ███   ███    ███ ███            ███    ███  ███    ███  ▄███▄▄▄       ███            ███   ▀ 
▀█████████▀  ▀███████████ ███            ███    ███  ███    ███ ▀▀███▀▀▀     ▀███████████     ███     
  ███          ███    ███ ███            ███    ███  ███    ███   ███    █▄           ███     ███     
  ███          ███    ███ ███▌    ▄      ███  ▀ ███  ███    ███   ███    ███    ▄█    ███     ███     
 ▄████▀        ███    █▀  █████▄▄██       ▀██████▀▄█ ████████▀    ██████████  ▄████████▀     ▄████▀   
                          ▀                                                                           
+===================================================================================================+ 
";
        public bool Show()
        {
            Console.Clear();
            Console.WriteLine(titleAndLogo);
            TextDisplay.TypeLine("1. Start your journey");
            TextDisplay.TypeLine("2. About game");
            TextDisplay.TypeLine("3. Quit game");
            TextDisplay.TypeLine("");
            Console.Write("> ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartJourney();
                    return true;
                case "2":
                    AboutGame();
                    return Show(); // Return to the main menu instead of exiting
                case "3":
                    QuitGame();
                    return false;
                default:
                    // Silent handling of invalid input - just redisplay the prompt
                    Console.Write("> ");
                    return Show(); // Recursive call to show the menu again
            }
        }

        private void StartJourney()
        {
            Console.Clear();
            string? playerName = "";
            
            // Loop until the player enters a valid name
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Write("What is your name, adventurer?\n> ");
                playerName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Please enter a name to continue.");
                }
            }

            TextDisplay.TypeLine("");
            TextDisplay.TypeLine($@"
Behold, {playerName}! The gods have bestowed upon you a vessel, a body crafted with the utmost precision and delicacy. With it, you shall navigate the vast and mysterious realms that lie ahead.");
            TextDisplay.TypeLine("");
            
            // ASCII art of the character placeholder
            Console.WriteLine(@"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠂⠀⠌⠻⣿⣿⡟⠩⠀⠁⠒⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠄⢠⣿⣷⡀⠐⠘⣿⠁⠀⢸⣿⣦⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⠀⣾⣿⣿⡇⠀⠀⡿⠂⠀⢸⣿⣿⡇⠀⠁⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⢰⣿⣏⣿⣷⠀⠀⡇⠦⠀⣿⣿⣿⣿⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠠⠀⢸⣿⣗⣿⡟⠀⠀⡗⠀⠀⣿⣿⣿⣿⠀⢀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢸⣿⢹⣿⡇⠀⠀⡄⠂⠀⣿⣿⣿⣿⠀⠘⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⢸⣿⡿⣿⡇⠀⠀⣇⠀⠀⣿⣿⣿⣿⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⠀⣿⣇⣿⡇⠀⠀⠃⠀⠀⣿⣿⣿⡇⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⠛⠋⠀⠀⠘⣿⣾⣧⠀⠀⠂⠀⢰⣿⣿⣿⠁⠀⡘⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⡿⢛⠭⠒⢀⣀⣤⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣾⣶⣤⣤⣄⣀⠐⠀⠽⡻⢿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⠟⠅⠂⢀⣴⣶⣿⣿⣿⣿⣿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢿⣿⣿⣿⣿⣿⣶⣤⡈⠐⢝⢻⣿⣿⣿⣿
⣿⣿⣿⡿⠑⠁⣠⣾⣿⣿⣿⣿⡿⠟⢉⣤⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣤⣌⠙⠻⣿⣿⣿⣿⣿⣦⡀⠑⠝⣿⣿⣿
⣿⣿⠟⠀⢀⣴⣿⣿⣿⣿⣿⠟⢁⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠈⢿⣿⣿⣿⣿⣿⣆⠀⠈⢿⣿
⣿⠏⠀⢀⣾⣿⣿⣿⣿⣿⡏⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⢻⣿⣿⣿⣿⣿⣆⠈⠈⣿
⡟⠀⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⣤⢤⠈⢻⣿⣿⣿⣿⣿⣿⣿⠋⢠⣤⡌⠻⣿⣿⣿⣧⣾⣿⣿⣿⣿⣿⣿⡄⢰⢸
⡇⠁⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⢈⣠⠀⢸⣿⣿⣿⣿⣿⣿⣿⠀⠠⣥⡄⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠈
⠀⠀⢸⣿⣿⣿⣿⣿⣿⠋⠿⠻⡿⢹⢻⣷⣦⣤⣴⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣤⣴⣿⢿⡟⣿⣿⡿⢿⣿⣿⣿⣿⣿⣿⠀⠀
⠀⠀⢸⣿⣿⣿⣿⣿⡇⢠⠃⣰⠁⡀⢀⣿⣿⣿⣿⣿⠿⣿⡏⠁⣼⣿⢿⣿⣿⣿⣿⠁⢠⠁⡘⠀⠁⢨⣿⣿⣿⣿⣿⡏⠀⠀
⡆⠄⠘⣿⣿⣿⣿⣿⣷⣿⣶⣿⣾⣷⣾⣿⣿⣿⣿⣿⣦⣌⣡⣤⣈⣡⣴⣿⣿⣿⣿⣶⣿⣤⣧⣾⣤⣾⣿⣿⣿⣿⣿⠃⠀⣰
⣷⡀⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⣉⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⢠⢠⣿
⣿⣿⣄⢀⠘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⡐⣴⣿⣿
⣿⣿⣿⣧⡕⠀⠉⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⠉⣀⠀⢽⣾⣿⣿⣿
⣿⣿⣿⣿⣿⡇⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⣿⡄⠀⢸⣿⣿⣿⣿
⣿⣿⣿⣿⣿⡧⠀⠨⣿⡀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⣿⡇⠀⢸⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣧⠀⡀⠿⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⠻⠁⠀⣾⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣷⣤⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢀⣤⣾⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⠂⠀⣸⣇⠀⠉⠉⠛⠛⠛⠛⠿⠿⠿⠿⠿⠿⠿⠿⠟⠛⠛⠛⠋⠉⢙⣳⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⢸⡇⠀⢨⣤⣼⣦⣼⣧⣤⣠⣀⣒⣂⣀⣀⣤⣠⣼⣬⣥⡄⠀⢸⡿⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣠⢀⠁⣂⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⡀⡈⠁⣀⣾⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("");
            TextDisplay.TypeLine("Fuck..... It's not perfect, but it does the job.");
            TextDisplay.TypeLine("Press any key to start your adventure.");
            Console.Write("> ");
            Console.ReadKey();
        }

        private void AboutGame()
        {
            Console.Clear();
            Console.WriteLine(titleAndLogo);
            TextDisplay.TypeLine("");
            TextDisplay.TypeLine("PalQuest is an adventure game where you explore a");
            TextDisplay.TypeLine("mysterious world, battle enemies, and discover");
            TextDisplay.TypeLine("treasures throughout your journey.");
            TextDisplay.TypeLine("");
            TextDisplay.TypeLine("Press any key to return to the main menu.");
            Console.Write("> ");
            Console.ReadKey();
        }

        private void QuitGame()
        {
            Console.Clear();
            TextDisplay.TypeLine("Thank you for playing PalQuest!");
            TextDisplay.TypeLine("Press any key to exit.");
            Console.Write("> ");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

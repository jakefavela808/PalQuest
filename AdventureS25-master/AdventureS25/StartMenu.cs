using System;

namespace AdventureS25
{
    public class StartMenu
    {
        public bool Show()
        {
            Console.Clear();
            TextDisplay.TypeLine(@"
                            ⢀⣀⣀⣀⣀⣀⣠⣼⠀⠀⠀⠀⠈⠙⡆⢤⠀⠀⠀⠀⠀⣷⣄⣀⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣴⣾⣿⣿⣿⣿⣿⣿⡿⢿⡷⡆⠀⣵⣶⣿⣾⣷⣸⣄⠀⠀⠀⢰⠾⡿⢿⣿⣿⣿⣿⣿⣿⣷⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⣿⣿⣿⣿⣽⣿⣿⣿⣿⡟⠀⠀⠀⠀⣾⣿⣿⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⢀⡾⣻⣵⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⠐⣻⣿⣿⡏⢹⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣮⣟⢷⡀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢿⣿⣿⣿⡄⠀⠀⠀⠀⢻⣿⣿⣷⡌⠸⣿⣾⢿⡧⠀⠀⠀⠀⠀⢀⣿⣿⣿⡿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⣠⣾⡿⢛⣵⣾⣿⣿⣿⣿⣿⣯⣾⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⢻⣿⣿⣿⣶⣌⠙⠋⠁⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣷⣽⣿⣿⣿⣿⣿⣷⣮⡙⢿⣿⣆⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⣰⡿⢋⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣿⣿⣿⣿⣧⡀⠀⠀⠀⣠⣽⣿⣿⣿⣿⣷⣦⡀⠀⠀⠀⢀⣼⣿⣿⣿⣿⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣝⢿⣇⠀⠀⠀⠀
        ⠀⠀⠀⣴⣯⣴⣿⣿⠿⢿⣿⣿⣿⣿⣿⣿⡿⢫⣾⣿⣿⣿⣿⣿⣿⡦⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⢴⣿⣿⣿⣿⣿⣿⣷⣝⢿⣿⣿⣿⣿⣿⣿⡿⠿⣿⣿⣧⣽⣦⠀⠀⠀
        ⠀⠀⣼⣿⣿⣿⠟⢁⣴⣿⡿⢿⣿⣿⡿⠛⣰⣿⠟⣻⣿⣿⣿⣿⣿⣿⣿⡿⠿⠋⢿⣿⣿⣿⣿⣿⠻⢿⣿⣿⣿⣿⣿⣿⣿⣟⠻⣿⣆⠙⢿⣿⣿⡿⢿⣿⣦⡈⠻⣿⣿⣿⣧⠀⠀
        ⠀⡼⣻⣿⡟⢁⣴⡿⠋⠁⢀⣼⣿⠟⠁⣰⣿⠁⢰⣿⣿⣿⡿⣿⣿⣿⠿⠀⣠⣤⣾⣿⣿⣿⣿⣿⠀⠀⠽⣿⣿⣿⢿⣿⣿⣿⡆⠈⢿⣆⠀⠻⣿⣧⡀⠈⠙⢿⣦⡈⠻⣿⣟⢧⠀
        ⠀⣱⣿⠋⢠⡾⠋⠀⢀⣠⡾⠟⠁⠀⢀⣿⠟⠀⢸⣿⠙⣿⠀⠈⢿⠏⠀⣾⣿⠛⣻⣿⣿⣿⣿⣯⣤⠀⠀⠹⡿⠁⠀⣿⠏⣿⡇⠀⠹⣿⡄⠀⠈⠻⢷⣄⡀⠀⠙⢷⣄⠙⣿⣎⠂
        ⢠⣿⠏⠀⣏⢀⣠⠴⠛⠉⠀⠀⠀⠀⠈⠁⠀⠀⠀⠛⠀⠈⠀⠀⠀⠀⠈⢿⣿⣼⣿⣿⣿⣿⢿⣿⣿⣶⠀⠀⠀⠀⠀⠁⠀⠛⠀⠀⠀⠀⠁⠀⠀⠀⠀⠉⠛⠦⣄⣀⣹⠀⠹⣿⡄
        ⣼⡟⠀⣼⣿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠛⠛⠛⠋⠁⠀⢹⣿⣿⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⢿⣧⠀⢻⣷
        ⣿⠃⢰⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣰⣶⣦⣤⠀⠀⣿⡿⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢻⡆⠘⣿
        ⣿⠀⢸⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⡟⠁⠈⢻⣷⣸⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣧⠀⣿
        ⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣷⣀⣀⣸⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⣿
        ⢸⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠛⣿⡿⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⡇
        ⠈⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠁
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢷⣴⡿⣷⠀⠀⢰⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠴⡿⣟⣿⣿⣶⡶⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ");
            Console.WriteLine(@"
+==============================================================================================+

   ▄███████▄    ▄████████  ▄█       ████████▄   ███    █▄     ▄████████    ▄████████     ███     
  ███    ███   ███    ███ ███       ███    ███  ███    ███   ███    ███   ███    ███ ▀█████████▄ 
  ███    ███   ███    ███ ███       ███    ███  ███    ███   ███    █▀    ███    █▀     ▀███▀▀██ 
  ███    ███   ███    ███ ███       ███    ███  ███    ███  ▄███▄▄▄       ███            ███   ▀ 
▀█████████▀  ▀███████████ ███       ███    ███  ███    ███ ▀▀███▀▀▀     ▀███████████     ███     
  ███          ███    ███ ███       ███    ███  ███    ███   ███    █▄           ███     ███     
  ███          ███    ███ ███▌    ▄ ███  ▀ ███  ███    ███   ███    ███    ▄█    ███     ███     
 ▄████▀        ███    █▀  █████▄▄██  ▀██████▀▄█ ████████▀    ██████████  ▄████████▀     ▄████▀   
                          ▀                                                                     
+==============================================================================================+ 
");
            TextDisplay.TypeLine("");
            TextDisplay.TypeLine("1. Start your journey");
            TextDisplay.TypeLine("2. About game");
            TextDisplay.TypeLine("3. Quit game");
            TextDisplay.TypeLine("");
            Console.Write("Enter your choice (1-3): ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartJourney();
                    return true;
                case "2":
                    AboutGame();
                    return false;
                case "3":
                    QuitGame();
                    return false;
                default:
                    TextDisplay.TypeLine("Invalid option. Please try again.");
                    Console.ReadKey();
                    return false;
            }
        }

        private void StartJourney()
        {
            Console.Clear();
            Console.Write("What is your name, adventurer? ");
            string? playerName = Console.ReadLine();

            TextDisplay.TypeLine("");
            TextDisplay.TypeLine($@"
Behold, {playerName}! The gods have bestowed upon you a vessel, a body crafted with divine precision and purpose. With it, you shall navigate the vast and mysterious realms that lie ahead.");
            TextDisplay.TypeLine("");
            
            // ASCII art of the character placeholder
            TextDisplay.TypeLine(@"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠛⠛⠛⠛⠛⠛⠛⠛⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⢋⣁⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣶⣦⣄⣉⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⠟⢋⣠⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⡈⠻⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⡿⠋⣠⣴⣿⣿⣿⣿⣿⣿⠿⠛⠋⣉⣉⣉⣉⣉⣉⣉⠙⠛⠿⢿⣿⣿⣿⣿⣦⠈⢿⣿⣿⣿⣿
⣿⣿⣿⠏⢠⣾⣿⣿⣿⣿⡿⠛⢉⣠⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣤⣈⠙⠿⣿⣿⣷⡀⢻⣿⣿⣿
⣿⣿⠃⣰⣿⣿⣿⣿⠟⢉⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⡈⠻⣿⣿⡄⢻⣿⣿
⣿⠏⣰⣿⣿⣿⡿⠁⣴⣿⣿⡿⢛⣉⣭⣭⣭⣭⣽⣿⣿⣿⣿⣿⣿⣿⣿⢋⣩⣍⣛⠻⢆⠘⢿⣷⠘⣿⣿
⡟⢠⣿⣿⣿⠏⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣦⠈⢿⡆⢻⣿
⡇⣸⣿⣿⠏⢠⣿⣿⣿⣿⠟⣋⣥⣶⣶⣶⣤⣍⠻⣿⣿⣿⣿⣿⡿⢋⣥⣴⣶⣶⣦⣬⡙⢿⣇⠘⣧⠸⣿
⠀⣿⣿⡟⢠⣿⣿⣿⡿⢁⣾⣿⣿⣿⣿⣿⣿⣿⣷⣌⢻⣿⣿⠏⣴⣿⣿⣿⣿⣿⣿⣿⣿⣦⠹⡄⢻⠀⣿
⠀⣿⣿⠃⣾⣿⣿⣿⠁⣿⣿⡟⠻⣿⣿⣿⣿⣿⣿⣿⡄⣿⡏⣸⣿⣿⠛⢻⣿⣿⣿⣿⣿⣿⣇⢡⢸⠀⣿
⠀⣿⣿⠀⣿⣿⣿⣿⠀⣿⣿⣷⣾⣿⣿⣿⣿⣿⣿⣿⠇⣿⡇⢹⣿⣿⣶⣾⣿⣿⣿⣿⣿⣿⡿⢸⠘⠀⣿
⡆⢻⣿⠀⣿⣿⣿⣿⣧⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⣰⣿⣷⡈⢿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⣾⣤⡀⢻
⣇⠘⣿⠀⠛⣿⣿⣿⣿⣷⣌⠻⢿⣿⣿⣿⡿⠟⣩⣴⣿⣿⣭⣝⠂⡙⠿⣿⣿⣿⣿⠿⢋⣴⣿⣿⣿⣧⠈
⣿⡄⢁⣴⣿⣿⣿⣿⣿⣿⣿⣙⡲⢶⠶⠶⢶⣿⣿⣿⣧⡙⠿⠿⣠⣯⣕⣲⣶⣶⣀⣾⣿⣿⣿⣿⣿⡿⠀
⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠛⢁⣼
⣿⣧⠈⢿⣿⣿⡿⢿⣿⣿⣿⣿⣿⣿⣿⡿⠋⢡⣶⠂⣿⣿⠐⣶⡄⣉⠻⣿⣿⣿⣿⣿⣿⣿⣿⠁⣶⣿⣿
⣿⣿⣷⣤⣉⣉⠐⣿⣿⣿⣿⣿⣿⣿⡿⢠⣿⣮⣩⣶⣦⡴⣶⣬⣴⣿⣦⠘⣿⣿⣿⣿⣿⣿⠃⣸⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣧⠈⢿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⢋⣥⡐⢶⡌⢻⣿⣿⠀⣿⣿⣿⣿⡿⠁⣴⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣷⡈⠻⣿⣿⣿⣿⣷⡈⠿⡿⠁⠛⣛⣛⣀⠒⠿⠿⠏⣠⣿⣿⡿⠋⣠⣾⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡈⠻⢿⣿⣿⣿⣶⣶⣾⣿⣿⣿⣿⣿⣷⣶⣿⣿⠿⠋⣠⣶⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣄⡉⠛⠿⢿⣿⣿⣿⣿⣿⣿⡿⠿⠛⢉⣠⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣦⣤⣤⣄⣤⣤⣤⣤⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("");
            TextDisplay.TypeLine("This build might not be perfect, but it does the job.");
            TextDisplay.TypeLine("Press any key to start your adventure...");
            Console.ReadKey();
        }

        private void AboutGame()
        {
            Console.Clear();
            TextDisplay.TypeLine(@"
+=========================================================================+
 ▄▄▄· ▄▄▄▄·       ▄• ▄▌▄▄▄▄▄     ▄▄▄· ▄▄▄· ▄▄▌  .▄▄▄  ▄• ▄▌▄▄▄ ..▄▄ · ▄▄▄▄▄
▐█ ▀█ ▐█ ▀█▪▪     █▪██▌•██      ▐█ ▄█▐█ ▀█ ██•  ▐▀•▀█ █▪██▌▀▄.▀·▐█ ▀. •██  
▄█▀▀█ ▐█▀▀█▄ ▄█▀▄ █▌▐█▌ ▐█.▪     ██▀·▄█▀▀█ ██▪  █▌·.█▌█▌▐█▌▐▀▀▪▄▄▀▀▀█▄ ▐█.▪
▐█ ▪▐▌██▄▪▐█▐█▌.▐▌▐█▄█▌ ▐█▌·    ▐█▪·•▐█ ▪▐▌▐█▌▐▌▐█▪▄█·▐█▄█▌▐█▄▄▌▐█▄▪▐█ ▐█▌·
 ▀  ▀ ·▀▀▀▀  ▀█▄▀▪ ▀▀▀  ▀▀▀     .▀    ▀  ▀ .▀▀▀ ·▀▀█.  ▀▀▀  ▀▀▀  ▀▀▀▀  ▀▀▀ 
+=========================================================================+
");
            TextDisplay.TypeLine("");
            TextDisplay.TypeLine("PalQuest is an adventure game where you explore a");
            TextDisplay.TypeLine("mysterious world, battle enemies, and discover");
            TextDisplay.TypeLine("treasures throughout your journey.");
            TextDisplay.TypeLine("");
            TextDisplay.TypeLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        private void QuitGame()
        {
            Console.Clear();
            TextDisplay.TypeLine("Thank you for playing PalQuest!");
            TextDisplay.TypeLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

namespace AdventureS25;

public static class NPCs
{
    private static Dictionary<string, NPC> nameToNPC = new Dictionary<string, NPC>();
    
    public static void Initialize()
    {
        // Create NPCs with ASCII art representation
        NPC nurse = new NPC(
            "Nurse", 
            "A kind nurse with a calm demeanor who helps heal injured Pals.",
            "The Nurse is here, ready to help with your Pals.",
            @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣴⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⠂⢲⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⠾⠿⠿⠿⠻⠿⠿⠿⠿⠿⠟⠻⠿⠛⠂⠘⢸⣿⣿⣷⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠠⡄⣀⣀⣀⣀⣀⣀⠀⣀⡀⣀⡠⠄⢤⠀⢢⣤⣿⣿⣿⣿⣿⡷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣶⣿⣾⣿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⡗⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⣿⣿⠋⠱⣿⡍⠘⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠏⢀⣶⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣻⣿⣣⢀⢀⠿⣀⣀⢸⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⠁⢴⣽⣿⡿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢿⣿⠡⠜⠹⣦⠋⠩⢸⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠛⠀⣠⣾⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣧⡀⠰⣿⡂⢠⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⠀⢠⣼⣺⣿⣿⣿⣿⣿⣿⣿⣿⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⣿⣿⣿⣿⣦⣭⡴⠿⠿⠿⠿⠿⠟⠛⠛⠙⡋⣉⠉⠀⡠⣴⣾⣿⣳⣽⣞⣿⣿⣿⣿⣿⣪⠛⢿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⡿⠋⢉⣁⣁⣠⣤⣤⣴⣶⣶⣶⣾⡿⠿⠛⠛⢉⣥⠀⠨⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⡈⠣⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠉⠀⣠⣴⣿⣿⣟⢧⠀⠲⢽⡿⠟⢛⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡤⠄⠀⠀⠀⠀⠀⢻⡿⠿⠿⠿⠻⣿⣿⣿⣿⠁⢰⣿⣿⢿⢻⣿⣿⣿⠇⠀⠀⠠⠶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠰⠈⡀⠄⢠⠀⠀⠀⢤⠀⢀⣀⣤⣤⣄⡤⣿⣿⣿⣿⣿⣿⣿⣾⣿⠿⠏⠀⠀⣠⡀⠀⢶⡀⢿⢿⣿⣿⣿⣿⣿⣿⣿⣿⢷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢀⡶⠒⠒⡀⢂⠁⠜⡀⠀⠀⠀⢸⣷⠌⠋⢀⡉⢉⡑⠻⡒⢿⣿⣿⣷⣄⣤⣤⣤⣤⣶⣿⣿⣿⣦⡀⠘⠳⣤⣋⠟⣿⣿⣿⣿⣿⣿⡏⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢻⠁⠆⡑⠨⠄⢊⠀⠄⠀⠀⠀⠈⢁⠀⢰⣿⡀⠈⠁⠠⡄⠺⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡗⠀⢄⡀⠉⠟⢷⡛⣭⡟⡻⣿⣿⢸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠉⠢⠑⠠⢈⠄⠈⠀⠀⠀⠀⠈⢻⣷⠀⠛⠿⠦⣠⣼⠇⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠠⢎⢼⣡⢾⣕⣿⡎⡛⣿⣌⢻⡠⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠀⠀⠀⠀⠀⠸⣶⠄⠈⢿⣿⣿⣿⣶⣦⣤⣽⣿⣿⣿⣿⣿⠿⣿⣿⣿⣷⠇⢀⣅⣴⣫⣿⣿⣿⣿⣿⣶⣴⢿⣦⢏⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠞⣇⠠⠀⠻⣝⣿⣿⣿⣯⠉⢈⣀⠌⠉⠀⣴⣿⣿⣿⠋⠀⢌⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣤⣤⣤⣤⣤⣤⣠⣄⣀⠁⢀⡈⠛⠿⣿⣿⣿⣤⡀⢀⢀⣴⣿⣿⠟⠁⠠⡜⣾⢹⢫⣿⣿⣿⣿⣿⢿⡝⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢑⡚⠿⢿⣿⣿⣿⣿⣷⡀⠉⡷⣄⡀⠉⠛⠽⣿⡿⣿⣿⠟⢁⠀⠰⢡⢳⡏⢦⣿⣿⣿⣿⢯⠓⢀⣄⣐⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⢞⢿⣿⣿⣾⠿⣻⣿⣿⡿⡃⠀⠝⡁⠂⠁⢀⡀⠀⠈⢉⣁⣠⡍⠀⠎⣘⣹⠰⢸⣿⣿⣿⠋⢴⢄⣹⣯⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠴⠚⣡⡾⣦⡙⠟⣁⣶⣿⠿⠋⢅⠁⣀⠀⠌⢀⡴⠟⠋⣄⣴⣿⣿⣿⣇⠀⢚⠤⢽⠘⢬⣿⣿⣏⠰⢸⣞⣼⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣼⠋⣠⣿⠉⣤⡟⠉⠀⠀⠈⠀⣠⡟⠀⢀⣀⡀⢠⢀⣀⠀⠈⠙⣿⠙⠀⠈⢸⠀⢋⣈⢻⣿⣏⠘⢸⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠘⠋⠀⠚⠉⠠⣒⠁⠐⣶⣿⡟⠀⠀⠀⠁⠅⠂⠭⠔⠛⠂⠀⠀⣠⡺⣄⠈⠞⣬⢂⣈⢿⣿⡀⢚⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠂⠀⢻⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⡿⠿⠚⠃⠀⠀⠋⠴⣉⠙⢻⡐⢤⡛⣿⣷⡤⠔⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠣⠀⠀⠀⠀⠐⠀⠀⠴⠚⠋⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠀⠁⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
");
        nameToNPC.Add("Nurse", nurse);
        
        NPC professor = new NPC(
            "Professor Jon", 
            "A local professor who studies Pals and their habitats. Seems to be perpetually on the brink of a coding breakthrough... or a mental breakdown.",
            "Professor Jon is examining some research notes.",
            @"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢁⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢃⣿⡏⣿⡿⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⡍⣉⠽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢡⣾⣿⣷⠲⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣷⢻⣷⣮⣝⡻⢿⣿⣿⣿⣿⣿⢣⣿⣿⣿⣿⡄⠀⠀⢸⣿⣿⣿⣿⣿⣿⠿⠛⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣟⡊⣿⣿⣿⣿⣷⣬⣛⠛⣻⢃⣿⣿⣿⣿⣿⣧⠀⠀⠀⣛⣻⣿⠟⣋⣵⡞⠀⣿⣟⣛⠛⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡘⣿⣿⣿⣿⣿⣿⣷⣍⣼⣿⣿⣿⣿⣿⣿⡄⠀⢀⣨⣭⣶⣿⣿⣿⠁⠰⠟⠋⠁⢰⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣷⡹⣿⣿⣿⣿⣿⣿⡿⢟⣛⣫⣭⣽⣛⣻⠷⣾⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡹⣿⣿⣿⢛⣵⣾⣿⣿⣿⣿⣿⣿⣿⣿⣶⣭⡻⣿⣿⣿⣿⠃⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿
⣫⢿⣿⣿⣿⣿⣿⣷⣶⣶⣶⣿⡿⣱⣿⣿⣿⣿⠿⣛⡭⢟⣩⣽⣿⠿⠿⠿⣎⢿⣿⣿⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿
⣿⣷⣮⠛⢿⣿⣿⣿⣿⣿⣿⣿⢱⣿⣿⣟⣻⣴⣭⡷⢟⣻⣭⠷⣞⣛⣯⣥⣾⣦⢻⣿⣦⣤⣤⣄⡀⢉⣬⡉⠙⣿⣿⣿⣿
⣿⣿⣿⣿⣦⡝⢿⣿⣿⣿⣿⡏⡾⢛⣯⣭⣭⡵⠶⢟⣫⣥⣶⢿⣿⣿⣿⣷⣭⡻⣏⢿⣿⣿⣿⠟⣡⡿⠋⠐⠺⠿⠿⣿⣿
⣿⣿⣿⣿⣿⣿⣬⣛⢿⣿⣿⡇⣷⡶⣾⣶⣶⣿⣶⣝⢿⣿⢧⣿⡿⠿⠟⢛⣿⣧⡸⡜⣿⣿⠋⠒⠉⠀⠀⠀⠀⠀⢀⣠⣾
⣿⣿⣿⣿⣿⣿⣿⠿⣃⣼⣿⡇⠟⣼⣿⣿⣿⠿⢟⣛⣃⢿⡄⣶⣾⣿⣶⣿⣿⣿⢃⣇⢿⣿⣷⣄⡀⠀⠀⠀⣠⣶⣿⣿⣿
⣿⣿⡟⠟⣛⣭⣵⣾⣿⣿⣿⣷⢰⢙⣭⣷⣦⣼⣿⣿⡟⣸⣷⣝⠿⣿⣿⣿⠿⣫⣾⣿⠸⣿⣿⣿⡿⢂⣤⠀⠙⠿⢿⣿⣿
⣿⣿⣿⣷⣮⣝⡻⢿⣿⣿⣿⣿⡸⣧⡹⢿⣿⣿⡿⢟⣵⢻⣿⡏⣿⣶⣶⣶⠟⠋⣰⣿⠄⣭⡻⡏⠰⠛⠁⠀⠀⠀⠙⠻⣿
⣿⣿⣿⣿⣿⣿⣦⡄⠈⠙⢿⣿⡇⢿⣿⢷⣶⣶⡾⢟⣽⡇⣿⣿⢸⣦⣤⣤⣴⣾⣿⣿⠀⢸⡿⢠⠀⠀⠀⠀⠀⠀⣠⣴⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⣸⣿⢋⣸⣿⣷⣶⣶⣿⣿⣿⣿⡜⠟⣸⣿⣿⣿⣿⣿⣿⣿⠀⠀⢠⣿⡄⣄⠀⢠⣶⣶⣶⣾⣿
⣿⣿⣿⣿⣿⣿⡿⢟⣩⣾⣿⡇⣿⣧⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠿⠿⠿⠿⣯⡉⠀⡈⠐⠒⠛⠉⠀⠀⠙⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣭⣤⣭⣭⣛⣛⡍⣮⣙⡘⣿⣿⣿⣿⡿⢛⡩⣽⣶⣶⢇⣾⣿⣿⣿⡿⠁⠁⣷⠀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⣁⠀⣿⣿⣷⡹⣇⠻⣵⣾⣿⣿⢶⡹⣿⢹⠿⣿⣿⣿⠏⡀⠈⠉⠁⠀⠀⠰⣶⣶⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣃⣟⣋⡡⠀⡙⣿⣿⣿⣿⣿⣤⣿⣶⣾⣤⣼⡿⠋⠀⠁⠀⠀⠀⠀⢀⣀⣹⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠉⢈⡻⢿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣿⣿⣿⠀⣈⢩⣝⣛⣫⣭⣄⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶⣶⡶⢖⡂⡄⣨⣛⡿⠿⠿⢟⡀⠀⠀⣀⠰⣶⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⣋⣵⣾⡟⣵⠇⣿⣿⣿⣿⣿⣿⣿⡄⣷⡩⣽⡶⣭⡛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿
");
        nameToNPC.Add("Professor Jon", professor);
        
        NPC trainer = new NPC(
            "Rival", 
            "Your childhood rival who is always one step ahead of you. Has a constant smirk that makes you want to defeat them even more.",
            "Your rival is here, looking confident as always.",
            @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣄⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠋⠁⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿⣿⣿⣿⣿⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⡟⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⠃⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⣿⣿⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⡟⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⠟⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣿⣿⣿⣿⣿⣷⣦⣤⣀⡀⠀⠀⠀⠀⠀⢀⣀⣤⣴⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠻⠿⠿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀");
        nameToNPC.Add("Rival", trainer);
        
        // Spawn NPCs after initialization
        SpawnNPCs();
    }
    
    private static void SpawnNPCs()
    {
        // Spawn NPCs in locations
        Map.AddNPC("Nurse", "Pal Center");
        Map.AddNPC("Professor Jon", "Fusion Lab");
        Map.AddNPC("Rival", "Battle Arena");
    }
    
    public static NPC? GetNPCByName(string npcName)
    {
        if (nameToNPC.ContainsKey(npcName))
        {
            return nameToNPC[npcName];
        }
        
        return null;
    }
}

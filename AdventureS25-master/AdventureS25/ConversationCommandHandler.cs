namespace AdventureS25;

public static class ConversationCommandHandler
{
    // Static variables to track the current conversation
    public static NPC? CurrentNPC { get; set; }
    public static bool AwaitingResponse { get; set; } = false;
    public static string OfferType { get; set; } = string.Empty;
    
    // Helper method to properly end a conversation and reset all state
    public static void EndConversation(bool showLocation = true)
    {
        // Clear conversation tracking variables
        CurrentNPC = null;
        AwaitingResponse = false;
        OfferType = string.Empty;
        
        // Change to Exploring state without showing commands yet (prevents duplication)
        States.ChangeStateQuiet(StateTypes.Exploring);
        
        // Simply call Player.Look() in all cases to show the location and commands consistently
        Player.Look();
    }
    
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"leave", Leave},
            {"yes", Yes},
            {"no", No},
            {"accept", Yes},
            {"decline", No},
            {"help", ShowHelp}
        };
    
    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> action = commandMap[command.Verb];
            action.Invoke(command);
            
            // After any command is processed, check if the conversation ended
            if (!AwaitingResponse && CurrentNPC == null)
            {
                // Ensure the state is set back to Exploring
                if (States.CurrentStateType == StateTypes.Talking)
                {
                    States.ChangeState(StateTypes.Exploring);
                }
            }
        }
        else
        {
            TextDisplay.TypeLine("Valid options are: yes, no, or leave.");
        }
    }

    public static void StartConversation(NPC npc)
    {
        // Set state to Talking without automatically showing commands
        States.ChangeStateQuiet(StateTypes.Talking);
        
        // Show commands once manually
        States.ShowAvailableCommands();
        
        CurrentNPC = npc;
        AwaitingResponse = false;
        OfferType = "";
        
        // Display NPC info for all NPCs
        if (npc != null)
        {
            // Display just the ASCII art and detailed description
            npc.DisplayArtAndDescription();
            
            if (npc.Name == "Professor Jon")
            {
                ProfessorJonConversation();
            }
            else if (npc.Name == "Nurse")
            {
                NurseJoyConversation();
            }
            else
            {
                // Generic greeting for other NPCs
                TextDisplay.TypeLine($"{npc.Name} greets you warmly.");
            }
        }
    }
    
    private static void ProfessorJonConversation()
    {
        // Check if player has already spoken to Jon
        if (Conditions.IsTrue(ConditionTypes.HasSpokenToJon))
        {
            // Rick Sanchez-style dismissive responses
            string[] responses = {
                "Jon: \"*BUUURP* I'm in the m-middle of IMPORTANT *burp* Computer Science shit! Can't you see I'm p-programming?! G-go bother someone who gives a shit!\"",
                "Jon: \"Oh great, it's you again. What do you want now? A medal? *burp* Some kind of participation trophy for b-being annoying?\"",
                "Jon: \"*burp* WHAT?! Can't you see I'm b-busy?! The fate of the *burp* multiverse hangs in the balance, and you're here asking for what? T-tech support?! Go play with your *burp* Pal and leave the REAL science to the g-grown-ups!\"",
                "Jon: \"Oh, for f-fuck's sake! If I wanted company, I'd *burp* program myself an AI that doesn't ask stupid questions! *Burp* Unlike you, I've got w-work to do. The greatest mind in the universe doesn't have t-time for hanging out!\""
            };
            
            // Pick a random dismissive response
            Random random = new Random();
            int index = random.Next(responses.Length);
            TextDisplay.TypeLine(responses[index]);
            
            // Use EndConversation method to ensure commands are displayed
            EndConversation(false);
            return;
        }
        
        // First-time conversation
        TextDisplay.TypeLine("Professor Jon looks up from his computer, his eyes squinting slightly as he focuses on you.\n");
        TextDisplay.TypeLine("Jon: \"P-P-Perfect timing! I've been *burp* coding all night, kid! I finally managed to bring Sandie to life! *burp* It's a Pal unlike any other!\"");
        TextDisplay.TypeLine("Jon: \"It's a rare corgi-*burp*-type Pal named Sandie. Th-they're like little furry test subjects but with less screaming, y'know? Wanna s-see it? It's a hundred times better than those *burp* lame government-approved creatures.\"");
        
        // Mark that the player has spoken to Jon
        Conditions.ChangeCondition(ConditionTypes.HasSpokenToJon, true);
        
        AwaitingResponse = true;
        OfferType = "sandie_introduction";
    }

    private static void Yes(Command command)
    {
        if (!AwaitingResponse)
        {
            TextDisplay.TypeLine("You agreed, but to what?");
            return;
        }
        
        if (OfferType == "sandie_introduction")
        {
            TextDisplay.TypeLine("Jon: \"Fuck y-yeahhh! *burp* That's what I'm talking about! Let me introduce you to S-Sandie, the greatest achievement in *burp* Pal Computer Science!\"");
            TextDisplay.TypeLine("Professor Jon takes a swig from his Everclear, belches loudly, then whistles as a small corgi-like Pal bounces into view.");
            Pal? sandie = Pals.GetPalByName("Sandie");
            if (sandie != null)
            {
                sandie.DisplayInfo();
            }
            TextDisplay.TypeLine("Jon: \"I-i-isn't she *burp* wonderful? A hundred times better than any of those m-mass produced Pals! Would you like to t-take Sandie with you on your *burp* journey?\"");
            
            AwaitingResponse = true;
            OfferType = "sandie_offer";
        }
        else if (OfferType == "sandie_offer")
        {
            TextDisplay.TypeLine("Sandie jumps excitedly and follows you now, wagging her little nub tail.");
            
            // Add Sandie to player's pals collection and set the condition
            Pals.GivePalToPlayer("Sandie");
            Conditions.ChangeCondition(ConditionTypes.HasSandiePal, true);
            
            TextDisplay.TypeLine("You've received Sandie the Corgi!");
            
            TextDisplay.TypeLine("Jon: \"That's *burrrp* right! She's all yours now, k-kid! Don't feed her after midnight, and d-don't get her wet unless you want to see some REAL *burp* Computer Science happen!\"");
            
            TextDisplay.TypeLine("Jon: \"But wait! *burp* You think I'm just going to give you a top of the line Pal for free? No way! First, we need to see if you're *burp* worthy! Let's test your Pal's capabilities in a battle!\"");
            
            // End the conversation but don't show location since we're starting battle
            CurrentNPC = null;
            AwaitingResponse = false;
            
            // Set state to Exploring before starting battle
            States.ChangeState(StateTypes.Exploring);
            
            // Start a battle with Jon's Pal
            PalBattle.StartBattle();
        }
        else if (OfferType == "heal_pals")
        {
            // Get the player's Pals and heal them
            var playerPals = Pals.GetPlayerPals();
            bool anyHealed = false;
            
            foreach (var pal in playerPals)
            {
                if (pal.Health < pal.MaxHealth)
                {
                    // Heal the Pal to full health
                    int healAmount = pal.MaxHealth - pal.Health;
                    pal.Health = pal.MaxHealth;
                    TextDisplay.TypeLine($"{pal.Name} was healed for {healAmount} health points!");
                    anyHealed = true;
                }
            }
            
            if (anyHealed)
            {
                TextDisplay.TypeLine("\nNurse: \"Your Pals have been completely restored! We hope to see you again!\"");
            }
            else
            {
                TextDisplay.TypeLine("\nNurse: \"Your Pals are already in perfect health! We hope to see you again!\"");
            }
            
            // End the conversation without showing commands again
            // Clear conversation tracking variables
            CurrentNPC = null;
            AwaitingResponse = false;
            OfferType = string.Empty;
            
            // Change to Exploring state without showing commands
            States.ChangeStateQuiet(StateTypes.Exploring);
            
            // Show the current location which will display commands once
            Player.Look();
        }
    }
    
    private static void No(Command command)
    {
        if (!AwaitingResponse)
        {
            TextDisplay.TypeLine("You declined, but why?");
            return;
        }
        
        if (OfferType == "sandie_introduction")
        {
            TextDisplay.TypeLine("Jon: \"Oh, p-please, *burp* Mr. I'm-A-Snobby-Little-Turd-Who-Doesn't-Want-To-See-Amazing-Computer-Science-Discoveries! Listen here, you tiny d-dickhead, I d-don't give a flying f-fuck what you think! You s-simply *burp* MUST behold the g-glory of Sandie, the most m-majestic Pal the galaxy has ever s-seen! It's a matter of g-galactic importance, and if you don't comply, I'll *burp* fucking kill you!\"");
            Yes(command); // Force the introduction anyway
        }
        else if (OfferType == "sandie_offer")
        {
            TextDisplay.TypeLine("Jon: \"W-what? *BURP* Not take Sandie?! Are you out of your m-mind?! Look at me! I'm Professor Jon, motherfucker! I turned myself into a *burp* professor! I'm PROFESSOR JOOOOON!\"");
            TextDisplay.TypeLine("Jon: \"Listen here, you l-little piece of shit, Sandie has already chosen you, and a Pal's choice is f-final! That's just how the universe *burp* works, kid! Did you think you had a c-choice in this? HAHAHAHA!\"");
            
            // Add Sandie to player's pals collection anyway and set the condition
            Pals.GivePalToPlayer("Sandie");
            Conditions.ChangeCondition(ConditionTypes.HasSandiePal, true);
            TextDisplay.TypeLine("You've received Sandie the Corgi Pal despite your objections!");
            
            TextDisplay.TypeLine("Jon: \"Now you two are s-stuck together! *burp* That's computer science, biiitch!\"");
            TextDisplay.TypeLine("Jon: \"But wait! *burp* You think I'm just going to give you a top of the line Pal for free? No way! First, we need to see if you're *burp* worthy! Let's test your Pal's capabilities in a battle!\"");
            
            // End the conversation but don't show location since we're starting battle
            CurrentNPC = null;
            AwaitingResponse = false;
            
            // Set state to Exploring before starting battle
            States.ChangeState(StateTypes.Exploring);
            
            // Start a battle with Jon's Pal
            PalBattle.StartBattle();
        }
        else if (OfferType == "heal_pals")
        {
            TextDisplay.TypeLine("\nNurse: \"I understand. Please come back if your Pals need healing. We're always here to help!\"");
            
            // End the conversation without showing commands again
            // Clear conversation tracking variables
            CurrentNPC = null;
            AwaitingResponse = false;
            OfferType = string.Empty;
            
            // Change to Exploring state without showing commands
            States.ChangeStateQuiet(StateTypes.Exploring);
            
            // Show the current location which will display commands once
            Player.Look();
        }
    }

    private static void Leave(Command command)
    {
        if (AwaitingResponse && CurrentNPC != null && CurrentNPC.Name == "Professor Jon")
        {
            TextDisplay.TypeLine("Jon: \"W-w-wait! *BUUURP* Where do you think you're going?! We're not done yet, you ungrateful little piece of shit! This is important Computer Science happening right here! Do you have any idea how long it took  me to code this Pal?\"");
            return;
        }
        
        TextDisplay.TypeLine("You end the conversation.");
        
        // End the conversation properly
        EndConversation();
    }
    
    private static void NurseJoyConversation()
    {
        // Check if the player has any Pals
        var playerPals = Pals.GetPlayerPals();
        
        if (playerPals.Count == 0)
        {
            TextDisplay.TypeLine("Nurse: \"Welcome to the Pal Center! I'm afraid I can't help you until you have some Pals to heal. Come back when you've got a Pal companion!\"");
            
            // Use EndConversation but set showLocation to false to avoid showing the location description again
            // This will ensure commands are displayed
            EndConversation(false);
            return;
        }
        
        TextDisplay.TypeLine("Nurse: \"Welcome to the Pal Center! We can restore your Pals to perfect health. Would you like me to heal your Pals?\"");
        
        AwaitingResponse = true;
        OfferType = "heal_pals";
    }
    
    // Shows available commands for conversations
    private static void ShowHelp(Command command)
    {
        TextDisplay.TypeLine("Here are the available commands for conversations:");
        States.ShowAvailableCommands();
    }
    
    // Method to handle the conversation with Professor Jon after the battle
    public static void PostBattleWithJon()
    {
        // Get the professor NPC
        NPC? professor = NPCs.GetNPCByName("Professor Jon");
        if (professor != null)
        {
            // Set current NPC to Professor Jon
            CurrentNPC = professor;
        }
        
        // Post-battle dialogue with Professor Jon
        TextDisplay.TypeLine("Jon: \"*Burp* Well look at that! Y-your Pal did pretty good out there! I knew you had some f-fight in you, kid!\"");
        TextDisplay.TypeLine("Jon: \"Hey, your Sandie looks a little banged up from that b-battle. You should head over to the *burp* Pal Center west of the Verdant Grasslands to get her fixed up!\"");
        
        TextDisplay.TypeLine("Jon: \"The Pal Center is a bright, modern building with a large red and white sign. They can heal up your Sandie to full health. Just talk to the n-nurse at the *burp* counter. I've got some important Computer Science to get back to!\"");
        TextDisplay.TypeLine("Professor Jon stumbles off, taking occasional swigs from his flask as he mutters about 'dimensional portal technology'.");
        
        // Ensure Nurse is at the Pal Center
        Map.AddNPC("Nurse", "Pal Center");
        
        // End the conversation properly
        EndConversation();
    }
}
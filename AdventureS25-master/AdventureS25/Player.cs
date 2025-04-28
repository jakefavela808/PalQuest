using static Typewriter;
namespace AdventureS25;

public static class Player
{
    public static List<Quest> ActiveQuests = new List<Quest>();
    public static List<Quest> DeclinedQuests = new List<Quest>();
    public static List<Quest> CompletedQuests = new List<Quest>();
    // Ensure quest lists are cleared on new game

    public static void AddPal(Pal pal)
    {
        if (pal != null && !CaughtPals.Contains(pal))
        {
            CaughtPals.Add(pal);
            pal.Acquire();
            // Remove pal from current location if present
            if (CurrentLocation.Pals.Contains(pal))
                CurrentLocation.RemovePal(pal);
            if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
            {
                Console.WriteLine($"\n{pal.AsciiArt}");
            }
            Typewriter.Print($"You received {pal.Name}! {pal.Description}\n");
        }
        else
        {
            Typewriter.Print("Pal not found or already owned.\n");
        }
    }

    public static void OfferQuest(Quest quest)
    {
        if (ActiveQuests.Contains(quest) || CompletedQuests.Contains(quest))
            return;
        Console.Clear();
        States.ChangeState(StateTypes.Talking);
        Console.WriteLine(CommandList.conversationCommands);
        Typewriter.Print($"\n=== Quest Offer ===\n");
        Typewriter.Print($"Quest: {quest.Name}\n");
        Typewriter.Print($"{quest.Description}\n");
        Typewriter.Print("\nDo you want to accept this quest?\n");
        PendingQuestOffer = quest;
    }

    public static Quest PendingQuestOffer = null;

    public static void AcceptQuest()
    {
        if (PendingQuestOffer != null)
        {
            if (DeclinedQuests.Contains(PendingQuestOffer))
                DeclinedQuests.Remove(PendingQuestOffer);
            ReceiveQuest(PendingQuestOffer);
            Typewriter.Print($"You accepted the quest: {PendingQuestOffer.Name}");
            PendingQuestOffer = null;
            Console.Clear();
            States.ChangeState(StateTypes.Exploring);
            Look();
        }
    }

    public static void DeclineQuest()
    {
        if (PendingQuestOffer != null)
        {
            DeclinedQuests.Add(PendingQuestOffer);
            Typewriter.Print($"You declined the quest: {PendingQuestOffer.Name}\n");
            PendingQuestOffer = null;
            Console.Clear();
            States.ChangeState(StateTypes.Exploring);
            Look();
        }
    }

    public static void ReceiveQuest(Quest quest)
    {
        if (!ActiveQuests.Contains(quest))
        {
            ActiveQuests.Add(quest);
            CurrentLocation.NewQuestAccepted = quest;
        }
    }

    public static void CompleteQuest(Quest quest, bool returnToTalking = false)
    {
        if (ActiveQuests.Contains(quest) && !quest.IsComplete())
        {
            quest.Complete();
            ActiveQuests.Remove(quest);
            CompletedQuests.Add(quest);
            Typewriter.Print($"\n=== Quest Completed! ===\n");
            Typewriter.Print($"Quest: {quest.Name}\n");
            GrantReward(quest.Reward);
            Console.Clear();
            if (returnToTalking)
                States.ChangeState(StateTypes.Talking);
            else
                States.ChangeState(StateTypes.Exploring);
            Look();
        }
    }

    public static void ShowActiveQuests()
    {
        if (ActiveQuests.Count == 0)
        {
            Typewriter.Print("No active quests.\n");
            return;
        }
        Typewriter.Print("\n=== Active Quests ===\n");
        foreach (var quest in ActiveQuests)
        {
            Typewriter.Print($"Quest: {quest.Name}\n");
            Typewriter.Print($"Description: {quest.Description}\n");
        }
    }

    public static void ShowCompletedQuests()
    {
        if (CompletedQuests.Count == 0)
        {
            Typewriter.Print("No completed quests.\n");
            return;
        }
        Typewriter.Print("=== Completed Quests ===\n");
        foreach (var quest in CompletedQuests)
        {
            Typewriter.Print($"\nQuest: {quest.Name}\n");
            Typewriter.Print($"Description: {quest.Description}\n");
            if (quest.Objectives != null && quest.Objectives.Count > 0)
            {
                Typewriter.Print("Objectives:\n");
                foreach (var obj in quest.Objectives)
                    Typewriter.Print($"- {obj}\n");
            }
            Typewriter.Print($"Reward: {quest.Reward}\n");
        }
    }

    public static void GrantReward(string reward)
    {
        if (string.IsNullOrWhiteSpace(reward))
            return;
        // Try to match an item in the Items database (pseudo-code, adapt as needed)
        var item = Items.GetItemByName(reward);
        if (item != null)
        {
            Inventory.Add(item);
            Typewriter.Print($"You received: {item.Name}\n");
        }
        else if (reward.ToLower().EndsWith("xp"))
        {
            // Example: "100 XP"
            var parts = reward.Split(' ');
            if (parts.Length > 0 && int.TryParse(parts[0], out int xp))
            {
                // Add XP to player (implement XP system as needed)
                Typewriter.Print($"You gained {xp} XP!\n");
            }
        }
        else
        {
            Typewriter.Print($"Reward: {reward}\n");
        }
    }
    public static void GiveQuestByName(string questName)
    {
        var quest = Quests.GetQuestByName(questName);
        if (quest == null)
        {
            Typewriter.Print($"Quest not found: {questName}\n");
            return;
        }
        if (ActiveQuests.Contains(quest))
        {
            Typewriter.Print($"You already have the quest: {quest.Name}\n");
            return;
        }
        if (CompletedQuests.Contains(quest))
        {
            Typewriter.Print($"You have already completed the quest: {quest.Name}\n");
            return;
        }
        ActiveQuests.Add(quest);
        Typewriter.Print($"\n=== New Quest Given! ===\n");
        Typewriter.Print($"Quest: {quest.Name}\n");
        Typewriter.Print($"{quest.Description}\n");
    }
    // ...
    public static void ShowCaughtPals()
    {
        Console.Clear();
        Player.Look();
        
        if (CaughtPals.Count == 0)
        {
            Typewriter.Print("You haven't caught any Pals yet.\n");
            return;
        }
        Typewriter.Print("Your caught Pals:\n");
        foreach (var pal in CaughtPals)
        {
            Typewriter.Print($"\n=== {pal.Name} ===\n");
            if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                Console.WriteLine(pal.AsciiArt);
            Typewriter.Print($"Description: {pal.Description}\n");
            Typewriter.Print($"Stats: {pal.GetBattleStatsString(pal.HP)}\n");
        }
    }
    public static List<Pal> CaughtPals = new List<Pal>();
    public static Location CurrentLocation;
    public static List<Item> Inventory;

    public static void Initialize()
{
    Inventory = new List<Item>();
    CurrentLocation = Map.StartLocation;
    // Starter Pal is now chosen via Professor Jon interaction, not here.

    // Automatically add 'Get Your Starter!' quest if not already active or completed
    var starterQuest = Quests.GetQuestByName("Get Your Starter!");
    if (starterQuest != null && !ActiveQuests.Contains(starterQuest) && !CompletedQuests.Contains(starterQuest))
    {
        ReceiveQuest(starterQuest);
    }
}

    public static void Move(Command command)
    {
        if (CurrentLocation.CanMoveInDirection(command))
        {
            Console.Clear();
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            Typewriter.Print("You can't move " + command.Noun + ".\n");
            Console.Clear();
            Player.Look();
        }
    }

    public static string GetLocationDescription()
    {
        return CurrentLocation.GetDescription();
    }

    public static void Take(Command command)
    {
        // figure out which item to take: turn the noun into an item
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            Typewriter.Print("I don't know what " + command.Noun + " is.\n");
            Console.Clear();
            Player.Look();
        }
        else if (!CurrentLocation.HasItem(item))
        {
            Typewriter.Print("There is no " + command.Noun + " here.\n");
            Console.Clear();
            Player.Look();
        }
        else if (!item.IsTakeable)
        {
            Typewriter.Print("The " + command.Noun + " can't be taked.\n");
            Console.Clear();
            Player.Look();
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            Typewriter.Print("You take the " + command.Noun + ".\n");
            Console.Clear();
            States.ChangeState(StateTypes.Exploring);
            Look();
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Typewriter.Print("You are empty-handed.\n");
            Console.Clear();
            Look();
        }
        else
        {
            Typewriter.Print("You are carrying:\n");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Typewriter.Print(" " + article + " " + item.Name + "\n");
            }
        }
    }

    public static void Look()
    {
        Console.WriteLine(CurrentLocation.GetDescription());
    }

    public static void Drop(Command command)
    {       
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            string article = SemanticTools.CreateArticle(command.Noun);
            Console.WriteLine("I don't know what " + article + " " + command.Noun + " is.");
            Console.Clear();
            Player.Look();
        }
        else if (!Inventory.Contains(item))
        {
            Typewriter.Print("You're not carrying the " + command.Noun + ".\n");
            Console.Clear();
            Player.Look();
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            Typewriter.Print("You drop the " + command.Noun + ".\n");
            Console.Clear();
            Player.Look();
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            Typewriter.Print("** drinking beer\n");
            Conditions.ChangeCondition(ConditionTypes.IsDrunk, true);
            RemoveItemFromInventory("beer");
            AddItemToInventory("beer-bottle");
        }
    }

    public static void AddItemToInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);

        if (item == null)
        {
            return;
        }
        
        Inventory.Add(item);
    }

    public static void RemoveItemFromInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);
        if (item == null)
        {
            return;
        }
        Inventory.Remove(item);
    }

    public static HashSet<string> DefeatedTrainers = new HashSet<string>();
    public static void Talk(Command command)
    {
        if (CurrentLocation.NPCs != null && CurrentLocation.NPCs.Count > 0)
        {
            NPC npc = CurrentLocation.NPCs[0];
            Console.Clear();
            npc.Interact();
            if (npc.IsTrainer && npc.Pals != null && npc.Pals.Count > 0)
            {
                if (DefeatedTrainers.Contains(npc.Name))
                {
                    Typewriter.Print($"Trainer {npc.Name}: You already beat me! Maybe next time...\n");
                    States.ChangeState(StateTypes.Talking);
                    return;
                }
                Typewriter.Print($"Trainer {npc.Name} challenges you to a battle!\n");
                States.ChangeState(StateTypes.Fighting);
                // Pick the first Pal for now (can be extended to multiple)
                var battle = new Battle(npc.Pals[0]);
                battle.StartBattle();
                CombatCommandHandler.CurrentBattle = battle;
                while (battle.State != BattleState.Won && battle.State != BattleState.Lost)
                {
                    if (battle.State == BattleState.PlayerTurn)
                    {
                        Console.WriteLine("Type 'basic', 'special', 'defend', 'potion', 'tame', or 'run'.");
                        string input = CommandProcessor.GetInput().Trim().ToLower();
                        Command combatCommand = new Command();
                        combatCommand.Verb = input;
                        if (CombatCommandValidator.IsValid(combatCommand))
                        {
                            CombatCommandHandler.Handle(combatCommand);
                            if (battle.State == BattleState.Won)
                            {
                                Typewriter.Print($"You defeated Trainer {npc.Name}!\n");
                                States.ChangeState(StateTypes.Exploring);
                                return;
                            }
                            if (States.CurrentStateType == StateTypes.Exploring)
                            {
                                return;
                            }
                        }
                        else
                        {
                            Typewriter.Print("Invalid command. Valid commands are: basic, special, defend, potion, tame, run.\n");
                        }
                    }
                    else if (battle.State == BattleState.PalTurn)
                    {
                        battle.PalAttack();
                    }
                }
                if (battle.State == BattleState.Won)
                {
                    Console.WriteLine($"You defeated Trainer {npc.Name}!");
                    DefeatedTrainers.Add(npc.Name);
                }
                States.ChangeState(StateTypes.Exploring);
                return;
            }
            // Non-trainer NPCs
            Console.WriteLine(CommandList.conversationCommands);
            Typewriter.Print($"You talk to {npc.Name}.\n");
            if (!string.IsNullOrWhiteSpace(npc.AsciiArt))
            {
                Console.WriteLine(npc.AsciiArt);
            }
            Typewriter.Print(npc.Description + "\n");
            if (!string.IsNullOrWhiteSpace(npc.Dialogue))
            {
                Typewriter.Print(npc.Dialogue + "\n");
            }
            // Nurse Noelia quest logic
            if (npc.Name.ToLower().Contains("nurse noelia"))
            {
                var visitNurseQuest = Quests.GetQuestByName("Visit the Nurse!");
                var deliverPotionQuest = Quests.GetQuestByName("Deliver the Potion to Matt!");
                // If player has 'Visit the Nurse!' quest active, heal and offer next quest
                if (visitNurseQuest != null && ActiveQuests.Contains(visitNurseQuest) && !visitNurseQuest.IsComplete())
                {
                    foreach (var pal in CaughtPals)
                    {
                        pal.GetType().GetProperty("HP").SetValue(pal, pal.GetType().GetProperty("HP").GetValue(pal));
                        // pal.CurrentHP = pal.HP;
                    }
                    Console.WriteLine("All your Pals have been fully healed!");
                    CompleteQuest(visitNurseQuest, true);
                    if (deliverPotionQuest != null && !ActiveQuests.Contains(deliverPotionQuest) && !deliverPotionQuest.IsComplete())
                    {
                        OfferQuest(deliverPotionQuest);
                    }
                }
                else
                {
                    foreach (var pal in CaughtPals)
                    {
                        pal.GetType().GetProperty("HP").SetValue(pal, pal.GetType().GetProperty("HP").GetValue(pal));
                    }
                    Console.WriteLine("All your Pals have been fully healed!");
                }
            }

            // Matt quest logic
            if (npc.Name.ToLower().Contains("matt"))
            {
                var deliverPotionQuest = Quests.GetQuestByName("Deliver the Potion to Matt!");
                var potion = Items.GetItemByName("Potion");
                if (deliverPotionQuest != null && ActiveQuests.Contains(deliverPotionQuest) && !deliverPotionQuest.IsComplete())
                {
                    // Check if player has the potion
                    if (Inventory.Contains(potion))
                    {
                        Inventory.Remove(potion);
                        Typewriter.Print("Thank you for bringing me this potion! I feel much better now. Please take this as a token of my gratitude.\n");
                        CompleteQuest(deliverPotionQuest, false); // Stay in Exploring after Matt's quest
                    }
                    else
                    {
                        Typewriter.Print("Do you have my potion?\n");
                    }
                }
                else if (deliverPotionQuest != null && !ActiveQuests.Contains(deliverPotionQuest) && !deliverPotionQuest.IsComplete() && PendingQuestOffer == null)
                {
                    var visitNurseQuest = Quests.GetQuestByName("Visit the Nurse!");
                    if (visitNurseQuest != null && visitNurseQuest.IsComplete())
                    {
                        OfferQuest(deliverPotionQuest);
                    }
                }
            }

            // Starter selection logic for Professor Jon
            if (npc.Name.ToLower().Contains("professor jon") && !CaughtPals.Any())
            {
                // Get all starter pals from Pals
                var starterPals = Pals.GetStarterPals();
                if (starterPals.Count > 0)
                {
                    Typewriter.Print("Jon: Ah, shit! You're just in time, kid! *burp* I've been up all damn night coding these fuckin' Pals into existence! *burp* They're wild, they're unstable, they're probably gonna blow something up, but hell, that's what makes 'em special! Now quit standing there like an idiot and pick your starter!\n");
                    for (int i = 0; i <starterPals.Count; i++)
                    {
                        Typewriter.Print($"[{i + 1}] {starterPals[i].Name} - {starterPals[i].Description}\n");
                    }
                    int choice = -1;
                    while (choice < 1 || choice > starterPals.Count)
                    {
                        string input = CommandProcessor.GetInput();
                        if (!int.TryParse(input, out choice))
                        {
                            Typewriter.Print("Please enter the number of the Pal you want to choose.\n");
                            continue;
                        }
                        if (choice < 1 || choice > starterPals.Count)
                        {
                            Typewriter.Print($"Please enter a number between 1 and {starterPals.Count}.\n");
                        }
                    }
                    var chosenPal = starterPals[choice - 1];
                    AddPal(chosenPal);
                    Typewriter.Print($"\nYou chose {chosenPal.Name} as your starter!\n");
                    // Complete 'Get Your Starter!' quest if active
                    var starterQuest = Quests.GetQuestByName("Get Your Starter!");
                    if (starterQuest != null && ActiveQuests.Contains(starterQuest) && !starterQuest.IsComplete())
                    {
                        CompleteQuest(starterQuest, false); // Stay in Exploring after starter quest
                    }                  // No longer immediately give 'Test Your Battle Skills!' quest here. It will be offered after 'Get Your Starter!' is completed.
                }
                else
                {
                    Typewriter.Print("No starter Pals available!\n");
                }
            }

            npc.OfferQuests();
            States.ChangeState(StateTypes.Talking);
            return;
        }
        else
        {
            Typewriter.Print("There is no one here to talk to.\n");
            Console.Clear();
            Player.Look();
        }
    }

    public static void Battle(Command command)
    {
        if (CurrentLocation.Pals != null && CurrentLocation.Pals.Count > 0)
        {
            if (CaughtPals == null || CaughtPals.Count == 0)
            {
                Typewriter.Print("You don't have any Pals to battle with!\n");
                Console.Clear();
                Player.Look();
                return;
            }
            // Use only the currently spawned wild Pal from the location
            var palField = CurrentLocation.GetType().GetField("currentWildPal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pal = palField?.GetValue(CurrentLocation) as Pal;
            if (pal == null || CaughtPals.Any(c => c.Name == pal.Name))
            {
                Typewriter.Print("There are no wild Pals here to battle!\n");
                Console.Clear();
                Player.Look();
                return;
            }
            Typewriter.Print($"A wild {pal.Name} has appeared!\n");
            States.ChangeState(StateTypes.Fighting);

            // Start the battle
            var battle = new Battle(pal);
            battle.StartBattle();
            // Set the current battle context for the handler
            CombatCommandHandler.CurrentBattle = battle;
            while (battle.State != BattleState.Won && battle.State != BattleState.Lost)
            {
                if (battle.State == BattleState.PlayerTurn)
                {
                    
                    string input = CommandProcessor.GetInput().Trim().ToLower();
                    Command combatCommand = new Command();
                    combatCommand.Verb = input;
                    if (CombatCommandValidator.IsValid(combatCommand))
                    {
                        CombatCommandHandler.Handle(combatCommand);
                        // Tame can win instantly, so check for win
                        if (battle.State == BattleState.Won)
                        {
                            Typewriter.Print($"You defeated {pal.Name}!\n");
                            // Complete 'Test Your Battle Skills!' quest if active and not complete
                            foreach (var quest in ActiveQuests.ToList())
                            {
                                if (quest.Name == "Test Your Battle Skills!" && !quest.IsComplete())
                                {
                                    CompleteQuest(quest);
                                    // After completing, automatically add 'Visit the Nurse!' quest
                                    var visitNurseQuest = Quests.GetQuestByName("Visit the Nurse!");
                                    if (visitNurseQuest != null && !ActiveQuests.Contains(visitNurseQuest) && !visitNurseQuest.IsComplete())
                                    {
                                        ActiveQuests.Add(visitNurseQuest);
                                        CurrentLocation.NewQuestAccepted = visitNurseQuest;
                                    }
                                }
                            }
                            States.ChangeState(StateTypes.Exploring);
                            return;
                        }
                        // Run can escape, so check for exploring state
                        if (States.CurrentStateType == StateTypes.Exploring)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Typewriter.Print("Invalid command. Valid commands are: basic, special, defend, potion, tame, run.\n");
                    }
                }
                else if (battle.State == BattleState.PalTurn)
                {
                    battle.PalAttack();
                }
            }
            if (battle.State == BattleState.Won)
            {
                Typewriter.Print($"You caught {pal.Name}! {pal.Name} is now your Pal.\n");
                if (!CaughtPals.Contains(pal))
                {
                    CaughtPals.Add(pal);
                    if (CurrentLocation.Pals.Contains(pal))
                    {
                        CurrentLocation.RemovePal(pal);
                    }
                    if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                    {
                        Console.WriteLine($"\n{pal.AsciiArt}\n");
                    }
                    Console.WriteLine(pal.Description);
                }
                // Remove from location's available wild Pals so it cannot respawn
                CurrentLocation.RemoveTamedPal(pal);
                // Fallback: always remove from location if present
                if (CurrentLocation.Pals.Contains(pal))
                {
                    CurrentLocation.RemovePal(pal);
                }
                // Complete 'Test Your Battle Skills!' quest if active and not complete
                foreach (var quest in ActiveQuests.ToList())
                {
                    if (quest.Name == "Test Your Battle Skills!" && !quest.IsComplete())
                        CompleteQuest(quest);
                }
            }
            else
            {
                // If the Pal was defeated (not tamed), allow respawn
                CurrentLocation.RespawnWildPal();
            }
            CurrentLocation.Pals.Remove(pal);
            States.ChangeState(StateTypes.Exploring);
        }
        else
        {
            Typewriter.Print("There are no Pals here to battle!\n");
        }
    }

    public static void MoveToLocation(string locationName)
    {
        // look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        // if there's no location with that name
        if (newLocation == null)
        {
            Typewriter.Print("Trying to move to unknown location: " + locationName + ".\n");
            return;
        }
            
        // set our current location to the new location
        CurrentLocation = newLocation;
        
        // print out a description of the location
        Look();
    }
}
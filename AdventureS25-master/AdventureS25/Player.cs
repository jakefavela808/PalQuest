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
            if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
            {
                Console.WriteLine($"\n{pal.AsciiArt}\n");
            }
            Console.WriteLine($"You received {pal.Name}! {pal.Description}");
        }
        else
        {
            Console.WriteLine("Pal not found or already owned.");
        }
    }

    public static void OfferQuest(Quest quest)
    {
        if (ActiveQuests.Contains(quest) || CompletedQuests.Contains(quest))
            return;
        Console.WriteLine($"\n=== Quest Offer ===");
        Console.WriteLine($"Quest: {quest.Name}");
        Console.WriteLine($"{quest.Description}");
        Console.WriteLine("Do you want to accept this quest? (yes/no)");
        States.ChangeState(StateTypes.Talking);
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
            PendingQuestOffer = null;
        }
    }

    public static void DeclineQuest()
    {
        if (PendingQuestOffer != null)
        {
            DeclinedQuests.Add(PendingQuestOffer);
            Console.WriteLine($"You declined the quest: {PendingQuestOffer.Name}");
            PendingQuestOffer = null;
        }
    }

    public static void ReceiveQuest(Quest quest)
    {
        if (!ActiveQuests.Contains(quest))
        {
            ActiveQuests.Add(quest);
            Console.WriteLine("\n=== New Quest Accepted! ===");
            Console.WriteLine($"Quest: {quest.Name}");
            Console.WriteLine($"{quest.Description}\n");
        }
    }

    public static void CompleteQuest(Quest quest)
    {
        if (ActiveQuests.Contains(quest) && !quest.IsComplete())
        {
            quest.Complete();
            ActiveQuests.Remove(quest);
            CompletedQuests.Add(quest);
            Console.WriteLine($"\n=== Quest Completed! ===");
            Console.WriteLine($"Quest: {quest.Name}");
            Console.WriteLine($"Reward: {quest.Reward}");
            GrantReward(quest.Reward);
        }
    }

    public static void ShowActiveQuests()
    {
        if (ActiveQuests.Count == 0)
        {
            Console.WriteLine("No active quests.");
            return;
        }
        Console.WriteLine("=== Active Quests ===");
        foreach (var quest in ActiveQuests)
        {
            Console.WriteLine($"\nQuest: {quest.Name}");
            Console.WriteLine($"Description: {quest.Description}");
            if (quest.Objectives != null && quest.Objectives.Count > 0)
            {
                Console.WriteLine("Objectives:");
                foreach (var obj in quest.Objectives)
                    Console.WriteLine($"- {obj}");
            }
        }
    }

    public static void ShowCompletedQuests()
    {
        if (CompletedQuests.Count == 0)
        {
            Console.WriteLine("No completed quests.");
            return;
        }
        Console.WriteLine("=== Completed Quests ===");
        foreach (var quest in CompletedQuests)
        {
            Console.WriteLine($"\nQuest: {quest.Name}");
            Console.WriteLine($"Description: {quest.Description}");
            if (quest.Objectives != null && quest.Objectives.Count > 0)
            {
                Console.WriteLine("Objectives:");
                foreach (var obj in quest.Objectives)
                    Console.WriteLine($"- {obj}");
            }
            Console.WriteLine($"Reward: {quest.Reward}");
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
            Console.WriteLine($"You received: {item.Name}");
        }
        else if (reward.ToLower().EndsWith("xp"))
        {
            // Example: "100 XP"
            var parts = reward.Split(' ');
            if (parts.Length > 0 && int.TryParse(parts[0], out int xp))
            {
                // Add XP to player (implement XP system as needed)
                Console.WriteLine($"You gained {xp} XP!");
            }
        }
        else
        {
            Console.WriteLine($"Reward: {reward}");
        }
    }
    public static void GiveQuestByName(string questName)
    {
        var quest = Quests.GetQuestByName(questName);
        if (quest == null)
        {
            Console.WriteLine($"Quest not found: {questName}");
            return;
        }
        if (ActiveQuests.Contains(quest))
        {
            Console.WriteLine($"You already have the quest: {quest.Name}");
            return;
        }
        if (CompletedQuests.Contains(quest))
        {
            Console.WriteLine($"You have already completed the quest: {quest.Name}");
            return;
        }
        ActiveQuests.Add(quest);
        Console.WriteLine($"\n=== New Quest Given! ===");
        Console.WriteLine($"Quest: {quest.Name}");
        Console.WriteLine($"{quest.Description}\n");
    }
    // ...
    public static void ShowCaughtPals()
    {
        if (CaughtPals.Count == 0)
        {
            Console.WriteLine("You haven't caught any Pals yet.");
            return;
        }
        Console.WriteLine("Your caught Pals:");
        foreach (var pal in CaughtPals)
        {
            Console.WriteLine($"\n=== {pal.Name} ===");
            if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                Console.WriteLine(pal.AsciiArt);
            Console.WriteLine($"Description: {pal.Description}");
            Console.WriteLine($"Stats: {pal.GetBattleStatsString(pal.HP)}");
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
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            Console.WriteLine("You can't move " + command.Noun + ".");
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
            Console.WriteLine("I don't know what " + command.Noun + " is.");
        }
        else if (!CurrentLocation.HasItem(item))
        {
            Console.WriteLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            Console.WriteLine("The " + command.Noun + " can't be taked.");
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            Console.WriteLine("You take the " + command.Noun + ".");
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("You are empty-handed.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Console.WriteLine(" " + article + " " + item.Name);
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
        }
        else if (!Inventory.Contains(item))
        {
            Console.WriteLine("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            Console.WriteLine("You drop the " + command.Noun + ".");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            Console.WriteLine("** drinking beer");
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
            npc.Interact();
            if (npc.IsTrainer && npc.Pals != null && npc.Pals.Count > 0)
            {
                if (DefeatedTrainers.Contains(npc.Name))
                {
                    Console.WriteLine($"Trainer {npc.Name}: You already beat me! Maybe next time...");
                    States.ChangeState(StateTypes.Talking);
                    return;
                }
                Console.WriteLine($"Trainer {npc.Name} challenges you to a battle!");
                States.ChangeState(StateTypes.Fighting);
                // Pick the first Pal for now (can be extended to multiple)
                var battle = new Battle(npc.Pals[0]);
                battle.StartBattle();
                CombatCommandHandler.CurrentBattle = battle;
                while (battle.State != BattleState.Won && battle.State != BattleState.Lost)
                {
                    if (battle.State == BattleState.PlayerTurn)
                    {
                        Console.Write("> ");
                        string input = Console.ReadLine()?.Trim().ToLower();
                        Command combatCommand = new Command();
                        combatCommand.Verb = input;
                        if (CombatCommandValidator.IsValid(combatCommand))
                        {
                            CombatCommandHandler.Handle(combatCommand);
                            if (battle.State == BattleState.Won)
                            {
                                Console.WriteLine($"You defeated Trainer {npc.Name}!");
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
                            Console.WriteLine("Invalid command. Valid commands are: basic, special, defend, potion, tame, run.");
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
            Console.WriteLine($"You talk to {npc.Name}.");
            if (!string.IsNullOrWhiteSpace(npc.AsciiArt))
            {
                Console.WriteLine(npc.AsciiArt);
            }
            Console.WriteLine(npc.Description);
            if (!string.IsNullOrWhiteSpace(npc.Dialogue))
            {
                Console.WriteLine(npc.Dialogue);
            }

            // Starter selection logic for Professor Jon
            if (npc.Name.ToLower().Contains("professor jon") && !CaughtPals.Any())
            {
                // Get all starter pals from Pals
                var starterPals = Pals.GetStarterPals();
                if (starterPals.Count > 0)
                {
                    Console.WriteLine("\nProfessor Jon: Please choose your starter Pal!");
                    for (int i = 0; i <starterPals.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {starterPals[i].Name} - {starterPals[i].Description}");
                    }
                    int choice = -1;
                    while (choice < 1 || choice > starterPals.Count)
                    {
                        Console.Write("Enter the number of your choice: ");
                        string input = Console.ReadLine();
                        int.TryParse(input, out choice);
                    }
                    var chosenPal = starterPals[choice - 1];
                    AddPal(chosenPal);
                    Console.WriteLine($"\nYou chose {chosenPal.Name} as your starter!");
                    // No longer immediately give 'Test Your Battle Skills!' quest here. It will be offered after 'Get Your Starter!' is completed.
                }
                else
                {
                    Console.WriteLine("No starter Pals available!");
                }
            }

            npc.OfferQuests();
            // Complete 'Get Your Starter!' quest after talking to Professor Jon and after player has a Pal
            foreach (var quest in ActiveQuests.ToList())
            {
                if (npc.Name.ToLower().Contains("professor jon") && quest.Name == "Get Your Starter!" && !quest.IsComplete() && CaughtPals.Any())
                {
                    CompleteQuest(quest);
                    // Immediately offer the new quest: Test Your Battle Skills!
                    var testBattleQuest = Quests.GetQuestByName("Test Your Battle Skills!");
                    if (testBattleQuest != null && !ActiveQuests.Contains(testBattleQuest) && !testBattleQuest.IsComplete())
                    {
                        OfferQuest(testBattleQuest);
                    }
                }
                if (npc.Name.ToLower().Contains("joey") && quest.Name == "Defeat Youngster Joey!" && !quest.IsComplete())
                {
                    CompleteQuest(quest);
                }
            }
            States.ChangeState(StateTypes.Talking);
        }
        else
        {
            Console.WriteLine("There is no one here to talk to.");
        }
    }

    public static void Battle(Command command)
    {
        if (CurrentLocation.Pals != null && CurrentLocation.Pals.Count > 0)
        {
            if (CaughtPals == null || CaughtPals.Count == 0)
            {
                Console.WriteLine("You don't have any Pals to battle with!");
                return;
            }
            Pal pal = CurrentLocation.Pals[0];
            Console.WriteLine($"A wild {pal.Name} appears! You are pulled into battle.");
            if (!string.IsNullOrWhiteSpace(pal.Description))
            {
                Console.WriteLine(pal.Description);
            }
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
                    Console.Write("> ");
                    string input = Console.ReadLine()?.Trim().ToLower();
                    Command combatCommand = new Command();
                    combatCommand.Verb = input;
                    if (CombatCommandValidator.IsValid(combatCommand))
                    {
                        CombatCommandHandler.Handle(combatCommand);
                        // Tame can win instantly, so check for win
                        if (battle.State == BattleState.Won)
                        {
                            Console.WriteLine($"You defeated {pal.Name}!");
                            // Complete 'Test Your Battle Skills!' quest if active and not complete
                            foreach (var quest in ActiveQuests.ToList())
                            {
                                if (quest.Name == "Test Your Battle Skills!" && !quest.IsComplete())
                                    CompleteQuest(quest);
                            }
                            CurrentLocation.Pals.Remove(pal);
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
                        Console.WriteLine("Invalid command. Valid commands are: basic, special, defend, potion, tame, run.");
                    }
                }
                else if (battle.State == BattleState.PalTurn)
                {
                    battle.PalAttack();
                }
            }
            if (battle.State == BattleState.Won)
            {
                Console.WriteLine($"You caught {pal.Name}! {pal.Name} is now your Pal.");
                if (!CaughtPals.Contains(pal))
                {
                    CaughtPals.Add(pal);
                    if (!string.IsNullOrWhiteSpace(pal.AsciiArt))
                    {
                        Console.WriteLine($"\n{pal.AsciiArt}\n");
                    }
                    Console.WriteLine(pal.Description);
                    // Offer Sandie quest if catching Sandie
                    if (pal.Name.ToLower().Contains("Sandie"))
                    {
                        var SandieQuest = Quests.GetQuestByName("Catch a Sandie!");
                        if (SandieQuest != null)
                            OfferQuest(SandieQuest);
                        // Complete 'Catch a Sandie!' quest if active
                        foreach (var quest in ActiveQuests.ToList())
                        {
                            if (quest.Name == "Catch a Sandie!" && !quest.IsComplete())
                                CompleteQuest(quest);
                        }
                    }
                }
                // Complete 'Test Your Battle Skills!' quest if active and not complete
                foreach (var quest in ActiveQuests.ToList())
                {
                    if (quest.Name == "Test Your Battle Skills!" && !quest.IsComplete())
                        CompleteQuest(quest);
                }
            }
            CurrentLocation.Pals.Remove(pal);
            States.ChangeState(StateTypes.Exploring);
        }
        else
        {
            Console.WriteLine("There are no Pals here to battle!");
        }
    }

    public static void MoveToLocation(string locationName)
    {
        // look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        // if there's no location with that name
        if (newLocation == null)
        {
            Console.WriteLine("Trying to move to unknown location: " + locationName + ".");
            return;
        }
            
        // set our current location to the new location
        CurrentLocation = newLocation;
        
        // print out a description of the location
        Look();
    }
}
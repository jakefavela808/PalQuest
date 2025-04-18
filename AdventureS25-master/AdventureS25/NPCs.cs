namespace AdventureS25;

public static class NPCs
{
    private static Dictionary<string, NPC> nameToNPC = new Dictionary<string, NPC>();
    
    public static void Initialize()
    {
        // Create NPCs
        NPC nurse = new NPC("Nurse Joy", 
            "A kind nurse with pink hair who helps heal injured Pals.",
            "Nurse Joy is here, ready to help with your Pals.");
        nameToNPC.Add("Nurse Joy", nurse);
        
        NPC professor = new NPC("Professor Jon", 
            "A local professor who studies Pals and their habitats.",
            "Professor Jon is examining some research notes.");
        nameToNPC.Add("Professor Jon", professor);
        
        NPC trainer = new NPC("Rival", 
            "Your childhood rival who is always one step ahead of you.",
            "Your rival is here, looking confident as always.");
        nameToNPC.Add("Rival", trainer);
        
        // Spawn NPCs after initialization
        SpawnNPCs();
    }
    
    private static void SpawnNPCs()
    {
        // Spawn NPCs in locations
        Map.AddNPC("Nurse Joy", "Pal Center");
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

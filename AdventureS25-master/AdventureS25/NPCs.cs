using System.Text.Json;

namespace AdventureS25;

public static class NPCs
{
    private static Dictionary<string, NPC> nameToNPC = new Dictionary<string, NPC>();

    public static void Initialize()
    {
        nameToNPC.Clear();
        string path = Path.Combine(Environment.CurrentDirectory, "NPCs.json");
        string rawText = File.ReadAllText(path);
        NPCsJsonData data = JsonSerializer.Deserialize<NPCsJsonData>(rawText);
        foreach (NPCJsonData npcData in data.NPCs)
        {
            List<Quest> quests = new List<Quest>();
            if (npcData.Quests != null)
            {
                foreach (var questName in npcData.Quests)
                {
                    var quest = Quests.GetQuestByName(questName);
                    if (quest != null)
                        quests.Add(quest);
                }
            }
            NPC npc = new NPC(
                npcData.Name,
                npcData.Description,
                npcData.InitialDescription,
                npcData.IsInteractable,
                npcData.Dialogue,
                npcData.AsciiArt,
                quests
            );
            nameToNPC.Add(npc.Name, npc);
            Map.AddNPC(npc.Name, npcData.Location);
        }
    }

    public static NPC GetNPCByName(string npcName)
    {
        if (nameToNPC.ContainsKey(npcName))
            return nameToNPC[npcName];
        return null;
    }
}

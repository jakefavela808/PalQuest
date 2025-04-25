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
            List<Pal> pals = new List<Pal>();
if (npcData.Pals != null)
{
    foreach (var palName in npcData.Pals)
    {
        var pal = Pals.GetPalByName(palName);
        if (pal != null)
            pals.Add(pal);
    }
}
string asciiArt = npcData.AsciiArt;
if (!string.IsNullOrWhiteSpace(asciiArt) && asciiArt.StartsWith("AsciiArt."))
{
    string artField = asciiArt.Substring("AsciiArt.".Length);
    var artType = typeof(AsciiArt);
    var artProp = artType.GetProperty(artField, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
    if (artProp != null)
    {
        asciiArt = artProp.GetValue(null)?.ToString() ?? string.Empty;
    }
    else
    {
        var artFieldInfo = artType.GetField(artField, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
        if (artFieldInfo != null)
        {
            asciiArt = artFieldInfo.GetValue(null)?.ToString() ?? string.Empty;
        }
    }
}
NPC npc = new NPC(
    npcData.Name,
    npcData.Description,
    npcData.InitialDescription,
    npcData.IsInteractable,
    npcData.Dialogue,
    asciiArt,
    quests,
    npcData.IsTrainer,
    pals
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

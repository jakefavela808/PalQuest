using System.Text.Json;

namespace AdventureS25;

public static class Quests
{
    private static Dictionary<string, Quest> nameToQuest = new Dictionary<string, Quest>(StringComparer.OrdinalIgnoreCase);

    public static void Initialize()
    {
        nameToQuest.Clear();
        string path = Path.Combine(Environment.CurrentDirectory, "Quests.json");
        string rawText = File.ReadAllText(path);
        QuestsJsonData data = JsonSerializer.Deserialize<QuestsJsonData>(rawText);
        foreach (QuestJsonData questData in data.Quests)
        {
            Quest quest = new Quest(
                questData.Name,
                questData.Description,
                questData.InitialDescription,
                questData.IsRepeatable,
                questData.Reward,
                questData.Objectives,
                questData.Location
            );
            nameToQuest[quest.Name.Trim()] = quest;
        }
    }

    public static Quest GetQuestByName(string questName)
    {
        if (string.IsNullOrWhiteSpace(questName)) return null;
        nameToQuest.TryGetValue(questName.Trim(), out var quest);
        return quest;
    }
}

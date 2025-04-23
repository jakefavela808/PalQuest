using System.Text.Json;
using System.Reflection;

namespace AdventureS25;

public static class Pals
{
    private static Dictionary<string, Pal> nameToPal = new Dictionary<string, Pal>();

    public static void Initialize()
    {
        nameToPal.Clear();
        string path = Path.Combine(Environment.CurrentDirectory, "Pals.json");
        string rawText = File.ReadAllText(path);
        PalsJsonData data = JsonSerializer.Deserialize<PalsJsonData>(rawText);
        foreach (PalJsonData palData in data.Pals)
        {
            string asciiArt = palData.AsciiArt;
            if (!string.IsNullOrWhiteSpace(asciiArt) && asciiArt.StartsWith("AsciiArt."))
            {
                string artField = asciiArt.Substring("AsciiArt.".Length);
                var artValue = typeof(AsciiArt).GetField(artField, BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as string;
                asciiArt = artValue ?? string.Empty;
            }
            Pal pal = new Pal(
                palData.Name,
                palData.Description,
                palData.InitialDescription,
                palData.IsAcquirable,
                asciiArt
            );
            nameToPal.Add(pal.Name, pal);
            Map.AddPal(pal.Name, palData.Location);
        }
    }

    public static Pal GetPalByName(string palName)
    {
        if (nameToPal.ContainsKey(palName))
            return nameToPal[palName];
        return null;
    } // Already public, no change needed
}

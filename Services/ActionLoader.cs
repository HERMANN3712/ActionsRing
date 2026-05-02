using System.IO;
using System.Text.Json;
using ActionsRing.Models;

namespace ActionsRing.Services
{
    public static class ActionLoader
    {
        public static List<RingAction> Load(string basePath, string path)
        {
            try
            {
                if (!File.Exists(path))
                    return new List<RingAction>();

                string jsonPath = Path.Combine(basePath, path);
                

                string json = File.ReadAllText(jsonPath);

                return JsonSerializer.Deserialize<List<RingAction>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<RingAction>();
            }
            catch
            {
                return new List<RingAction>();
            }           
        }
    }
}
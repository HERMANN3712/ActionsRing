using System.IO;
using System.Text.Json;
using ActionsRing.Models;

namespace ActionsRing.Services
{
    public static class ActionLoader
    {
        public static List<RingAction> Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return new List<RingAction>();

                string json = File.ReadAllText(path);

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
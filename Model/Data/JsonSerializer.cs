using Newtonsoft.Json;

namespace Model;

public class JsonSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string path)
    {
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public override T Deserialize(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException("Cannot deserialize JSON");
    }
}
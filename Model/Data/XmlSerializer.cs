using System.Xml.Serialization;

namespace Model.Data;

public class XmlSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string path)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var writer = File.CreateText(path);
        serializer.Serialize(writer, obj);
    }

    public override T Deserialize(string path)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = File.OpenText(path);
        return (T?)serializer.Deserialize(reader) ?? throw new InvalidOperationException("Cannot deserialize XML");
    }
}
namespace Model.Data;

public abstract class SerializerBase<T>
{
    public abstract void Serialize(T obj, string path);
    public abstract T Deserialize(string path);
}
namespace Model.Data;

public class FileRepository<T> : IRepository<T>
{
    private readonly string _dirPath;
    private readonly SerializerBase<T> _serializer;

    public FileRepository(string directoryPath, SerializerBase<T> serializer)
    {
        _dirPath = directoryPath ?? 
                   throw new ArgumentNullException("Directory path cannot be null", nameof(directoryPath));
        _serializer = serializer ?? 
                      throw new ArgumentNullException("Serializer cannot be null", nameof(serializer));
        Directory.CreateDirectory(_dirPath);
    }

    public void Save(string key, T item)
    {
        var path = Path.Combine(_dirPath, key);
        _serializer.Serialize(item, path);
    }

    public T Load(string key)
    {
        var path = Path.Combine(_dirPath, key);
        if (!File.Exists(path)) throw new FileNotFoundException("File not found", path);
        return _serializer.Deserialize(path);
    }

    public IReadOnlyList<string> Data()
    {
        return Directory.GetFiles(_dirPath);
    }
}
namespace Model;

public interface IRepository<T>
{
    void Save(string key, T item);
    T Load(string key);
    IReadOnlyList<string> Data();
}
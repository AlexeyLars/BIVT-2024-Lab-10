using Model.Data;

namespace Model.Service;

public static class FormatSwitchService
{
    public static void SwitchFormat<T>
        (string oldPath, SerializerBase<T> oldSerializer, string newPath, SerializerBase<T> newSerializer)
    {
        if(string.IsNullOrWhiteSpace(oldPath)) throw new ArgumentException("Old path cannot be empty", nameof(oldPath));
        if(string.IsNullOrWhiteSpace(newPath)) throw new ArgumentException("New path cannot be empty", nameof(newPath));
        
        Directory.CreateDirectory(newPath);
        Directory.CreateDirectory(oldPath);
        
        var files = Directory.GetFiles(oldPath);
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var baseName = Path.GetFileNameWithoutExtension(file);
            // todo
        }
    }
}
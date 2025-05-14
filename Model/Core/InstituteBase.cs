namespace Model.Core;

// Абстрактный класс института, имеющий массив различных программ обучения.
public abstract class InstituteBase
{
    private readonly List<EducationalProgram> _educationalPrograms = new List<EducationalProgram>();
    
    public string Name { get; private set; }
    public IReadOnlyList<EducationalProgram> EducationalPrograms => _educationalPrograms.AsReadOnly();

    protected InstituteBase(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentNullException("Institute name cannot be null or empty", nameof(name));
        Name = name;
    }

    public void AddProgram(EducationalProgram program)
    {
        if(program == null) throw new ArgumentNullException("Educational program cannot be null", nameof(program));
        if(_educationalPrograms.Contains(program)) throw new InvalidOperationException("Institute's programs already contains an educational program");
        _educationalPrograms.Add(program);
    }

    public void RemoveProgram(EducationalProgram program)
    {
        if(program == null) throw new ArgumentNullException("Educational program cannot be null", nameof(program));
        if(!_educationalPrograms.Contains(program)) throw new InvalidOperationException("Institute's program does not contain an educational program");
        _educationalPrograms.Remove(program);
    }
}
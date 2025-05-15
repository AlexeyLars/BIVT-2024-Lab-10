namespace Model.Core;

// Класс, описывающий образовательную программу: название и массив дисциплин.
public class EducationalProgram
{
    private readonly List<Subject> _subjects = new List<Subject>();
    
    public string Name {get; private set;}
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    public EducationalProgram(string name, List<Subject> subjects = null)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Educational program name cannot be null or empty", nameof(name));
        // if(subjects == null) throw new ArgumentNullException("Educational program subjects cannot be null", nameof(subjects));

        Name = name;
        if(subjects != null) _subjects.AddRange(subjects);
    }

    public void AddSubject(Subject subject)
    {
        if(subject == null) throw new ArgumentNullException("Subject cannot be null", nameof(subject));
        _subjects.Add(subject);
    }

    public void RemoveSubject(Subject subject)
    {
        if(subject == null) throw new ArgumentNullException("Subject cannot be null", nameof(subject));
        _subjects.Remove(subject);
    }
}
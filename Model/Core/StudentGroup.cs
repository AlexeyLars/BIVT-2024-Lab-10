namespace Model.Core;

public class StudentGroup : IGroup
{
    private readonly List<Student> _students = new List<Student>();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public EducationalProgram EducationalProgram { get; private set; }

    public StudentGroup(EducationalProgram educationalProgram, List<Student>? students = null)
    {
        EducationalProgram = educationalProgram ?? 
                             throw new ArgumentNullException("Educational program cannot be null", nameof(educationalProgram));
        if(students != null) _students.AddRange(students);
    }

    public void Enroll(Student student)
    {
        if(student == null) throw new ArgumentNullException("Student cannot be null", nameof(student));
        if(_students.Any(s => s.Id == student.Id)) throw new ArgumentException("Student already exists in group", nameof(student));
        _students.Add(student);
    }

    public void Expel(Student student)
    {
        if(student == null) throw new ArgumentNullException("Student cannot be null", nameof(student));
        _students.Remove(student);
    }

    public void Transfer(EducationalProgram newProgram)
    {
        if(newProgram == null) throw new ArgumentNullException("New program cannot be null", nameof(newProgram));
        if(ReferenceEquals(newProgram, EducationalProgram)) throw new ArgumentException("New program cannot be the same program", nameof(newProgram));
        EducationalProgram = newProgram;
    }
}
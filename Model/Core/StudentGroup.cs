using ArgumentNullException = System.ArgumentNullException;

namespace Model.Core;

public partial class StudentGroup : IGroup
{
    private readonly List<Student> _students = new List<Student>();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public EducationalProgram EducationalProgram { get; private set; }
    public InstituteBase Institute { get; private set; }

    public StudentGroup(EducationalProgram educationalProgram, InstituteBase institute, List<Student>? students = null)
    {
        EducationalProgram = educationalProgram ?? 
                             throw new ArgumentNullException("Educational program cannot be null", nameof(educationalProgram));
        Institute = institute ??
                    throw new ArgumentNullException("Institute cannot be null", nameof(institute));
        if(students != null) _students.AddRange(students);
    }

    public void Enroll(Student student)
    {
        if(student == null) throw new ArgumentNullException("Student cannot be null", nameof(student));
        if(_students.Contains(student)) throw new ArgumentException("Student already exists in group", nameof(student));
        _students.Add(student);
    }

    public void Expel(Student student)
    {
        if(student == null) throw new ArgumentNullException("Student cannot be null", nameof(student));
        if(!_students.Contains(student)) throw new ArgumentException("Student does not exist in group", nameof(student));
        _students.Remove(student);
    }

    public void TransferStudent(Student student, IGroup newGroup)
    {
        if(student == null) throw new ArgumentNullException("Student cannot be null", nameof(student));
        if(newGroup == null) throw new ArgumentNullException("New group cannot be null", nameof(newGroup));
        if(!_students.Contains(student)) throw new ArgumentException("Student does not exist in this group", nameof(student));
        
        
        newGroup.Enroll(student);
        Expel(student);
    }
}
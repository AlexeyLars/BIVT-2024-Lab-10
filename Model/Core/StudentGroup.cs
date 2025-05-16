using ArgumentNullException = System.ArgumentNullException;

namespace Model.Core;

public partial class StudentGroup : IGroup
{
    public Guid Id { get; }
    
    private readonly List<Student> _students = new List<Student>();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public EducationalProgram EducationalProgram { get; private set; }
    public InstituteBase Institute { get; private set; }

    public StudentGroup(EducationalProgram educationalProgram, InstituteBase institute, List<Student>? students = null)
    {
        Id = Guid.NewGuid();
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
        if (newGroup is StudentGroup newStudentGroup)
        {
            // Пока не реализован функционал перевода в этих особых слуачаях - будем кидать эксепшены
            if(newStudentGroup.Institute.GetType() != Institute.GetType()) 
                throw new ArgumentException(
                    "New group institute has to be the same as current group institute", nameof(newGroup));
            if (newStudentGroup.EducationalProgram.GetType() != EducationalProgram.GetType())
                throw new ArgumentException(
                    "New group educational program has to be the same as current group educational program",
                    nameof(newGroup));
        }
        newGroup.Enroll(student);
        Expel(student);
    }
}
namespace Model.Core;

// Класс, описывающий студента
public class Student
{
    public Guid Id { get; }
    public string Name { get; private set; }
    
    public Student(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Student name cannot be null or empty", nameof(name));
        Id = Guid.NewGuid();
        Name = name;
    }
}

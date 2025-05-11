namespace Model.Core;

// Класс, описывающий предмет: название, номер семестра в котором он читается и количество часов.
public class Subject
{
    public string Name {get; private set;}
    public int SemesterNumber {get; private set;}
    public int Hours {get; private set;}

    public Subject(string name, int semesterNumber, int hours)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Subject name cannot be null or empty", nameof(name));
        if(semesterNumber <= 0) throw new ArgumentOutOfRangeException("Subject semester must be greater than 0", nameof(semesterNumber));
        if(hours <= 0) throw new ArgumentOutOfRangeException("Subject hours must be greater than 0", nameof(hours));
        
        Name = name;
        SemesterNumber = semesterNumber;
        Hours = hours;
    }
}
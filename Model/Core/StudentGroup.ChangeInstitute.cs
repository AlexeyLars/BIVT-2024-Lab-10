namespace Model.Core;

public partial class StudentGroup
{
    public void ChangeInstitute(InstituteBase newInstitute, EducationalProgram newProgram)
    {
        if(newProgram == null) throw new ArgumentNullException("New educational program cannot be null", nameof(newProgram));
        if(newInstitute == null) throw new ArgumentNullException("Institute cannot be null", nameof(newInstitute));
        if(ReferenceEquals(newInstitute, Institute)) throw new InvalidOperationException("New program cannot be the same as the current");
        if(!newInstitute.EducationalPrograms.Contains(newProgram)) throw new InvalidOperationException("New institute does not contains the specified educational program");
        
        EducationalProgram = newProgram;
        Institute = newInstitute;
    }
}
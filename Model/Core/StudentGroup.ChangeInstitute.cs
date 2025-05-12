namespace Model.Core;

public partial class StudentGroup
{
    public void ChangeInstitute(InstituteBase newInstitute, EducationalProgram newProgram)
    {
        if(newProgram == null) throw new ArgumentNullException("New educational program cannot be null", nameof(newProgram));
        if(ReferenceEquals(newInstitute, Institute)) throw new InvalidOperationException("New program cannot be the same as the current");
        EducationalProgram = newProgram;
        Institute = newInstitute;
    }
}
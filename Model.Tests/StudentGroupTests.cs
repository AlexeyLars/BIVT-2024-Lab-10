namespace Model.Tests;
using Model.Core;

[TestClass]
public class StudentGroupTests
{
    private InstituteBase _institute1, _institute2;
    private EducationalProgram _program1, _program2, _program3;
    private Student _student1, _student2;
    private StudentGroup _group1, _group2, _group3;
    
    [TestInitialize]
    public void Setup()
    {
        _institute1 = new MathInstitute();
        _institute2 = new PhysicsInstitute();
        
        _program1 = new EducationalProgram("Интеллектуальные системы анализа данных");
        _program2 = new EducationalProgram("Программная инженерия");
        _program3 = new EducationalProgram("Прикладная физика");

        _student1 = new Student("Ролдугин Ярослав");
        _student2 = new Student("Тимофей Тимуров");
        
        _group1 = new StudentGroup(_program1, _institute1);
        _group2 = new StudentGroup(_program2, _institute1);
        _group3 = new StudentGroup(_program1, _institute2);
    }

    [TestMethod]
    public void Enroll_Test()
    {
        _group1.Enroll(_student1);
        Assert.IsTrue(_group1.Students.Contains(_student1));
        if (_group1.Students.Count > 1) Assert.AreSame(_student1, _group1.Students[^1]);
    }

    [TestMethod]
    public void Enroll_NullStudent_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.Enroll(null));
    }

    [TestMethod]
    public void Enroll_EnrolledStudent_Test()
    {
        _group1.Enroll(_student1);
        Assert.ThrowsException<ArgumentException>(() => _group1.Enroll(_student1));
    }

    [TestMethod]
    public void Expel_Test()
    {
        _group1.Enroll(_student1);
        _group1.Expel(_student1);
        Assert.AreEqual(0, _group1.Students.Count);
    }

    [TestMethod]
    public void Expel_NotEnrolledStudent_Test()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.Expel(_student1));
    }

    [TestMethod]
    public void TransferStudent_Test()
    {
        _group1.Enroll(_student1);
        _group1.TransferStudent(_student1, _group2);
        Assert.AreEqual(0, _group1.Students.Count);
        Assert.IsTrue(_group2.Students.Contains(_student1));
    }

    [TestMethod]
    public void TransferStudent_NullStudent_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.TransferStudent(null, _group2));
    }

    [TestMethod]
    public void TransferStudent_NotEnrolledStudent_Test()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.TransferStudent(_student1, _group2));
    }

    [TestMethod]
    public void TransferStudent_ToNull_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.TransferStudent(_student1, null));
    }

    [TestMethod]
    public void TransferStudent_ToOtherInstituteGroup_Test()
    {
        _group1.Enroll(_student1);
        Assert.ThrowsException<ArgumentException>(() => _group1.TransferStudent(_student1, _group3));
    }

    public void TransferStudent_ToOtherEducationalProgram_Test()
    {
        _group1.Enroll(_student1);
        Assert.ThrowsException<ArgumentException>(() => _group1.TransferStudent(_student1, _group3));
    }

    [TestMethod]
    public void ChangeProgram_Test()
    {
        _group1.ChangeProgram(_program2);
        Assert.AreSame(_program2, _group1.EducationalProgram);
    }

    [TestMethod]
    public void ChangeProgram_ToNull_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.ChangeProgram(null));
    }

    [TestMethod]
    public void ChangeProgram_ToSame_Test()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.ChangeProgram(_program1));
    }

    [TestMethod]
    public void ChangeInstitute_Test()
    {
        _institute2.AddProgram(_program2);
        _group1.ChangeInstitute(_institute2, _program2);
        Assert.AreSame(_program2, _group1.EducationalProgram);
        Assert.AreSame(_institute2, _group1.Institute);
    }

    [TestMethod]
    public void ChangeInstitute_WhenProgramNotExistAtInstitute_Test()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.ChangeInstitute(_institute2, _program1));
    }

    [TestMethod]
    public void ChangeInstitute_ToNullInstitute_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.ChangeInstitute(null, _program2));
    }

    [TestMethod]
    public void ChangeInstitute_ToNullProgram_Test()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.ChangeInstitute(_institute2, null));
    }
}
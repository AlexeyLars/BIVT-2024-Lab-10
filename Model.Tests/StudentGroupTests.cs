namespace Model.Tests;
using Model.Core;

[TestClass]
public class StudentGroupTests
{
    private InstituteBase _institute;
    private EducationalProgram _program1, _program2;
    private Student _student1, _student2;
    private StudentGroup _group1, _group2;
    
    [TestInitialize]
    public void Setup()
    {
        _institute = new MathInstitute();
        
        _program1 = new EducationalProgram("Интеллектуальные системы анализа данных");
        _program2 = new EducationalProgram("Программная инженерия");

        _student1 = new Student("Ролдугин Ярослав");
        _student2 = new Student("Ларсов Алексей");
        
        _group1 = new StudentGroup(_program1, _institute);
        _group2 = new StudentGroup(_program2, _institute);
    }

    [TestMethod]
    public void EnrollStudentTest()
    {
        _group1.Enroll(_student1);
        Assert.IsTrue(_group1.Students.Contains(_student1));
        if (_group1.Students.Count > 1) Assert.IsTrue(ReferenceEquals(_student1, _group1.Students[^1]));
    }

    [TestMethod]
    public void EnrollNullStudentTest()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.Enroll(null));
    }

    [TestMethod]
    public void EnrollEnrolledStudentTest()
    {
        _group1.Enroll(_student1);
        Assert.ThrowsException<ArgumentException>(() => _group1.Enroll(_student1));
    }

    [TestMethod]
    public void ExpelStudentTest()
    {
        _group1.Enroll(_student1);
        _group1.Expel(_student1);
        Assert.AreEqual(0, _group1.Students.Count);
    }

    [TestMethod]
    public void ExpelNotEnrolledStudentTest()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.Expel(_student1));
    }

    [TestMethod]
    public void TransferStudentTest()
    {
        _group1.Enroll(_student1);
        _group1.TransferStudent(_student1, _group2);
        Assert.AreEqual(0, _group1.Students.Count);
        Assert.IsTrue(_group2.Students.Contains(_student1));
    }

    [TestMethod]
    public void TransferNullStudentTest()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.TransferStudent(null, _group2));
    }

    [TestMethod]
    public void TransferNotEnrolledStudentTest()
    {
        Assert.ThrowsException<ArgumentException>(() => _group1.TransferStudent(_student1, _group2));
    }

    [TestMethod]
    public void TransferStudentToNullTest()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _group1.TransferStudent(_student1, null));
    }

    [TestMethod]
    public void ChangeProgramTest()
    {
        // todo
    }
    
    
}
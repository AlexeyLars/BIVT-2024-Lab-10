namespace Model.Core;

// Интерфейс, описывающий группу студентов с действиями зачисления, перевода и отчисления.
public interface IGroup
{
    void Enroll(Student student); // Зачисление нового студента
    void TransferStudent(Student student, IGroup newGroup); // Перевод группы на новую программу
    void Expel(Student student); // Отчисление студента
}



namespace Model.Core;

// Интерфейс, описывающий группу студентов с действиями зачисления, перевода и отчисления.
public interface IGroup
{
    void Enroll(Student student); // Зачисление нового студента
    void Transfer(); // Перевод группы на новую программу
    void Expel(Student student); // Отчисление студента
}



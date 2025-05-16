using Model.Core;
using Model.Data;

namespace Model.Service;

public class CollegeManager
{
    private readonly IRepository<InstituteBase> _instituteRepo;
    private readonly IRepository<StudentGroup> _groupRepo;
    
    public string Extension { get; private set; } = string.Empty;
    public IReadOnlyList<InstituteBase> Institutes { get; private set; } = new List<InstituteBase>();

    public CollegeManager(IRepository<InstituteBase> instituteRepo, IRepository<StudentGroup> groupRepo)
    {
        _instituteRepo = instituteRepo ?? throw new ArgumentNullException(
            "Institute repository cannot be null", nameof(instituteRepo));
        _groupRepo = groupRepo ?? throw new ArgumentNullException(
            "Student group repository cannot be null", nameof(groupRepo));
    }
    
    
    // Инициализатор CollegeManager при первом старте, создающий все институты
    public void Initialize(IEnumerable<InstituteBase> defaultInstitutes, string defaultExtension = ".json")
    {
        if(defaultInstitutes == null) throw new ArgumentNullException("Default institutes cannot be null", nameof(defaultInstitutes));
        
        if (defaultInstitutes.Any(i => i == null))
            throw new ArgumentException("Default institutes cannot contain null element", nameof(defaultInstitutes));
        
        /* todo:
         по-хорошему следует добавить guid к каждому институту и по нему отслеживать, чтобы не было повторок,
         а также использовать именно его при создании файла для уникальности имени, но пока оставим так 
        */
        var duplicateNames = defaultInstitutes
            .GroupBy(i => i.Name, StringComparer.Ordinal)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key).ToList();
        if(duplicateNames.Any()) throw new ArgumentException(
            "Duplicate institute names are not allowed", string.Join(", ", duplicateNames));
        
        Extension = defaultExtension;
        
        var data = _instituteRepo.Data();
        
        // Кейс первого старта, заполняем данными по умолчанию
        if (!data.Any())
        {
            foreach (var institute in defaultInstitutes) _instituteRepo.Save($"{institute.Name}.{Extension}", institute);
            Institutes = defaultInstitutes.ToList();
        }
        else Institutes = data.Select(i => _instituteRepo.Load(i)).ToList(); // иначе загружаем имеющиеся данные
    }
    
    public IReadOnlyList<StudentGroup> GetGroups(InstituteBase institute)
    {
        if (institute == null) throw new ArgumentNullException("Institute cannot be null", nameof(institute));
        string prefix = institute.Name + "_";
        return _groupRepo.Data()
            .Where(g => g.StartsWith(prefix, StringComparison.Ordinal))
            .Select(g => _groupRepo.Load(g))
            .ToList();
    }
    
    public void AddInstitute(InstituteBase institute)
    {
        if (institute == null) throw new ArgumentNullException("Institute cannot be null", nameof(institute));
        
        if (Institutes.Any(i => string.Equals(i.Name, institute.Name, StringComparison.Ordinal)))
            throw new InvalidOperationException($"Institute with name {institute.Name} already exists");
        
        _instituteRepo.Save($"{institute.Name}.{Extension}", institute);

        var list = Institutes.ToList();
        list.Add(institute);
        Institutes = list;
    }

    // Метод сохранения института как обновление информации о нём
    public void SaveInstitute(InstituteBase institute)
    {
        if(institute == null) throw new ArgumentNullException("Institute cannot be null", nameof(institute));
        _instituteRepo.Save($"{institute.Name}.{Extension}", institute);
    }

    
    // Метод добавления группы к определённому институту и её сохранения
    public void AddGroup(InstituteBase institute, StudentGroup group)
    {
        if (institute == null) throw new ArgumentNullException("Institute cannot be null", nameof(institute));
        if (group == null) throw new ArgumentNullException("Group cannot be null", nameof(group));
        if (!Institutes.Contains(institute))
            throw new ArgumentException("Institute not found in initialized", nameof(institute));
        
        _groupRepo.Save($"{institute.Name}_{group.EducationalProgram.Name}_{group.Id}.{Extension}", group);
    }

    // Метод удаления группы из хранилища
    public void RemoveGroup()
    {
        // todo
        throw new NotImplementedException("RemoveGroup is not implemented");
    }
    
    // Метод перевода студентов между группами
    public void TransferStudent(Student student, StudentGroup group, StudentGroup newGroup)
    {
        group.TransferStudent(student, newGroup);
    }
}
using Lab4.Models;

namespace Lab4.Data;

public static class DataStore
{
    private static int _studentAllowId = 1;
    private static int _teacherAllowId = 1;
    private static int _groupAllowId = 1;
    
    private static readonly List<Student> _students = new();
    private static readonly List<Teacher> _teachers = new();
    private static readonly List<Group> _groups = new();
    public static List<Student> Students => new(_students);
    public static List<Teacher> Teachers => new(_teachers);
    public static List<Group> Groups => new(_groups);

    public static Student GetStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) throw new InvalidDataException("Student with such id doesn't exist");
        return student;
    }
    
    public static Teacher GetTeacher(int id)
    {
        var teacher = _teachers.FirstOrDefault(t => t.Id == id);
        if (teacher == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        return teacher;
    }
    
    public static Group GetGroup(int id)
    {
        var group = _groups.FirstOrDefault(s => s.Id == id);
        if (group == null) throw new InvalidDataException("Group with such id doesn't exist");
        return group;
    }

    public static void AddStudent(Student student)
    {
        var groupToInclude = _groups.FirstOrDefault(g => g.Id == student.GroupId);
        if (groupToInclude == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        student.Id = _studentAllowId;
        ++_studentAllowId;
        _students.Add(student);
    }

    public static void AddTeacher(Teacher teacher)
    {
        teacher.Id = _teacherAllowId;
        ++_teacherAllowId;
        _teachers.Add(teacher);
    }

    public static void AddGroup(Group group)
    {
        var teacherToInclude = _teachers.FirstOrDefault(t => t.Id == group.TeacherId);
        if (teacherToInclude == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        group.Id = _groupAllowId;
        ++_groupAllowId;
        _groups.Add(group);
    }

    public static void EditStudent(int id, Student student)
    {
        var studentToEdit = _students.FirstOrDefault(s => s.Id == id);
        if (studentToEdit == null) throw new InvalidDataException("Student with such id doesn't exist");
        
        studentToEdit.Name = student.Name;
        studentToEdit.Age = student.Age;
        
        var isGroupExist = _groups.FirstOrDefault(g => g.Id == student.GroupId);
        if (isGroupExist == null) throw new InvalidDataException("Group with such id doesn't exist");
        studentToEdit.GroupId = student.GroupId;
    }
    
    public static void EditTeacher(int id, Teacher teacher)
    {
        var teacherToEdit = _teachers.FirstOrDefault(t => t.Id == id);
        if (teacherToEdit == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        
        teacherToEdit.Name = teacher.Name;
    }
    
    public static void EditGroup(int id, Group group)
    {
        var groupToEdit = _groups.FirstOrDefault(g => g.Id == id);
        if (groupToEdit == null) throw new InvalidDataException("Group with such id doesn't exist");
        
        groupToEdit.Name = group.Name;
        
        var isTeacherExist = _teachers.FirstOrDefault(t => t.Id == group.TeacherId);
        if (isTeacherExist == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        groupToEdit.TeacherId = group.TeacherId;
    }
    
    public static void DeleteStudent(int id)
    {
        var studentToRemove = _students.FirstOrDefault(s => s.Id == id);
        if (studentToRemove == null) throw new InvalidDataException("Student with such id doesn't exist");
        _students.Remove(studentToRemove);
    }
    
    public static void DeleteTeacher(int id)
    {
        var teacherToRemove = _teachers.FirstOrDefault(t => t.Id == id);
        if (teacherToRemove == null) throw new InvalidDataException("Teacher with such id doesn't exist");
        DeleteGroupsCascade(teacherToRemove.Id);
        _teachers.Remove(teacherToRemove);
    }
    
    public static void DeleteGroup(int id)
    {
        var groupToRemove = _groups.FirstOrDefault(g => g.Id == id);
        if (groupToRemove == null) throw new InvalidDataException("Group with such id doesn't exist");
        DeleteStudentsCascade(groupToRemove.Id);
        _groups.Remove(groupToRemove);
    }

    private static void DeleteGroupsCascade(int teacherId)
    {
        var groupsToRemove = _groups.FindAll(g => g.TeacherId == teacherId);
        foreach (var group in groupsToRemove)
        {
            DeleteStudentsCascade(group.Id);
            _groups.Remove(group);
        }
    }

    private static void DeleteStudentsCascade(int groupId)
    {
        var studentsToRemove = _students.FindAll(s => s.GroupId == groupId);
        foreach (var student in studentsToRemove)
        {
            _students.Remove(student);
        }
    }
}
public interface IStudentService
{
    List<StudentDTO> GetAllStudents();

    List<StudentDTO> GetStudentsInHostel();

    void UpdateRoom(int studentId, int roomNo);

    void DeleteStudent(int studentId);
}
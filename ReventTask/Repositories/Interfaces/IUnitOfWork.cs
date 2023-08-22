namespace ReventTask.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository TeacherRepository { get; }
        IStudentRepository StudentRepository { get; }
        IClassroomRepository ClassroomRepository { get; }
    }
}

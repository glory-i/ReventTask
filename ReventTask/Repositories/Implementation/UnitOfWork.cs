using ReventTask.Data;
using ReventTask.Repositories.Interfaces;

namespace ReventTask.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ITeacherRepository? _teacherRepository;
        private IStudentRepository? _studentRepository;
        private IClassroomRepository? _classroomRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITeacherRepository TeacherRepository => _teacherRepository ??= new TeacherRepository(_context);

        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_context);
        public IClassroomRepository ClassroomRepository => _classroomRepository ??= new ClassroomRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}

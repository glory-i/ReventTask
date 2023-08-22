using Microsoft.EntityFrameworkCore;
using ReventTask.Data;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;

namespace ReventTask.Repositories.Implementation
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Student>> getStudentsEagerLoading()
        {
            return await _context.Students
                .Include(s => s.Teacher)
                .Include(s => s.Classroom)
                .ToListAsync();
        }


    }
}

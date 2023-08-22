using Microsoft.EntityFrameworkCore;
using ReventTask.Data;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;

namespace ReventTask.Repositories.Implementation
{
    public class ClassroomRepository : BaseRepository<Classroom> ,IClassroomRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassroomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classroom>> getClassroomsEagerLoading()
        {
            return await  _context.Classrooms
                .Include(c => c.Teacher)
                .ToListAsync();
 
        }
    }
}

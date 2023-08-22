using Microsoft.EntityFrameworkCore;
using ReventTask.Data;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;

namespace ReventTask.Repositories.Implementation
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

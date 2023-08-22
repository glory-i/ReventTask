using ReventTask.Models;
using ReventTask.Responses;

namespace ReventTask.Repositories.Interfaces
{
    public interface IClassroomRepository : IBaseRepository<Classroom>
    {
        //returns all classrooms and includes the virtual properties  in each classroom.
        public Task<IEnumerable<Classroom>> getClassroomsEagerLoading();
    }
}

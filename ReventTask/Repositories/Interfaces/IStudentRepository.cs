using ReventTask.Models;
using ReventTask.Responses;

namespace ReventTask.Repositories.Interfaces
{
    public interface IStudentRepository: IBaseRepository<Student>
    {
        //returns all students and includes the virtual properties Classroom and Teacher in each student.
        public Task<IEnumerable<Student>> getStudentsEagerLoading();
    }
}

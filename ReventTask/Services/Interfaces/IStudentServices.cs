using ReventTask.DTOs.StudentDTO;
using ReventTask.DTOs.TeacherDTO;
using ReventTask.Responses;

namespace ReventTask.Services.Interfaces
{
    public interface IStudentServices
    {
        public Task<ApiResponse> createStudent(CreateStudentDTO createStudentDTO);
        public Task<ApiResponse> getStudents();
        public Task<ApiResponse> getStudent(int id);

        public Task<ApiResponse> assignTeacherToStudent(int studentId, int teacherId);
        public Task<ApiResponse> reAssignTeacherToStudent(int studentId, int teacherId);
        public Task<ApiResponse> getTeacherAssignedToStudent(int studentId);

        public Task<ApiResponse> assignClassroomToStudent(int studentId, int classroomId);
        public Task<ApiResponse> reAssignClassroomToStudent(int studentId, int classroomId);
        public Task<ApiResponse> getClassroomAssignedToStudent(int studentId);
    }
}

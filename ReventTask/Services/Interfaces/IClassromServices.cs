using ReventTask.DTOs.ClassroomDTO;
using ReventTask.Responses;

namespace ReventTask.Services.Interfaces
{
    public interface IClassromServices
    {
        public Task<ApiResponse> createClassroom(CreateClassroomDTO createClassroomDTO);
        public Task<ApiResponse> getClassrooms();
        public Task<ApiResponse> getClassroom(int id);


        public Task<ApiResponse> assignTeacherToClassroom(int classroomId, int teacherId);
        public Task<ApiResponse> reAssignTeacherToClassroom(int classroomId, int teacherId);
        public Task<ApiResponse> getTeacherAssignedToClassroom(int classroomId);
        public Task<ApiResponse> getStudentsAssignedToClassroom(int classroomId);

    }
}

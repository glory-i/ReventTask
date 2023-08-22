using ReventTask.DTOs.TeacherDTO;
using ReventTask.Models;
using ReventTask.Responses;

namespace ReventTask.Services.Interfaces
{
    public interface ITeacherServices
    {
        public Task<ApiResponse> createTeacher(CreateTeacherDTO createTeacherDTO);
        public Task<ApiResponse> getTeachers();
        public Task<ApiResponse> getTeacher(int id);
        public Task<ApiResponse> getClassroomsAssignedToTeacher(int teacherId);
        public Task<ApiResponse> getStudentsAssignedToTeacher(int teacherId);
    }
}

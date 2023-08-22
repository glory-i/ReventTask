using AutoMapper;
using ReventTask.DTOs.ClassroomDTO;
using ReventTask.DTOs.StudentDTO;
using ReventTask.DTOs.TeacherDTO;
using ReventTask.Models;

namespace ReventTask.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTeacherDTO, Teacher>();
            CreateMap<CreateStudentDTO, Student>();
            CreateMap<CreateClassroomDTO, Classroom>();
            
            CreateMap<Teacher, ViewTeacherDTO>();
            CreateMap<Student, ViewStudentDTO>();
            CreateMap<Classroom, ViewClassroomDTO>();


        }

    }
}

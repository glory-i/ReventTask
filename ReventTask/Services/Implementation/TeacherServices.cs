using AutoMapper;
using Newtonsoft.Json;
using ReventTask.Data;
using ReventTask.DTOs.ClassroomDTO;
using ReventTask.DTOs.StudentDTO;
using ReventTask.DTOs.TeacherDTO;
using ReventTask.Mapper;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;
using ReventTask.Services.Interfaces;

namespace ReventTask.Services.Implementation
{
    public class TeacherServices : ITeacherServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> createTeacher(CreateTeacherDTO createTeacherDTO)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                var teacher = _mapper.Map<Teacher>(createTeacherDTO);
                teacher.Students = new List<Student>();
                teacher.Classrooms = new List<Classroom>();

                await _unitOfWork.TeacherRepository.AddAsync(teacher);
                return returnedResponse.CorrectResponse(teacher);


            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }

        }

        public async Task<ApiResponse> getClassroomsAssignedToTeacher(int teacherId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);



                //get all classrooms and check which classrooms have a teacherId foreign key that matches the id of the teacher in question.
                var allClassrooms = await _unitOfWork.ClassroomRepository.getClassroomsEagerLoading();
                var classroomsAssigned = allClassrooms.Where(s => s.TeacherId == teacherId).ToList();



                List<ViewClassroomDTO> viewClassroomDTOs = new List<ViewClassroomDTO>();
                foreach (var classroom in classroomsAssigned)
                {
                    var viewClassroomDTO = _mapper.Map<ViewClassroomDTO>(classroom);
                    viewClassroomDTOs.Add(viewClassroomDTO);
                }
                return returnedResponse.CorrectResponse(viewClassroomDTOs);


            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }
        }

        public async Task<ApiResponse> getStudentsAssignedToTeacher(int teacherId)
        {

            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);



                //get all students and check which students have a teacherId foreign key that matches the id of the teacher in question.

                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var studentsAssigned = allStudents.Where(s => s.TeacherId == teacherId).ToList();



                List<ViewStudentDTO> viewStudentDTOs = new List<ViewStudentDTO>();
                foreach (var student in studentsAssigned)
                {
                    var viewStudentDTO = _mapper.Map<ViewStudentDTO>(student);
                    viewStudentDTOs.Add(viewStudentDTO);
                }
                return returnedResponse.CorrectResponse(viewStudentDTOs);


            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }
        }

        public async Task<ApiResponse> getTeacher(int id)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(id);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }
                var teacher = await _unitOfWork.TeacherRepository.GetAsync(id);
                var viewTeacherDTO = _mapper.Map<ViewTeacherDTO>(teacher);
                return returnedResponse.CorrectResponse(viewTeacherDTO);

            }
            catch(Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);
            }



        }

        public async Task<ApiResponse> getTeachers()
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                List<ViewTeacherDTO> viewTeacherDTOs = new List<ViewTeacherDTO>();
                var allTeachers = await _unitOfWork.TeacherRepository.GetAll();
                foreach (var teacher in allTeachers)
                {
                    var viewTeacherDTO = _mapper.Map<ViewTeacherDTO>(teacher);
                    viewTeacherDTOs.Add(viewTeacherDTO);
                }
                return returnedResponse.CorrectResponse(viewTeacherDTOs);

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);
            }
        }
    }
}




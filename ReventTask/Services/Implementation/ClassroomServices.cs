using AutoMapper;
using Newtonsoft.Json;
using ReventTask.DTOs.ClassroomDTO;
using ReventTask.DTOs.StudentDTO;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;
using ReventTask.Services.Interfaces;

namespace ReventTask.Services.Implementation
{
    public class ClassroomServices : IClassromServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassroomServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> assignTeacherToClassroom(int classroomId, int teacherId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }

                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }


                //eager load the classroom with that id
                var allClassrooms = await _unitOfWork.ClassroomRepository.getClassroomsEagerLoading();
                var classroom = allClassrooms.First(s => s.Id == classroomId);

                //if the classroom already has a teacher assigned, do not assign again
                if (classroom.Teacher != null)
                {
                    return returnedResponse.ErrorResponse($"Classroom {classroom.Name} is already assigned to a teacher -Teacher {classroom.Teacher.Name}.Please use the Re-assign endpoint to assign Classroom {classroom.Name} to another teacher", null);
                }


                //if the classroom does not have a teacher assigned, get the teacher with the specified id and assign to classroom, then save changes.

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);

                classroom.TeacherId = teacher.Id;
                classroom.Teacher = teacher;

                await _unitOfWork.ClassroomRepository.UpdateAsync(classroom);

                return returnedResponse.CorrectResponse($"Successfully assigned Teacher {teacher.Name} to Classroom {classroom.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }

        }

        public async Task<ApiResponse> createClassroom(CreateClassroomDTO createClassroomDTO)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                var classroom = _mapper.Map<Classroom>(createClassroomDTO);
                classroom.Students = new List<Student>();

                await _unitOfWork.ClassroomRepository.AddAsync(classroom);
                return returnedResponse.CorrectResponse(classroom);


            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }


        }

        public async Task<ApiResponse> getClassroom(int id)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(id);
                
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }

                var classroom = await _unitOfWork.ClassroomRepository.GetAsync(id);
                var viewClassroomDTO = _mapper.Map<ViewClassroomDTO>(classroom);
                return returnedResponse.CorrectResponse(viewClassroomDTO);

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);
            }

        }

        public async Task<ApiResponse> getClassrooms()
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                List<ViewClassroomDTO> viewClassroomDTOs = new List<ViewClassroomDTO>();
                var allClassrooms = await _unitOfWork.ClassroomRepository.GetAll();
                foreach(var classroom in allClassrooms)
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

        public async Task<ApiResponse> getStudentsAssignedToClassroom(int classroomId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }

                //eager load the classroom with that id
                var allClassrooms = await _unitOfWork.ClassroomRepository.getClassroomsEagerLoading();
                var classroom = allClassrooms.First(s => s.Id == classroomId);

                
                //get all students and check which students have a classroomiId foreign key that matches the id of the classroom in question.

                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var studentsAssigned = allStudents.Where(s => s.ClassromId == classroomId).ToList();



                List<ViewStudentDTO> viewStudentDTOs = new List<ViewStudentDTO>();
                foreach(var student in studentsAssigned)
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

        public async Task<ApiResponse> getTeacherAssignedToClassroom(int classroomId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }

                //eager load the classroom with that id
                var allClassrooms = await _unitOfWork.ClassroomRepository.getClassroomsEagerLoading();
                var classroom = allClassrooms.First(s => s.Id == classroomId);


                //return the name of the teacher assigned to a classroom(if a teacher has been assigned to the student)
                if (classroom.Teacher == null)
                {
                    return returnedResponse.ErrorResponse("No teacher assigned to classroom", null);
                }

                return returnedResponse.CorrectResponse($" The name of the teacher assigned to classroom {classroom.Name} is : Teacher {classroom.Teacher.Name}");

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }

        }

        public async Task<ApiResponse> reAssignTeacherToClassroom(int classroomId, int teacherId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.StudentRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }

                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }


                //eager load the classroom with that id
                var allClassrooms = await _unitOfWork.ClassroomRepository.getClassroomsEagerLoading();
                var classroom = allClassrooms.First(s => s.Id == classroomId);


                // get the teacher with the specified id and assign to classroom, then save changes.

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);

                classroom.TeacherId = teacher.Id;
                classroom.Teacher = teacher;

                await _unitOfWork.ClassroomRepository.UpdateAsync(classroom);

                return returnedResponse.CorrectResponse($"Successfully re-assigned Teacher {teacher.Name} to Classroom {classroom.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }

        }
    }
}

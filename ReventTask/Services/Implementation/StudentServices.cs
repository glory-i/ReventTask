using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReventTask.DTOs.StudentDTO;
using ReventTask.DTOs.TeacherDTO;
using ReventTask.Models;
using ReventTask.Repositories.Interfaces;
using ReventTask.Responses;
using ReventTask.Services.Interfaces;

namespace ReventTask.Services.Implementation
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> assignTeacherToStudent(int studentId, int teacherId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }
                

                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);

                //if the student already has a teacher assigned, do not assign again
                if(student.Teacher != null)
                {
                    return returnedResponse.ErrorResponse($"Student {student.Name} is already assigned to a teacher -Teacher {student.Teacher.Name}.Please use the Re-assign endpoint to assign Student {student.Name} to another teacher", null);
                }

                
                //if the student does not have a teacher assigned, get the teacher with the specified id and assign to student, then save changes.

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);

                student.TeacherId = teacher.Id;
                student.Teacher = teacher;

                await _unitOfWork.StudentRepository.UpdateAsync(student);

                return returnedResponse.CorrectResponse($"Successfully assigned Teacher {teacher.Name} to Student {student.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }


        }

        public async Task<ApiResponse> assignClassroomToStudent(int studentId, int classroomId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }


                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);

                //if the student already has a classroom assigned, do not assign again
                if (student.Classroom != null)
                {
                    return returnedResponse.ErrorResponse($"Student {student.Name} is already assigned to a classroom - Classroom {student.Classroom.Name}.Please use the Re-assign endpoint to assign Student {student.Name} to another classroom", null);
                }


                //if the student does not have a classroom assigned, get the classroom with the specified id and assign to student, then save changes.

                var classroom = await _unitOfWork.ClassroomRepository.GetAsync(classroomId);

                student.ClassromId = classroom.Id;
                student.Classroom = classroom;

                await _unitOfWork.StudentRepository.UpdateAsync(student);

                return returnedResponse.CorrectResponse($"Successfully assigned Classroom {classroom.Name} to Student {student.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }


        }

        public async Task<ApiResponse> createStudent(CreateStudentDTO createStudentDTO)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                var student = _mapper.Map<Student>(createStudentDTO);
                await _unitOfWork.StudentRepository.AddAsync(student);
                return returnedResponse.CorrectResponse(student);

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }

        }

        public async Task<ApiResponse> getStudent(int id)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                bool studentExists = await _unitOfWork.StudentRepository.Exists(id);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }
                
                var student = await _unitOfWork.StudentRepository.GetAsync(id);
                var viewStudentDTO = _mapper.Map<ViewStudentDTO>(student);
                return returnedResponse.CorrectResponse(viewStudentDTO);

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);
            }
        }

        public async Task<ApiResponse> getStudents()
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();

            try
            {
                List<ViewStudentDTO> viewStudentDTOs = new List<ViewStudentDTO>();
                var allStudents = await _unitOfWork.StudentRepository.GetAll();
                foreach (var student in allStudents)
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

        public async Task<ApiResponse> getTeacherAssignedToStudent(int studentId)
        {

            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);


                //return the name of the teacher assigned to a student(if a teacher has been assigned to the student)
                if (student.Teacher == null)
                {
                    return returnedResponse.ErrorResponse("No teacher assigned to student", null);
                }

                return returnedResponse.CorrectResponse($" The name of the teacher assigned to student {student.Name} is : Teacher {student.Teacher.Name}");

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }
        }


        public async Task<ApiResponse> getClassroomAssignedToStudent(int studentId)
        {

            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);


                //return the name of the classroom assigned to a student(if a classroom has been assigned to the student)
                if (student.Classroom == null)
                {
                    return returnedResponse.ErrorResponse("No classroom assigned to student", null);
                }

                return returnedResponse.CorrectResponse($" The name of the classroom assigned to student {student.Name} is : Classroom {student.Classroom.Name}");

            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }
        }



        public async Task<ApiResponse> reAssignTeacherToStudent(int studentId, int teacherId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //check if the teacher with that id exists
                bool teacherExists = await _unitOfWork.TeacherRepository.Exists(teacherId);
                if (!teacherExists)
                {
                    return returnedResponse.ErrorResponse("Teacher does not exist", null);
                }


                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);


                // get the teacher with the specified id and assign to student, then save changes.

                var teacher = await _unitOfWork.TeacherRepository.GetAsync(teacherId);

                student.TeacherId = teacher.Id;
                student.Teacher = teacher;

                await _unitOfWork.StudentRepository.UpdateAsync(student);

                return returnedResponse.CorrectResponse($"Successfully re-assigned Teacher {teacher.Name} to Student {student.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }


        }

        public async Task<ApiResponse> reAssignClassroomToStudent(int studentId, int classroomId)
        {
            ReturnedResponse returnedResponse = new ReturnedResponse();
            try
            {
                //check if the student with that id exists
                bool studentExists = await _unitOfWork.StudentRepository.Exists(studentId);
                if (!studentExists)
                {
                    return returnedResponse.ErrorResponse("Student does not exist", null);
                }

                //check if the classroom with that id exists
                bool classroomExists = await _unitOfWork.ClassroomRepository.Exists(classroomId);
                if (!classroomExists)
                {
                    return returnedResponse.ErrorResponse("Classroom does not exist", null);
                }


                //eager load the student with that id
                var allStudents = await _unitOfWork.StudentRepository.getStudentsEagerLoading();
                var student = allStudents.First(s => s.Id == studentId);



                //get the classroom with the specified id and assign to student, then save changes.

                var classroom = await _unitOfWork.ClassroomRepository.GetAsync(classroomId);

                student.ClassromId = classroom.Id;
                student.Classroom = classroom;

                await _unitOfWork.StudentRepository.UpdateAsync(student);

                return returnedResponse.CorrectResponse($"Successfully re-assigned Classroom {classroom.Name} to Student {student.Name}");



            }
            catch (Exception e)
            {
                return returnedResponse.ErrorResponse(e.ToString(), null);

            }


        }

    }
}

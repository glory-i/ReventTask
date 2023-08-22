using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReventTask.DTOs.StudentDTO;
using ReventTask.Responses;
using ReventTask.Services.Implementation;
using ReventTask.Services.Interfaces;

namespace ReventTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        /// <summary>
        /// This endpoint creates a student instance in the database.
        /// </summary>
        [HttpPost("CreateStudent")]
        public async Task<ActionResult<ApiResponse>> CreateStudent(CreateStudentDTO model)
        {

            var response = await _studentServices.createStudent(model);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        /// <summary>
        /// This endpoint returns all the students in the database.
        /// </summary>
        [HttpGet("GetStudents")]
        public async Task<ActionResult<ApiResponse>> GetStudents()
        {

            var response = await _studentServices.getStudents();
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        /// <summary>
        /// This endpoint returns a specific student in the databse with the id passed.
        /// </summary>
        [HttpGet("GetStudent/{id}")]
        public async Task<ActionResult<ApiResponse>> GetStudents(int id)
        {

            var response = await _studentServices.getStudent(id);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint assigns a teacher to a student, if the student does not already have a teacher assigned to it.
        /// </summary>
        [HttpPost("AssignTeacherToStudent")]
        public async Task<ActionResult<ApiResponse>> AssignTeacherToStudent(int studentId, int teacherId)
        {

            var response = await _studentServices.assignTeacherToStudent(studentId, teacherId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint gets the teacher assigned to a particular student (if any teacher has been assigned to the student)
        /// </summary>
        [HttpGet("GetTeacherAssignedToStudent")]
        public async Task<ActionResult<ApiResponse>> GetTeacherAssignedToStudent(int studentId)
        {

            var response = await _studentServices.getTeacherAssignedToStudent(studentId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint assigns a classroom to a student, if the student does not already have a classroom assigned to it.
        /// </summary>
        [HttpPost("AssignClassroomToStudent")]
        public async Task<ActionResult<ApiResponse>> AssignClassroomToStudent(int studentId, int classroomId)
        {

            var response = await _studentServices.assignClassroomToStudent(studentId, classroomId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint gets the classroom assigned to a particular student (if any classroom has been assigned to the student)
        /// </summary>
        [HttpGet("GetClassrooomAssignedToStudent")]
        public async Task<ActionResult<ApiResponse>> GetClassrooomAssignedToStudent(int studentId)
        {

            var response = await _studentServices.getClassroomAssignedToStudent(studentId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint assigns a different classroom to a student. Thus, the student will no longer be assigned to its former/previous classroom.
        /// </summary>
        [HttpPost("ReAssignClassroomToStudent")]
        public async Task<ActionResult<ApiResponse>> ReAssignClassroomToStudent(int studentId, int classroomId)
        {

            var response = await _studentServices.reAssignClassroomToStudent(studentId, classroomId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        /// <summary>
        /// This endpoint assigns a different teacher to a student. Thus, the student will no longer be assigned to its former/previous teacher.
        /// </summary>
        [HttpPost("ReAssignTeacherToStudent")]
        public async Task<ActionResult<ApiResponse>> ReAssignTeacherToStudent(int studentId, int teacherId)
        {

            var response = await _studentServices.reAssignTeacherToStudent(studentId, teacherId);
            if (response.Message == ApiResponseEnum.success.ToString())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

    }
}

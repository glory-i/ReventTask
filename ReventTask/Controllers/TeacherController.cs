using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReventTask.DTOs.TeacherDTO;
using ReventTask.Responses;
using ReventTask.Services.Implementation;
using ReventTask.Services.Interfaces;

namespace ReventTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices _teacherServices;
        public TeacherController(ITeacherServices teacherServices)
        {
            _teacherServices = teacherServices;
        }


        /// <summary>
        /// This endpoint creates a teacher instance in the database.
        /// </summary>
        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<ApiResponse>> CreateTeacher(CreateTeacherDTO model)
        {

            var response = await _teacherServices.createTeacher(model);
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
        /// This endpoint returns all the teachers in the database.
        /// </summary>
        [HttpGet("GetTeachers")]
        public async Task<ActionResult<ApiResponse>> GetTeachers()
        {

            var response = await _teacherServices.getTeachers();
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
        /// This endpoint returns a specific teacher in the databse with the id passed.
        /// </summary>
        [HttpGet("GetTeacher/{id}")]
        public async Task<ActionResult<ApiResponse>> GetTeachers(int id)
        {

            var response = await _teacherServices.getTeacher(id);
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
        /// This endpoint gets all the students assigned to a particular teacher.
        /// </summary>
        [HttpGet("GetStudentsAssignedToTeacher")]
        public async Task<ActionResult<ApiResponse>> GetStudentsAssignedToTeacher(int teacherId)
        {

            var response = await _teacherServices.getStudentsAssignedToTeacher(teacherId);
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
        /// This endpoint gets all the classrooms assigned to a particular teacher.
        /// </summary>
        [HttpGet("GetClassroomsAssignedToTeacher")]
        public async Task<ActionResult<ApiResponse>> GetClassroomsAssignedToTeacher(int teacherId)
        {

            var response = await _teacherServices.getClassroomsAssignedToTeacher(teacherId);
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

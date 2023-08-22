using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReventTask.DTOs.ClassroomDTO;
using ReventTask.Responses;
using ReventTask.Services.Implementation;
using ReventTask.Services.Interfaces;

namespace ReventTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassromServices _classroomServices;
        public ClassroomController(IClassromServices classroomServices)
        {
            _classroomServices = classroomServices;
        }

        /// <summary>
        /// This endpoint creates a classroom instance in the database.
        /// </summary>
        [HttpPost("CreateClassroom")]
        public async Task<ActionResult<ApiResponse>> CreateClassroom(CreateClassroomDTO model)
        {

            var response = await _classroomServices.createClassroom(model);
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
        /// This endpoint returns all the classrooms in the database.
        /// </summary>
        [HttpGet("GetClassrooms")]
        public async Task<ActionResult<ApiResponse>> GetClassrooms()
        {

            var response = await _classroomServices.getClassrooms();
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
        /// This endpoint returns a specific classroom in the databse with the id passed.
        /// </summary>
        [HttpGet("GetClassroom/{id}")]
        public async Task<ActionResult<ApiResponse>> GetClassrooms(int id)
        {

            var response = await _classroomServices.getClassroom(id);
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
        /// This endpoint assigns a teacher to a classroom, if the classroom does not already have a teacher assigned to it.
        /// </summary>
        [HttpPost("AssignTeacherToClassroom")]
        public async Task<ActionResult<ApiResponse>> AssignTeacherToClassroom(int classroomId, int teacherId)
        {

            var response = await _classroomServices.assignTeacherToClassroom(classroomId, teacherId);
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
        /// This endpoint gets the teacher assigned to a particular classroom (if any teacher has been assigned to the classroom)
        /// </summary>
        [HttpGet("GetTeacherAssignedToClassroom")]
        public async Task<ActionResult<ApiResponse>> GetTeacherAssignedToClassroom(int classroomId)
        {

            var response = await _classroomServices.getTeacherAssignedToClassroom(classroomId);
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
        /// This endpoint assigns a different teacher to a classroom. Thus, the classroom will no longer be assigned to its former/previous teacher.
        /// </summary>
        [HttpPost("ReAssignTeacherToClassroom")]
        public async Task<ActionResult<ApiResponse>> ReAssignTeacherToClassroom(int classroomId, int teacherId)
        {

            var response = await _classroomServices.reAssignTeacherToClassroom(classroomId, teacherId);
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
        /// This endpoint gets all the students assigned to a particular classroom.
        /// </summary>

        [HttpGet("GetStudentsAssignedToClassroom")]
        public async Task<ActionResult<ApiResponse>> GetStudentsAssignedToClassroom(int classroomId)
        {

            var response = await _classroomServices.getStudentsAssignedToClassroom(classroomId);
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

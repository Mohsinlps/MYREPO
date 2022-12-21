using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersManageController : ControllerBase
    {
        private ITeachersManageRepository _teacherRepo;
        public TeachersManageController(ITeachersManageRepository repo)
        {
            _teacherRepo = repo;
        }

        [HttpPost]
        [Route("AssignCourse")]
        public IActionResult AssignCourse([FromForm] AssignTeacherDto dto)
        {
            _teacherRepo.AssignCourse(dto);
            return Ok();
        }

        [HttpGet]
        [Route("gettesting")]
        public IActionResult testing()
        {
            return Ok();
        }

        }
}

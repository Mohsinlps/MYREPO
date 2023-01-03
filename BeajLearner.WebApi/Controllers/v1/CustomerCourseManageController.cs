using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCourseManageController : ControllerBase
    {

        ICustomerCourseManageRepository _repoCustomerCourse;
        public CustomerCourseManageController(ICustomerCourseManageRepository repoCustomer)
        {
            _repoCustomerCourse = repoCustomer;
        }



        [HttpPost]
        [Route("purchaseCourse")]
        public IActionResult purchaseCourse(purchaseCourseDto dto)
        {
           dto = _repoCustomerCourse.savePurchasedCourse(dto);
            bool success = true;

            return Ok(new ResponseOtp(success, "course purchased successfully"));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCourseManage : ControllerBase
    {

        [HttpPost]
        [Route("purchaseCourse")]
        public IActionResult purchaseCourse()
        {
            return null;
        }
    }
}

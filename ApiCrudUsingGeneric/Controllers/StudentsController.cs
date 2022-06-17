using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudUsingGeneric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : GenericController<Student>
    {
        public StudentsController(IGenericService<Student> genericService) : base(genericService)
        {
        }

        public override void BeforeProcess()
        {

        }
    }
}

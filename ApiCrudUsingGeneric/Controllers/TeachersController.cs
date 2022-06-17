using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudUsingGeneric.Controllers
{
    public class TeachersController : GenericController<Teacher>
    {
        public TeachersController(IGenericService<Teacher> genericService) : base(genericService)
        {
        }
    }
}

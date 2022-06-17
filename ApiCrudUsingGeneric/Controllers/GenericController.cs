using ApiCrudUsingGeneric.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudUsingGeneric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : Controller where T : class
    {
        private IGenericService<T> _genericService;

        public GenericController(IGenericService<T> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public List<T> Get()
        {
            return _genericService.GetAll();
        }

        [HttpGet("{id}")]
        public T Get([FromRoute] int id)
        {
            this.BeforeProcess();
            return _genericService.GetById(id);
        }

        [HttpPost]
        public List<T> Post(T item)
        {
            return _genericService.Insert(item);
        }

        [HttpDelete("{id}")]
        public List<T> Post(int id)
        {
            return _genericService.Delete(id);
        }

        public virtual void BeforeProcess()
        {

        }
    }
}

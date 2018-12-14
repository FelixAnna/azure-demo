using Felix.Azure.MvcMovie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Felix.Azure.MvcMovie.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsApiController : ControllerBase
    {
        private readonly MvcMovieContext _context;
        private readonly AzureStorageCreator _storageCreator;

        public ActorsApiController(MvcMovieContext context, AzureStorageCreator storageCreator)
        {
            _context = context;
            _storageCreator = storageCreator;
        }

        // GET: api/ActorsApi
        [HttpGet]
        public ActionResult<IList<ActorViewModel>> Get()
        {
            var results = _context.Actor.Select(x => new ActorViewModel()
            {
                ID = x.ID,
                Name = x.Name,
                Gender = x.Gender ? "Female" : "Male",
                Birthday = x.Birthday,
                Description = x.Description,
            }).ToArray();
            return Ok(results);
        }

        // GET: api/ActorsApi/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ActorViewModel> Get(int id)
        {
            var results = _context.Actor.Where(x => x.ID == id).Select(x => new ActorViewModel()
            {
                ID = x.ID,
                Name = x.Name,
                Gender = x.Gender ? "Female" : "Male",
                Birthday = x.Birthday,
                Description = x.Description,
            }).FirstOrDefault();
            return Ok(results);
        }

        [HttpGet("test/create-file")]
        public async Task<ActionResult<string>> TestCreateFileAsync()
        {
            var result = await _storageCreator.CreateLogFileAsync();

            return Ok(result);
        }

        [HttpGet("test/exception")]
        public ActionResult TestException()
        {
            throw new System.Exception("Custom exception");
        }
    }
}

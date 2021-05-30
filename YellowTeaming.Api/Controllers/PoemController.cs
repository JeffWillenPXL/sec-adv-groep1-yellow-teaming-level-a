using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowTeaming.Data;


namespace YellowTeaming.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PoemController : ControllerBase
    {
        
        private readonly IPoemRepository _poemRepo;

        public PoemController(IPoemRepository poemRepo)
        {
            _poemRepo = poemRepo;
        }

        [HttpGet]
        
        public string Get()
        {
            
            return _poemRepo.GetFirstPoem().Content;
        }
    }
}

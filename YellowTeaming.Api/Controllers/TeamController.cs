using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using YellowTeaming.Data;
using YellowTeaming.Data.Entities;

namespace YellowTeaming.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepo;
        public TeamController(ITeamRepository teamRepo)
        {
            _teamRepo = teamRepo;
        }
        [HttpGet]
        public IEnumerable<Member> Get()
        {
            return _teamRepo.GetAllMembers().ToArray();
        }
    }
}

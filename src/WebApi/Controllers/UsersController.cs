using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Domains;
using WebApi.Data.EntityFramework;
using WebApi.HttpRequests;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
            if (user == null)
            {
                ModelState.AddModelError("id", $"User [{id}] not found");
                return BadRequest(ModelState);
            }
            
            return Ok(_mapper.Map<UserDto>(user));
        }
        
        [HttpGet("populars")]
        public IActionResult GetPopularUsers([FromQuery]int count)
        {
            var users = _db.Users
                .Include(x => x.Followers)
                .OrderByDescending(x => x.Followers.Count)
                .Take(count)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToArray();
            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddUserHttpRequest request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name);
            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new {id = user.Id}, user);
        }
        
        [HttpPost("{id}/subscribe/{userId}")]
        public async Task<IActionResult> Add([FromRoute] int id, [FromRoute]int userId, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Include(x => x.Subscriptions)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
            if (user == null)
                return NotFound($"Not found user [{id}]");
            
            var subUser = await _db.Users
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            
            if (subUser == null)
                return NotFound($"Not found user [{userId}]");
            
            user.Subscribe(subUser);
            
            await _db.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
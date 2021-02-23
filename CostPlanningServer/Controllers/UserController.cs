using CostPlanningServer.DataBase;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CostPlanningServer.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly CostPlanningContext _context;
        //private readonly ILogger _logger;
        public UserController(CostPlanningContext context/*, ILogger logger*/)
        {
            _context = context;
            //_logger = logger;
        }
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.ToList());
        }
        //TODO: Delete serverID and update database
        public async Task<IActionResult> PostAppUser(User user)
        {
            //promena
            user.Id = 0;
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
            return Ok(user);
        }
        public IActionResult GetNumberOfUsers()
        {
            return Ok(_context.Users.Count());
        }
        public IActionResult GetAllUsersWithoutAppUser(int appUserId)
        {
            return Ok(_context.Users.Where(x => x.Id != appUserId).ToList());
        }
        public IActionResult GetLastUserServerId()
        {
            if (!_context.Users.Any())
            {
                return Ok(0);
            }
            return Ok(_context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id);
        }
        [Route("{lastUserId}")]
        public IActionResult GetUnsyncUsers([FromRoute]int lastUserId)
        {
            return Ok(_context.Users.Where(x => x.Id > lastUserId));
        }
        public IActionResult PostCategory(Category category)
        {
            _context.Categories.AddAsync(category);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

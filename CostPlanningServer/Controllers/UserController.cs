using CostPlanningServer.DataBase;
using CostPlanningServer.Interface;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public UserController(CostPlanningContext context, ISynchronization synchronization/*, ILogger logger*/)
        {
            _context = context;
            //_logger = logger;
        }
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.ToList());
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
        public IActionResult GetUnsyncUsers([FromRoute] int lastUserId)
        {
            return Ok(_context.Users.Where(x => x.Id > lastUserId));
        }
        public IActionResult PostDevice(Device device)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok();
        }
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(JsonConvert.SerializeObject(user));
        }
    }
}

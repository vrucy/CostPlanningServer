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
        public async Task<IActionResult> PostAppUser(User user)
        {
            return Ok(_context.Users.Single(x => x.Id == 6));
            //user.Id = 0;
            //await _context.Users.AddAsync(user);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (System.Exception e)
            //{

            //    throw;
            //}
            
            //return Ok(user);
        }
        public IActionResult GetNumberOfUsers()
        {
            return Ok(_context.Users.Count());
        }
        public IActionResult GetAllUsersWithoutAppUser(int appUserId)
        {
            return Ok(_context.Users.Where(x=>x.Id != appUserId).ToList());
        }
        public IActionResult GetLastUserServerId()
        {
            return Ok(_context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id);
        }
    }
}

using CostPlanningServer.DataBase;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostPlanningServer.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CostPlanningContext _context;
        public CategoryController(CostPlanningContext context)
        {
            _context = context;
        }
        [Route("{idUser}")]
        public async Task<IActionResult> PostCategories(List<Category> categories,[FromRoute] int idUser)
        {
            var Ids = new Dictionary<int, int>();

            foreach (var item in categories)
            {
                Category c = new Category()
                {
                    IsVisible = item.IsVisible,
                    Name = item.Name,
                    ServerId = item.ServerId
                };
                await _context.Categories.AddAsync(c);
                //await _context.SaveChangesAsync();

                var userVisible = new SyncUser<Category>()
                {
                    ItemId = c.Id,
                    UserId = idUser
                };
                await _context.SyncUserCategory.AddAsync(userVisible);
                //TODO: Check if savechabge works fine outside forech 
                await _context.SaveChangesAsync();
                Ids.Add(item.Id, c.Id);
            }
            return Ok(JsonConvert.SerializeObject(Ids));
        }
        public IActionResult GetAllCategoriesByIds(List<int> ids)
        {
            var orders = _context.Categories.Where(o => !ids.Contains(o.Id));

            return Ok(orders);
        }
        public IActionResult GetLastCategoryServerId()
        {
            var categories = _context.Categories.OrderByDescending(x => x.Id);

            if (!categories.Any())
            {
                return Ok(-1);
            }

            return Ok(categories.FirstOrDefault().Id);
        }
        public IActionResult GetGategories()
        {
            return Ok(_context.Categories);
        }
        [Route("{userId}")]
        public IActionResult EditCategory(Category cat,[FromRoute]int userId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == cat.ServerId);
            if (category != null)
            {
                try
                {
                    var categoresForDelete = _context.SyncUserCategory.Where(x => x.ItemId == category.Id);
                    _context.SyncUserCategory.RemoveRange(categoresForDelete);
                    //_context.SaveChanges();

                    category.IsVisible = cat.IsVisible;
                    category.Name = cat.Name;
                    var syncUser = new SyncUser<Category>()
                    {
                        ItemId = category.Id,
                        UserId = userId
                    };
                    _context.SyncUserCategory.Add(syncUser);
                    _context.SaveChanges();
                }
                catch (System.Exception e)
                {

                    throw;
                }
                

                return Ok();
            }

            return BadRequest();
        }
        //TODO: sync visibility
        [Route("{idUser}")]
        public IActionResult SyncDisable([FromRoute] int idUser) 
        {
            //TODO: Refactor
            Dictionary<int, bool> categoresForSync = new Dictionary<int, bool>();
            var userForSync = new List<User>();
            var allcategoresId = _context.Categories.Select(c=>c.Id);
            var userCategoresId = _context.SyncUserCategory.Where(c=>c.UserId == idUser).Select(x=>x.ItemId);

            var res = allcategoresId.Except(userCategoresId);
            foreach (var item in res)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == item);
                categoresForSync.Add(item, category.IsVisible);
                var user = new SyncUser<Category>()
                {
                    UserId = idUser,
                    ItemId = category.Id
                };
                _context.SyncUserCategory.Add(user);
            }
            if (res.Any())
            {
                _context.SaveChanges();
            }
                 
            return Ok(JsonConvert.SerializeObject(categoresForSync));
        }
        public IActionResult SyncDisableOnServer(List<int> ids)
        {
            var categories = _context.Categories.Where(c => ids.Contains(c.Id));

            foreach (var item in categories)
            {
                item.IsVisible = true;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}

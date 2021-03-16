using CostPlanningServer.DataBase;
using CostPlanningServer.Interface;
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
        private readonly ISynchronization _synchronization;

        public CategoryController(CostPlanningContext context, ISynchronization synchronization)
        {
            _context = context;
            _synchronization = synchronization;
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
        [Route("{deviceId}")]
        public async Task<IActionResult> EditCategory(Category cat, [FromRoute] string deviceId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == cat.Id);
            if (category != null)
            {
                var categoresForDelete = _context.SyncDataCategory.Where(x => x.ItemId == category.Id);
                _context.SyncDataCategory.RemoveRange(categoresForDelete);

                category.IsVisible = cat.IsVisible;
                category.Name = cat.Name;
                await _synchronization.SyncDataCategory(category,deviceId);
                //TODO: Check if not need saveChanges. Is enough savechange in syncDataCategory!!.
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
        //TODO: sync visibility
        [Route("{deviceId}")]
        public async Task<IActionResult> SyncVisbility([FromRoute] string deviceId)
        {
            //TODO: Refactor
            Dictionary<int, bool> categoresForSync = new Dictionary<int, bool>();
            var userForSync = new List<User>();
            var allcategoresId = _context.Categories.Select(c => c.Id);
            var userCategoresId = _context.SyncDataCategory.Where(c => c.DeviceId.Equals(deviceId)).Select(x => x.ItemId);

            var res = allcategoresId.Except(userCategoresId);
            foreach (var item in res)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == item);
                categoresForSync.Add(item, category.IsVisible);
                await _synchronization.SyncDataCategory(category, deviceId);
            }
            if (res.Any())
            {
                await _context.SaveChangesAsync();
            }

            return Ok(JsonConvert.SerializeObject(categoresForSync));
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> PostCategory(Category category, string deviceId)
        {
            _context.Categories.Add(category);
            //TODO: code repeat new file syncData
            await _synchronization.SyncDataCategory(category, deviceId);
            //TODO: Is need savechabge we have in syncdatadatgory??
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Route("{lastCategoryId}")]
        public IActionResult GetUnsyncCategories([FromRoute] int lastCategoryId)
        {
            return Ok(_context.Categories.Where(x => x.Id > lastCategoryId));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;
        public RedisController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surName)
        {
            await _distributedCache.SetStringAsync("name", name, options: new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5),
            }) ;
            await _distributedCache.SetAsync("surName", Encoding.UTF8.GetBytes(surName) ,options: new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5),
            });

            return Ok();
        }
        [HttpGet("get")]
        public async Task<IActionResult> Set()
        {
            var name = await _distributedCache.GetStringAsync("name");
           var surNameBinary= await _distributedCache.GetAsync("surName");
            var surname = Encoding.UTF8.GetString(surNameBinary);

            return Ok(new {name,surname});
        }
    }
}

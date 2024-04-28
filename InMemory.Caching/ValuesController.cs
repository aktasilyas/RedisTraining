using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //[HttpGet("setName/{name}")]
        // public void SetName(string name)
        // {
        //     _memoryCache.Set("name", name);
        // }
        // [HttpGet]
        // public string GetName()
        // {
        //     // get ile memoriye aldığımız değeri alıyoruz
        //    // return  _memoryCache.Get<string>("name");


        //     //TryGetValue: önce bu name'e karşılık gelen bir değer var mı kontrolü yapar daha sonra varsa eğer bu değeri geriye döndürür.
        //     if(_memoryCache.TryGetValue<string>("name",out string name))
        //     {
        //         return name.Substring(3);
        //     }
        //     return "name is null";
        // }

        [HttpGet("setDate")]
        public void setDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),//verinin ne kadar süre bellekte kalacağını belirtiyoruz bu süre dolduğunda otomatik siler
                SlidingExpiration = TimeSpan.FromSeconds(5)//eğer 5 saniye içinde herhangi bir işlem yapılmaz ise veri otomatik olarak cache ten silinir.
            }) ;
        }

        [HttpGet]
        public DateTime getDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}

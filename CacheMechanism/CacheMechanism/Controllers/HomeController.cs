using System.Diagnostics;
using CacheMechanism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheMechanism.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*
             * Lazy initialization:
             *  Eðer cache'de varsa kullan
             *  Yoksa iþlemi yap sonucu cache'e at
             */

            var option = new MemoryCacheEntryOptions();            
            option.SetAbsoluteExpiration(DateTime.Now.AddMinutes(1));
            //option.SetSlidingExpiration(TimeSpan.FromMinutes(1));

            option.SetPriority(CacheItemPriority.Low);
            option.SetSize(1024 * 1024);

            option.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration
            {
                EvictionCallback = (key,value, reason, state) =>
                {
                    _logger.LogInformation($"{key} anahtar deðerindeki cache verisi {reason} sebebiyle cache'den çýktý");
                }

            });


            if (!_memoryCache.TryGetValue("Employees", out List<string> employees))
            {
                employees = new()
                {
                    "Mete","Hasan","Melis","Ýpek"
                };

                _memoryCache.Set("Employees", employees, option);

            }

            ViewBag.Employees = employees;

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Employees = _memoryCache.Get<List<string>>("Employees");
            return View();
        }

        [ResponseCache(Duration =60, Location = ResponseCacheLocation.Client, VaryByQueryKeys = ["id"])]
        public IActionResult CacheHelper(int? id)
        {
            var employee = new { Name = "Türkay", Id = id, Department="IT" };

            return Ok(new { data = employee, dateInfo = DateTime.Now });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

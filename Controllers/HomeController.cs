using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FileDelivery.Models;
using Microsoft.AspNetCore.Hosting;
using FileDelivery.DAL;

namespace FileDelivery.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _db;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IDataService dataContext)
        {
            _environment = environment;
            _db = dataContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            TempData["TranxID"] = Guid.NewGuid().ToString("N");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("FirstName,LastName,EmailAddress,PhoneNumber,Age,TransactionID")] EntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!String.Equals(TempData["TranxID"], entry.TransactionID))
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {
                    // Catch controller exception.
                    return NotFound();
                }
            }
            return View(entry);
        }

        public async Task<IActionResult> Browse()
        {
            return View();
        }


        /**
         * <see cref="Details(int)"/> displays the uploaded entry UI to users.
         * When provided with an `id`, the particular <see cref="Entry"/> with that id is displayed.
         * 
         * 
         * param name="id" the entry identifier.
         */
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

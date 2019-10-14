using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FileDelivery.Models;
using Microsoft.AspNetCore.Hosting;
using FileDelivery.DAL;
using Microsoft.Extensions.Configuration;

namespace FileDelivery.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, AppDBContext db, IConfiguration configuration)
        {
            _environment = environment;
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            TempData["TranxID"] = Guid.NewGuid().ToString("N");
            _logger.LogInformation(TempData.Peek("TranxID") as string);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,EmailAddress,PhoneNumber,Age,TransactionID,Files")] EntryViewModel entry)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Verify if transaction ID is valid. This was generated in the GET request.
                    // If transaction I is invalid, client should reload the GET request for Index.
                    _logger.LogInformation(TempData.Peek("TranxID") as string);
                    if (!String.Equals(TempData["TranxID"], entry.TransactionID))
                    {
                        return Unauthorized(new { Status = "Expired transaction id! Refresh the page!" });
                    }

                    // Parse upload and save files to system.
                    var freshEntry = await entry.Parse(entry,_environment.WebRootPath);

                    // Add entry to database.
                    _db.Entries.Add(freshEntry);
                    _db.SaveChanges();

                    if (isApiRequest())
                    {
                        return new JsonResult(new { Status = "Successful" });
                    }
                    else
                    {
                        ViewData["Response"] = "Successful";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error in request processing");
                    // Catch controller exception.
                    return BadRequest(new { Status = e.Message });
                }
            }
            _logger.LogInformation("Model is invalid {0}", ModelState.ErrorCount);


            if (isApiRequest())
            {
                return new JsonResult(new { Status = "Invalid data" });
            }
            else
            {
                return View(entry);
            }
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

        protected bool isApiRequest()
        {
            return String.Equals(Request.Headers["x-api"], "true");
        }
    }

}

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
using FileDelivery.Util;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;

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
            TempData["TransactionID"] = Guid.NewGuid().ToString("N");
            _logger.LogInformation("TransactionID is: " + TempData.Peek("TransactionID") as string);

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
                    _logger.LogInformation("TransactionID is:  " + TempData.Peek("TransactionID") as string);
                    if (!String.Equals(TempData["TransactionID"], entry.TransactionID))
                    {
                        return Unauthorized(new { Status = "Expired transaction id! Refresh the page!" });
                    }

                    // Parse upload and save files to system.
                    var freshEntry = await entry.Parse(entry, _environment.ContentRootPath);

                    // Add entry to database.
                    _db.Entries.Add(freshEntry);

                    // Send email to user.
                    await Mail.SendEmailAsync(freshEntry, new MimeKit.MailboxAddress(freshEntry.Name, freshEntry.EmailAddress));

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

        /**
         * <see cref="Browse(string, string)"/> displays the uploaded entry UI to users.
         * When provided with a `TransactionID` and `EmailAddress`, the particular <see cref="Entry"/> with that id is displayed.
         * 
         * 
         * param name="TransactionID" the entry identifier.
         * param name="EmailAddress" the email address used for submission.
         */
        public ActionResult Browse(string TransactionID, string EmailAddress)
        {
            if (!String.IsNullOrWhiteSpace(TransactionID) || !String.IsNullOrWhiteSpace(EmailAddress))
            {
                var result = _db.Entries
                    .Where(e => e.EmailAddress.Equals(EmailAddress) && e.TransactionID.Equals(TransactionID))
                    .Include(e => e.Uploads)
                    .FirstOrDefault();
                Debug.WriteLine(result);
                return View(result);
            }
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

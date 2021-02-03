#region Using Directives

using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using DocumentManager.Models;

#endregion Using Directives

namespace DocumentManager.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private IWebHostEnvironment _hostEnvironment;

        private readonly ILogger<HomeController> _logger;

        #endregion Fields

        #region Constructors

        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        #endregion Constructors

        #region Actions

        public IActionResult Index()
        {
            // Retrieve the index of documents
            var documentsDirectory = Path.Combine(_hostEnvironment.WebRootPath, "documents");
            var index = Documents.GetIndex(documentsDirectory);

            // Serialize the index of documents in a way that is benefitial in the UI.
            var documents = 
                from document in index.List
                select document.ForClient()
            ;

            ViewData["Documents"] = documents.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion Actions
    }
}

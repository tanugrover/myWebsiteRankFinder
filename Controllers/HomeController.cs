using InfoTrackSEOApp.Models;
using InfoTrackSEOApp.Provider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace InfoTrackSEOApp.Controllers
{
    public class HomeController : Controller
    {
        ISearchProvider _searchProvider { get; set; }
        public HomeController(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }
        public ActionResult Index()
        {
            var EngineType = Enum.GetValues(typeof(SearchEngineOptions));
            ViewBag.EngineType = new SelectList(EngineType);
            return View();
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public PartialViewResult GetSearchResults(SearchModel searchModel)
        {

             List<ResultsModel> res= _searchProvider.searchService(searchModel);
            //TempData["SearchModel"] = searchModel;
             return PartialView("GetResults",res);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
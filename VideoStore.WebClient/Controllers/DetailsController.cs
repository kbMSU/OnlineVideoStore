using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoStore.Services.MessageTypes;
using VideoStore.WebClient.ViewModels;

namespace VideoStore.WebClient.Controllers
{
    public class DetailsController : Controller
    {
        // GET: Details
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowDetails(int mediaId)
        {
            return View(new MediaDetailsViewModel(mediaId));
        }
    }
}
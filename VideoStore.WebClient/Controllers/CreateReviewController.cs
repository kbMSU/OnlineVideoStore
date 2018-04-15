using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoStore.Services.Interfaces;
using VideoStore.Services.MessageTypes;
using VideoStore.WebClient.ViewModels;

namespace VideoStore.WebClient.Controllers
{
    public class CreateReviewController : Controller
    {
        private ICatalogueService CatalogueService
        {
            get
            {
                return ServiceFactory.Instance.CatalogueService;
            }
        }

        private IUserService UserService
        {
            get
            {
                return ServiceFactory.Instance.UserService;
            }
        }

        [HttpGet]
        public ActionResult CreateReview(int mediaId)
        {
            var vm = new CreateReviewViewModel();
            vm.mediaId = mediaId;
            vm.Item = CatalogueService.GetMediaById(mediaId);
            /*User currentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name);
            vm.ReviewerName = currentUser.Name;
            vm.ReviewerLocation = currentUser.City + ", " + currentUser.Country;
            vm.AuthorId = currentUser.Id;*/

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReview(CreateReviewViewModel vm, int pMediaId)
        {
            /*if(ModelState.IsValid)
            {
                //ServiceFactory.Instance.ReviewService.SubmitReview(pModel.toMessageType()); 
            }*/

            vm.mediaId = pMediaId;
            vm.Item = CatalogueService.GetMediaById(pMediaId);
            User currentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name);
            vm.ReviewerName = currentUser.Name;
            vm.ReviewerLocation = currentUser.City + ", " + currentUser.Country;
            vm.AuthorId = currentUser.Id;

            var ReviewMessage = vm.toMessageType();

            //return View(new MediaDetailsViewModel(vm.Item.Id));

            return RedirectToAction("ShowDetails", "Details", routeValues: new { mediaId = pMediaId });
        }
    }
}
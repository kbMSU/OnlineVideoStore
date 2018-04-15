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
            User currentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name);
            vm.ReviewerName = currentUser.Name;
            vm.ReviewerLocation = currentUser.City + ", " + currentUser.Country;
            vm.ReviewDate = DateTime.Now;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReview(CreateReviewViewModel vm, int pMediaId)
        {
            vm.mediaId = pMediaId;
            Media media = CatalogueService.GetMediaById(pMediaId);
            vm.Item = media;
            User currentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name);
            vm.AuthorId = currentUser.Id;

            // Get the Review Message Type
            var reviewMessage = vm.toMessageType();

            // Update the rating sum and count on the media
            media.RatingCount++;
            media.RatingSum += vm.Rating;

            // Save the review to the database
            ServiceFactory.Instance.ReviewService.SubmitReview(reviewMessage, media, currentUser);

            // Go back to the media details page
            return RedirectToAction("ShowDetails", "Details", routeValues: new { mediaId = pMediaId });
        }
    }
}
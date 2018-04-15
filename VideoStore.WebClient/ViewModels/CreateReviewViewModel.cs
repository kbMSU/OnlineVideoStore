using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoStore.Services.Interfaces;
using VideoStore.Services.MessageTypes;

namespace VideoStore.WebClient.ViewModels
{
    public class CreateReviewViewModel
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

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public Media Item { get; set; }

        public int mediaId { get; set; }

        public string ReviewerName { get; set; }

        public string ReviewerLocation { get; set; }

        public int AuthorId { get; set; }

        /*public CreateReviewViewModel(int mediaId)
        {
            this.mediaId = mediaId;
            Item = CatalogueService.GetMediaById(mediaId);
            User currentUser = UserService.GetUserByUserName(HttpContext.Current.User.Identity.Name);
            ReviewerName = currentUser.Name;
            ReviewerLocation = currentUser.City + ", " + currentUser.Country;
            AuthorId = currentUser.Id;
        }*/

        public Review toMessageType()
        {
            Review review = new Review()
            {
                Title = this.Title,
                ReviewDate = DateTime.Now,
                ReviewerName = this.ReviewerName,
                ReviewLocation = this.ReviewerLocation,
                AuthorId = this.AuthorId,
                Rating = this.Rating,
                ReviewDetails = this.Details,
                MediaId = this.mediaId
            };
            return review;
        }
    }
}
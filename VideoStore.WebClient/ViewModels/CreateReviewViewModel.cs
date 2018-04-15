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

        public IEnumerable<int> RatingOptions = new List<int> { 1, 2, 3, 4, 5 };

        public Media Item { get; set; }

        public int mediaId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Reviewer (You)")]
        public string ReviewerName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Review Location")]
        public string ReviewerLocation { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date and time of Review")]
        public DateTime ReviewDate { get; set; }

        public int AuthorId { get; set; }

        public Review toMessageType()
        {
            Review review = new Review()
            {
                Title = this.Title,
                ReviewDate = this.ReviewDate,
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
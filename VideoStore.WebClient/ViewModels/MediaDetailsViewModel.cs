using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Services.Interfaces;
using VideoStore.Services.MessageTypes;

namespace VideoStore.WebClient.ViewModels
{
    public class MediaDetailsViewModel
    {
        private ICatalogueService CatalogueService
        {
            get
            {
                return ServiceFactory.Instance.CatalogueService;
            }
        }

        private IReviewService ReviewService
        {
            get
            {
                return ServiceFactory.Instance.ReviewService;
            }
        }

        public MediaDetailsViewModel(int mediaId)
        {
            Item = CatalogueService.GetMediaById(mediaId);
            Reviews = ReviewService.GetReviewsForMediaId(mediaId);
            if(Item.RatingCount > 0)
            {
                AverageRating = Item.RatingSum / Item.RatingCount;
            }
        }

        public Media Item;

        public float AverageRating;

        public List<Review> Reviews;
    }
}
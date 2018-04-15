using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components
{
    public class ReviewProvider : IReviewProvider
    {
        public List<Review> GetReviewsForMediaId(int mediaId)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                return (from Review in lContainer.Reviews where Review.MediaId == mediaId select Review).ToList();
            }
        }

        public void SubmitReview(Review pReview, Media pMedia, User pUser)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                // Update the media rating
                lContainer.Media.First(m => m.Id == pMedia.Id).RatingCount = pMedia.RatingCount;
                lContainer.Media.First(m => m.Id == pMedia.Id).RatingSum = pMedia.RatingSum;

                // Add the review to the reviews list
                pReview.User = pUser;
                pReview.Media = lContainer.Media.First(m => m.Id == pMedia.Id);
                lContainer.Reviews.Add(pReview);      

                // Commit changes
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }
    }
}

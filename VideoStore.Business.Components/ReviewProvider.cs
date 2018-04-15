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

        public void SubmitReview(Review pReview)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                //lContainer.Users.Attach(pReview.User);
                //lContainer.Media.Attach(pReview.Media);
                lContainer.Reviews.Add(pReview);

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }
    }
}

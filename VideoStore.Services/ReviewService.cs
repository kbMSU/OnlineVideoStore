using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Services.Interfaces;
using VideoStore.Services.MessageTypes;

namespace VideoStore.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewProvider ReviewProvider
        {
            get
            {
                return ServiceFactory.GetService<IReviewProvider>();
            }
        }

        public List<Review> GetReviewsForMediaId(int mediaId)
        {
            var internalResult = ReviewProvider.GetReviewsForMediaId(mediaId);
            var externalResult = MessageTypeConverter.Instance.Convert<
                List<VideoStore.Business.Entities.Review>,
                List<VideoStore.Services.MessageTypes.Review>>(internalResult);
            return externalResult;
        }

        public void SubmitReview(Review pReview, Media pMedia, User pUser)
        {
            ReviewProvider.SubmitReview(
                MessageTypeConverter.Instance.Convert<
                VideoStore.Services.MessageTypes.Review,
                VideoStore.Business.Entities.Review>(pReview),
                MessageTypeConverter.Instance.Convert<
                VideoStore.Services.MessageTypes.Media,
                VideoStore.Business.Entities.Media>(pMedia),
                MessageTypeConverter.Instance.Convert<
                VideoStore.Services.MessageTypes.User,
                VideoStore.Business.Entities.User>(pUser)
            );
        }
    }
}

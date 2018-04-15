using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Services.MessageTypes;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface IReviewService
    {
        [OperationContract]
        List<Review> GetReviewsForMediaId(int mediaId);

        [OperationContract]
        void SubmitReview(Review pReview);
    }
}

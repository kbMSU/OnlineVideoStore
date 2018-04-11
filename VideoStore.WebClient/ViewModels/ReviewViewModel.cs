using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Services.Interfaces;

namespace VideoStore.WebClient.ViewModels
{
    public class ReviewViewModel
    {
        private ICatalogueService CatalogueService
        {
            get
            {
                return ServiceFactory.Instance.CatalogueService;
            }
        }

        public ReviewViewModel(int mediaId)
        {
            this.mediaId = mediaId;
        }

        private int mediaId;
    }
}
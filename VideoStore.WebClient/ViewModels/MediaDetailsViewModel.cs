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

        public MediaDetailsViewModel(int mediaId)
        {
            Item = CatalogueService.GetMediaById(mediaId);
        }

        public Media Item;
    }
}
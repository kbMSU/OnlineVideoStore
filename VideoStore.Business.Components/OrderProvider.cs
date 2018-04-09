using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using System.Transactions;
using System.Data.Entity;

namespace VideoStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        public void SubmitOrder(Entities.Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                AttachEntitiesToContext(lContainer, pOrder);

                pOrder.UpdateStockLevels();

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        private void AttachEntitiesToContext(VideoStoreEntityModelContainer pContainer, Order pOrder)
        {
 
            pContainer.Users.Attach(pOrder.Customer);
            pOrder.OrderItems.ToList().ForEach(p => pContainer.Media.Attach(p.Media));

            pContainer.Orders.Add(pOrder);
            //load stock entities so that we'll be able to update their details
            LoadEntitiesForOrderSubmission(pOrder, pContainer);
        }


        private void LoadEntitiesForOrderSubmission(Order pOrder, VideoStoreEntityModelContainer pContainer)
        {
            foreach(OrderItem item in pOrder.OrderItems)
            {
                item.Media.Stocks = pContainer.Media.Include("Stocks").First(p => p.Id == item.Media.Id).Stocks;
            }
        }


    }
}

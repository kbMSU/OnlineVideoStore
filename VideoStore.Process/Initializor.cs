using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VideoStore.Business.Entities;

namespace VideoStore.Process
{
    public class Initializor
    {
        public static void Initialize(bool pHostWCFServices = true)
        {
            ResolveDependencies();
            if (pHostWCFServices)
            {
                InsertDummyEntities();
                HostServices();
            }
        }

        private static void InsertDummyEntities()
        {
            InsertCatalogueEntities();
        }

        private static void InsertCatalogueEntities()
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                if (lContainer.Media.Count() == 0)
                {
                    Media lGreatExpectations = new Media()
                    {
                        Director = "Rene Clair",
                        Genre = "Fiction",
                        Price = 20.0M,
                        Title = "And Then there were None"
                    };

                    lContainer.Media.Add(lGreatExpectations);


                    Stock lGreatExpectationsStock = new Stock()
                    {
                        Media = lGreatExpectations,
                        Quantity = 5,
                        Warehouse = "Neutral Bay"
                    };

                    lContainer.Stocks.Add(lGreatExpectationsStock);

                    Media lSoloist = new Media()
                    {
                        Director = "The Soloist",
                        Genre = "Fiction",
                        Price = 15.0M,
                        Title = "The Soloist"
                    };

                    lContainer.Media.Add(lSoloist);

                    Stock lSoloistStock = new Stock()
                    {
                        Media = lSoloist,
                        Quantity = 7,
                        Warehouse = "Neutral Bay"
                    };

                    lContainer.Stocks.Add(lSoloistStock);

                    for (int i = 0; i < 30; i++)
                    {
                        Media lItem = new Media()
                        {
                            Director = String.Format("Director {0}", i.ToString()),
                            Genre = String.Format("Genre {0}", i),
                            Price = i,
                            Title = String.Format("Title {0}", i)
                        };

                        lContainer.Media.Add(lItem);

                        Stock lStock = new Stock()
                        {
                            Media = lItem,
                            Quantity = 7,
                            Warehouse = String.Format("Warehouse {0}", i)
                        };

                        lContainer.Stocks.Add(lStock);
                    }


                    lContainer.SaveChanges();
                    lScope.Complete();
                }



            }

        }


        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }


        private static void HostServices()
        {
            List<ServiceHost> lHosts = new List<ServiceHost>();
            try
            {

                Configuration lAppConfig = GetConfiguration();// ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ServiceModelSectionGroup lServiceModel = ServiceModelSectionGroup.GetSectionGroup(lAppConfig);

                System.ServiceModel.Configuration.ServicesSection lServices = lServiceModel.Services;
                foreach (ServiceElement lServiceElement in lServices.Services)
                {
                    ServiceHost lHost = new ServiceHost(Type.GetType(GetAssemblyQualifiedServiceName(lServiceElement.Name)));
                    lHost.Open();
                    lHosts.Add(lHost);
                }
                Console.WriteLine("Service Started, press Q key to quit");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
            finally
            {
                foreach (ServiceHost lHost in lHosts)
                {
                    lHost.Close();
                }
            }
        }

        private static String GetAssemblyQualifiedServiceName(String pServiceName)
        {
            return String.Format("{0}, {1}", pServiceName, System.Configuration.ConfigurationManager.AppSettings["ServiceAssemblyName"].ToString());
        }

        private static Configuration GetConfiguration()
        {
            System.Configuration.Configuration config = null;
            if (System.Web.HttpContext.Current != null)
                config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            else
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config;
        }
    }

}

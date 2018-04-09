using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using VideoStore.Services;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.ComponentModel.Composition.Hosting;
using VideoStore.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using VideoStore.Business.Entities;
using System.Transactions;
using System.ServiceModel.Description;
using VideoStore.Business.Components.Interfaces;

namespace VideoStore.Process
{
    public class Program
    {
        static void Main(string[] args)
        {
            Initializor.Initialize();
        }


    }
}

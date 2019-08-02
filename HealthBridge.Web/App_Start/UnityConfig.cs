using HealthBridge.BusinessLogic.Implementation;
using HealthBridge.BusinessLogic.Interfaces;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace HealthBridge.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IPatient, PatientManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoice, InvoiceManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoiceLine, InvoiceLineManager>(new HierarchicalLifetimeManager());
            //container.RegisterType(IInvoiceLine, InvoiceManager)(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
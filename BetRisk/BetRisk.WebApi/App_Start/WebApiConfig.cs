using System.Web.Http;
using BetRisk.Data;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;

namespace BetRisk.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );

            IWindsorContainer container = new WindsorContainer();

            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleScoped()
                );

            container.Register(
                Component.For<IBetDataAccess>().ImplementedBy<BetDataAccess>(),
                Component.For<IBetRiskCalculator>().ImplementedBy<BetRiskCalculator>(),
                Component.For<ICustomerRiskCalculator>().ImplementedBy<CustomerRiskCalculator>(),
                Component.For<IRiskService>().ImplementedBy<RiskService>()
                );

            config.DependencyResolver = new WindsorDependencyResolver(container.Kernel);
        }
    }
}

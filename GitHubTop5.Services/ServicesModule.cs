using Autofac;
using GitHubTop5.Services.Interfaces;
using System.Net.Http;

namespace GitHubTop5.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Register all services */
            var type = typeof(IService);
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => type.IsAssignableFrom(t))
                .AsImplementedInterfaces();

            builder.Register(c => new HttpClient())
                .As<HttpClient>();

        }
    }
}

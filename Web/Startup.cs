using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Web.Models;
using Web.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System.Web.Mvc;
using System.Reflection;
using SimpleInjector.Integration.Web.Mvc;
using System.Configuration;
using AutoMapper;
using Web.Services.Interfaces;
using Web.Repositories.Interfaces;
using Web.Repositories;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public class Startup
    {
        public Startup()
        {
        }
        public void Configuration(IAppBuilder app)
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var mydb = new MyDBContext();

            mydb.AddDataIfEmpty();

            container.Register<MyDBContext>(() => new MyDBContext(), Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IUserRepo, UserRepo>(Lifestyle.Scoped);
            container.Register<IRegionalCenterRepo, RegionalCenterRepo>(Lifestyle.Scoped);

            container.Register(typeof(IGenericRepo<>), typeof(GenericRepo<>), Lifestyle.Transient);



            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(typeof(Startup).Assembly);
            });

            container.Register<IMapper>(() => mapperConfig.CreateMapper(container.GetInstance));

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // настраиваем контекст и менеджер
            //app.CreatePerOwinContext<MyDBContext>(MyDBContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
            });
        }
    }
}
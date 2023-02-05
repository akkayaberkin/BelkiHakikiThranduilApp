using Autofac;
using BelkiHakiki.Caching;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;
using BelkiHakiki.Repository;
using BelkiHakiki.Repository.Repositories;
using BelkiHakiki.Repository.UnitOfWorks;
using BelkiHakiki.Service.Mapping;
using BelkiHakiki.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace BelkiHakiki.API.Modules
{
    public class RepoServiceModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


           // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}

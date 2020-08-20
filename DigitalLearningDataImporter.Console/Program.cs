using System;
using DigitalLearningIntegration.Application.Services.Prod;
using DigitalLearningIntegration.Application.Services.Seg;
using AutoMapper;
//using DigitalLearningIntegration.Infraestructure.Repository.Users;
//using DigitalLearningIntegration.DAL;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using System.Configuration;
//using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.Linq;
using Mindbox.Data.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DigitalLearningDataImporter.DALstd;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Application.Services.GobEntity;

namespace DigitalLearningDataImporter.Console
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnecionString"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<HCMKomatsuSegContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var segContext = new HCMKomatsuSegContext(optionsBuilder.Options);

            connectionString = ConfigurationManager.ConnectionStrings["DatabaseProdConnecionString"].ConnectionString;
            var optionsBuilder2 = new DbContextOptionsBuilder<HCMKomatsuProdContext>();
            optionsBuilder2.UseSqlServer(connectionString);
            var prodContext = new HCMKomatsuProdContext(optionsBuilder2.Options);

            services.AddSingleton(segContext);
            services.AddSingleton(prodContext);

            ISegAppServices segAppServices = new SegAppServices(segContext);
            IProdAppServices prodAppServices = new ProdAppServices(prodContext);
            IGopEntityServices gopEntityServices = new GopEntityServices();

            services.AddSingleton(segAppServices);
            services.AddSingleton(prodAppServices);
            services.AddSingleton(gopEntityServices);
            services.AddSingleton<ConsoleApplication>();

            _serviceProvider = services.BuildServiceProvider(true);
        }
        static void Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
            DisposeServices();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}

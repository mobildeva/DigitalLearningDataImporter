using DigitalLearningIntegration.Application.Services.Seg;
using System.Configuration;
using AutoMapper;
using System.Linq;
using System;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using DigitalLearningIntegration.Application.Services.Prod;

namespace DigitalLearningDataImporter.Console
{
    class ConsoleApplication
    {
        //private static IMapper _mapper;
        //public ConsoleApplication(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}

        private static ISegAppServices _segServ;
        private static IProdAppServices _prodServ;

        public ConsoleApplication(ISegAppServices segService, IProdAppServices prodAppServices)
        {
            _segServ = segService;
            _prodServ = prodAppServices;
        }

        public void Run()
        {
            //_customer.CreateCustomer();
            //var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder();
            //var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnecionString"].ConnectionString;
            //optionsBuilder.UseSqlServer(connectionString);
            //using (var dbContext = new DatabaseContext(optionsBuilder.Options))
            //{
            //    dbContext.Users.Add(new User() { Name = "Bassam" });
            //    dbContext.SaveChanges();
            //}
            //var dc = new SegDataContext(connectionString);

            //var segServ = new SegAppServices(connectionString);//, _mapper);
            var users = _segServ.GetUsers();

            var user = users.FirstOrDefault();

            var pi = _prodServ.GetInfos();

            var ipf = pi.FirstOrDefault();

            //segServ.AddUser(new UserDto
            //{
            //    Username = "Yosbel",
            //    Password = "Yobsel"
            //});

            //Console.WriteLine("Hello World!");
        }
    }
}

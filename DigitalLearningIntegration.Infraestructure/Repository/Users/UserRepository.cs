using DigitalLearningDataImporter.DALstd;
using System;
using System.Collections.Generic;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using DigitalLearningIntegration.Infraestructure.Dto;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;

namespace DigitalLearningIntegration.Infraestructure.Repository.Users
{
    public class UserRepository : Repository<DigitalLearningDataImporter.DALstd.Users>, IUserRepository
    {
        private readonly HCMKomatsuSegContext _context;

        public UserRepository(HCMKomatsuSegContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        //public UserRepository(string connectionString)
        //{
        //    var optionsBuilder = new DbContextOptions<HCMKomatsuSegContext>().;
        //    _context = new HCMKomatsuSegContext(optionsBuilder);
        //}

        public ResultDto CreatedOrUpdate(DigitalLearningDataImporter.DALstd.Users entity)
        {
            ResultDto Result = new ResultDto
            {
                Result = true,
                Message = "Success"
            };
            try
            {
                if (entity.Id.Equals(0))
                {
                    Add(entity);
                    Result.Id = entity.Id;
                }
                else
                {
                    Update(entity);
                }
            }
            catch (Exception ex)
            {
                Result = new ResultDto
                {
                    Result = false,
                    Message = ex.Message
                };
            }
            return Result;
        }

        public override DigitalLearningDataImporter.DALstd.Users GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DigitalLearningDataImporter.DALstd.Users> GetUsers()
        {
            return GetAll();
        }
    }
}

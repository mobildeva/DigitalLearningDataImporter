using AutoMapper;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using DigitalLearningIntegration.Infraestructure.Repository.Users;
using DigitalLearningDataImporter.DALstd;
using System;
//using Serilog.Sinks.File;
//using Serilog.Sinks.SystemConsole;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DigitalLearningIntegration.Application.Services.Seg
{
    public class SegAppServices : ISegAppServices
    {
        //private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        //public SegAppServices(IUserRepository repository)//IMapper mapper, IUserRepository repository)
        //{
        //    //_mapper = mapper;
        //    _repository = repository;
        //}

        //public SegAppServices(string dbConnectionString)//, IMapper mapper)
        //{
        //    //_mapper = mapper;

        //    var optionsBuilder = new DbContextOptionsBuilder<HCMKomatsuSegContext>();
        //    optionsBuilder.UseSqlServer(dbConnectionString);

        //    var segContext = new HCMKomatsuSegContext(optionsBuilder.Options);

        //    _repository = new UserRepository(segContext);
        //}

        public SegAppServices(HCMKomatsuSegContext context)
        {
            _userRepository = new UserRepository(context);
        }
        public int AddUser(UserDto userDto)
        {
            try
            {
                var entity = new DigitalLearningDataImporter.DALstd.Users
                {
                    Username = userDto.Username,
                    Password = userDto.Password
                };//_mapper.Map<DigitalLearningDataImporter.DALstd.Users>(userDto);
                _userRepository.CreatedOrUpdate(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
                //return new ResultDto
                //{
                //    Result = false,
                //    Message = ex.Message
                //};
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _userRepository.Get().Select(u => new UserDto(u)); //_mapper.Map<UserDto>(u));
        }
    }
}

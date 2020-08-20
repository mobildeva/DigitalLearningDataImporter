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
        private readonly IUserRepository _userRepository;
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
                };
                _userRepository.CreatedOrUpdate(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public UserDto GetUserByRUTUserName(string usernameRut)
        {
            var aux = _userRepository.GetUserByRUTUserName(usernameRut);
            if (aux != null)
                return new UserDto(aux);
            else return null;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _userRepository.Get().Select(u => new UserDto(u));
        }
    }
}

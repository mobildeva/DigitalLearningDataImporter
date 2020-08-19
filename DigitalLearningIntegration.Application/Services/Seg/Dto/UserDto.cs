using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningDataImporter.DALstd;

namespace DigitalLearningIntegration.Application.Services.Seg.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserDto(Users user)
        {
            Username = user.Username;
            Password = user.Password;
        }
        public UserDto()
        {

        }
    }
}

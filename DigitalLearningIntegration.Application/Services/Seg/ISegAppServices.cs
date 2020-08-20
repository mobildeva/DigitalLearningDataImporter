using DigitalLearningIntegration.Application.Services.Seg.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Seg
{
    public interface ISegAppServices
    {
        int AddUser(UserDto userDto);
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByRUTUserName(string usernameRut);
    }
}

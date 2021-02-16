using DigitalLearningIntegration.Application.Services.Seg.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Seg
{
    public interface ISegAppServices
    {
        int AddUser(UserDto userDto);
        void AddUsers(IEnumerable<UserDto> users);
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByRUTUserName(string usernameRut);
        UserDto GetUserById(int userId);
        IEnumerable<ClienteUsersDto> GetUsersByClientId(int clientId);
        int AddClientUser(ClienteUsersDto clienteUsersDto);
        void AddClientsUsers(IEnumerable<ClienteUsersDto> clienteUsersDtos);
        void DeactivateUsers(IEnumerable<ClienteUsersDto> clienteUsersDtos);
        void SaveChanges();
        ClientDto GetClientBySocietyId(int societyId);
        UserProfileDto GetUserByUserIdAndPerfilId(int userId, int profileId);
        UserProfileDto GetUserProfileById(int userProfileId);
        UserProfileDto GetUserByUserId(int userId);
        void AddProfiles(IEnumerable<UserProfileDto> profilesToAdd);
        UserDto GetUserByName(string userName);
    }
}

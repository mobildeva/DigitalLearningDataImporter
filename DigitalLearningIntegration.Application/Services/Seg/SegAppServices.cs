using AutoMapper;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using DigitalLearningIntegration.Infraestructure.Repository.Users;
using DigitalLearningIntegration.Infraestructure.Repository.Clients;
using DigitalLearningDataImporter.DALstd;
using System;
//using Serilog.Sinks.File;
//using Serilog.Sinks.SystemConsole;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using Users = DigitalLearningDataImporter.DALstd.Users;
using DigitalLearningIntegration.Infraestructure.Repository.UserProfile;

namespace DigitalLearningIntegration.Application.Services.Seg
{
    public class SegAppServices : ISegAppServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClientUsersRepository _clientUsersRepository;
        private readonly IUserProfile _userProfRepository;
        public SegAppServices(HCMKomatsuSegContext context)
        {
            _userRepository = new UserRepository(context);
            _clientUsersRepository = new ClientUsersRepository(context);
            _clientRepository = new ClientRepository(context);
            _userProfRepository = new UserProfile(context);
        }

        public void AddClientsUsers(IEnumerable<ClienteUsersDto> clienteUsersDtos)
        {
            try
            {
                _clientUsersRepository.AddRange(clienteUsersDtos.Select(userDto => new ClienteUsers
                {
                    IdClientes = userDto.IdClientes,
                    IdUsers = userDto.IdUsers,
                    Activo = true
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AddClientUser(ClienteUsersDto clienteUsersDto)
        {
            try
            {
                var entity = new ClienteUsers
                {
                    Activo = clienteUsersDto.Activo,
                    IdClientes = clienteUsersDto.IdClientes,
                    IdUsers = clienteUsersDto.IdUsers
                };
                _clientUsersRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void AddProfiles(IEnumerable<UserProfileDto> profilesToAdd)
        {
            try
            {
                _userProfRepository.AddRange(profilesToAdd.Select(userDto => new UsersPerfil
                {
                    IdPerfil = userDto.IdPerfil,
                    IdUsers = userDto.IdUsers,
                    Activo = true
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AddUser(UserDto userDto)
        {
            try
            {
                var entity = new Users
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Bloqueado = userDto.Bloqueado,
                    Activo = userDto.Activo,
                    Nombres = userDto.Nombres,
                    Fecha = userDto.Fecha
                };
                _userRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void AddUsers(IEnumerable<UserDto> users)
        {
            try
            {
                _userRepository.AddRange(users.Select(userDto => new Users
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Bloqueado = userDto.Bloqueado,
                    Activo = userDto.Activo,
                    Nombres = userDto.Nombres,
                    Fecha = userDto.Fecha,
                    ClienteUsers = new List<ClienteUsers>(userDto.ClienteUsers.Select(cu => new ClienteUsers { Activo = cu.Activo, IdClientes = cu.IdClientes })),
                    UsersPerfil = new List<UsersPerfil>(userDto.ProfileUsers.Select(cu => new UsersPerfil { Activo = cu.Activo, IdPerfil = cu.IdPerfil }))
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeactivateUsers(IEnumerable<ClienteUsersDto> clienteUsersDtos)
        {
            try
            {
                Users userAux;
                ClienteUsers clienteUsersAux;
                foreach (ClienteUsersDto clientUserDto in clienteUsersDtos)
                {
                    userAux = _userRepository.GetByIdSingle(clientUserDto.IdUsers.Value);
                    if (userAux != null)
                    {
                        userAux.Activo = false;
                        userAux.Bloqueado = true;
                        userAux.PrimerIngreso = false;
                        //_userRepository.Update(userAux);
                    }

                    clienteUsersAux = _clientUsersRepository.GetClientUsersByClientUserId(clientUserDto.IdClientes, clientUserDto.IdUsers);
                    if (clienteUsersAux != null)
                    {
                        clienteUsersAux.Activo = false;
                        //_clientUsersRepository.Update(clienteUsersAux);
                    }
                }

                _userRepository.Commit();
                _clientUsersRepository.Commit();
            }
            catch (Exception)
            {
                return;
            }
        }

        public ClientDto GetClientBySocietyId(int societyId)
        {
            var aux = _clientRepository.GetClientBySocietyId(societyId);
            if (aux != null)
                return new ClientDto(aux);
            else return null;
        }

        public UserDto GetUserById(int userId)
        {
            var aux = _userRepository.GetByIdSingle(userId);
            if (aux != null)
                return new UserDto(aux);
            else return null;
        }

        public UserDto GetUserByName(string userName)
        {
            var aux = _userRepository.GetByUserName(userName);
            if (aux != null)
                return new UserDto(aux);
            else return null;
        }

        public UserDto GetUserByRUTUserName(string usernameRut)
        {
            var aux = _userRepository.GetUserByRUTUserName(usernameRut);
            if (aux != null)
                return new UserDto(aux);
            else return null;
        }

        public UserProfileDto GetUserByUserIdAndPerfilId(int userId, int profileId)
        {
            var aux = _userProfRepository.GetUserByUserIdAndPerfilId(userId, profileId);
            if (aux != null)
                return new UserProfileDto(aux);
            else return null;
        }

        public UserProfileDto GetUserProfileById(int userProfileId)
        {
            var aux = _userProfRepository.GetByIdSingle(userProfileId);
            if (aux != null)
                return new UserProfileDto(aux);
            else return null;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _userRepository.Get().Select(u => new UserDto(u));
        }

        public IEnumerable<ClienteUsersDto> GetUsersByClientId(int clientId)
        {
            return _clientUsersRepository.GetUsersByClientId(clientId).Select(cu => new ClienteUsersDto(cu));
        }

        public void SaveChanges()
        {
            _userRepository.Commit();
            _clientUsersRepository.Commit();
        }
    }
}

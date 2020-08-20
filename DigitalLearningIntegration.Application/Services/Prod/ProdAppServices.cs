﻿using DigitalLearningIntegration.Infraestructure.Repository.PersonalInfo;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.Repository.Genre;
using DigitalLearningIntegration.Infraestructure.Repository.CivilStatus;
using DigitalLearningIntegration.Infraestructure.Repository.Country;
using DigitalLearningIntegration.Infraestructure.Repository.Peoples;
using DigitalLearningIntegration.Infraestructure.Repository.Job;
using DigitalLearningIntegration.Infraestructure.Repository.BussUnit;
using DigitalLearningIntegration.Infraestructure.Repository.OrgUnit;
using DigitalLearningIntegration.Infraestructure.Repository.ContractType;
using DigitalLearningIntegration.Infraestructure.Repository.Society;

namespace DigitalLearningIntegration.Application.Services.Prod
{
    public class ProdAppServices : IProdAppServices
    {
        private readonly IPersonalInfoRepository _pInfoRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICivilStatusRepository _civilStaRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IPeoplesRepository _peopleRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IBussUnitRepository _buRepository;
        private readonly IOrgUnitRepository _ouRepository;
        private readonly IContractTypeRepository _contTypeRepository;
        private readonly ISocietyRepository _societyRepository;

        public ProdAppServices(HCMKomatsuProdContext context)
        {
            _pInfoRepository = new PersonalInfoRepository(context);
            _genreRepository = new GenreRepository(context);
            _civilStaRepository = new CivilStatusRepository(context);
            _countryRepository = new CountryRepository(context);
            _peopleRepository = new PeoplesRepository(context);
            _jobRepository = new JobRepository(context);
            _buRepository = new BussUnitRepository(context);
            _ouRepository = new OrgUnitRepository(context);
            _contTypeRepository = new ContractTypeRepository(context);
            _societyRepository = new SocietyRepository(context);
        }

        public int AddBussUnit(BussUnitDto piDto)
        {
            try
            {
                var entity = new UnidadesNegocio
                {
                    Activo = piDto.Activo,
                    Nombre = piDto.Nombre,
                    IdCentroCosto = piDto.IdCentroCosto,
                    IdSociedad = piDto.IdSociedad
                };
                _buRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddCivilStatus(CivilStatusDto piDto)
        {
            try
            {
                var entity = new EstadoCivil
                {
                    Activo = piDto.Activo,
                    Nombre = piDto.Nombre
                };
                _civilStaRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddContType(ContractTypeDto contractTypeDto)
        {
            try
            {
                var entity = new TipoContrato
                {
                    Activo = contractTypeDto.Activo,
                    Nombre = contractTypeDto.Nombre
                };
                _contTypeRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddCountry(CountryDto piDto)
        {
            try
            {
                var entity = new Pais
                {
                    CodigoArea = piDto.CodigoArea,
                    CodigoSence = piDto.CodigoSence,
                    Activo = piDto.Activo,
                    Nombre = piDto.Nombre
                };
                _countryRepository.Add(entity);
                return entity.IdPais;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddGenre(GenreDto piDto)
        {
            try
            {
                var entity = new Genero
                {
                    Activo = piDto.Activo,
                    Nombre = piDto.Nombre
                };
                _genreRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Addjob(JobDto piDto)
        {
            try
            {
                var entity = new Cargos
                {
                    Activo = true,
                    Nombre = piDto.Nombre
                };
                _jobRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddOrgUnit(OrgUnitDto orgUnitDto)
        {
            try
            {
                var entity = new UnidadesNegocio
                {
                    Activo = orgUnitDto.Activo,
                    Nombre = orgUnitDto.Nombre,
                    IdCentroCosto = orgUnitDto.IdCentroCosto,
                    IdSociedad = orgUnitDto.IdSociedad
                };
                _buRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddPeople(PeoplesDto people)
        {
            try
            {
                var entity = new Personas
                {
                    Activo = true,
                    ApellidoMaterno = people.ApellidoMaterno,
                    ApellidoPaterno = people.ApellidoPaterno,
                    IdentificacionUnica = people.IdentificacionUnica,
                    Dv = people.Dv,
                    Nombre = people.Nombre,
                    Email = people.Email,
                    Fono = people.Fono,
                    Celular = people.Celular
                };
                _peopleRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddPersonalInfo(PersonalInfoDto piDto)
        {
            try
            {
                var entity = new InformacionPersonal
                {
                    FechaNacimiento = piDto.FechaNacimiento,
                    EmailPersonal = piDto.EmailPersonal,
                    Direccion = piDto.Direccion.Trim(),
                };
                _pInfoRepository.Add(entity);
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

        public int AddSociety(SocietyDto societyDto)
        {
            try
            {
                var entity = new Sociedad
                {
                    IdentificacionUnica = societyDto.IdentificacionUnica,
                    Activo = societyDto.Activo,
                    Nombre = societyDto.Nombre
                };
                _societyRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public BussUnitDto GetBussUnitByNameSociety(string name, int sociatyId)
        {
            var aux = _buRepository.GetByName(name, sociatyId);
            if (aux != null)
            {
                return new BussUnitDto(aux);
            }
            else return null;
        }

        public CivilStatusDto GetCivilStatusByName(string name)
        {
            var aux = _civilStaRepository.GetByName(name);
            if (aux != null)
            {
                return new CivilStatusDto(aux);
            }
            else return null;
        }

        public ContractTypeDto GetContTypeByName(string name)
        {
            var aux = _contTypeRepository.GetByName(name);
            if (aux != null)
            {
                return new ContractTypeDto(aux);
            }
            else return null;
        }

        public CountryDto GetCountryByName(string name)
        {
            var aux = _countryRepository.GetByName(name);
            if (aux != null)
            {
                return new CountryDto(aux);
            }
            else return null;
        }

        public GenreDto GetGenreByName(string name)
        {
            var aux = _genreRepository.GetByName(name);
            if (aux != null)
            {
                return new GenreDto(aux);
            }
            else return null;
        }

        public IEnumerable<PersonalInfoDto> GetInfos()
        {
            return _pInfoRepository.Get().Select(pi => new PersonalInfoDto(pi));
        }

        public JobDto GetJobByName(string name)
        {
            var aux = _jobRepository.GetByName(name);
            if (aux != null)
            {
                return new JobDto(aux);
            }
            else return null;
        }

        public OrgUnitDto GetOrgUnitByNameSociety(string name, int sociatyId)
        {
            var aux = _ouRepository.GetByName(name, sociatyId);
            if (aux != null)
            {
                return new OrgUnitDto(aux);
            }
            else return null;
        }

        public PeoplesDto GetPeopleByRUT(string rut)
        {
            var splitRut = rut.Split('-');
            if (splitRut.Any() && splitRut.Count() == 2)
            {
                var aux = _peopleRepository.GetByIuAndDv(splitRut[0], splitRut[1]);
                if (aux != null)
                {
                    return new PeoplesDto(aux);
                }
                else return null;
            }
            else
                return null;
        }

        public SocietyDto GetSocietyByName(string name)
        {
            var aux = _societyRepository.GetByName(name);
            if (aux != null)
            {
                return new SocietyDto(aux);
            }
            return null;
        }

        public SocietyDto GetSocietyByUniqueId(string ui)
        {
            var aux = _societyRepository.GetByUniqueIdentifier(ui);
            if (aux != null)
            {
                return new SocietyDto(aux);
            }
            return null;
        }
    }
}

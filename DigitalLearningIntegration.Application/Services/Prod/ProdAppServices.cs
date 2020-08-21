using DigitalLearningIntegration.Infraestructure.Repository.PersonalInfo;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
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
using DigitalLearningIntegration.Infraestructure.Repository.Family;
using DigitalLearningIntegration.Infraestructure.Repository.CostCenter;
using DigitalLearningIntegration.Infraestructure.Repository.Location;
using DigitalLearningIntegration.Infraestructure.Repository.BloodG;
using DigitalLearningIntegration.Infraestructure.Repository.Areas;
using DigitalLearningIntegration.Infraestructure.Repository.Scholarship;
using DigitalLearningIntegration.Infraestructure.Repository.OcupLevel;
using DigitalLearningIntegration.Infraestructure.Repository.Isapre;
using DigitalLearningIntegration.Infraestructure.Repository.CurrentJob;
using DigitalLearningIntegration.Infraestructure.Repository.SchedulesRule;
using DigitalLearningIntegration.Infraestructure.Repository.WorkingDay;
using DigitalLearningIntegration.Infraestructure.Repository.Afp;
using DigitalLearningIntegration.Infraestructure.Repository.Local;

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
        private readonly IFamilyRepository _familyRepository;
        private readonly ICostCenterRepository _costCentRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IBloodGRepository _bgRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IScholarshipRepository _schoRepository;
        private readonly IOcupLevelRepository _ocLevelRepository;
        private readonly IIsapreRepository _isapreRepository;
        private readonly ICurrentJobRepository _cuJobRepository;
        private readonly ISchedRuleRepository _schedRuleRepository;
        private readonly IWorkingDayRepository _workDayRepository;
        private readonly IAfpRepository _afpRepository;
        private readonly ILocalRepository _localRepository;

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
            _familyRepository = new FamilyRepository(context);
            _costCentRepository = new CostCenterRepository(context);
            _locationRepository = new LocationRepository(context);
            _bgRepository = new BloodGRepository(context);
            _areaRepository = new AreaRepository(context);
            _schoRepository = new ScholarshipRepository(context);
            _ocLevelRepository = new OcupLevelRepository(context);
            _isapreRepository = new IsapreRepository(context);
            _cuJobRepository = new CurrentJobRepository(context);
            _schedRuleRepository = new SchedRuleRepository(context);
            _workDayRepository = new WorkingDayRepository(context);
            _afpRepository = new AfpRepository(context);
            _localRepository = new LocalRepository(context);
        }

        public int AddAfp(AfpDto afp)
        {
            try
            {
                var entity = new Afp
                {
                    Nombre = afp.Nombre,
                    Activo = afp.Activo
                };
                _afpRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddArea(AreaDto area)
        {
            try
            {
                var entity = new Area
                {
                    Nombre = area.Nombre,
                    Activo = area.Activo,
                    FechaCr = DateTime.Now
                };
                _areaRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddBloodG(BloodGDto bg)
        {
            try
            {
                var entity = new GrupoSanguineo
                {
                    Nombre = bg.Nombre,
                    Activo = bg.Activo
                };
                _bgRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
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
                    IdSociedad = piDto.IdSociedad,
                    CodigoErp = piDto.CodigoErp
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

        public int AddCostCenter(CostCenterDto costCenterDto)
        {
            try
            {
                var entity = new CentroCosto
                {
                    IdSociedad = costCenterDto.IdSociedad,
                    Nombre = costCenterDto.Nombre,
                    Activo = costCenterDto.Activo,
                    Codigo = costCenterDto.Codigo,
                };
                _costCentRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception e)
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

        public int AddCurrentJob(CurrentJobsDto currentJobDto)
        {
            try
            {
                var entity = new PosicionLaboral
                {
                    IdSociedad = currentJobDto.IdSociedad,
                    NombrePosicion = currentJobDto.NombrePosicion,
                    Activo = currentJobDto.Activo,
                    IdPersona = currentJobDto.IdPersona,
                    IdSociedadContratante = currentJobDto.IdSociedadContratante,
                    IdUnidadOrganizacional = currentJobDto.IdUnidadOrganizacional,
                    IdUnidadNegocio = currentJobDto.IdUnidadNegocio,
                    IdCargo = currentJobDto.IdCargo,
                    IdEscolaridadSence = currentJobDto.IdEscolaridadSence,
                    IdPersonaJefe = currentJobDto.IdPersonaJefe,
                    FranquiciaSence = currentJobDto.FranquiciaSence,
                    IdUbicacion = currentJobDto.IdUbicacion,
                    IdTipoContrato = currentJobDto.IdTipoContrato,
                    FechaInicioContrato = currentJobDto.FechaInicioContrato,
                    FechaTerminoContrato = currentJobDto.FechaTerminoContrato,
                    IdNivelOcupacional = currentJobDto.IdNivelOcupacional,
                    IdCentroCosto = currentJobDto.IdCentroCosto
                };
                _cuJobRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddFamily(FamilyDto familyDto)
        {
            try
            {
                var entity = new FamiliaCargo
                {
                    IdSociedad = familyDto.IdSociedad,
                    Nombre = familyDto.Nombre,
                    Activo = familyDto.Activo
                };
                _familyRepository.Add(entity);
                return entity.Id;
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

        public int AddIsapre(IsapreDto isa)
        {
            try
            {
                var entity = new Isapres
                {
                    Nombre = isa.Nombre,
                    Activo = isa.Activo
                };
                _isapreRepository.Add(entity);
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

        public int AddLocal(LocalDto loc)
        {
            try
            {
                var entity = new Locales
                {
                    CodigoLocal = loc.CodigoLocal,
                    NombreLocal = loc.NombreLocal,
                    Activo = loc.Activo,
                    IdFormato = loc.IdFormato
                };
                _localRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int AddLocation(LocationDto loc)
        {
            try
            {
                var entity = new Ubicacion
                {
                    CodigoArea = loc.CodigoArea,
                    Orden = loc.Orden,
                    Nombre = loc.Nombre,
                    Activo = loc.Activo
                };
                _locationRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddOcupLevel(OcupLevelDto ocupLevelDto)
        {
            try
            {
                var entity = new NivelOcupacional
                {
                    IdSociedad = ocupLevelDto.IdSociedad,
                    Nombre = ocupLevelDto.Nombre,
                    Activo = ocupLevelDto.Activo
                };
                _ocLevelRepository.Add(entity);
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
                var entity = new UnidadesOrganizacional
                {
                    Activo = orgUnitDto.Activo,
                    Nombre = orgUnitDto.Nombre,
                    IdCentroCosto = orgUnitDto.IdCentroCosto,
                    IdSociedad = orgUnitDto.IdSociedad
                };
                _ouRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception e)
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
                    Celular = people.Celular,
                    IdCodigoArea = people.IdCodigoArea,
                    ConectaSence = people.ConectaSence,
                    Instructor = people.Instructor,
                    IdPersonaForo = people.IdPersonaForo,
                    IdConexion = people.IdConexion,
                    ClaveSence = people.ClaveSence
                };
                _peopleRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddPersonalInfo(PersonalInfoDto ip)
        {
            try
            {
                var entity = new InformacionPersonal
                {
                    FechaNacimiento = ip.FechaNacimiento,
                    EmailPersonal = ip.EmailPersonal,
                    Direccion = ip.Direccion.Trim(),
                    IdPersona = ip.IdPersona,
                    IdGenero = ip.IdGenero,
                    IdEstadoCivil = ip.IdEstadoCivil,
                    IdUbicacion = ip.IdUbicacion,
                    IdPaisNacionalidad = ip.IdPaisNacionalidad,
                    IdPaisResidencia = ip.IdPaisResidencia,
                    IdIsapre = ip.IdIsapre,
                    IdAfp = ip.IdAfp,
                    IdLocal = ip.IdLocal,
                    Activo = ip.Activo,
                    TallaPantalon = ip.TallaPantalon,
                    TallaCamisa = ip.TallaCamisa,
                    TallaZapatos = ip.TallaZapatos,
                    NumeroSeguridadSocial = ip.NumeroSeguridadSocial,
                    IdArea = ip.IdArea,
                    TelefonoFijo = ip.TelefonoFijo,
                    TelefonoMovil = ip.TelefonoMovil,
                    IdFamiliaCargo = ip.IdFamiliaCargo,
                    Numero = ip.Numero,
                    Otro = ip.Otro,
                    Altura = ip.Altura,
                    Peso = ip.Peso,
                    UsuarioMod = ip.UsuarioMod,
                    FechaMod = ip.FechaMod,
                    CurriculumVitae = ip.CurriculumVitae,
                    JornadaLaboral = ip.JornadaLaboral,
                    IdReglaPlanHorario = ip.IdReglaPlanHorario
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

        public int AddSchedRule(SchedulesRuleDto schedulesRuleDto)
        {
            try
            {
                var entity = new ReglaPlanHorario
                {
                    FechaCr = schedulesRuleDto.FechaCr,
                    FechaUp = schedulesRuleDto.FechaUp,
                    UsuarioCr = schedulesRuleDto.UsuarioCr,
                    UsuarioUp = schedulesRuleDto.UsuarioUp,
                    Nombre = schedulesRuleDto.Nombre,
                    Activo = schedulesRuleDto.Activo
                };
                _schedRuleRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddScholarship(ScholarshipDto scholarship)
        {
            try
            {
                var entity = new EscolaridadSence
                {
                    Activo = scholarship.Activo,
                    Nombre = scholarship.Nombre
                };
                _schoRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
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
                    Nombre = societyDto.Nombre,
                    Direccion = societyDto.Direccion,
                    Logo = societyDto.Logo
                };
                _societyRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddWorkingDay(WorkingDayDto wd)
        {
            try
            {
                var entity = new JornadaLaboral
                {
                    Descripcion = wd.Descripcion,
                    Nombre = wd.Nombre,
                    Activo = wd.Activo
                };
                _workDayRepository.Add(entity);
                return entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public AfpDto GetAfpByName(string name)
        {
            var aux = _afpRepository.GetAfpByName(name);
            if (aux != null)
            {
                return new AfpDto(aux);
            }
            else return null;
        }

        public AreaDto GetAreaByName(string name)
        {
            var aux = _areaRepository.GetByName(name);
            if (aux != null)
            {
                return new AreaDto(aux);
            }
            else return null;
        }

        public BloodGDto GetBloodGrByName(string name)
        {
            var aux = _bgRepository.GetByName(name);
            if (aux != null)
            {
                return new BloodGDto(aux);
            }
            else return null;
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

        public CostCenterDto GetCostCenterByNameSociety(string name, int societyId)
        {
            var aux = _costCentRepository.GetByName(name, societyId);
            if (aux != null)
                return new CostCenterDto(aux);
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

        public CurrentJobsDto GetCurrentJobByPeopleSociety(int peopleId, int societyId)
        {
            var aux = _cuJobRepository.GetCurrentJobByPeopleSociety(peopleId, societyId);
            if (aux != null)
                return new CurrentJobsDto(aux);
            else return null;
        }

        public FamilyDto GetFamilyByNameSociety(string name, int societyId)
        {
            var aux = _familyRepository.GetByName(name, societyId);
            if (aux != null)
                return new FamilyDto(aux);
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

        public IsapreDto GetIsapreByName(string name)
        {
            var aux = _isapreRepository.GetByName(name);
            if (aux != null)
                return new IsapreDto(aux);
            else return null;
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

        public LocalDto GetLocalByCode(string code)
        {
            var aux = _localRepository.GetByCode(code);
            if (aux != null)
            {
                return new LocalDto(aux);
            }
            else return null;
        }

        public LocationDto GetLocationByName(string name)
        {
            var aux = _locationRepository.GetByName(name);
            if (aux != null)
            {
                return new LocationDto(aux);
            }
            else return null;
        }

        public OcupLevelDto GetOcupLevelByNameSociety(string name, int societyId)
        {
            var aux = _ocLevelRepository.GetByName(name, societyId);
            if (aux != null)
                return new OcupLevelDto(aux);
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

        public PersonalInfoDto GetPersonalInfoById(int id)
        {
            var aux = _pInfoRepository.GetByIdSingle(id);
            if (aux != null)
            {
                return new PersonalInfoDto(aux);
            }
            else return null;
        }

        public PersonalInfoDto GetPersonalInfoByPersona(int peopleId)
        {
            var aux = _pInfoRepository.GetByPeopleId(peopleId);
            if (aux != null)
            {
                return new PersonalInfoDto(aux);
            }
            else return null;
        }

        public SchedulesRuleDto GetSchedRuleByName(string name)
        {
            var aux = _schedRuleRepository.GetByName(name);
            if (aux != null)
            {
                return new SchedulesRuleDto(aux);
            }
            else return null;
        }

        public ScholarshipDto GetScholarshipByName(string name)
        {
            var aux = _schoRepository.GetByName(name);
            if (aux != null)
            {
                return new ScholarshipDto(aux);
            }
            else return null;
        }

        public SocietyDto GetSocietyById(int societyId)
        {
            var aux = _societyRepository.GetByIdSingle(societyId);
            if (aux != null)
            {
                return new SocietyDto(aux);
            }
            else return null;
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

        public WorkingDayDto GetWorkingDayByName(string name)
        {
            var aux = _workDayRepository.GetByName(name);
            if (aux != null)
            {
                return new WorkingDayDto(aux);
            }
            else return null;
        }

        public void UpdateCurrentJob(CurrentJobsDto currentJobsDto)
        {
            var cj = _cuJobRepository.GetById(currentJobsDto.Id);

            if (cj != null)
            {
                cj.Activo = currentJobsDto.Activo;
                //cj.IdPersona =
                cj.IdUnidadOrganizacional = currentJobsDto.IdUnidadOrganizacional;
                cj.IdUnidadNegocio = currentJobsDto.IdUnidadNegocio;
                cj.IdUbicacion = currentJobsDto.IdUbicacion;
                cj.IdCargo = currentJobsDto.IdCargo;
                cj.IdEscolaridadSence = currentJobsDto.IdEscolaridadSence;
                cj.IdNivelOcupacional = currentJobsDto.IdNivelOcupacional;
                cj.FranquiciaSence = currentJobsDto.FranquiciaSence;
                cj.IdTipoContrato = currentJobsDto.IdTipoContrato;
                cj.FechaInicioContrato = currentJobsDto.FechaInicioContrato;
                cj.FechaTerminoContrato = currentJobsDto.FechaTerminoContrato;
                cj.IdPersonaJefe = currentJobsDto.IdPersonaJefe;
                cj.IdSociedadContratante = currentJobsDto.IdSociedadContratante;
                cj.IdCentroCosto = currentJobsDto.IdCentroCosto;
            }
        }

        public void UpdatePeople(PeoplesDto people)
        {
            var p = _peopleRepository.GetById(people.Id);

            if (p != null)
            {
                p.Activo = people.Activo;
                p.ApellidoMaterno = people.ApellidoMaterno;
                p.Nombre = people.Nombre;
                p.IdentificacionUnica = people.IdentificacionUnica;
                p.Dv = people.Dv;
                p.Email = people.Email;
                p.Fono = p.Fono;
                p.Celular = p.Celular;
                p.IdCodigoArea = p.IdCodigoArea;

                _peopleRepository.Update(p);
            }
        }

        public void UpdatePersonalInfo(PersonalInfoDto personalInfo)
        {
            var pi = _pInfoRepository.GetById(personalInfo.Id);

            if (pi != null)
            {
                pi.Activo = personalInfo.Activo;
                //pi.IdPersona = personalInfo.IdPersona;
                pi.FechaNacimiento = personalInfo.FechaNacimiento;
                pi.EmailPersonal = personalInfo.EmailPersonal;
                pi.IdGenero = personalInfo.IdGenero;
                pi.IdEstadoCivil = personalInfo.IdEstadoCivil;
                pi.IdPaisNacionalidad = personalInfo.IdPaisNacionalidad;
                pi.IdGrupoSanguineo = personalInfo.IdGrupoSanguineo;
                pi.IdIsapre = personalInfo.IdIsapre;
                pi.IdAfp = personalInfo.IdAfp;
                pi.Direccion = personalInfo.Direccion;
                pi.Numero = personalInfo.Numero;
                pi.Otro = personalInfo.Otro;
                pi.IdFamiliaCargo = personalInfo.IdFamiliaCargo;
                pi.IdArea = personalInfo.IdArea;
                pi.IdReglaPlanHorario = personalInfo.IdReglaPlanHorario;
                pi.JornadaLaboral = personalInfo.JornadaLaboral;
                pi.IdLocal = personalInfo.IdLocal;

                _pInfoRepository.Update(pi);
            }
        }
    }
}

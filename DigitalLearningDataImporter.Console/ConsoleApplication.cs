using DigitalLearningIntegration.Application.Services.Seg;
using System.Configuration;
using AutoMapper;
using System.Linq;
using System;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using DigitalLearningIntegration.Application.Services.Prod;
using DigitalLearningIntegration.Application.Utils;
using DigitalLearningIntegration.Application.GobEntity;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;
using DigitalLearningIntegration.Application.Services.GobEntity;
using DigitalLearningIntegration.Infraestructure.Repository.Genre;
using DigitalLearningIntegration.Application.GobEntity.Dto;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using DigitalLearningDataImporter.DALstd.ProdEntities;

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
        private static IGopEntityServices _gopServ;

        public ConsoleApplication(ISegAppServices segService, IProdAppServices prodAppServices, IGopEntityServices gopServ)
        {
            _segServ = segService;
            _prodServ = prodAppServices;
            _gopServ = gopServ;
        }

        public void Run()
        {
            try
            {
                var sftpHost = ConfigurationManager.AppSettings.Get("sftpHost");
                var sftpPort = (ConfigurationManager.AppSettings.Get("sftpPort") != null) ? int.Parse(ConfigurationManager.AppSettings.Get("sftpPort")) : 22;
                var sftpUserName = ConfigurationManager.AppSettings.Get("sftpUserName");
                var sftpPassword = ConfigurationManager.AppSettings.Get("sftpPassword");
                var sftpPath = ConfigurationManager.AppSettings.Get("sftpPath");
                var logsPath = @ConfigurationManager.AppSettings.Get("logsFullPath");
                var idSociedad = (ConfigurationManager.AppSettings.Get("idSociedad") != null) ? int.Parse(ConfigurationManager.AppSettings.Get("idSociedad")) : -1;
                var excelFileName = ConfigurationManager.AppSettings.Get("excelFileName");

                if (string.IsNullOrEmpty(logsPath))
                {
                    logsPath = "Logs.txt";
                }

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(logsPath, rollingInterval: RollingInterval.Minute)
                    .CreateLogger();

                Log.Information("------------------------------------------------");

                Log.Information("Starting the app");

                var xlsDestFilePath = @Environment.CurrentDirectory + "\\App_Data\\Import\\" + DateTime.Now.Ticks + "_" + excelFileName;

                var resultu = SftpManager.Get(excelFileName, sftpHost, sftpPort, sftpUserName, sftpPassword, xlsDestFilePath);

                var dataTable = ReadWriteExcel.ReadExcelSheet(xlsDestFilePath, true);

                var entities = _gopServ.GetEntities(dataTable);

                Log.Debug("Read " + entities.Count() + " entities");

                /*	Altas o Registros creados
	Actualización de registros activos
	Bajas o registros desactivados
	Registros inválidos*/
                var newUsers = 0;
                var updatesUsers = 0;
                var downUsers = 0;
                var invalidsUsers = 0;

                foreach (GopEntityDtoExpand item in entities)
                {
                    var user = _segServ.GetUserByRUTUserName(item.Rut);

                    if (user != null)
                    {//Update
                        item.hasUser = true;
                    }
                    else
                    {//Add
                        item.hasUser = false;
                    }

                    var boosExist = entities.Any(e => e.Rut == item.BossRut);
                    item.hasBoss = boosExist;

                    var defaultValue = 0;
                    var defaultTextValue = "Sin Información";

                    var idGenre = defaultValue;
                    GenreDto g;
                    if (!string.IsNullOrEmpty(item.Gender))
                    {
                        g = _prodServ.GetGenreByName(item.Gender);

                        if (g == null)
                        {
                            g = new GenreDto()
                            {
                                Activo = true,
                                Nombre = item.Gender
                            };
                            g.Id = _prodServ.AddGenre(g);
                        }
                    }
                    else
                    {
                        g = _prodServ.GetGenreByName(defaultTextValue);
                    }
                    if (g != null)
                        idGenre = g.Id;

                    var civilStatusId = defaultValue;
                    CivilStatusDto cs;
                    if (!string.IsNullOrEmpty(item.Civil_status))
                    {
                        cs = _prodServ.GetCivilStatusByName(item.Civil_status);

                        if (cs == null)
                        {
                            cs = new CivilStatusDto
                            {
                                Activo = true,
                                Nombre = item.Civil_status
                            };
                            cs.Id = _prodServ.AddCivilStatus(cs);
                        }
                    }
                    else
                    {
                        cs = _prodServ.GetCivilStatusByName(defaultTextValue);
                    }
                    if (cs != null)
                        civilStatusId = cs.Id;

                    var natId = defaultValue;
                    CountryDto country;
                    if (!string.IsNullOrEmpty(item.Country_code))
                    {
                        country = _prodServ.GetCountryByName(item.Country_code);
                        if (country == null)
                        {
                            country = new CountryDto
                            {
                                Activo = true,
                                Nombre = item.Country_code
                            };
                            country.IdPais = _prodServ.AddCountry(country);
                        }
                    }
                    else
                    {
                        country = _prodServ.GetCountryByName(defaultTextValue);
                    }
                    if (country != null)
                        natId = country.IdPais;

                    var bloodId = defaultValue;
                    BloodGDto bg;
                    if (!string.IsNullOrEmpty(item.BloodG))
                    {
                        bg = _prodServ.GetBloodGrByName(item.BloodG);
                        if (bg == null)
                        {
                            bg = new BloodGDto
                            {
                                Activo = true,
                                Nombre = item.BloodG
                            };
                            bg.Id = _prodServ.AddBloodG(bg);
                        }
                    }
                    else
                    {
                        bg = _prodServ.GetBloodGrByName(defaultTextValue);
                    }
                    if (bg != null)
                        bloodId = bg.Id;

                    var isapId = defaultValue;
                    IsapreDto isapre;
                    if (!string.IsNullOrEmpty(item.Isapre))
                    {
                        isapre = _prodServ.GetIsapreByName(item.Isapre);
                        if (isapre == null)
                        {
                            isapre = new IsapreDto
                            {
                                Activo = true,
                                Nombre = item.Isapre
                            };
                            isapre.Id = _prodServ.AddIsapre(isapre);
                        }
                    }
                    else
                    {
                        isapre = _prodServ.GetIsapreByName(defaultTextValue);
                    }
                    if (isapre != null)
                        isapId = isapre.Id;

                    var afpId = defaultValue;
                    AfpDto afp;
                    if (!string.IsNullOrEmpty(item.Afp))
                    {
                        afp = _prodServ.GetAfpByName(item.Afp);
                        if (afp == null)
                        {
                            afp = new AfpDto
                            {
                                Activo = true,
                                Nombre = item.Afp
                            };
                            afp.Id = _prodServ.AddAfp(afp);
                        }
                    }
                    else
                    {
                        afp = _prodServ.GetAfpByName(defaultTextValue);
                    }
                    if (afp != null)
                        afpId = afp.Id;

                    var orgUnitId = defaultValue;
                    OrgUnitDto orgUnit;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.Gerencia))
                    {
                        orgUnit = _prodServ.GetOrgUnitByNameSociety(item.CurrentJob.CustomAttributes.Gerencia, idSociedad);
                        if (orgUnit == null)
                        {
                            orgUnit = new OrgUnitDto()
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.CustomAttributes.Gerencia,
                                IdSociedad = idSociedad
                            };
                            orgUnit.Id = _prodServ.AddOrgUnit(orgUnit);
                        }
                    }
                    else
                    {
                        orgUnit = _prodServ.GetOrgUnitByNameSociety(defaultTextValue, idSociedad);
                    }
                    if (orgUnit != null)
                        orgUnitId = orgUnit.Id;

                    var bussUnitId = defaultValue;
                    BussUnitDto bussUnit;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.AreaGastos))
                    {
                        bussUnit = _prodServ.GetBussUnitByNameSociety(item.CurrentJob.CustomAttributes.AreaGastos, idSociedad);
                        if (bussUnit == null)
                        {
                            bussUnit = new BussUnitDto()
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.CustomAttributes.AreaGastos,
                                IdSociedad = idSociedad
                                //IdUnidadOrganizacional = null,//orgUnitId,
                                //CodigoErp = string.Empty
                            };
                            bussUnit.Id = _prodServ.AddBussUnit(bussUnit);
                        }
                    }
                    else
                    {
                        bussUnit = _prodServ.GetBussUnitByNameSociety(defaultTextValue, idSociedad);
                    }
                    if (bussUnit != null)
                        bussUnitId = bussUnit.Id;

                    var locId = defaultValue;
                    LocationDto location;
                    if (!string.IsNullOrEmpty(item.District))
                    {
                        location = _prodServ.GetLocationByName(item.District);
                        if (location == null)
                        {
                            location = new LocationDto()
                            {
                                Activo = true,
                                Nombre = item.District
                            };
                            location.Id = _prodServ.AddLocation(location);
                        }
                    }
                    else
                    {
                        location = _prodServ.GetLocationByName(defaultTextValue);
                    }
                    if (location != null)
                        locId = location.Id;

                    var scholid = defaultValue;
                    ScholarshipDto scholarship;
                    if (!string.IsNullOrEmpty(item.Scolarship))
                    {
                        scholarship = _prodServ.GetScholarshipByName(item.Scolarship);
                        if (scholarship == null)
                        {
                            scholarship = new ScholarshipDto()
                            {
                                Activo = true,
                                Nombre = item.Scolarship
                            };
                            scholarship.Id = _prodServ.AddScholarship(scholarship);
                        }
                    }
                    else
                    {
                        scholarship = _prodServ.GetScholarshipByName(defaultTextValue);
                    }
                    if (scholarship != null)
                        scholid = scholarship.Id;

                    var ocupLevelId = defaultValue;
                    OcupLevelDto ocupLevel;
                    if (!string.IsNullOrEmpty(item.OcupationalLevel))
                    {
                        ocupLevel = _prodServ.GetOcupLevelByNameSociety(item.OcupationalLevel, idSociedad);
                        if (ocupLevel == null)
                        {
                            ocupLevel = new OcupLevelDto()
                            {
                                Activo = true,
                                Nombre = item.OcupationalLevel,
                                IdSociedad = idSociedad
                            };
                            ocupLevel.Id = _prodServ.AddOcupLevel(ocupLevel);
                        }
                    }
                    else
                    {
                        ocupLevel = _prodServ.GetOcupLevelByNameSociety(defaultTextValue, idSociedad);
                    }
                    if (ocupLevel != null)
                        ocupLevelId = ocupLevel.Id;

                    var contTypeId = defaultValue;
                    ContractTypeDto contractType;
                    if (!string.IsNullOrEmpty(item.ContractType))
                    {
                        contractType = _prodServ.GetContTypeByName(item.ContractType);
                        if (contractType == null)
                        {
                            contractType = new ContractTypeDto()
                            {
                                Activo = true,
                                Nombre = item.ContractType
                            };
                            contractType.Id = _prodServ.AddContType(contractType);
                        }
                    }
                    else
                    {
                        contractType = _prodServ.GetContTypeByName(defaultTextValue);
                    }
                    if (contractType != null)
                        contTypeId = contractType.Id;

                    var contSocietyId = defaultValue;
                    SocietyDto contSociety;
                    if (!string.IsNullOrEmpty(item.Health_company))
                    {
                        contSociety = _prodServ.GetSocietyByUniqueId(item.Health_company);
                        if (contSociety == null)
                        {
                            contSociety = new SocietyDto()
                            {
                                Activo = true,
                                Nombre = item.Health_company,
                                IdentificacionUnica = item.Health_company,
                                Logo = string.Empty,
                                Direccion = string.Empty
                            };
                            contSociety.Id = _prodServ.AddSociety(contSociety);
                        }
                    }
                    else
                    {
                        contSociety = _prodServ.GetSocietyById(idSociedad);
                    }
                    if (contSociety != null)
                        contSocietyId = contSociety.Id;

                    var costCenterId = defaultValue;
                    CostCenterDto costCenter;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CostCenter))
                    {
                        costCenter = _prodServ.GetCostCenterByNameSociety(item.CurrentJob.CostCenter, idSociedad);
                        if (costCenter == null)
                        {
                            costCenter = new CostCenterDto()
                            {
                                Activo = true,
                                Codigo = item.CurrentJob.CostCenter,
                                IdSociedad = idSociedad,
                                Nombre = item.CurrentJob.CostCenter
                            };
                            costCenter.Id = _prodServ.AddCostCenter(costCenter);
                        }
                    }
                    else
                    {
                        costCenter = _prodServ.GetCostCenterByNameSociety(defaultTextValue, idSociedad);
                    }
                    if (costCenter != null)
                        costCenterId = costCenter.Id;

                    var localId = defaultValue;
                    LocalDto local;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.CodigoLocal))
                    {
                        local = _prodServ.GetLocalByCode(item.CurrentJob.CustomAttributes.CodigoLocal);
                        if (local == null)
                        {
                            local = new LocalDto()
                            {
                                Activo = true,
                                NombreLocal = item.CurrentJob.CustomAttributes.CodigoLocal,
                                //IdFormato = orgUnitId,
                                CodigoLocal = item.CurrentJob.CustomAttributes.CodigoLocal
                            };
                            local.Id = _prodServ.AddLocal(local);
                        }
                    }
                    else
                    {
                        local = _prodServ.GetLocalByCode(defaultTextValue);
                    }
                    if (local != null)
                        localId = local.Id;

                    var jobId = defaultValue;
                    JobDto job;
                    if (!string.IsNullOrEmpty(item.CurrentJob.Role.Name))
                    {
                        job = _prodServ.GetJobByName(item.CurrentJob.Role.Name);
                        if (job == null)
                        {
                            job = new JobDto()
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.Role.Name,
                                IdSociedad = idSociedad,
                                FechaCreacion = DateTime.Now
                            };
                            job.Id = _prodServ.Addjob(job);
                        }
                    }
                    else
                    {
                        job = _prodServ.GetJobByName(defaultTextValue);
                    }
                    if (job != null)
                        jobId = job.Id;

                    //REVIEW THAT
                    var planRuleId = -1;
                    var schedRule = _prodServ.GetSchedRuleByName(defaultTextValue);
                    if (schedRule != null)
                        planRuleId = schedRule.Id;

                    var workingDayId = -1;
                    var workingDay = _prodServ.GetWorkingDayByName(defaultTextValue);
                    if (workingDay != null)
                        workingDayId = workingDay.Id;

                    var areaId = -1;
                    var area = _prodServ.GetAreaByName(defaultTextValue);
                    if (area != null)
                        areaId = area.Id;

                    var familyId = -1;
                    var family = _prodServ.GetFamilyByNameSociety(defaultTextValue, idSociedad);
                    if (family != null)
                        familyId = family.Id;
                    //REVIEW END

                    item.afpId = afpId;
                    item.familyId = familyId;
                    item.areaId = areaId;
                    item.workingDayId = workingDayId;
                    item.planRuleId = planRuleId;
                    item.localId = localId;
                    item.locationId = locId;
                    item.costCenterId = costCenterId;
                    item.contSocId = contSocietyId;
                    item.contTypeId = contTypeId;
                    item.ocupLevelId = ocupLevelId;
                    item.scholid = scholid;
                    item.orgUnitId = orgUnitId;
                    item.bussUnitId = bussUnitId;
                    item.isapId = isapId;
                    item.idGenre = idGenre;
                    item.bloodId = bloodId;
                    item.civilStatusId = civilStatusId;
                    item.natId = natId;
                    item.jobId = jobId;
                    item.hasBoss = boosExist;
                    if (!item.hasBoss)
                        item.boosId = null;
                    else item.boosId = -1;
                }

                Log.Debug("Creating / Updating entities");

                foreach (GopEntityDtoExpand item in entities)
                {
                    if (!item.hasUser)
                    {
                        _segServ.AddUser(new UserDto()
                        {
                            Activo = true,
                            Username = item.Rut,
                            Password = item.Rut,
                            Bloqueado = false,
                            Nombres = item.FullName,
                            Fecha = DateTime.Now
                        });

                        item.hasUser = true;
                    }

                    var people = _prodServ.GetPeopleByRUT(item.Rut);

                    var newPeople = new PeoplesDto()
                    {
                        Activo = true,
                        ApellidoMaterno = item.Second_surname,
                        ApellidoPaterno = item.First_name,
                        Nombre = item.FullName,
                        IdentificacionUnica = item.Rut.Split('-')[0],
                        Dv = item.Rut.Split('-')[1],
                        Email = item.CurrentJob.Email,
                        Fono = item.Office_phone,
                        Celular = item.Phone,
                        IdCodigoArea = item.areaId
                    };
                    if (people == null)
                    {
                        item.PeopleId = _prodServ.AddPeople(newPeople);
                        newUsers++;
                    }
                    else if (newPeople.Activo != people.Activo || newPeople.ApellidoMaterno != people.ApellidoMaterno || newPeople.ApellidoPaterno != people.ApellidoPaterno || newPeople.Nombre != people.Nombre || newPeople.IdCodigoArea != people.IdCodigoArea || newPeople.Email != people.Email)
                    {
                        item.PeopleId = people.Id;
                        newPeople.Id = people.Id;
                        _prodServ.UpdatePeople(newPeople);
                        updatesUsers++;
                    }
                    else
                    {
                        item.PeopleId = people.Id;
                    }

                    var persInfo = _prodServ.GetPersonalInfoByPersona(item.PeopleId.Value);

                    var newPersonalInf = new PersonalInfoDto()
                    {
                        Activo = true,
                        IdPersona = item.PeopleId.Value,
                        FechaNacimiento = item.Dbirthday,
                        EmailPersonal = item.Email,
                        IdGenero = item.idGenre,
                        IdEstadoCivil = item.civilStatusId,
                        IdPaisNacionalidad = item.natId,
                        IdGrupoSanguineo = item.bloodId,
                        IdIsapre = item.isapId,
                        IdAfp = item.afpId,
                        Direccion = item.Address,
                        Numero = item.AddressNumber,
                        Otro = item.NameEmergencyContact,
                        IdFamiliaCargo = item.familyId,
                        IdArea = item.areaId,
                        IdReglaPlanHorario = item.planRuleId,
                        JornadaLaboral = item.workingDayId,
                        IdLocal = item.localId
                    };

                    if (persInfo == null)
                    {
                        item.PersonalInfoId = _prodServ.AddPersonalInfo(newPersonalInf);
                    }
                    else if (persInfo.Activo != newPersonalInf.Activo || persInfo.FechaNacimiento != newPersonalInf.FechaNacimiento || persInfo.IdEstadoCivil != newPersonalInf.IdEstadoCivil || persInfo.IdLocal != newPersonalInf.IdLocal)
                    {
                        item.PersonalInfoId = persInfo.Id;
                        newPersonalInf.Id = persInfo.Id;
                        _prodServ.UpdatePersonalInfo(newPersonalInf);
                    }
                    else
                    {
                        item.PersonalInfoId = persInfo.Id;
                    }
                }

                foreach (GopEntityDtoExpand item in entities)
                {
                    int? bossIdAux = item.hasBoss ? entities.FirstOrDefault(e => e.Rut == item.BossRut)?.PeopleId : null;

                    var currentJob = _prodServ.GetCurrentJobByPeopleSociety(item.PeopleId.Value, idSociedad);

                    var newCurrentJob = new CurrentJobsDto()
                    {
                        Activo = true,
                        IdPersona = item.PeopleId.Value,
                        IdUnidadOrganizacional = item.orgUnitId,
                        IdUnidadNegocio = item.bussUnitId,
                        IdUbicacion = item.locationId,
                        IdCargo = item.jobId,
                        IdEscolaridadSence = item.scholid,
                        IdNivelOcupacional = item.ocupLevelId,
                        FranquiciaSence = item.FranchiseSence,
                        IdTipoContrato = item.contTypeId,
                        FechaInicioContrato = item.CurrentJob.DstartDate,
                        FechaTerminoContrato = item.CurrentJob.DendDate,
                        IdPersonaJefe = bossIdAux,
                        IdSociedadContratante = item.contSocId,
                        IdCentroCosto = item.costCenterId
                    };

                    if (currentJob == null)
                        item.CurrentJobId = _prodServ.AddCurrentJob(newCurrentJob);
                    else if (currentJob.IdCargo != newCurrentJob.IdCargo || currentJob.IdCentroCosto != newCurrentJob.IdCentroCosto || currentJob.IdEscolaridadSence != newCurrentJob.IdEscolaridadSence || currentJob.FechaInicioContrato != newCurrentJob.FechaInicioContrato || currentJob.FechaTerminoContrato != currentJob.FechaTerminoContrato)
                    {
                        item.CurrentJobId = currentJob.Id;
                        newCurrentJob.Id = currentJob.Id;
                        _prodServ.UpdateCurrentJob(newCurrentJob);
                    }
                    //var newCurrentJob = new CurrentJobsDto()
                    //{
                    //    Activo = true,
                    //    IdPersona = item.PeopleId.Value,
                    //    IdUnidadOrganizacional = item.orgUnitId,
                    //    IdUnidadNegocio = item.bussUnitId,
                    //    IdUbicacion = item.locationId,
                    //    IdCargo = item.jobId,
                    //    IdEscolaridadSence = item.scholid,
                    //    IdNivelOcupacional = item.ocupLevelId,
                    //    FranquiciaSence = item.FranchiseSence,
                    //    IdTipoContrato = item.contTypeId,
                    //    FechaInicioContrato = item.CurrentJob.DstartDate,
                    //    FechaTerminoContrato = item.CurrentJob.DendDate,
                    //    IdPersonaJefe = bossIdAux,
                    //    IdSociedadContratante = item.contSocId,
                    //    IdCentroCosto = item.costCenterId
                    //};

                    //var currentJob = _prodServ.GetCurrentJobByPeopleSociety(item.PeopleId.Value, idSociedad);
                    //item.CurrentJobId = _prodServ.AddCurrentJob(newCurrentJob);
                }

                Log.Debug("Entities has been procesed");

                Log.Information("End the app. Review logs.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                Log.Information("End the app whit errors. Review errors, fix its and retry.");

                Log.Information("------------------------------------------------");

                //var info = new LogInfo
                //{
                //    CountOfRows = 0,
                //    NameOfFile = string.Empty,
                //    State = "No Procesado"
                //};

                //var txtFilePath = @Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo.txt";
                //GopReportManager.GenerateLogMonitorFile(txtFilePath, info);
                //SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPathBackup);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

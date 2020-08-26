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
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

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
            var sftpPathBackup = string.Empty;
            var sftpHost = string.Empty;
            var sftpUserName = string.Empty;
            var sftpPassword = string.Empty;
            var sftpPath = string.Empty;
            var logsPath = string.Empty;
            int sftpPort = 0;
            int idSociedad = 0;
            int deactivateUsersBit = 0;

            try
            {
                sftpHost = ConfigurationManager.AppSettings.Get("sftpHost");
                sftpPort = (ConfigurationManager.AppSettings.Get("sftpPort") != null) ? int.Parse(ConfigurationManager.AppSettings.Get("sftpPort")) : 22;
                sftpUserName = ConfigurationManager.AppSettings.Get("sftpUserName");
                sftpPassword = ConfigurationManager.AppSettings.Get("sftpPassword");
                sftpPath = ConfigurationManager.AppSettings.Get("sftpPath");
                logsPath = @ConfigurationManager.AppSettings.Get("logsFullPath");
                idSociedad = (ConfigurationManager.AppSettings.Get("idSociedad") != null) ? int.Parse(ConfigurationManager.AppSettings.Get("idSociedad")) : -1;
                deactivateUsersBit = (ConfigurationManager.AppSettings.Get("deactivateUsersBit") != null) ? int.Parse(ConfigurationManager.AppSettings.Get("deactivateUsersBit")) : -1;

                sftpPathBackup = sftpPath + "\\Respaldo";

                if (string.IsNullOrEmpty(logsPath))
                {
                    logsPath = "Logs.txt";
                }

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(logsPath, rollingInterval: RollingInterval.Hour)
                    .CreateLogger();

                Log.Information("------------------------------------------------");

                Log.Information("Starting the app");

                var txtFilePath = @Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";

                var xlsDestFilePath = @Environment.CurrentDirectory + "\\App_Data\\Import\\";// + DateTime.Now.Ticks + "_" + excelFileName;

                var resultu = SftpManager.GetLastExcelFile(sftpHost, sftpPort, sftpUserName, sftpPassword, xlsDestFilePath);

                var dataTable = ReadWriteExcel.ReadExcelSheet(resultu, true);

                var entities = _gopServ.GetEntities(dataTable);

                Log.Debug("Read " + entities.Count() + " entities");

                var downUsers = 0;
                if (deactivateUsersBit == 1)
                {
                    var clientUsers = _segServ.GetUsersByClientId(idSociedad).ToList();

                    var usersToDeactivate = new List<ClienteUsersDto>();

                    foreach (var item in clientUsers)
                    {
                        if (item.IdUsers != null)
                        {
                            var user = _segServ.GetUserById(item.IdUsers.Value);
                            if (user != null && !entities.Any(e => e.Rut == user.Username) && user.Activo.HasValue && user.Activo.Value)
                            {
                                usersToDeactivate.Add(new ClienteUsersDto() { IdUsers = user.Id, IdClientes = idSociedad });
                            }
                        }
                    }

                    if (usersToDeactivate.Any())
                    {
                        _segServ.DeactivateUsers(usersToDeactivate);

                        downUsers = usersToDeactivate.Count;

                        Log.Debug("Had been deactivated: " + downUsers + " users.");
                    }
                }
                /*	Altas o Registros creados
                  	Actualización de registros activos
                  	Bajas o registros desactivados
                  	Registros inválidos*/
                var newUsers = 0;
                var updatesUsers = 0;
                //var invalidsUsers = 0;

                var genreDic = new Dictionary<string, GenreDto>();
                var csDic = new Dictionary<string, CivilStatusDto>();
                var countryDic = new Dictionary<string, CountryDto>();
                var bgDic = new Dictionary<string, BloodGDto>();
                var isapDic = new Dictionary<string, IsapreDto>();
                var afpDic = new Dictionary<string, AfpDto>();
                //var orgUnitDic = new Dictionary<string, OrgUnitDto>();
                //var bussUnitDic = new Dictionary<string, BussUnitDto>();
                //var locationDic = new Dictionary<string, LocationDto>();
                //var localDic = new Dictionary<string, LocalDto>();
                //var contTypeDic = new Dictionary<string, ContractTypeDto>();
                //var costCenterDic = new Dictionary<string, CostCenterDto>();

                foreach (GopEntityDtoExpand item in entities)
                {
                    var user = _segServ.GetUserByRUTUserName(item.Rut);

                    if (user != null)
                    {
                        item.hasUser = true;
                    }
                    else
                    {
                        item.hasUser = false;
                    }

                    var boosExist = entities.Any(e => e.Rut == item.BossRut);
                    item.hasBoss = boosExist;

                    var defaultValue = 0;
                    var defaultTextValue = "Sin Información";

                    var idGenre = defaultValue;
                    GenreDto g = null;
                    if (!string.IsNullOrEmpty(item.Gender))
                    {
                        if (!genreDic.ContainsKey(item.Gender))
                        {
                            g = _prodServ.GetGenreByName(item.Gender);
                            genreDic.Add(item.Gender, g);
                        }
                        else
                        {
                            g = genreDic[item.Gender];
                        }
                        if (g == null)
                        {
                            g = new GenreDto()
                            {
                                Activo = true,
                                Nombre = item.Gender
                            };
                            g.Id = _prodServ.AddGenre(g);
                            genreDic.Add(g.Nombre, g);
                        }
                    }
                    else
                    {
                        if (!genreDic.ContainsKey(defaultTextValue))
                        {
                            g = _prodServ.GetGenreByName(defaultTextValue);
                            genreDic.Add(defaultTextValue, g);
                        }
                        else
                        {
                            g = genreDic[defaultTextValue];
                        }
                    }
                    if (g != null)
                        idGenre = g.Id;

                    var civilStatusId = defaultValue;
                    CivilStatusDto cs = null;
                    if (!string.IsNullOrEmpty(item.Civil_status))
                    {
                        if (!csDic.ContainsKey(item.Civil_status))
                        {
                            cs = _prodServ.GetCivilStatusByName(item.Civil_status);
                            csDic.Add(item.Civil_status, cs);
                        }
                        else
                        {
                            cs = csDic[item.Civil_status];
                        }
                        if (cs == null)
                        {
                            cs = new CivilStatusDto
                            {
                                Activo = true,
                                Nombre = item.Civil_status
                            };
                            cs.Id = _prodServ.AddCivilStatus(cs);
                            csDic.Add(cs.Nombre, cs);
                        }
                    }
                    else
                    {
                        if (!csDic.ContainsKey(defaultTextValue))
                        {
                            cs = _prodServ.GetCivilStatusByName(defaultTextValue);
                            csDic.Add(defaultTextValue, cs);
                        }
                        else
                        {
                            cs = csDic[defaultTextValue];
                        }
                    }
                    if (cs != null)
                        civilStatusId = cs.Id;

                    var natId = defaultValue;
                    CountryDto country;
                    if (!string.IsNullOrEmpty(item.Country_code))
                    {
                        if (!countryDic.ContainsKey(item.Country_code))
                        {
                            country = _prodServ.GetCountryByName(item.Country_code);
                            countryDic.Add(item.Country_code, country);
                        }
                        else
                        {
                            country = countryDic[item.Country_code];
                        }
                        if (country == null)
                        {
                            country = new CountryDto
                            {
                                Activo = true,
                                Nombre = item.Country_code
                            };
                            country.IdPais = _prodServ.AddCountry(country);
                            countryDic.Add(country.Nombre, country);
                        }
                    }
                    else
                    {
                        if (!countryDic.ContainsKey(defaultTextValue))
                        {
                            country = _prodServ.GetCountryByName(defaultTextValue);
                            countryDic.Add(defaultTextValue, country);
                        }
                        else
                        {
                            country = countryDic[defaultTextValue];
                        }
                    }
                    if (country != null)
                        natId = country.IdPais;

                    var bloodId = defaultValue;
                    BloodGDto bg;
                    if (!string.IsNullOrEmpty(item.BloodG))
                    {
                        if (!bgDic.ContainsKey(item.BloodG))
                        {
                            bg = _prodServ.GetBloodGrByName(item.BloodG);
                            bgDic.Add(item.BloodG, bg);
                        }
                        else
                        {
                            bg = bgDic[item.BloodG];
                        }
                        if (bg == null)
                        {
                            bg = new BloodGDto
                            {
                                Activo = true,
                                Nombre = item.BloodG
                            };
                            bg.Id = _prodServ.AddBloodG(bg);
                            bgDic.Add(bg.Nombre, bg);
                        }
                    }
                    else
                    {
                        if (!bgDic.ContainsKey(defaultTextValue))
                        {
                            bg = _prodServ.GetBloodGrByName(defaultTextValue);
                            bgDic.Add(defaultTextValue, bg);
                        }
                        else
                        {
                            bg = bgDic[defaultTextValue];
                        }
                    }
                    if (bg != null)
                        bloodId = bg.Id;

                    var isapId = defaultValue;
                    IsapreDto isapre;
                    if (!string.IsNullOrEmpty(item.Isapre))
                    {
                        if (!isapDic.ContainsKey(item.Isapre))
                        {
                            isapre = _prodServ.GetIsapreByName(item.Isapre);
                            isapDic.Add(item.Isapre, isapre);
                        }
                        else
                        {
                            isapre = isapDic[item.Isapre];
                        }
                        if (isapre == null)
                        {
                            isapre = new IsapreDto
                            {
                                Activo = true,
                                Nombre = item.Isapre
                            };
                            isapre.Id = _prodServ.AddIsapre(isapre);
                            isapDic.Add(isapre.Nombre, isapre);
                        }
                    }
                    else
                    {
                        if (!isapDic.ContainsKey(defaultTextValue))
                        {
                            isapre = _prodServ.GetIsapreByName(defaultTextValue);
                            isapDic.Add(defaultTextValue, isapre);
                        }
                        else
                        {
                            isapre = isapDic[defaultTextValue];
                        }
                    }
                    if (isapre != null)
                        isapId = isapre.Id;

                    var afpId = defaultValue;
                    AfpDto afp;
                    if (!string.IsNullOrEmpty(item.Afp))
                    {
                        if (!afpDic.ContainsKey(item.Afp))
                        {
                            afp = _prodServ.GetAfpByName(item.Afp);
                            afpDic.Add(item.Afp, afp);
                        }
                        else
                        {
                            afp = afpDic[item.Afp];
                        }
                        if (afp == null)
                        {
                            afp = new AfpDto
                            {
                                Activo = true,
                                Nombre = item.Afp
                            };
                            afp.Id = _prodServ.AddAfp(afp);
                            afpDic.Add(item.Afp, afp);
                        }
                    }
                    else
                    {
                        if (!afpDic.ContainsKey(defaultTextValue))
                        {
                            afp = _prodServ.GetAfpByName(defaultTextValue);
                        }
                        else
                        {
                            afp = afpDic[defaultTextValue];
                        }
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

                genreDic.Clear();
                genreDic = null;
                countryDic.Clear();
                countryDic = null;
                bgDic.Clear();
                bgDic = null;
                isapDic.Clear();
                isapDic = null;
                afpDic.Clear();
                afpDic = null;

                var usersToAdd = new List<KeyValuePair<UserDto, ClienteUsersDto>>();
                var peoplesToAdd = new List<KeyValuePair<Personas, InformacionPersonal>>();

                var client = _segServ.GetClientBySocietyId(idSociedad);

                foreach (GopEntityDtoExpand item in entities)
                {
                    if (!item.hasUser)
                    {
                        var user = new UserDto
                        {
                            Activo = true,
                            Username = item.Rut,
                            Password = item.Rut,
                            Bloqueado = false,
                            Nombres = item.FullName,
                            Fecha = DateTime.Now
                        };

                        ClienteUsersDto userClient = null;
                        if (client != null)
                        {
                            userClient = new ClienteUsersDto
                            {
                                Activo = true,
                                IdClientes = client.Id
                            };

                            user.ClienteUsers.Add(userClient);
                        }

                        usersToAdd.Add(new KeyValuePair<UserDto, ClienteUsersDto>(user, userClient));
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

                    var peopAux = new KeyValuePair<Personas, InformacionPersonal>();

                    if (people == null)
                    {
                        peopAux = new KeyValuePair<Personas, InformacionPersonal>(new Personas
                        {
                            Activo = newPeople.Activo,
                            ApellidoMaterno = newPeople.ApellidoMaterno,
                            ApellidoPaterno = newPeople.ApellidoPaterno,
                            Nombre = newPeople.Nombre,
                            Celular = newPeople.Celular,
                            ClaveSence = newPeople.ClaveSence,
                            ConectaSence = newPeople.ConectaSence,
                            Dv = newPeople.Dv,
                            IdentificacionUnica = newPeople.IdentificacionUnica,
                            IdCodigoArea = newPeople.IdCodigoArea,
                            Email = newPeople.Email,
                            Fono = newPeople.Fono,
                            Instructor = newPeople.Instructor,
                            IdConexion = newPeople.IdConexion,
                            IdPersonaForo = newPeople.IdPersonaForo
                        }, null);
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

                    PersonalInfoDto persInfo = null;

                    if (item.PeopleId.HasValue)
                        persInfo = _prodServ.GetPersonalInfoByPersona(item.PeopleId.Value);

                    var newPersonalInf = new PersonalInfoDto()
                    {
                        Activo = true,
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
                        peopAux = new KeyValuePair<Personas, InformacionPersonal>(peopAux.Key, new InformacionPersonal
                        {
                            Activo = newPersonalInf.Activo,
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
                        });

                        peoplesToAdd.Add(peopAux);
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

                _prodServ.SaveChanges();

                if (usersToAdd.Any())
                    _segServ.AddUsers(usersToAdd.Select(ua => ua.Key));

                if (peoplesToAdd.Any())
                {
                    _prodServ.AddPeoples(peoplesToAdd.Select(p => p.Key));
                    for (int i = 0; i < peoplesToAdd.Count; i++)
                    {
                        peoplesToAdd[i].Value.IdPersona = peoplesToAdd[i].Key.Id;
                    }
                    _prodServ.AddPersonalInfos(peoplesToAdd.Select(ppi => ppi.Value));
                }

                foreach (GopEntityDtoExpand item in entities)
                {
                    if (!item.PeopleId.HasValue || item.PeopleId.Value <= 0)
                    {
                        item.PeopleId = peoplesToAdd.FirstOrDefault(e => (e.Key.IdentificacionUnica + "-" + e.Key.Dv) == item.Rut).Key.Id;
                    }
                }

                var jobToAdd = new List<CurrentJobsDto>();

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
                    {
                        jobToAdd.Add(newCurrentJob);
                    }
                    else if (currentJob.IdCargo != newCurrentJob.IdCargo || currentJob.IdCentroCosto != newCurrentJob.IdCentroCosto || currentJob.IdEscolaridadSence != newCurrentJob.IdEscolaridadSence || currentJob.FechaInicioContrato != newCurrentJob.FechaInicioContrato || currentJob.FechaTerminoContrato != currentJob.FechaTerminoContrato)
                    {
                        item.CurrentJobId = currentJob.Id;
                        newCurrentJob.Id = currentJob.Id;
                        _prodServ.UpdateCurrentJob(newCurrentJob);
                    }
                }

                _prodServ.AddCurrentJobs(jobToAdd);

                _prodServ.SaveChanges();

                var info = new ImportLogInfo
                {
                    CountOfRowsInserted = newUsers,
                    CountOfRowsUpdates = updatesUsers,
                    CountOfDeactivates = downUsers,
                    Invalids = 0,
                    State = "Procesado"
                };

                GopReportManager.GenerateLogMonitorFile(txtFilePath, info);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPath);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPathBackup);

                Log.Debug("Entities has been procesed");

                Log.Information("End the app. Review logs.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                Log.Information("End the app whit errors. Review errors, fix its and retry.");

                Log.Information("------------------------------------------------");

                var info = new ImportLogInfo
                {
                    CountOfRowsInserted = 0,
                    CountOfRowsUpdates = 0,
                    CountOfDeactivates = 0,
                    Invalids = 0,
                    State = "No Procesado"
                };

                var txtFilePath = @Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";

                GopReportManager.GenerateLogMonitorFile(txtFilePath, info);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPath);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPathBackup);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Application.GobEntity.Dto;
using DigitalLearningIntegration.Application.Services.GobEntity;
using DigitalLearningIntegration.Application.Services.Prod;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using DigitalLearningIntegration.Application.Services.Seg;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using DigitalLearningIntegration.Application.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                sftpPathBackup = (ConfigurationManager.AppSettings.Get("sftpBackupPath") != null) ? ConfigurationManager.AppSettings.Get("sftpBackupPath") : sftpPath + "\\Respaldo";


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
                System.Console.Write("");

                Log.Information("Starting the app. Version: 3.8");
                System.Console.Write("..");

                Log.Information("Running in Directory: " + @AppDomain.CurrentDomain.BaseDirectory);
                System.Console.Write("..");

                System.Console.WriteLine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));

                var txtFilePath = @AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";//@Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";

                var xlsDestFilePath = @AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Import\\";// + DateTime.Now.Ticks + "_" + excelFileName;

                //var resultbu = SftpManager.GetBefLastExcelFile(sftpHost, sftpPort, sftpUserName, sftpPassword, xlsDestFilePath);

                var resultu = SftpManager.GetLastExcelFile(sftpHost, sftpPort, sftpUserName, sftpPassword, xlsDestFilePath);

                //var strValue = GetCellValue();  

                var dataTable = ReadWriteExcel.ReadExcelSheet(resultu, true);

                var entities = _gopServ.GetEntities(dataTable);

                Log.Debug("Read " + entities.Count() + " entities");
                System.Console.Write(".....");

                var downUsers = 0;
                if (deactivateUsersBit == 1)
                {
                    Log.Debug("It does not posible to do the deactivation process. Sorry, contact to administrators");
                    System.Console.Write("........");
                }
                //if (deactivateUsersBit == 1)
                //{
                //    var clientUsers = _segServ.GetUsersByClientId(idSociedad).ToList();

                //    var usersToDeactivate = new List<ClienteUsersDto>();

                //    foreach (var item in clientUsers)
                //    {
                //        if (item.IdUsers != null)
                //        {
                //            var user = _segServ.GetUserById(item.IdUsers.Value);
                //            if (user != null && !entities.Any(e => e.Rut == user.Username) && user.Activo.HasValue && user.Activo.Value)
                //            {
                //                usersToDeactivate.Add(new ClienteUsersDto() { IdUsers = user.Id, IdClientes = idSociedad });
                //            }
                //        }
                //    }

                //    if (usersToDeactivate.Any())
                //    {
                //        _segServ.DeactivateUsers(usersToDeactivate);

                //        downUsers = usersToDeactivate.Count;

                //        Log.Debug("Had been deactivated: " + downUsers + " users.");
                //    }
                //}

                var client = _segServ.GetClientBySocietyId(idSociedad);

                var principalOrgUnitId = 0;
                if (client != null)
                {
                    var principalOrgUnit = _prodServ.GetOrgUnitByClientNameSociety(idSociedad, client.Nombre);
                    if (principalOrgUnit != null)
                        principalOrgUnitId = principalOrgUnit.Id;
                }

                var ownerImportId = 0;
                var importPeopleId = 0;

                var importUser = _segServ.GetUserByName("Dataimporter");
                if (importUser != null)
                {
                    ownerImportId = importUser.Id;
                    importUser.Activo = true;
                    importUser.Bloqueado = false;
                    _segServ.SaveChanges();
                }
                else
                {
                    var userIm = new UserDto
                    {
                        Activo = true,
                        Username = "Dataimporter",
                        Password = "Dataimporter",
                        Bloqueado = false,
                        Nombres = "Dataimporter".ToUpper(),
                        Fecha = DateTime.Now,
                        PrimerIngreso = true
                    };

                    ClienteUsersDto userClientIm = null;
                    if (client != null)
                    {
                        userClientIm = new ClienteUsersDto
                        {
                            Activo = true,
                            IdClientes = client.Id
                        };

                        userIm.ClienteUsers.Add(userClientIm);
                    }

                    userIm.ProfileUsers.Add(new UserProfileDto { IdPerfil = 2, Activo = true });
                    ownerImportId = _segServ.AddUser(userIm);
                }

                if (ownerImportId != 0)
                {
                    var importPeoples = _prodServ.GetByIdConexion(ownerImportId);

                    if (importPeoples == null)
                    {
                        var newImportPeople = new PeoplesDto
                        {
                            Activo = true,
                            Dv = string.Empty,
                            IdentificacionUnica = string.Empty,
                            Nombre = "Data Importer",
                            ApellidoMaterno = string.Empty,
                            ApellidoPaterno = string.Empty,
                            Instructor = false,
                            IdConexion = ownerImportId,
                            IdCodigoArea = null,
                            IdPersonaForo = null,
                            Email = "dataimporter@gmail.com",
                            ConectaSence = false
                        };

                        importPeopleId = _prodServ.AddPeople(newImportPeople);
                    }
                    else
                    {
                        importPeoples.Activo = true;
                        importPeopleId = importPeoples.Id;
                        importPeoples.Nombre = "Data Importer";
                        importPeoples.ApellidoMaterno = string.Empty;
                        importPeoples.ApellidoPaterno = string.Empty;

                        //_prodServ.UpdatePeople(importPeoples);

                        _prodServ.SaveChanges();
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
                var orgUnitDic = new Dictionary<string, OrgUnitDto>();
                var bussUnitDic = new Dictionary<string, BussUnitDto>();
                var locationDic = new Dictionary<string, LocationDto>();
                var localDic = new Dictionary<string, LocalDto>();
                var contTypeDic = new Dictionary<string, ContractTypeDto>();
                var costCenterDic = new Dictionary<string, CostCenterDto>();
                var schoShipDic = new Dictionary<string, ScholarshipDto>();
                var ocupLevelDic = new Dictionary<string, OcupLevelDto>();
                var societyDic = new Dictionary<string, SocietyDto>();
                var jobDic = new Dictionary<string, JobDto>();


                //if (item.PeopleId.HasValue)
                //var psInfo = _prodServ.GetPersonalInfoByPersona(3969);
                //psInfo.Altura = 5;
                //psInfo.TallaPantalon = "34";
                //_prodServ.UpdatePersonalInfo(psInfo);

                //_prodServ.SaveChanges();

                //Review this
                var defaultValue = 0;
                var defaultTextValue = "Sin Información";

                System.Console.Write(".");

                var planRuleId = 0;
                var schedRule = _prodServ.GetSchedRuleByName(defaultTextValue);
                if (schedRule != null)
                    planRuleId = schedRule.Id;
                else
                {
                    planRuleId = _prodServ.AddSchedRule(new SchedulesRuleDto
                    {
                        Activo = true,
                        Nombre = defaultTextValue
                    });
                }

                var workingDayId = 0;
                var workingDay = _prodServ.GetWorkingDayByName(defaultTextValue);
                if (workingDay != null)
                    workingDayId = workingDay.Id;
                else
                {
                    workingDayId = _prodServ.AddWorkingDay(new WorkingDayDto
                    {
                        Nombre = defaultTextValue,
                        Descripcion = defaultTextValue,
                        Activo = true
                    });
                }

                System.Console.Write(".");

                var areaId = 0;
                var area = _prodServ.GetAreaByName(defaultTextValue);
                if (area != null)
                    areaId = area.Id;
                else
                {
                    areaId = _prodServ.AddArea(new AreaDto
                    {
                        Nombre = defaultTextValue,
                        Activo = true
                    });
                }

                System.Console.Write(".");

                var familyId = 0;
                var family = _prodServ.GetFamilyByNameSociety(defaultTextValue, idSociedad);
                if (family != null)
                    familyId = family.Id;
                else
                {
                    familyId = _prodServ.AddFamily(new FamilyDto
                    {
                        Activo = true,
                        Nombre = defaultTextValue,
                        IdSociedad = idSociedad,
                        Descripcion = defaultTextValue
                    });
                }
                //End review this

                //Introduce default values
                var ocup = _prodServ.GetOcupLevelByNameSociety(defaultTextValue, idSociedad);
                if (ocup == null)
                {
                    _prodServ.AddOcupLevel(new OcupLevelDto
                    {
                        Activo = true,
                        IdSociedad = idSociedad,
                        Nombre = defaultTextValue
                    });
                }

                System.Console.Write(".");

                var contTypeD = _prodServ.GetContTypeByName(defaultTextValue);
                if (contTypeD == null)
                {
                    _prodServ.AddContType(new ContractTypeDto
                    {
                        Activo = true,
                        Nombre = defaultTextValue
                    });
                }

                System.Console.Write(".");

                var socD = _prodServ.GetSocietyByName(defaultTextValue);
                if (socD == null)
                {
                    _prodServ.AddSociety(new SocietyDto
                    {
                        Activo = true,
                        Nombre = defaultTextValue,
                        IdUbicacion = defaultValue,
                        IdentificacionUnica = "0-0",
                        Logo = string.Empty,
                        Direccion = string.Empty,
                        CorreoContacto = "spacheco@dl.cl",
                        SiglaSociedad = "0-0"
                    });
                }

                //Build Entities in BD
                System.Console.Write(".");
                var orgUnits = entities.Where(i => !string.IsNullOrEmpty(i.CurrentJob.CustomAttributes.Gerencia)).Select(item => item.CurrentJob.CustomAttributes.Gerencia).Distinct();
                OrgUnitDto orgUnitDtoAux;
                foreach (var item in orgUnits)
                {
                    orgUnitDtoAux = _prodServ.GetOrgUnitByClientNameSociety(idSociedad, item);
                    if (orgUnitDtoAux != null)
                    {
                        if (!orgUnitDic.ContainsKey(item))
                            orgUnitDic.Add(item, orgUnitDtoAux);
                    }
                    else
                    {
                        orgUnitDtoAux = new OrgUnitDto
                        {
                            Activo = true,
                            Nombre = item,
                            IdSociedad = idSociedad,
                            IdPadre = principalOrgUnitId,
                            CodigoErp = string.Empty
                        };
                        orgUnitDtoAux.Id = _prodServ.AddOrgUnit(orgUnitDtoAux);
                        if (!orgUnitDic.ContainsKey(item))
                            orgUnitDic.Add(item, orgUnitDtoAux);
                    }
                }
                orgUnits = null;

                System.Console.Write(".");
                var jobs = entities.Where(i => !string.IsNullOrEmpty(i.CurrentJob.Role.Name)).Select(item => item.CurrentJob.Role.Name).Distinct();
                JobDto jobDtoAux;
                foreach (var item in jobs)
                {
                    System.Console.Write(".");
                    jobDtoAux = _prodServ.GetJobByName(item);
                    if (jobDtoAux != null)
                    {
                        if (!jobDic.ContainsKey(item))
                            jobDic.Add(item, jobDtoAux);
                    }
                    else
                    {
                        jobDtoAux = new JobDto
                        {
                            Activo = true,
                            Nombre = item,
                            IdSociedad = idSociedad,
                            FechaCreacion = DateTime.Now,
                            IdUnidadOrganizacional = 0,
                            IdFamiliaCargo = familyId,//0, 
                            IdEscalaSalarial = 0,
                            IdJornadaLaboral = workingDayId,
                            IdUbicacionesFisicas = null,
                            IdEspecialidadCargo = null
                        };
                        jobDtoAux.Id = _prodServ.Addjob(jobDtoAux);
                        if (!jobDic.ContainsKey(item))
                            jobDic.Add(item, jobDtoAux);
                    }
                }
                jobs = null;

                System.Console.Write(".");
                var locals = entities.Where(i => !string.IsNullOrEmpty(i.CurrentJob.CustomAttributes.CodigoLocal)).Select(item => item.CurrentJob.CustomAttributes.CodigoLocal).Distinct();
                LocalDto localDtoAux;
                foreach (var item in locals)
                {
                    System.Console.Write(".");
                    localDtoAux = _prodServ.GetLocalByCode(item);
                    if (localDtoAux != null)
                    {
                        if (!localDic.ContainsKey(item))
                            localDic.Add(item, localDtoAux);
                    }
                    else
                    {
                        localDtoAux = new LocalDto
                        {
                            Activo = true,
                            NombreLocal = item,
                            //IdFormato = orgUnitId,
                            CodigoLocal = item
                        };
                        localDtoAux.Id = _prodServ.AddLocal(localDtoAux);
                        if (!localDic.ContainsKey(item))
                            localDic.Add(item, localDtoAux);
                    }
                }
                locals = null;

                System.Console.Write(".");
                var societies = entities.Where(i => !string.IsNullOrEmpty(i.Health_company)).Select(item => item.Health_company).Distinct();
                SocietyDto socDtoAux;
                ProvSocietyDto provSocietyDtoAux;
                int locationAuxId = 9999;

                var societyType = _prodServ.GetSocietyTypeByName("Contrante");

                var locationDtoAux2 = _prodServ.GetByNameAndType("Sin Información", 3);

                if (locationDtoAux2 != null)
                    locationAuxId = locationDtoAux2.Id;

                foreach (var item in societies)
                {
                    System.Console.Write(".");
                    socDtoAux = _prodServ.GetSocietyByUniqueId(item);
                    if (socDtoAux != null)
                    {
                        if (socDtoAux.IdUbicacion == 0)
                        {
                            socDtoAux.IdUbicacion = locationAuxId;
                            _prodServ.UpdateSocietyWhitSave(socDtoAux);
                        }

                        //Start Repair
                        provSocietyDtoAux = _prodServ.GetProvSocBySocProv(3240, idSociedad, 3);

                        if (provSocietyDtoAux != null && societyType != null && provSocietyDtoAux.IdTipoSociedad != societyType.Id)
                        {
                            provSocietyDtoAux.IdTipoSociedad = societyType.Id;
                            _prodServ.UpdateProvSocietyWhitSave(provSocietyDtoAux);
                            //_prodServ.SaveChanges();
                        }
                        //End Repair

                        provSocietyDtoAux = _prodServ.GetProvSocBySocProv(socDtoAux.Id, idSociedad, societyType != null ? new int?(societyType.Id) : null);

                        if (provSocietyDtoAux == null)
                        {
                            _prodServ.AddProvSociety(new ProvSocietyDto
                            {
                                Activo = true,
                                IdProveedor = socDtoAux.Id,
                                IdTipoSociedad = societyType != null ? new int?(societyType.Id) : null, //2,
                                IdSociedad = idSociedad
                            });
                        }
                        //else if(provSocietyDtoAux.IdTipoSociedad)
                        //{
                        //    _prodServ.UpdateProvSocietyWhitSave(new ProvSocietyDto
                        //    {
                        //        Id = provSocietyDtoAux.Id,
                        //        Activo = true,
                        //        IdProveedor = provSocietyDtoAux.IdProveedor,
                        //        IdTipoSociedad = societyType != null ? new int?(societyType.Id) : null, //2,
                        //        IdSociedad = idSociedad
                        //    });
                        //}

                        if (!societyDic.ContainsKey(item))
                            societyDic.Add(item, socDtoAux);
                    }
                    else
                    {
                        if (societyType != null)
                        {
                            socDtoAux = new SocietyDto
                            {
                                Activo = true,
                                Nombre = item,
                                IdentificacionUnica = item,
                                Logo = string.Empty,
                                Direccion = string.Empty,
                                IdUbicacion = locationAuxId,
                                CorreoContacto = string.Empty,
                                SiglaSociedad = string.Empty
                            };
                            socDtoAux.Id = _prodServ.AddSociety(socDtoAux);

                            societyDic.Add(item, socDtoAux);

                            _prodServ.AddProvSociety(new ProvSocietyDto
                            {
                                Activo = true,
                                IdProveedor = idSociedad,
                                IdTipoSociedad = societyType != null ? new int?(societyType.Id) : null,//3,
                                IdSociedad = socDtoAux.Id
                            });
                        }
                    }
                }
                _prodServ.SaveChanges();
                societies = null;

                Log.Debug("Resolving entities db value's ids.");

                foreach (GopEntityDtoExpand item in entities)
                {
                    System.Console.Write(".");
                    var user = _segServ.GetUserByRUTUserName(item.Rut);

                    if (user != null)
                    {
                        item.HasUser = true;
                        item.IdConexion = user.Id;
                    }
                    else
                    {
                        item.HasUser = false;
                        item.IdConexion = null;
                    }

                    var boosExist = entities.Any(e => e.Rut == item.BossRut);
                    var boosAux = _prodServ.GetPeopleByRUT(item.BossRut);
                    if (boosAux != null)
                        item.BoosId = boosAux.Id;
                    boosExist = !string.IsNullOrEmpty(item.BossRut) && boosAux != null;

                    item.HasBoss = boosExist;

                    var idGenre = defaultValue;
                    GenreDto g = null;
                    if (!string.IsNullOrEmpty(item.Gender))
                    {
                        if (!genreDic.ContainsKey(item.Gender))
                        {
                            g = _prodServ.GetGenreByName(item.Gender);
                            if (g != null)
                            {
                                if (g.Activo != true)
                                    _prodServ.ReActiveGenre(g.Id);
                                genreDic.Add(item.Gender, g);
                            }
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
                            if (g != null)
                            {
                                if (g.Activo != true)
                                    _prodServ.ReActiveGenre(g.Id);
                                genreDic.Add(defaultTextValue, g);
                            }
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
                            if (cs != null)
                            {
                                if (cs.Activo != true)
                                    _prodServ.ReActiveCivilStatus(cs.Id);
                                csDic.Add(item.Civil_status, cs);
                            }
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
                            if (cs != null)
                            {
                                if (cs.Activo != true)
                                    _prodServ.ReActiveCivilStatus(cs.Id);
                                csDic.Add(defaultTextValue, cs);
                            }
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
                            if (country != null)
                            {
                                if (country.Activo != true)
                                    _prodServ.ReActiveCountry(country.IdPais);
                                countryDic.Add(item.Country_code, country);
                            }
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
                                Nombre = item.Country_code,
                                CodigoArea = "0",
                                CodigoSence = 0
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
                            if (country != null)
                            {
                                if (country.Activo != true)
                                    _prodServ.ReActiveCountry(country.IdPais);
                                countryDic.Add(defaultTextValue, country);
                            }
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
                            if (bg != null)
                            {
                                if (bg.Activo != true)
                                    _prodServ.ReActiveBloodG(bg.Id);
                                bgDic.Add(item.BloodG, bg);
                            }
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
                            if (bg != null)
                            {
                                if (bg.Activo != true)
                                    _prodServ.ReActiveBloodG(bg.Id);
                                bgDic.Add(defaultTextValue, bg);
                            }
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
                            if (isapre != null)
                            {
                                if (isapre.Activo != true)
                                    _prodServ.ReActiveIsapre(isapre.Id);
                                isapDic.Add(item.Isapre, isapre);
                            }
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
                            if (isapre != null)
                            {
                                if (isapre.Activo != true)
                                    _prodServ.ReActiveIsapre(isapre.Id);
                                isapDic.Add(defaultTextValue, isapre);
                            }
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
                            if (afp != null)
                            {
                                if (afp.Activo != true)
                                    _prodServ.ReActiveAfp(afp.Id);
                                afpDic.Add(item.Afp, afp);
                            }
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
                            if (afp != null)
                            {
                                if (afp.Activo != true)
                                    _prodServ.ReActiveAfp(afp.Id);
                                afpDic.Add(defaultTextValue, afp);
                            }
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
                        if (!orgUnitDic.ContainsKey(item.CurrentJob.CustomAttributes.Gerencia))
                        {
                            orgUnit = _prodServ.GetOrgUnitByClientNameSociety(idSociedad, item.CurrentJob.CustomAttributes.Gerencia);
                            if (orgUnit != null)
                            {
                                if (orgUnit.Activo != true)
                                    _prodServ.ReActiveOrgUnit(orgUnit.Id);
                                orgUnitDic.Add(item.CurrentJob.CustomAttributes.Gerencia, orgUnit);
                            }
                        }
                        else
                        {
                            orgUnit = orgUnitDic[item.CurrentJob.CustomAttributes.Gerencia];
                        }
                        if (orgUnit == null)
                        {
                            orgUnit = new OrgUnitDto
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.CustomAttributes.Gerencia,
                                IdSociedad = idSociedad,
                                IdPadre = principalOrgUnitId,
                                CodigoErp = string.Empty
                            };
                            orgUnit.Id = _prodServ.AddOrgUnit(orgUnit);
                            orgUnitDic.Add(item.CurrentJob.CustomAttributes.Gerencia, orgUnit);
                        }
                    }
                    else
                    {
                        if (!orgUnitDic.ContainsKey(defaultTextValue))
                        {
                            orgUnit = _prodServ.GetOrgUnitByClientNameSociety(idSociedad, defaultTextValue);
                            if (orgUnit != null)
                            {
                                if (orgUnit.Activo != true)
                                    _prodServ.ReActiveOrgUnit(orgUnit.Id);
                                orgUnitDic.Add(defaultTextValue, orgUnit);
                            }
                        }
                        else
                        {
                            orgUnit = orgUnitDic[defaultTextValue];
                        }
                    }
                    if (orgUnit != null)
                        orgUnitId = orgUnit.Id;

                    var bussUnitId = defaultValue;
                    BussUnitDto bussUnit;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.AreaGastos))
                    {
                        if (!bussUnitDic.ContainsKey(item.CurrentJob.CustomAttributes.AreaGastos))
                        {
                            bussUnit = _prodServ.GetBussUnitByNameSociety(item.CurrentJob.CustomAttributes.AreaGastos, idSociedad);
                            if (bussUnit != null)
                            {
                                if (bussUnit.Activo != true)
                                    _prodServ.ReActiveBussUnit(bussUnit.Id);
                                bussUnitDic.Add(item.CurrentJob.CustomAttributes.AreaGastos, bussUnit);
                            }
                        }
                        else
                        {
                            bussUnit = bussUnitDic[item.CurrentJob.CustomAttributes.AreaGastos];
                        }
                        if (bussUnit == null)
                        {
                            bussUnit = new BussUnitDto
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.CustomAttributes.AreaGastos,
                                IdSociedad = idSociedad,
                                IdUnidadOrganizacional = defaultValue,
                                CodigoErp = null
                            };
                            bussUnit.Id = _prodServ.AddBussUnit(bussUnit);
                            bussUnitDic.Add(item.CurrentJob.CustomAttributes.AreaGastos, bussUnit);
                        }
                    }
                    else
                    {
                        if (!bussUnitDic.ContainsKey(defaultTextValue))
                        {
                            bussUnit = _prodServ.GetBussUnitByNameSociety(defaultTextValue, idSociedad);
                            if (bussUnit != null)
                            {
                                if (bussUnit.Activo != true)
                                    _prodServ.ReActiveBussUnit(bussUnit.Id);
                                bussUnitDic.Add(defaultTextValue, bussUnit);
                            }
                        }
                        else
                        {
                            bussUnit = bussUnitDic[defaultTextValue];
                        }
                    }
                    if (bussUnit != null)
                        bussUnitId = bussUnit.Id;

                    var locId = defaultValue;
                    LocationDto location;
                    if (!string.IsNullOrEmpty(item.District))
                    {
                        if (!locationDic.ContainsKey(item.District))
                        {
                            location = _prodServ.GetLocationByName(item.District);
                            if (location != null)
                            {
                                if (location.Activo != true)
                                    _prodServ.ReActiveLocation(location.Id);
                                locationDic.Add(item.District, location);
                            }
                        }
                        else
                        {
                            location = locationDic[item.District];
                        }
                        if (location == null)
                        {
                            location = new LocationDto
                            {
                                Activo = true,
                                Nombre = item.District
                            };
                            location.Id = _prodServ.AddLocation(location);
                            locationDic.Add(item.District, location);
                        }
                    }
                    else
                    {
                        if (!locationDic.ContainsKey(defaultTextValue))
                        {
                            location = _prodServ.GetLocationByName(defaultTextValue);
                            if (location != null)
                            {
                                if (location.Activo != true)
                                    _prodServ.ReActiveLocation(location.Id);
                                locationDic.Add(defaultTextValue, location);
                            }
                        }
                        else
                        {
                            location = locationDic[defaultTextValue];
                        }
                    }
                    if (location != null)
                        locId = location.Id;

                    var scholid = defaultValue;
                    ScholarshipDto scholarship;
                    if (!string.IsNullOrEmpty(item.Scolarship))
                    {
                        if (!schoShipDic.ContainsKey(item.Scolarship))
                        {
                            scholarship = _prodServ.GetScholarshipByName(item.Scolarship);
                            if (scholarship != null)
                            {
                                if (scholarship.Activo != true)
                                    _prodServ.ReActiveScholarship(scholarship.Id);
                                schoShipDic.Add(item.Scolarship, scholarship);
                            }
                        }
                        else
                        {
                            scholarship = schoShipDic[item.Scolarship];
                        }
                        if (scholarship == null)
                        {
                            scholarship = new ScholarshipDto
                            {
                                Activo = true,
                                Nombre = item.Scolarship
                            };
                            scholarship.Id = _prodServ.AddScholarship(scholarship);
                            schoShipDic.Add(item.Scolarship, scholarship);
                        }
                    }
                    else
                    {
                        if (!schoShipDic.ContainsKey(defaultTextValue))
                        {
                            scholarship = _prodServ.GetScholarshipByName(defaultTextValue);
                            if (scholarship.Activo != true)
                                _prodServ.ReActiveScholarship(scholarship.Id);
                            schoShipDic.Add(defaultTextValue, scholarship);
                        }
                        else
                        {
                            scholarship = schoShipDic[defaultTextValue];
                        }
                    }
                    if (scholarship != null)
                        scholid = scholarship.Id;

                    var ocupLevelId = defaultValue;
                    OcupLevelDto ocupLevel;
                    if (!string.IsNullOrEmpty(item.OcupationalLevel))
                    {
                        if (!ocupLevelDic.ContainsKey(item.OcupationalLevel))
                        {
                            ocupLevel = _prodServ.GetOcupLevelByNameSociety(item.OcupationalLevel, idSociedad);
                            if (ocupLevel != null)
                            {
                                if (ocupLevel.Activo != true)
                                    _prodServ.ReActiveOcupLevel(ocupLevel.Id);
                                ocupLevelDic.Add(item.OcupationalLevel, ocupLevel);
                            }
                        }
                        else
                        {
                            ocupLevel = ocupLevelDic[item.OcupationalLevel];
                        }
                        if (ocupLevel == null)
                        {
                            ocupLevel = new OcupLevelDto
                            {
                                Activo = true,
                                Nombre = item.OcupationalLevel,
                                IdSociedad = idSociedad
                            };
                            ocupLevel.Id = _prodServ.AddOcupLevel(ocupLevel);
                            ocupLevelDic.Add(item.OcupationalLevel, ocupLevel);
                        }
                    }
                    else
                    {
                        if (!ocupLevelDic.ContainsKey(defaultTextValue))
                        {
                            ocupLevel = _prodServ.GetOcupLevelByNameSociety(defaultTextValue, idSociedad);
                            if (ocupLevel != null)
                            {
                                if (ocupLevel.Activo != true)
                                    _prodServ.ReActiveOcupLevel(ocupLevel.Id);
                                ocupLevelDic.Add(defaultTextValue, ocupLevel);
                            }
                        }
                        else
                        {
                            ocupLevel = ocupLevelDic[defaultTextValue];
                        }
                    }
                    if (ocupLevel != null)
                        ocupLevelId = ocupLevel.Id;

                    var contTypeId = defaultValue;
                    ContractTypeDto contractType;
                    if (!string.IsNullOrEmpty(item.ContractType))
                    {
                        if (!contTypeDic.ContainsKey(item.ContractType))
                        {
                            contractType = _prodServ.GetContTypeByName(item.ContractType);
                            if (contractType != null)
                            {
                                if (contractType.Activo != true)
                                    _prodServ.ReActiveContType(contractType.Id);
                                contTypeDic.Add(item.ContractType, contractType);
                            }
                        }
                        else
                        {
                            contractType = contTypeDic[item.ContractType];
                        }
                        if (contractType == null)
                        {
                            contractType = new ContractTypeDto
                            {
                                Activo = true,
                                Nombre = item.ContractType
                            };
                            contractType.Id = _prodServ.AddContType(contractType);
                            contTypeDic.Add(item.ContractType, contractType);
                        }
                    }
                    else
                    {
                        if (!contTypeDic.ContainsKey(defaultTextValue))
                        {
                            contractType = _prodServ.GetContTypeByName(defaultTextValue);
                            if (contractType != null)
                            {
                                if (contractType.Activo != true)
                                    _prodServ.ReActiveContType(contractType.Id);
                                contTypeDic.Add(defaultTextValue, contractType);
                            }
                        }
                        else
                        {
                            contractType = contTypeDic[defaultTextValue];
                        }
                    }
                    if (contractType != null)
                        contTypeId = contractType.Id;

                    var contSocietyId = defaultValue;
                    SocietyDto contSociety;
                    if (!string.IsNullOrEmpty(item.Health_company))
                    {
                        if (!societyDic.ContainsKey(item.Health_company))
                        {
                            contSociety = _prodServ.GetSocietyByUniqueId(item.Health_company);
                            if (contSociety != null)
                            {
                                if (contSociety.Activo != true)
                                    _prodServ.ReActiveContSoc(contSociety.Id);
                                societyDic.Add(item.Health_company, contSociety);
                            }
                        }
                        else
                        {
                            contSociety = societyDic[item.Health_company];
                        }
                        if (contSociety == null)
                        {
                            contSociety = new SocietyDto()
                            {
                                Activo = true,
                                Nombre = item.Health_company,
                                IdentificacionUnica = item.Health_company,
                                Logo = string.Empty,
                                Direccion = string.Empty,
                                IdUbicacion = defaultValue,
                                CorreoContacto = "spacheco@dl.cl",
                                SiglaSociedad = item.Health_company
                            };
                            contSociety.Id = _prodServ.AddSociety(contSociety);
                            societyDic.Add(item.Health_company, contSociety);
                        }
                    }
                    else
                    {
                        if (!societyDic.ContainsKey(defaultTextValue))
                        {
                            contSociety = _prodServ.GetSocietyByUniqueId(defaultTextValue);
                            if (contSociety != null)
                            {
                                if (contSociety.Activo != true)
                                    _prodServ.ReActiveContSoc(contSociety.Id);
                                societyDic.Add(defaultTextValue, contSociety);
                            }
                        }
                        else
                        {
                            contSociety = societyDic[defaultTextValue];
                        }
                    }
                    if (contSociety != null)
                        contSocietyId = contSociety.Id;

                    var costCenterId = defaultValue;
                    CostCenterDto costCenter;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CostCenter))
                    {
                        if (!costCenterDic.ContainsKey(item.CurrentJob.CostCenter))
                        {
                            costCenter = _prodServ.GetCostCenterByNameSociety(item.CurrentJob.CostCenter, idSociedad);
                            if (costCenter != null)
                            {
                                if (costCenter.Activo != true)
                                    _prodServ.ReActiveCostCenter(costCenter.Id);

                                if (string.IsNullOrEmpty(costCenter.Codigo))
                                {
                                    costCenter.Codigo = costCenter.Nombre;
                                    _prodServ.UpdateCostCenterWhitSave(costCenter);
                                    _prodServ.SaveChanges();
                                }

                                costCenterDic.Add(item.CurrentJob.CostCenter, costCenter);
                            }
                        }
                        else
                        {
                            costCenter = costCenterDic[item.CurrentJob.CostCenter];

                            if (string.IsNullOrEmpty(costCenter.Codigo))
                            {
                                costCenter.Codigo = costCenter.Nombre;
                                _prodServ.UpdateCostCenterWhitSave(costCenter);
                                _prodServ.SaveChanges();
                            }
                        }
                        if (costCenter == null)
                        {
                            costCenter = new CostCenterDto
                            {
                                Activo = true,
                                Codigo = item.CurrentJob.CostCenter,
                                IdSociedad = idSociedad,
                                Nombre = item.CurrentJob.CostCenter
                            };
                            costCenter.Id = _prodServ.AddCostCenter(costCenter);
                            costCenterDic.Add(item.CurrentJob.CostCenter, costCenter);
                        }
                    }
                    else
                    {
                        if (!costCenterDic.ContainsKey(defaultTextValue))
                        {
                            costCenter = _prodServ.GetCostCenterByNameSociety(defaultTextValue, idSociedad);
                            if (costCenter != null)
                            {
                                if (costCenter.Activo != true)
                                    _prodServ.ReActiveCostCenter(costCenter.Id);
                                costCenterDic.Add(defaultTextValue, costCenter);
                            }
                        }
                        else
                        {
                            costCenter = costCenterDic[defaultTextValue];
                        }
                    }
                    if (costCenter != null)
                        costCenterId = costCenter.Id;

                    var localId = defaultValue;
                    LocalDto local;
                    if (!string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.CodigoLocal))
                    {
                        if (!localDic.ContainsKey(item.CurrentJob.CustomAttributes.CodigoLocal))
                        {
                            local = _prodServ.GetLocalByCode(item.CurrentJob.CustomAttributes.CodigoLocal);
                            if (local != null)
                            {
                                if (!local.Activo)
                                    _prodServ.ReActiveLocal(local.Id);
                                localDic.Add(item.CurrentJob.CustomAttributes.CodigoLocal, local);
                            }
                        }
                        else
                        {
                            local = localDic[item.CurrentJob.CustomAttributes.CodigoLocal];
                        }
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
                            localDic.Add(item.CurrentJob.CustomAttributes.CodigoLocal, local);
                        }
                    }
                    else
                    {
                        if (!localDic.ContainsKey(defaultTextValue))
                        {
                            local = _prodServ.GetLocalByCode(defaultTextValue);
                            if (local != null)
                            {
                                if (!local.Activo)
                                    _prodServ.ReActiveLocal(local.Id);
                                localDic.Add(defaultTextValue, local);
                            }
                        }
                        else
                        {
                            local = localDic[defaultTextValue];
                        }
                    }
                    if (local != null)
                        localId = local.Id;

                    var jobId = defaultValue;
                    JobDto job;
                    if (!string.IsNullOrEmpty(item.CurrentJob.Role.Name))
                    {
                        if (!jobDic.ContainsKey(item.CurrentJob.Role.Name))
                        {
                            job = _prodServ.GetJobByName(item.CurrentJob.Role.Name);
                            if (job != null)
                            {
                                if (job.Activo != true)
                                    _prodServ.ReActiveJob(job.Id);
                                jobDic.Add(item.CurrentJob.Role.Name, job);
                            }
                        }
                        else
                        {
                            job = jobDic[item.CurrentJob.Role.Name];
                        }
                        if (job == null)
                        {
                            job = new JobDto
                            {
                                Activo = true,
                                Nombre = item.CurrentJob.Role.Name,
                                IdSociedad = idSociedad,
                                FechaCreacion = DateTime.Now,
                                IdUnidadOrganizacional = 0,
                                IdFamiliaCargo = familyId,//0, 
                                IdEscalaSalarial = 0,
                                IdJornadaLaboral = workingDayId,
                                IdUbicacionesFisicas = null,
                                IdEspecialidadCargo = null
                            };
                            job.Id = _prodServ.Addjob(job);
                            jobDic.Add(item.CurrentJob.Role.Name, job);
                        }
                    }
                    else
                    {
                        if (!jobDic.ContainsKey(defaultTextValue))
                        {
                            job = _prodServ.GetJobByName(defaultTextValue);
                            if (job != null)
                            {
                                if (job.Activo != true)
                                    _prodServ.ReActiveJob(job.Id);
                                jobDic.Add(defaultTextValue, job);
                            }
                        }
                        else
                        {
                            job = jobDic[defaultTextValue];
                        }
                    }
                    if (job != null)
                        jobId = job.Id;

                    //REVIEW THAT
                    /*var planRuleId = 0;
                    var schedRule = _prodServ.GetSchedRuleByName(defaultTextValue);
                    if (schedRule != null)
                        planRuleId = schedRule.Id;

                    var workingDayId = 0;
                    var workingDay = _prodServ.GetWorkingDayByName(defaultTextValue);
                    if (workingDay != null)
                        workingDayId = workingDay.Id;

                    var areaId = 0;
                    var area = _prodServ.GetAreaByName(defaultTextValue);
                    if (area != null)
                        areaId = area.Id;

                    var familyId = 0;
                    var family = _prodServ.GetFamilyByNameSociety(defaultTextValue, idSociedad);
                    if (family != null)
                        familyId = family.Id;*/
                    //REVIEW END

                    item.AfpId = afpId;
                    item.FamilyId = familyId;
                    item.AreaId = areaId;
                    item.WorkingDayId = workingDayId;
                    item.PlanRuleId = planRuleId;
                    item.LocalId = localId;
                    item.LocationId = locId;
                    item.CostCenterId = costCenterId;
                    item.ContSocId = contSocietyId;
                    item.ContTypeId = contTypeId;
                    item.OcupLevelId = ocupLevelId;
                    item.Scholid = scholid;
                    item.OrgUnitId = orgUnitId;
                    item.BussUnitId = bussUnitId;
                    item.IsapId = isapId;
                    item.IdGenre = idGenre;
                    item.BloodId = bloodId;
                    item.CivilStatusId = civilStatusId;
                    item.NatId = natId;
                    item.JobId = jobId;
                    item.HasBoss = boosExist;
                    if (!item.HasBoss)
                        item.BoosId = null;
                    else item.BoosId = boosAux != null ? boosAux.Id : -1;
                }

                Log.Debug("Creating / Updating entities");
                System.Console.Write("..");

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
                orgUnitDic.Clear();
                orgUnitDic = null;
                bussUnitDic.Clear();
                bussUnitDic = null;
                locationDic.Clear();
                locationDic = null;
                localDic.Clear();
                localDic = null;
                contTypeDic.Clear();
                contTypeDic = null;
                costCenterDic.Clear();
                costCenterDic = null;
                schoShipDic.Clear();
                schoShipDic = null;
                ocupLevelDic.Clear();
                ocupLevelDic = null;
                societyDic.Clear();
                societyDic = null;
                jobDic.Clear();
                jobDic = null;

                var usersToAdd = new List<KeyValuePair<UserDto, ClienteUsersDto>>();
                var peoplesToAdd = new List<KeyValuePair<Personas, InformacionPersonal>>();
                var clientsUserToAdd = new List<ClienteUsersDto>();
                var profilesToAdd = new List<UserProfileDto>();

                var updatesPeoplesLog = string.Empty;
                var updatesPersInfoLog = string.Empty;
                var updatesJobLog = string.Empty;

                foreach (GopEntityDtoExpand item in entities)
                {
                    Log.Debug("Processing item: " + item.Rut);
                    System.Console.Write("..");

                    if (!item.HasUser)
                    {
                        var user = new UserDto
                        {
                            Activo = true,
                            Username = item.Rut,
                            Password = item.Rut,
                            Bloqueado = false,
                            Nombres = item.FullName,
                            Fecha = DateTime.Now,
                            PrimerIngreso = true,
                            NumeroIntentosFallidos = 0
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

                        user.ProfileUsers.Add(new UserProfileDto { IdPerfil = 2, Activo = true });

                        usersToAdd.Add(new KeyValuePair<UserDto, ClienteUsersDto>(user, userClient));
                    }
                    else
                    {
                        var uClient = _segServ.GetUsersByClientId(client.Id);

                        if (uClient.Any())
                        {
                            var userClientAux = uClient.FirstOrDefault(uc => uc.IdUsers == item.IdConexion.Value);

                            if (userClientAux == null)
                            {
                                clientsUserToAdd.Add(new ClienteUsersDto()
                                {
                                    Activo = true,
                                    IdClientes = client.Id,
                                    IdUsers = item.IdConexion.Value
                                });
                            }
                        }

                        if (item.IdConexion.HasValue)
                        {
                            //var userProfile = _segServ.GetUserByUserIdAndPerfilId(item.IdConexion.Value, 2);
                            var userProfile = _segServ.GetUserByUserId(item.IdConexion.Value);

                            if (userProfile == null)
                            {
                                profilesToAdd.Add(new UserProfileDto { IdPerfil = 2, IdUsers = item.IdConexion, Activo = true });
                            }
                        }
                    }

                    var people = _prodServ.GetPeopleByRUT(item.Rut);

                    var newPeople = new PeoplesDto
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
                        IdCodigoArea = item.AreaId,
                        IdConexion = item.IdConexion,
                        Instructor = false,
                        ClaveSence = string.Empty,
                        ConectaSence = null,
                        IdPersonaForo = null
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
                    else if ((!string.IsNullOrEmpty(newPeople.Fono) && newPeople.Fono != people.Fono)
                        || (!string.IsNullOrEmpty(newPeople.Celular) && newPeople.Celular != people.Celular)
                        || (newPeople.IdConexion.HasValue && newPeople.IdConexion != defaultValue && newPeople.IdConexion != people.IdConexion)
                        || (newPeople.Activo.HasValue && newPeople.Activo != people.Activo)
                        || (!string.IsNullOrEmpty(newPeople.ApellidoMaterno) && newPeople.ApellidoMaterno != people.ApellidoMaterno)
                        || (!string.IsNullOrEmpty(newPeople.ApellidoPaterno) && newPeople.ApellidoPaterno != people.ApellidoPaterno)
                        || (!string.IsNullOrEmpty(newPeople.Nombre) && newPeople.Nombre != people.Nombre)
                        || (newPeople.IdCodigoArea.HasValue && newPeople.IdCodigoArea.Value != defaultValue && newPeople.IdCodigoArea != people.IdCodigoArea)
                        || (!string.IsNullOrEmpty(newPeople.Email) && newPeople.Email != people.Email))
                    {
                        item.PeopleId = people.Id;
                        newPeople.Id = people.Id;

                        _prodServ.UpdatePeople(newPeople);

                        updatesPeoplesLog += item.PeopleId + "; ";
                        updatesUsers++;
                    }
                    else
                    {
                        item.PeopleId = people.Id;
                    }

                    PersonalInfoDto persInfo = null;

                    if (item.PeopleId.HasValue)
                        persInfo = _prodServ.GetPersonalInfoByPersona(item.PeopleId.Value);

                    var newPersonalInf = new PersonalInfoDto
                    {
                        Activo = true,
                        FechaNacimiento = item.Dbirthday,
                        EmailPersonal = item.Email,
                        IdGenero = item.IdGenre,
                        IdEstadoCivil = item.CivilStatusId,
                        IdPaisNacionalidad = item.NatId,
                        IdGrupoSanguineo = item.BloodId,
                        IdIsapre = item.IsapId,
                        IdAfp = item.AfpId,
                        Direccion = item.Address,
                        Numero = item.AddressNumber,
                        Otro = item.NameEmergencyContact,
                        IdFamiliaCargo = item.FamilyId,
                        IdArea = item.AreaId,
                        IdReglaPlanHorario = item.PlanRuleId,
                        JornadaLaboral = item.WorkingDayId,
                        IdLocal = item.LocalId,
                        IdUbicacion = 0,//item.LocationId,
                        IdGrupoEtnico = 0,
                        IdPaisResidencia = item.NatId,
                        IdTipoDireccion = 6,
                        CuentaReparto = false,
                        Sindizalizado = false,
                        Pensionado = false,
                        Discapacitado = false
                    };

                    if (persInfo == null)
                    {
                        peopAux = new KeyValuePair<Personas, InformacionPersonal>(peopAux.Key, new InformacionPersonal
                        {
                            Activo = newPersonalInf.Activo,
                            FechaNacimiento = item.Dbirthday,
                            EmailPersonal = item.Email,
                            IdGenero = item.IdGenre,
                            IdEstadoCivil = item.CivilStatusId,
                            IdPaisNacionalidad = item.NatId,
                            IdGrupoSanguineo = item.BloodId,
                            IdIsapre = item.IsapId,
                            IdAfp = item.AfpId,
                            Direccion = item.Address,
                            Numero = item.AddressNumber,
                            Otro = item.NameEmergencyContact,
                            IdFamiliaCargo = item.FamilyId,
                            IdArea = item.AreaId,
                            IdReglaPlanHorario = item.PlanRuleId,
                            JornadaLaboral = item.WorkingDayId,
                            IdLocal = item.LocalId,
                            IdTipoDireccion = newPersonalInf.IdTipoDireccion,
                            IdGrupoEtnico = newPersonalInf.IdGrupoEtnico,
                            IdUbicacion = newPersonalInf.IdUbicacion,
                            IdPaisResidencia = newPersonalInf.IdPaisResidencia,
                            CuentaReparto = newPersonalInf.CuentaReparto,
                            Sindizalizado = newPersonalInf.Sindizalizado,
                            Pensionado = newPersonalInf.Pensionado,
                            Discapacitado = newPersonalInf.Discapacitado
                        });

                        peoplesToAdd.Add(peopAux);
                    }
                    //newPersonalInf.IdGrupoEtnico != persInfo.IdGrupoEtnico || newPersonalInf.Discapacitado != persInfo.Discapacitado || newPersonalInf.Pensionado != persInfo.Pensionado || newPersonalInf.Sindizalizado != persInfo.Sindizalizado || newPersonalInf.CuentaReparto != persInfo.CuentaReparto || persInfo.IdReglaPlanHorario != newPersonalInf.IdReglaPlanHorario || persInfo.IdUbicacion != newPersonalInf.IdUbicacion || (newPersonalInf.IdPaisResidencia.HasValue && newPersonalInf.IdPaisResidencia.Value != defaultValue && newPersonalInf.IdPaisResidencia != persInfo.IdPaisResidencia) || (newPersonalInf.IdTipoDireccion.HasValue && newPersonalInf.IdTipoDireccion.Value != defaultValue && newPersonalInf.IdTipoDireccion != persInfo.IdTipoDireccion)
                    else if ((newPersonalInf.IdPaisNacionalidad.HasValue && newPersonalInf.IdPaisNacionalidad.Value != defaultValue && persInfo.IdPaisNacionalidad != newPersonalInf.IdPaisNacionalidad)
                        || persInfo.Activo != newPersonalInf.Activo
                        || (newPersonalInf.FechaNacimiento.HasValue && persInfo.FechaNacimiento != newPersonalInf.FechaNacimiento)
                        || (newPersonalInf.IdEstadoCivil.HasValue && newPersonalInf.IdEstadoCivil.Value != defaultValue && persInfo.IdEstadoCivil != newPersonalInf.IdEstadoCivil)
                        || (newPersonalInf.IdLocal.HasValue && newPersonalInf.IdLocal.Value != defaultValue && persInfo.IdLocal != newPersonalInf.IdLocal))
                    {
                        item.PersonalInfoId = persInfo.Id;
                        newPersonalInf.Id = persInfo.Id;

                        _prodServ.UpdatePersonalInfo(newPersonalInf);

                        updatesPersInfoLog += item.PersonalInfoId + "; ";
                    }
                    else
                    {
                        item.PersonalInfoId = persInfo.Id;
                    }
                }

                _prodServ.SaveChanges();

                if (usersToAdd.Any())
                {
                    Log.Debug("Adding users.");
                    System.Console.Write("...");

                    _segServ.AddUsers(usersToAdd.Select(ua => ua.Key));

                    var addUsersRuts = "";
                    foreach (var item in usersToAdd)
                    {
                        addUsersRuts += item.Key.Username + "; ";
                    }
                    Log.Debug("Added users: " + addUsersRuts);
                }

                if (profilesToAdd.Any())
                {
                    _segServ.AddProfiles(profilesToAdd);
                }

                if (clientsUserToAdd.Any())
                {
                    _segServ.AddClientsUsers(clientsUserToAdd);
                }

                if (peoplesToAdd.Any())
                {
                    Log.Debug("Adding peolples.");
                    System.Console.Write("...");

                    UserDto aux;
                    foreach (var p in peoplesToAdd.Where(peop => !peop.Key.IdConexion.HasValue))
                    {
                        aux = _segServ.GetUserByRUTUserName(p.Key.IdentificacionUnica + "-" + p.Key.Dv);
                        if (aux != null)
                        {
                            p.Key.IdConexion = aux.Id;
                        }
                    }

                    _prodServ.AddPeoples(peoplesToAdd.Select(p => p.Key));

                    var addedPeoplesIds = "";
                    foreach (var item in peoplesToAdd.Select(p => p.Key))
                    {
                        addedPeoplesIds += item.Id + "; ";
                    }

                    Log.Debug("Added peoples: " + addedPeoplesIds);
                    Log.Debug("Updated peoples: " + updatesPeoplesLog);
                    System.Console.Write("...");

                    for (int i = 0; i < peoplesToAdd.Count; i++)
                    {
                        peoplesToAdd[i].Value.IdPersona = peoplesToAdd[i].Key.Id;
                    }

                    _prodServ.AddPersonalInfos(peoplesToAdd.Select(ppi => ppi.Value));

                    addedPeoplesIds = "";
                    foreach (var item in peoplesToAdd.Select(p => p.Value))
                    {
                        addedPeoplesIds += item.Id + "; ";
                    }
                    Log.Debug("Added information of peoples: " + addedPeoplesIds);
                    Log.Debug("Updated information of peoples: " + updatesPersInfoLog);
                    System.Console.Write("....");
                }

                foreach (GopEntityDtoExpand item in entities)
                {
                    if (!item.PeopleId.HasValue || item.PeopleId.Value <= 0)
                    {
                        item.PeopleId = peoplesToAdd.FirstOrDefault(e => (e.Key.IdentificacionUnica + "-" + e.Key.Dv) == item.Rut).Key.Id;
                    }
                }

                var jobToAdd = new List<CurrentJobsDto>();

                var admin = _prodServ.GetAdminPeople();

                Log.Debug("Processing laboral's histories.");

                foreach (GopEntityDtoExpand item in entities)
                {
                    //int? bossIdAux = item.HasBoss ? entities.FirstOrDefault(e => e.Rut == item.BossRut)?.PeopleId : (admin != null ? new int?(admin.Id) : null);                                        
                    //Log.Debug("Processing laboral's history.");
                    System.Console.Write("Processing laboral's history: " + item.Rut + "; ");

                    int? bossIdAux = null;
                    if (item.BoosId.HasValue && item.BoosId.Value != -1)
                        bossIdAux = item.BoosId;
                    else
                    {
                        var boosInsideExcel = entities.FirstOrDefault(e => e.Rut == item.BossRut);
                        if (boosInsideExcel != null)
                            bossIdAux = boosInsideExcel.PeopleId;
                        else bossIdAux = null;
                    }

                    var currentJob = _prodServ.GetCurrentJobByPeopleSociety(item.PeopleId.Value, idSociedad);

                    var newCurrentJob = new CurrentJobsDto
                    {
                        Activo = true,
                        IdPersona = item.PeopleId.Value,
                        IdUnidadOrganizacional = item.OrgUnitId,
                        IdUnidadNegocio = item.BussUnitId,
                        IdUbicacion = item.LocationId,
                        IdCargo = item.JobId,
                        IdEscolaridadSence = item.Scholid,
                        IdNivelOcupacional = item.OcupLevelId,
                        FranquiciaSence = item.FranchiseSence,
                        IdTipoContrato = item.ContTypeId,
                        FechaInicioContrato = item.CurrentJob.DstartDate,
                        FechaTerminoContrato = item.CurrentJob.DendDate,
                        IdPersonaJefe = (bossIdAux.HasValue && bossIdAux.Value != -1) ? bossIdAux : null,
                        IdSociedadContratante = item.ContSocId,
                        IdSociedad = idSociedad,
                        IdCentroCosto = item.CostCenterId,
                        IdTipoPosicion = 0,
                        IdPosicionOrigen = null,
                        Estado = 2,
                        IdTipoCambioPosicion = 14,
                        NombrePosicion = string.Empty,
                        NombrePosicionAnterior = string.Empty,
                        IdPersonaCambio = importPeopleId
                    };

                    var updatesCount = 0;

                    if (currentJob == null)
                    {
                        jobToAdd.Add(newCurrentJob);
                    }
                    //newCurrentJob.IdPersonaCambio != currentJob.IdPersonaCambio || newCurrentJob.NombrePosicionAnterior != currentJob.NombrePosicionAnterior || newCurrentJob.NombrePosicion != currentJob.NombrePosicion || currentJob.IdEscolaridadSence != newCurrentJob.IdEscolaridadSence || || newCurrentJob.IdTipoCambioPosicion != currentJob.IdTipoCambioPosicion || || newCurrentJob.IdTipoPosicion != currentJob.IdTipoPosicion || currentJob.IdSociedad != newCurrentJob.IdSociedad
                    else if ((newCurrentJob.IdUbicacion.HasValue && newCurrentJob.IdUbicacion.Value != defaultValue && newCurrentJob.IdUbicacion != currentJob.IdUbicacion)
                        //|| newCurrentJob.Estado != currentJob.Estado
                        //|| newCurrentJob.Activo != currentJob.Activo
                        || (newCurrentJob.IdSociedadContratante.HasValue && newCurrentJob.IdSociedadContratante.Value != defaultValue && currentJob.IdSociedadContratante != newCurrentJob.IdSociedadContratante)
                        || (newCurrentJob.IdCargo.HasValue && newCurrentJob.IdCargo.Value != defaultValue && currentJob.IdCargo != newCurrentJob.IdCargo)
                        || (newCurrentJob.IdCentroCosto != defaultValue && currentJob.IdCentroCosto != newCurrentJob.IdCentroCosto)
                        || (newCurrentJob.FechaInicioContrato.HasValue && currentJob.FechaInicioContrato.HasValue && currentJob.FechaInicioContrato.Value.Date != newCurrentJob.FechaInicioContrato.Value.Date)
                        || (newCurrentJob.FechaTerminoContrato.HasValue && currentJob.FechaTerminoContrato != newCurrentJob.FechaTerminoContrato)
                        || (newCurrentJob.IdPersonaJefe.HasValue && newCurrentJob.IdPersonaJefe.Value != -1 && newCurrentJob.IdPersonaJefe.Value != defaultValue && currentJob.IdPersonaJefe != newCurrentJob.IdPersonaJefe)
                        || (newCurrentJob.IdUnidadNegocio.HasValue && newCurrentJob.IdUnidadNegocio.Value != defaultValue && currentJob.IdUnidadNegocio != newCurrentJob.IdUnidadNegocio)
                        || (newCurrentJob.IdUnidadOrganizacional.HasValue && newCurrentJob.IdUnidadOrganizacional.Value != defaultValue && currentJob.IdUnidadOrganizacional != newCurrentJob.IdUnidadOrganizacional))
                    {
                        item.CurrentJobId = currentJob.Id;

                        newCurrentJob.Id = currentJob.Id;
                        //newCurrentJob.IdTipoContrato

                        if ((newCurrentJob.IdUbicacion.HasValue && newCurrentJob.IdUbicacion.Value != defaultValue && newCurrentJob.IdUbicacion != currentJob.IdUbicacion))
                        {
                            newCurrentJob.IdTipoCambioPosicion = 13;
                            //newCurrentJob.IdUbicacion = 
                            updatesCount++;
                        }
                        if ((newCurrentJob.IdSociedadContratante.HasValue && newCurrentJob.IdSociedadContratante.Value != defaultValue && currentJob.IdSociedadContratante != newCurrentJob.IdSociedadContratante))
                        {
                            newCurrentJob.IdTipoCambioPosicion = 10;
                            updatesCount++;
                        }
                        if ((newCurrentJob.IdCargo.HasValue && newCurrentJob.IdCargo.Value != defaultValue && currentJob.IdCargo != newCurrentJob.IdCargo))
                        {
                            newCurrentJob.IdTipoCambioPosicion = 2;
                            updatesCount++;
                        }
                        if (newCurrentJob.IdCentroCosto != defaultValue && currentJob.IdCentroCosto != newCurrentJob.IdCentroCosto)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 17;
                            updatesCount++;
                        }
                        if (newCurrentJob.FechaInicioContrato.HasValue && currentJob.FechaInicioContrato != newCurrentJob.FechaInicioContrato)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 7;
                            updatesCount++;
                        }
                        if (newCurrentJob.FechaTerminoContrato.HasValue && currentJob.FechaTerminoContrato != newCurrentJob.FechaTerminoContrato)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 11;
                            updatesCount++;
                        }
                        if (newCurrentJob.IdPersonaJefe.HasValue && newCurrentJob.IdPersonaJefe.Value != -1 && newCurrentJob.IdPersonaJefe.Value != defaultValue && currentJob.IdPersonaJefe != newCurrentJob.IdPersonaJefe)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 12;
                            updatesCount++;
                        }
                        if (newCurrentJob.IdUnidadNegocio.HasValue && newCurrentJob.IdUnidadNegocio.Value != defaultValue && currentJob.IdUnidadNegocio != newCurrentJob.IdUnidadNegocio)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 4;
                            updatesCount++;
                        }
                        if (newCurrentJob.IdUnidadOrganizacional.HasValue && newCurrentJob.IdUnidadOrganizacional.Value != defaultValue && currentJob.IdUnidadOrganizacional != newCurrentJob.IdUnidadOrganizacional)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 3;
                            updatesCount++;
                        }


                        if (updatesCount > 1)
                        {
                            newCurrentJob.IdTipoCambioPosicion = 6;
                        }

                        jobToAdd.Add(newCurrentJob);

                        updatesJobLog += item.CurrentJobId + "; ";

                        //Log.Debug("Updating current jobs: " + updatesJobLog);
                        System.Console.Write(".");
                    }
                }

                if (jobToAdd.Any())
                    _prodServ.AddActiveCurrentJobs(jobToAdd);

                if (!string.IsNullOrEmpty(updatesJobLog))
                {
                    Log.Debug("Updated current jobs: " + updatesJobLog);
                }

                var info = new ImportLogInfo
                {
                    CountOfRowsInserted = newUsers,
                    CountOfRowsUpdates = updatesUsers + jobToAdd.Count,
                    CountOfDeactivates = downUsers,
                    Invalids = 0,
                    State = "Procesado"
                };

                GopReportManager.GenerateLogMonitorFile(txtFilePath, info);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPath);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPathBackup);

                Log.Debug("Entities has been procesed");
                //System.Console.WriteLine(".");

                Log.Information("End the app. Review logs.");
                System.Console.WriteLine("...");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                System.Console.WriteLine(ex.Message);

                Log.Information("End the app whit errors. Review errors, fix its and retry.");
                System.Console.WriteLine(".");

                Log.Information("------------------------------------------------");
                System.Console.WriteLine("-");

                var info = new ImportLogInfo
                {
                    CountOfRowsInserted = 0,
                    CountOfRowsUpdates = 0,
                    CountOfDeactivates = 0,
                    Invalids = 0,
                    State = "No Procesado"
                };

                var txtFilePath = @AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";//@Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Monitoreo_ImportExcelToDL.txt";

                GopReportManager.GenerateLogMonitorFile(txtFilePath, info);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPath);

                SftpManager.Send(txtFilePath, sftpHost, sftpPort, sftpUserName, sftpPassword, sftpPathBackup);
            }
            finally
            {
                Log.CloseAndFlush();
                System.Console.WriteLine("End app and logs.");
            }
        }
    }
}

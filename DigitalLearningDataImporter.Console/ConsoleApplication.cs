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

                //var users = _segServ.GetUsers();

                //var user = users.FirstOrDefault();

                //var pi = _prodServ.GetInfos();

                //var ipf = pi.FirstOrDefault();

                //20200731_Importador_Personas

                var xlsDestFilePath = @Environment.CurrentDirectory + "\\App_Data\\Import\\" + DateTime.Now.Ticks + "_20200820_Importador_Personas.xlsx";

                var resultu = SftpManager.Get(excelFileName, sftpHost, sftpPort, sftpUserName, sftpPassword, xlsDestFilePath);

                var dataTable = ReadWriteExcel.ReadExcelSheet(xlsDestFilePath, true);

                var entities = _gopServ.GetEntities(dataTable);

                foreach (GopEntityDto item in entities)
                {
                    var user = _segServ.GetUserByRUTUserName(item.Rut);

                    if (user != null)
                    {//Add

                    }
                    else
                    {//Update
                        var boosExist = entities.Any(e => e.Rut == item.BossRut);//_prodServ.GetPeopleByRUT(item.BossRut);

                        if (boosExist)
                        {
                            var defaultValue = 0;
                            var defaultTextValue = "Sin Información";

                            var idGenre = defaultValue;
                            GenreDto g;
                            if (!string.IsNullOrEmpty(item.Gender))
                            {
                                g = _prodServ.GetGenreByName(item.Gender);
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
                            }
                            else
                            {
                                country = _prodServ.GetCountryByName(defaultTextValue);
                            }
                            if (country != null)
                                civilStatusId = country.IdPais;

                            var bloodId = defaultValue;
                            BloodGDto bg;
                            if (!string.IsNullOrEmpty(item.BloodG))
                            {
                                bg = _prodServ.GetBloodGrByName(item.BloodG);
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
                                isapre = _prodServ.GetIsapreByName(item.BloodG);
                            }
                            else
                            {
                                isapre = _prodServ.GetIsapreByName(defaultTextValue);
                            }
                            if (isapre != null)
                                isapId = isapre.Id;

                            var afpId = defaultValue;
                            Afp afp;

                        }
                    }
                }

                //var genre = _prodServ.GetGenreByName("Femeniñó");

                //genre = _prodServ.GetGenreByName("mascúlíno");

                //var ce = _prodServ.GetCivilStatusByName("Soltero");

                //ce = _prodServ.GetCivilStatusByName("Casado");

                //var pai = _prodServ.GetCountryByName("Chile");

                //pai = _prodServ.GetCountryByName("Venezuela");


                var pl = _prodServ.GetPersonalInfoByPersona(2);

                //segServ.AddUser(new UserDto
                //{
                //    Username = "Yosbel",
                //    Password = "Yobsel"
                //});

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

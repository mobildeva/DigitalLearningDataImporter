﻿using System;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using Newtonsoft.Json;

namespace DigitalLearningIntegration.Application.GobEntity.Dto
{
    public class GopEntityContainerDto
    {
        [JsonProperty(PropertyName = "pagination")]
        public PaginationDto Pagination { get; set; }

        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        public GopEntityContainerDto()
        {

        }
    }

    public class PaginationDto
    {
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public int Total_pages { get; set; }
        public PaginationDto()
        {

        }
    }

    public class GopEntityDto
    {
        [JsonProperty(PropertyName = "rut")]
        public string Rut { get; set; }
        public string RutWhitOutDots
        {
            get
            {
                return !string.IsNullOrEmpty(Rut) ? Rut.Replace(".", string.Empty) : null;
            }
        }
        [JsonProperty(PropertyName = "first_name")]
        public string First_name { get; set; }
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "second_surname")]
        public string Second_surname { get; set; }
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "birthday")]
        public string Birthday { get; set; }
        public DateTime? Dbirthday
        {
            get
            {
                if (!string.IsNullOrEmpty(Birthday))
                {
                    //&& DateTime.TryParse(Birthday, out DateTime res)
                    var splitDate = Birthday.Split('-');
                    var res = new DateTime(int.Parse(splitDate[2]), int.Parse(splitDate[1]), int.Parse(splitDate[0]));
                    return res;
                }

                return null;
            }
        }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "civil_status")]
        public string Civil_status { get; set; }
        public string CivilStatusToDl
        {
            get
            {
                if (!string.IsNullOrEmpty(Civil_status))
                {
                    switch (Civil_status)
                    {
                        case "Acuerdo de Unión Civil": { return "Casado"; }
                        case "Divorciado": { return "Separado"; }
                        case "Casado":
                        case "Soltero":
                        case "Viudo":
                            {
                                return Civil_status;
                            }
                        default:
                            return string.Empty;
                    }
                }
                else return string.Empty;
            }
        }

        [JsonProperty(PropertyName = "country_code")]
        public string Country_code { get; set; }
        [JsonProperty(PropertyName = "health_company")]
        public string Health_company { get; set; }
        [JsonProperty(PropertyName = "pension_fund")]
        public string Pension_fund { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "office_phone")]
        public string Office_phone { get; set; }
        [JsonProperty(PropertyName = "Gerencia")]
        public string Gerencia { get; set; }
        [JsonProperty(PropertyName = "SucursalAmb")]
        public string SucursalAmb { get; set; }
        [JsonProperty(PropertyName = "district")]
        public string District { get; set; }

        [JsonProperty(PropertyName = "custom_attributes")]
        public CustomAttributesDto CustomAttributes { get; set; }
        [JsonProperty(PropertyName = "current_job")]
        public CurrentJobDto CurrentJob { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public string BloodG { get; set; }
        public string BossRut { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PantsSize { get; set; }
        public string ShirtSize { get; set; }
        public string ShoeSize { get; set; }
        public string Isapre { get; set; }
        public string Afp { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string PassportNumber { get; set; }
        public string AddressNumber { get; set; }
        public string NameEmergencyContact { get; set; }
        public string PhoneEmergencyContact { get; set; }
        public string Scolarship { get; set; }
        public string OcupationalLevel { get; set; }
        public string FranchiseSence { get; set; }
        public string ContractType { get; set; }

        public GopEntityDto()
        {

        }
    }

    public class GopEntityDtoExpand : GopEntityDto
    {
        public int? BoosId { get; set; }
        public int OrgUnitId { get; set; }
        public int BussUnitId { get; set; }
        public int IsapId { get; set; }
        public int FamilyId { get; set; }
        public int AreaId { get; set; }
        public int PlanRuleId { get; set; }
        public int WorkingDayId { get; set; }
        public int LocalId { get; set; }
        public int ContSocId { get; set; }
        public int CostCenterId { get; set; }
        public int ContTypeId { get; set; }
        public int OcupLevelId { get; set; }
        public int Scholid { get; set; }
        public int IdGenre { get; set; }
        public int BloodId { get; set; }
        public int CivilStatusId { get; set; }
        public int NatId { get; set; }
        public bool HasBoss { get; set; }
        public bool HasUser { get; set; }
        public int AfpId { get; set; }
        public int? PeopleId { get; set; }
        public int? PersonalInfoId { get; set; }
        public int LocationId { get; set; }
        public int JobId { get; set; }
        public int? CurrentJobId { get; set; }
        public int IdUser { get; set; }
        public int? IdConexion { get; set; }

        public GopEntityDtoExpand() : base()
        {

        }

    }
    public class RoleDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public RoleDto()
        {

        }
    }

    public class CurrentJobDto
    {
        [JsonProperty(PropertyName = "boss")]
        public BossDto Boss { get; set; }

        [JsonProperty(PropertyName = "cost_center")]
        public string CostCenter { get; set; }
        [JsonProperty(PropertyName = "role")]
        public RoleDto Role { get; set; }

        [JsonProperty(PropertyName = "contract_type")]
        public string ContractType { get; set; }
        public string ContractTypeToDl
        {
            get
            {
                if (!string.IsNullOrEmpty(ContractType))
                {
                    switch (ContractType)
                    {
                        default:
                            return "Indefinido";
                    }
                }
                else return string.Empty;
            }
        }
        [JsonProperty(PropertyName = "start_date")]
        public string StartDate { get; set; }
        public DateTime? DstartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(StartDate)) //&& DateTime.TryParse(StartDate, out DateTime res))
                {
                    var splitDate = StartDate.Split('-');
                    var res = new DateTime(int.Parse(splitDate[2]), int.Parse(splitDate[1]), int.Parse(splitDate[0]));
                    return res;
                }
                else
                {
                    return null;
                }
            }
        }

        [JsonProperty(PropertyName = "end_date")]
        public string EndDate { get; set; }
        public DateTime? DendDate
        {
            get
            {
                if (!string.IsNullOrEmpty(EndDate)) //&& DateTime.TryParse(EndDate, out DateTime res))
                {
                    var splitDate = EndDate.Split('-');
                    var res = new DateTime(int.Parse(splitDate[2]), int.Parse(splitDate[1]), int.Parse(splitDate[0]));
                    return res;
                }

                return null;
            }
        }

        [JsonProperty(PropertyName = "company_id")]
        public int CompanyId { get; set; }
        [JsonProperty(PropertyName = "custom_attributes")]
        public CustomAttributesDto CustomAttributes { get; set; }
        public string Email { get; set; }
        public CurrentJobDto()
        {

        }
    }
    public class CustomAttributesDto
    {
        [JsonProperty(PropertyName = "Gerencia")]
        public string Gerencia { get; set; }
        [JsonProperty(PropertyName = "Sucursal Amb")]
        public string SucursalAmb { get; set; }
        [JsonProperty(PropertyName = "Área de Gastos")]
        public string AreaGastos { get; set; }
        [JsonProperty(PropertyName = "Lugar de Pago/Oficina")]
        public string CodigoLocal { get; set; }
        public CustomAttributesDto()
        {

        }
    }

    public class BossDto
    {
        [JsonProperty(PropertyName = "rut")]
        public string Rut { get; set; }
    }

    public class LogInfoDto
    {
        public long ProcessId { get; private set; }
        public string NameOfFile { get; set; }
        public DateTime DateOfFile { get; set; }
        public string State { get; set; }
        public int CountOfRows { get; set; }
        public LogInfoDto()
        {
            DateOfFile = DateTime.Now;
            ProcessId = DateTime.Now.Ticks;
        }
    }

    public class CountryInfoDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameDL { get; set; }
        public string CountryISO { get; set; }
        public string Nationality { get; set; }
    }

    public class CompanyInfoDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRut { get; set; }
    }

    public class LogInfo
    {
        public long ProcessId { get; private set; }
        public string NameOfFile { get; set; }
        public DateTime DateOfFile { get; set; }
        public string State { get; set; }
        public int CountOfRows { get; set; }
        public LogInfo()
        {
            DateOfFile = DateTime.Now;
            ProcessId = DateTime.Now.Ticks;
        }
    }

    public class ImportLogInfo
    {
        public long ProcessId { get; private set; }
        public string NameOfFile { get; set; }
        public DateTime DateOfFile { get; set; }
        public string State { get; set; }
        public int CountOfRowsInserted { get; set; }
        public int CountOfRowsUpdates { get; set; }
        public int CountOfDeactivates { get; set; }

        public int Invalids { get; set; }
        public ImportLogInfo()
        {
            DateOfFile = DateTime.Now;
            ProcessId = DateTime.Now.Ticks;
        }
    }
}

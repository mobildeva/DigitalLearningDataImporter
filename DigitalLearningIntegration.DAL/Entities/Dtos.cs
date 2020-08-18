using System;
using Newtonsoft.Json;

namespace DigitalLearningIntegration.DAL.Entities
{
    public class GopEntityContainer
    {
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        public GopEntityContainer()
        {

        }
    }

    public class Pagination
    {
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public int Total_pages { get; set; }
        public Pagination()
        {

        }
    }

    public class GopEntity : Auditable, IIsDeleted
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
                if (!string.IsNullOrEmpty(Birthday) && DateTime.TryParse(Birthday, out DateTime res))
                {
                    return res;
                }

                return null;
            }
        }
        [JsonProperty(PropertyName = "gender")]
        public char Gender { get; set; }
        public string GenderToDl
        {
            get
            {
                if (!string.IsNullOrEmpty(Gender.ToString()))
                {
                    switch (Gender)
                    {
                        case 'F': { return "Femenino"; }
                        case 'M': { return "Masculino"; }
                        default:
                            return string.Empty;
                    }
                }
                else return string.Empty;
            }
        }

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
        public CustomAttributes CustomAttributes { get; set; }
        [JsonProperty(PropertyName = "current_job")]
        public CurrentJob CurrentJob { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public GopEntity()
        {

        }
    }

    public class Role
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public Role()
        {

        }
    }

    public class CurrentJob
    {
        [JsonProperty(PropertyName = "boss")]
        public Boss Boss { get; set; }

        [JsonProperty(PropertyName = "cost_center")]
        public string CostCenter { get; set; }
        [JsonProperty(PropertyName = "role")]
        public Role Role { get; set; }

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
                if (!string.IsNullOrEmpty(StartDate) && DateTime.TryParse(StartDate, out DateTime res))
                {
                    return res;
                }

                return null;
            }
        }

        [JsonProperty(PropertyName = "end_date")]
        public string EndDate { get; set; }
        public DateTime? DendDate
        {
            get
            {
                if (!string.IsNullOrEmpty(EndDate) && DateTime.TryParse(EndDate, out DateTime res))
                {
                    return res;
                }

                return null;
            }
        }

        [JsonProperty(PropertyName = "company_id")]
        public int CompanyId { get; set; }
        [JsonProperty(PropertyName = "custom_attributes")]
        public CustomAttributes CustomAttributes { get; set; }
        public CurrentJob()
        {

        }
    }
    public class CustomAttributes
    {
        [JsonProperty(PropertyName = "Gerencia")]
        public string Gerencia { get; set; }
        [JsonProperty(PropertyName = "Sucursal Amb")]
        public string SucursalAmb { get; set; }
        [JsonProperty(PropertyName = "Área de Gastos")]
        public string AreaGastos { get; set; }
        [JsonProperty(PropertyName = "Lugar de Pago/Oficina")]
        public string CodigoLocal { get; set; }
        public CustomAttributes()
        {

        }
    }

    public class Boss
    {
        [JsonProperty(PropertyName = "rut")]
        public string Rut { get; set; }
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

    public class CountryInfo
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameDL { get; set; }
        public string CountryISO { get; set; }
        public string Nationality { get; set; }
    }

    public class CompanyInfo
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRut { get; set; }
    }
}

using DigitalLearningIntegration.Application.GobEntity.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.GobEntity
{
    public class GopEntityServices : IGopEntityServices
    {
        public IEnumerable<GopEntityDtoExpand> GetEntities(DataTable entitiesTable)
        {
            var result = new List<GopEntityDtoExpand>();

            GopEntityDtoExpand aux;
            foreach (DataRow r in entitiesTable.Rows)
            {
                aux = new GopEntityDtoExpand()
                {
                    Rut = r[0].ToString(),
                    FullName = r[3].ToString(),
                    First_name = r[4].ToString(),
                    Second_surname = r[5].ToString(),
                    Birthday = r[6].ToString(),
                    Email = r[7].ToString(),
                    Gender = r[8].ToString(),
                    Civil_status = r[9].ToString(),
                    Country_code = r[10].ToString(),
                    BloodG = r[11].ToString(),
                    Height = r[12].ToString(),
                    Weight = r[13].ToString(),
                    PantsSize = r[14].ToString(),
                    ShirtSize = r[15].ToString(),
                    ShoeSize = r[16].ToString(),
                    Isapre = r[17].ToString(),
                    Afp = r[18].ToString(),
                    DriverLicenseNumber = r[19].ToString(),
                    PassportNumber = r[20].ToString(),
                    Address = r[21].ToString(),
                    AddressNumber = r[22].ToString(),
                    NameEmergencyContact = r[23].ToString(),
                    PhoneEmergencyContact = r[24].ToString(),
                    CurrentJob = new CurrentJobDto()
                    {
                        Email = r[25].ToString(),
                        CustomAttributes = new CustomAttributesDto()
                        {
                            Gerencia = r[27].ToString(),
                            AreaGastos = r[28].ToString(),
                            CodigoLocal = r[55].ToString()
                        },
                        Role = new RoleDto()
                        {
                            Name = r[30].ToString(),
                        },
                        ContractType = r[34].ToString(),
                        StartDate = r[35].ToString(),
                        EndDate = r[36].ToString(),
                        Boss = new BossDto()
                        {
                            Rut = r[37].ToString()
                        },
                        CostCenter = r[39].ToString(),
                    },
                    Office_phone = r[26].ToString(),
                    District = r[29].ToString(),
                    Scolarship = r[31].ToString(),
                    OcupationalLevel = r[32].ToString(),
                    FranchiseSence = r[33].ToString(),
                    ContractType = r[34].ToString(),
                    Health_company = r[38].ToString(),
                    Phone = r[45].ToString(),
                    BossRut = r[37].ToString()
                };
                result.Add(aux);

                Console.Write("Convert row to db entity: " + aux.Rut + "; ");
            }

            return result;
        }
    }
}

using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using DigitalLearningIntegration.Infraestructure.Repository.CivilStatus;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod
{
    public interface IProdAppServices
    {
        int AddPersonalInfo(PersonalInfoDto piDto);
        IEnumerable<PersonalInfoDto> GetInfos();
        int AddGenre(GenreDto gDto);
        GenreDto GetGenreByName(string name);
        int AddCivilStatus(CivilStatusDto csDto);
        CivilStatusDto GetCivilStatusByName(string name);
        int AddCountry(CountryDto cDto);
        CountryDto GetCountryByName(string name);
        int AddPeople(PeoplesDto peopleDto);
        PeoplesDto GetPeopleByRUT(string rut);
        int Addjob(JobDto jobDto);
        JobDto GetJobByName(string name);
        int AddBussUnit(BussUnitDto buDto);
        BussUnitDto GetBussUnitByNameSociety(string name, int societyId);
        int AddOrgUnit(OrgUnitDto ouDto);
        OrgUnitDto GetOrgUnitByNameSociety(string name, int societyId);
        int AddContType(ContractTypeDto people);
        ContractTypeDto GetContTypeByName(string name);
        int AddSociety(SocietyDto people);
        SocietyDto GetSocietyByName(string name);
        SocietyDto GetSocietyByUniqueId(string name);
    }
}

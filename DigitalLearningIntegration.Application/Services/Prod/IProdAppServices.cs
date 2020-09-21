﻿using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using DigitalLearningIntegration.Infraestructure.Repository.CivilStatus;
using DocumentFormat.OpenXml.CustomXmlSchemaReferences;
using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod
{
    public interface IProdAppServices
    {
        int AddPersonalInfo(PersonalInfoDto piDto);
        PersonalInfoDto GetPersonalInfoByPersona(int peopleId);
        PersonalInfoDto GetPersonalInfoById(int id);
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
        //OrgUnitDto GetOrgUnitByNameSociety(string name, int societyId);
        int AddContType(ContractTypeDto people);
        ContractTypeDto GetContTypeByName(string name);
        int AddSociety(SocietyDto people);
        SocietyDto GetSocietyByName(string name);
        SocietyDto GetSocietyByUniqueId(string name);
        SocietyDto GetSocietyById(int societyId);
        int AddFamily(FamilyDto people);
        FamilyDto GetFamilyByNameSociety(string name, int societyId);
        int AddCostCenter(CostCenterDto costCenterDto);
        CostCenterDto GetCostCenterByNameSociety(string name, int societyId);
        int AddLocation(LocationDto loc);
        LocationDto GetLocationByName(string name);
        int AddOcupLevel(OcupLevelDto ocupLevelDto);
        OcupLevelDto GetOcupLevelByNameSociety(string name, int societyId);
        int AddArea(AreaDto area);
        AreaDto GetAreaByName(string name);
        int AddBloodG(BloodGDto bg);
        BloodGDto GetBloodGrByName(string name);
        int AddScholarship(ScholarshipDto scholarship);
        ScholarshipDto GetScholarshipByName(string name);
        int AddIsapre(IsapreDto isa);
        IsapreDto GetIsapreByName(string name);
        int AddAfp(AfpDto afp);
        AfpDto GetAfpByName(string name);
        int AddCurrentJob(CurrentJobsDto currentJobDto);
        CurrentJobsDto GetCurrentJobByPeopleSociety(int peopleId, int societyId);
        int AddWorkingDay(WorkingDayDto wd);
        WorkingDayDto GetWorkingDayByName(string name);
        int AddSchedRule(SchedulesRuleDto schedulesRuleDto);
        SchedulesRuleDto GetSchedRuleByName(string name);
        int AddLocal(LocalDto local);
        LocalDto GetLocalByCode(string code);
        void UpdatePeople(PeoplesDto people);
        void UpdatePersonalInfo(PersonalInfoDto personalInfo);
        void UpdateCurrentJob(CurrentJobsDto currentJobsDto);
        void SaveChanges();
        void AddPeoples(IEnumerable<Personas> peoples);
        void AddPersonalInfos(IEnumerable<InformacionPersonal> infos);
        void AddCurrentJobs(IEnumerable<CurrentJobsDto> jobs);
        void AddActiveCurrentJobs(IEnumerable<CurrentJobsDto> jobs);
        OrgUnitDto GetOrgUnitBySociety(int idSociedad);
        void ReActiveGenre(int id);
        void ReActiveCivilStatus(int id);
        void ReActiveCountry(int idPais);
        void ReActiveBloodG(int id);
        void ReActiveIsapre(int id);
        void ReActiveAfp(int id);
        void ReActiveOrgUnit(int id);
        void ReActiveBussUnit(int id);
        void ReActiveLocation(int id);
        void ReActiveScholarship(int id);
        void ReActiveOcupLevel(int id);
        void ReActiveContType(int id);
        void ReActiveCostCenter(int id);
        void ReActiveContSoc(int id);
        void ReActiveLocal(int id);
        void ReActiveJob(int id);
        Personas GetByIdConexion(int id);
        OrgUnitDto GetOrgUnitByClientNameSociety(int idSociedad, string clientName);
        PeoplesDto GetAdminPeople();
        SocietyTypeDto GetSocietyTypeByName(string name);
        LocationDto GetByNameAndType(string name, int type);
        ProvSocietyDto GetProvSocBySocProv(int idProv, int idSociedad, int? idSocType);
        int AddProvSociety(ProvSocietyDto provSocietyDto);

        //void UpdateEntity(InformacionPersonal informacionPersonal);
    }
}

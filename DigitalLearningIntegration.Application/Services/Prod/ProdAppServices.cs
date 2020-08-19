using DigitalLearningIntegration.Infraestructure.Repository.PersonalInfo;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningIntegration.Application.Services.Prod.Dto;
using System.Linq;

namespace DigitalLearningIntegration.Application.Services.Prod
{
    public class ProdAppServices : IProdAppServices
    {
        private readonly IPersonalInfoRepository _pInfoRepository;

        public ProdAppServices(HCMKomatsuProdContext context)
        {
            _pInfoRepository = new PersonalInfoRepository(context);
        }

        public int AddPersonalInfo(PersonalInfoDto piDto)
        {
            try
            {
                var entity = new InformacionPersonal
                {
                    FechaNacimiento = piDto.FechaNacimiento,
                    EmailPersonal = piDto.EmailPersonal,

                };
                _pInfoRepository.CreatedOrUpdate(entity);
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

        public IEnumerable<PersonalInfoDto> GetInfos()
        {
            return _pInfoRepository.Get().Select(pi => new PersonalInfoDto(pi));
        }
    }
}

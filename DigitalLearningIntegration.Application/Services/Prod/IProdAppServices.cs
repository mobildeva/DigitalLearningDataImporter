using DigitalLearningIntegration.Application.Services.Prod.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod
{
    public interface IProdAppServices
    {
        int AddPersonalInfo(PersonalInfoDto piDto);
        IEnumerable<PersonalInfoDto> GetInfos();
    }
}

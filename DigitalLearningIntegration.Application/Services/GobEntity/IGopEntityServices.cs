using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DigitalLearningIntegration.Application.GobEntity.Dto;

namespace DigitalLearningIntegration.Application.Services.GobEntity
{
    public interface IGopEntityServices
    {
        IEnumerable<GopEntityDtoExpand> GetEntities(DataTable entitiesTable);
    }
}

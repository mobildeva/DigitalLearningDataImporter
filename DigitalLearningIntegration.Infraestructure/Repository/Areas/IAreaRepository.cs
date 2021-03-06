﻿using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Areas
{
    public interface IAreaRepository : IRepository<Area>
    {
        ResultDto CreatedOrUpdate(Area entity);
        Area GetByName(string name);
    }
}

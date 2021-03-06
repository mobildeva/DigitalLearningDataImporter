﻿using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Location
{
    public interface ILocationRepository : IRepository<Ubicacion>
    {
        ResultDto CreatedOrUpdate(Ubicacion entity);
        Ubicacion GetByName(string name);
        Ubicacion GetByNameAndType(string name, int type);
    }
}

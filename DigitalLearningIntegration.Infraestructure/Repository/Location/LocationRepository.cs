using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Location
{
    public class LocationRepository : Repository<Ubicacion>, ILocationRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public LocationRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(Ubicacion entity)
        {
            ResultDto Result = new ResultDto
            {
                Result = true,
                Message = "Success"
            };
            try
            {
                if (entity.Id.Equals(0))
                {
                    Add(entity);
                    Result.Id = entity.Id;
                }
                else
                {
                    Update(entity);
                }
            }
            catch (Exception ex)
            {
                Result = new ResultDto
                {
                    Result = false,
                    Message = ex.Message
                };
            }
            return Result;
        }

        public override Ubicacion GetById(int id)
        {
            return _context.Ubicacion.FirstOrDefault(x => x.Id == id);
        }

        public Ubicacion GetByName(string name)
        {
            return _context.Ubicacion.AsEnumerable().FirstOrDefault(g => Utils.Utils.CleanString(g.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper());
        }
    }
}

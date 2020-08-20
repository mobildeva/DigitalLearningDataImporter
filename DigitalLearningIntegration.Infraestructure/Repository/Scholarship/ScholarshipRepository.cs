using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Scholarship
{
    public class ScholarshipRepository : Repository<EscolaridadSence>, IScholarshipRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public ScholarshipRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(EscolaridadSence entity)
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

        public override EscolaridadSence GetById(int id)
        {
            return _context.EscolaridadSence.FirstOrDefault(x => x.Id == id);
        }

        public EscolaridadSence GetByName(string name)
        {
            return _context.EscolaridadSence.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper());
        }
    }
}

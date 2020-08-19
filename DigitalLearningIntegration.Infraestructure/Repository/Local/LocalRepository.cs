using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Local
{
    //public class LocalRepository : Repository<Local>, ILocalRepository
    //{
    //    private readonly HCMKomatsuProdContext _context;
    //    public LocalRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
    //    {
    //        _context = dataContext;
    //    }
    //    public ResultDto CreatedOrUpdate(Local entity)
    //    {
    //        ResultDto Result = new ResultDto
    //        {
    //            Result = true,
    //            Message = "Success"
    //        };
    //        try
    //        {
    //            if (entity.Id.Equals(0))
    //            {
    //                Add(entity);
    //                Result.Id = entity.Id;
    //            }
    //            else
    //            {
    //                Update(entity);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Result = new ResultDto
    //            {
    //                Result = false,
    //                Message = ex.Message
    //            };
    //        }
    //        return Result;
    //    }

    //    public override Locale GetById(int id)
    //    {
    //        return _context.Locales.FirstOrDefault(x => x.Id == id);
    //    }
    //}
}

using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.CurrentJob
{
    public class CurrentJobRepository : Repository<PosicionLaboral>, ICurrentJobRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public CurrentJobRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public void AddActiveCurrentJobs(IEnumerable<PosicionLaboral> entities)
        {
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.Estado = 2;
                entity.FechaInicioPosicion = now;
                entity.Activo = true;
                //entity.FechaInicioContrato=

                _context.Entry(entity).State = EntityState.Modified;
                _context.PosicionLaboral.Update(entity);

                var lastjobs = _context.PosicionLaboral.Where(j => j.IdPersona == entity.IdPersona && j.IdSociedad == entity.IdSociedad).OrderBy(j => j.Id);
                if (lastjobs.Any())
                {
                    var lastJ = lastjobs.Last();
                    if (lastJ != null)
                    {
                        lastJ.FechaTerminoPosicion = now;
                        lastJ.Estado = 1;
                        lastJ.Activo = true;//false;

                        _context.Entry(lastJ).State = EntityState.Modified;
                        _context.PosicionLaboral.Update(lastJ);
                    }
                    //foreach (var item in lastjobs)
                    //{
                    //    item.Estado = 1;
                    //    item.Activo = false;

                    //    _context.Entry(item).State = EntityState.Modified;
                    //    _context.PosicionLaboral.Update(item);
                    //}
                }
            }

            //_context.SaveChanges();

            AddRange(entities);
        }

        public ResultDto CreatedOrUpdate(PosicionLaboral entity)
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

        public override PosicionLaboral GetById(int id)
        {
            return _context.PosicionLaboral.FirstOrDefault(x => x.Id == id);
        }

        public PosicionLaboral GetCurrentJobByPeopleSociety(int peopleId, int societyId)
        {
            var filterCurrentJobs = _context.PosicionLaboral.Where(pl => pl.IdPersona == peopleId && pl.IdSociedad == societyId);
            if (filterCurrentJobs.Any())
            {
                var ordered = filterCurrentJobs.OrderByDescending(pl => pl.Id);

                if (ordered.Any())
                {
                    return ordered.FirstOrDefault();
                }
            }

            return null;
        }
    }
}

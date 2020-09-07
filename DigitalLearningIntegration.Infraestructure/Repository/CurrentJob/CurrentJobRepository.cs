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
                var lastjobs = _context.PosicionLaboral.Where(j => j.IdPersona == entity.IdPersona && j.IdSociedad == entity.IdSociedad).OrderBy(j => j.Id);

                PosicionLaboral lastJob = null;

                if (!lastjobs.Any())
                {
                    entity.Estado = 2;
                    entity.FechaInicioPosicion = now;
                    entity.Activo = true;

                    AddWhitOutSave(entity);
                }
                else if (entity.FechaInicioContrato.HasValue && !entity.FechaTerminoContrato.HasValue)
                {
                    lastJob = lastjobs.Last();

                    if (lastJob.FechaInicioContrato != entity.FechaInicioContrato)
                    {
                        if (!lastJob.FechaTerminoPosicion.HasValue && !lastJob.FechaTerminoContrato.HasValue)
                        {
                            lastJob.Estado = 1;
                            lastJob.Activo = true;
                            lastJob.FechaTerminoPosicion = now;
                        }
                        else
                        {
                            lastJob.Estado = 1;
                            lastJob.Activo = false;
                            lastJob.FechaTerminoPosicion = now;
                        }

                        _context.Entry(lastJob).State = EntityState.Modified;
                        _context.PosicionLaboral.Update(lastJob);

                        entity.Estado = 2;
                        entity.FechaInicioPosicion = now;
                        entity.Activo = true;

                        AddWhitOutSave(entity);
                    }
                    else if (lastJob.Estado != 2 && lastJob.Activo != true)
                    {
                        lastJob.Estado = 2;
                        lastJob.Activo = true;

                        _context.Entry(lastJob).State = EntityState.Modified;
                        _context.PosicionLaboral.Update(lastJob);
                    }
                }
                else if (entity.FechaInicioContrato.HasValue && entity.FechaTerminoContrato.HasValue)
                {
                    lastJob = lastjobs.Last();
                    lastJob.Estado = 2;
                    lastJob.Activo = false;
                    lastJob.FechaTerminoContrato = entity.FechaTerminoContrato.Value;
                    lastJob.FechaTerminoPosicion = now;

                    _context.Entry(lastJob).State = EntityState.Modified;
                    _context.PosicionLaboral.Update(lastJob);
                }
            }

            _context.SaveChanges();
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

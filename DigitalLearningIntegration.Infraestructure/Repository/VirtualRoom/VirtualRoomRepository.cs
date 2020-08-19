using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using DigitalLearningIntegration.Domain.Entities;
using DigitalLearningIntegration.Infraestructure.Migrations;
using DigitalLearningIntegration.Infraestructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace DigitalLearningIntegration.Infraestructure.Repository
{
    public class VirtualRoomRepository : Repository<VirtualRoom>, IVirtualRoomRepository
    {
        private readonly Context _context;

        public VirtualRoomRepository(Context context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public ResultDto CreatedOrUpdate(VirtualRoom entity)
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
                    Result.Id = Get().LastOrDefault().Id;
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
        public IEnumerable<VirtualRoom> GetAll()
        {
            return Get(x => !x.IsDeleted).ToList();

        }
        public VirtualRoom GetById(int id)
        {
            return _context.VirtualRooms.Where(x => !x.IsDeleted && x.Id == id).Include(x=>x.people)
                .FirstOrDefault();
        }
    }
}

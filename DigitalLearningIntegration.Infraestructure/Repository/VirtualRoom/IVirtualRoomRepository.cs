using DigitalLearningIntegration.Domain.Entities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System.Collections.Generic;

namespace DigitalLearningIntegration.Infraestructure.Repository
{
    public interface IVirtualRoomRepository: IRepository<VirtualRoom>
    {
        ResultDto CreatedOrUpdate(VirtualRoom entity);
        IEnumerable<VirtualRoom> GetAll();
        VirtualRoom GetById(int id);
    }
}

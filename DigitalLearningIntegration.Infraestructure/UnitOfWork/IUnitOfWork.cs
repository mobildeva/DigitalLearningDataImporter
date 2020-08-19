using System;

namespace DigitalLearningIntegration.Infraestructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }

}

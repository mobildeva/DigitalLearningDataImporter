using DigitalLearningIntegration.Infraestructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Data.Linq;

namespace DigitalLearningIntegration.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

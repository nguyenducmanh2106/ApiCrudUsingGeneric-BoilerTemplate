using System;
namespace DataProcess.Infrastructure.Interfaces
{
    public interface IDapperUnitOfWork : IDisposable
    {
        IDapperReposity GetRepository();
    }
}

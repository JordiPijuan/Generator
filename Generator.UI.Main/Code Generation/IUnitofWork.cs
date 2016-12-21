
using System;

public interface IUnitOfWork : IDisposable
{
    void Save();
}

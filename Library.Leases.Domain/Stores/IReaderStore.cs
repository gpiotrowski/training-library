using System;
using Library.Leases.Domain.Models;

namespace Library.Leases.Domain.Stores
{
    public interface IReaderStore
    {
        Reader GetReaderById(Guid id);
        void SaveReader(Reader reader);
    }
}
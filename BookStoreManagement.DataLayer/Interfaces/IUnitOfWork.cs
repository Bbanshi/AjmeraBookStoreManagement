using BookStoreManagement.Entities.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagement.DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<BookTable> Book { get; }
        IRepository<AuthorTable> Author { get; }

        int Save();
        void BeginTran();
        void Rollback();
    }
}

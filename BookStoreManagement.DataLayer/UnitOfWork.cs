using BookStoreManagement.DataLayer.Interfaces;
using BookStoreManagement.DataLayer.Repositories;
using BookStoreManagement.Entities.DbModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagement.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext Context;
        private IDbContextTransaction Transaction;
        private readonly ILogger<UnitOfWork> _logger;
        public UnitOfWork(BookStoreDbContext context, ILogger<UnitOfWork> logger)
        {
            Context = context;

            Book = new GenericRepository<BookTable>(Context);
            Author = new GenericRepository<AuthorTable>(Context);
            _logger = logger;
        }

        public IRepository<BookTable> Book { get;  set; }
        public IRepository<AuthorTable> Author { get;  set; }

        public int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UnitOfWork - Save : {0} - {1}", e.Message, e.InnerException.Message);
                Context.ChangeTracker.Clear();
                throw e;
            }
        }

        public void BeginTran()
        {
            if (Transaction is object)
            {
                Transaction = null;
            }
            if (Context.Database.CurrentTransaction is object)
            {
                Context.Database.CurrentTransaction.Rollback();
            }
            Transaction = Context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            Context.Dispose();
        }


  }
}

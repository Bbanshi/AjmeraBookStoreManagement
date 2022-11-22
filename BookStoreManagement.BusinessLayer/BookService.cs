using BookStoreManagement.DataLayer;
using BookStoreManagement.Entities.DbModels;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using BookStoreManagement.DataLayer.Interfaces;
using BookStoreManagement.BusinessLayer.Interfaces;

namespace BookStoreManagement.BusinessLayer
{
    public class BookService: IBookService
    {
        private readonly ILogger<BookService> _logger;
        private IUnitOfWork UnitOfWork;
    
        public BookService(ILogger<BookService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
        }

        public List<BookAuthorModel> GetAllBookDetails()
        {
            try
            {
                UnitOfWork.BeginTran();
                List<BookAuthorModel> responseList = new List<BookAuthorModel>();
                string authorName = string.Empty;
                var bookList = GetAll();
                foreach (var book in bookList)
                {
                    authorName = string.Empty;
                    try
                    {
                        authorName = UnitOfWork.Author.Find(x => x.Id == book.AuthorId).FirstOrDefault()?.Name;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Exception while fetching Author in GetAllBookDetails(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                        return null;
                    }
                    responseList.Add(
                         new BookAuthorModel
                         {
                             Id = book.Id,
                             BookName = book.Name,
                             AuthorName = authorName
                         });

                }
                return responseList;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in GetAllBookDetails(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                return null;
            }
        }

        public BookAuthorModel GetBookDetailsById(string id)
        {
            try
            {
                UnitOfWork.BeginTran();
                var response = new BookAuthorModel();
                var book = UnitOfWork.Book.Get(id);
                if (book != null)
                {
                    var authorName = string.Empty;
                    try
                    {
                        authorName = UnitOfWork.Author.Find(x => x.Id == book.AuthorId).FirstOrDefault()?.Name;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Exception while fetching Author in GetBookDetailsById(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                        throw;
                    }

                    response = new BookAuthorModel
                    {
                        Id = book.Id,
                        BookName = book.Name,
                        AuthorName = authorName
                    };
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetBookDetailsById(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<BookTable> GetAll()
        {
            var res = UnitOfWork.Book.GetAll();
            return res.ToList();
        }

        public bool AddBook(AddBookModel addBookModel)
        {

            UnitOfWork.BeginTran();
            try
            {
                Guid authorId = Guid.NewGuid();
                var authorData = new AuthorTable
                {
                    Id = authorId.ToString(),
                    Name = addBookModel.AuthorName,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                };

                try
                {
                    UnitOfWork.Author.Add(authorData);
                    UnitOfWork.Save();
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Exception While adding Author. Exception Message: {ex.Message}, StackTrace: {ex.StackTrace}");
                    UnitOfWork.Rollback();
                    return false;
                }
                
                Guid bookId = Guid.NewGuid();
                var bookData = new BookTable
                {
                    Id = bookId.ToString(),
                    Name = addBookModel.BookName,
                    AuthorId = authorId.ToString(),
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                };
                try
                {
                    UnitOfWork.Book.Add(bookData);
                    UnitOfWork.Save();
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Exception While adding Book. Exception Message: {ex.Message}, StackTrace: {ex.StackTrace}");
                    UnitOfWork.Rollback();
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception BookService-- AddBook. Exception Message: {ex.Message}, StackTrace: {ex.StackTrace}");
                UnitOfWork.Rollback();
                return false;
            }
            return true;
        }
    }
}

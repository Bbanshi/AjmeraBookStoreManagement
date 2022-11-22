using BookStoreManagement.Entities.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagement.BusinessLayer.Interfaces
{
    public interface IBookService
    {
        public List<BookAuthorModel> GetAllBookDetails();
        public BookAuthorModel GetBookDetailsById(string id);
        public List<BookTable> GetAll();
        public bool AddBook(AddBookModel addBookModel);
    }
}

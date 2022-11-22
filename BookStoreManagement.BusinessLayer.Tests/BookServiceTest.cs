using NUnit.Framework;
using System.Collections.Generic;
using BookStoreManagement.BusinessLayer;
using BookStoreManagement.Entities.DbModels;
using BookStoreManagement.BusinessLayer.Interfaces;
using Microsoft.Extensions.Logging;
using BookStoreManagement.DataLayer;
using Moq;
using BookStoreManagement.DataLayer.Interfaces;
using System.Linq;

namespace BookStoreManagement.BusinessLayer.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private readonly IBookService _BookService;
        
        private readonly ILogger<BookService> _logger;

        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        public BookServiceTests()
        {
            _BookService = new BookService(_logger, unitOfWorkMock.Object);
        }


        [SetUp]
        public void Setup()
        {

        }
        private List<BookAuthorModel> SampleData()
        {
            List<BookAuthorModel> BookAuthorModelList = new List<BookAuthorModel>();
            BookAuthorModelList.Add(new BookAuthorModel
            {
                Id = "1",
                BookName = "Book1",
                AuthorName = "Author1"
            });
            BookAuthorModelList.Add(new BookAuthorModel
            {
                Id = "2",
                BookName = "Book2",
                AuthorName = "Author2"
            });
            BookAuthorModelList.Add(new BookAuthorModel
            {
                Id = "3",
                BookName = "Book3",
                AuthorName = "Author3"
            });
            return BookAuthorModelList;
        }
        private List<BookTable> SampleDataOfBooks()
        {
            List<BookTable> BookAuthorModelList = new List<BookTable>();
            BookAuthorModelList.Add(new BookTable
            {
                Id = "1",
                Name = "Book1",
                AuthorId = "1",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now

            });
            BookAuthorModelList.Add(new BookTable
            {
                Id = "2",
                Name = "Book2",
                AuthorId = "2",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now
            });
            BookAuthorModelList.Add(new BookTable
            {
                Id = "3",
                Name = "Book3",
                AuthorId = "3",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now
            });
            return BookAuthorModelList;
        }

        private List<AuthorTable> SampleDataOfAuthor()
        {
            List<AuthorTable> AuthorTableList = new List<AuthorTable>();
            AuthorTableList.Add(new AuthorTable
            {
                Id = "1",
                Name = "Author1",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now
            }); ;
            AuthorTableList.Add(new AuthorTable
            {
                Id = "2",
                Name = "Author2",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now
            });
            AuthorTableList.Add(new AuthorTable
            {
                Id = "3",
                Name = "Author3",
                CreatedTime = System.DateTime.Now,
                UpdatedTime = System.DateTime.Now
            });
            return AuthorTableList;
        }

        [Test]
        public void GetAll_Returns_List_with_count3()
        {
            //Arrange
            unitOfWorkMock.Setup(x => x.Book.GetAll()).Returns(SampleDataOfBooks);

            //Act
            var response = _BookService.GetAll();

            //Assert
            Assert.AreEqual(response.Count,3); 
            Assert.AreEqual("Book1", response.Find(x => x.Id == "1").Name);
            Assert.AreEqual("Book2", response.Find(x => x.Id == "2").Name);
            Assert.AreEqual("Book3", response.Find(x => x.Id == "3").Name);
        }

    }
}
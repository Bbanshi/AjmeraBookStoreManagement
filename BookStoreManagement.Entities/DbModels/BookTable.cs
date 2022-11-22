using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreManagement.Entities.DbModels
{
    [Table("Book")]
    public class BookTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraBookStoreManagement.ApiRequestModels
{
    public class AddBookRequest
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
    }
}

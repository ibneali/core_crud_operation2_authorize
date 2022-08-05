using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core_crud_operation2.dbconnection
{
   
        public class Customer
        {
            [Key]
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string ZipCode { get; set; }
        }
    
}

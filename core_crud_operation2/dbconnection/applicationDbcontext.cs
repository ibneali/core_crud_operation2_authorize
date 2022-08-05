using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core_crud_operation2.dbconnection
{
   
        public class applicationDbcontext : DbContext
        {
            public applicationDbcontext(DbContextOptions<applicationDbcontext> options) : base(options)
            {

            }
            public DbSet<Customer> customers { get; set; }
        }
    
}

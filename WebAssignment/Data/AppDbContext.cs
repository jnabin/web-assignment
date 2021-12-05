using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAssignment.Models;

namespace WebAssignment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=Web_Assingment")
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
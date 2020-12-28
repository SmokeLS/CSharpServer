using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoSelf_Server.Models
{
    public class ServerDBContex : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public ServerDBContex(DbContextOptions<ServerDBContex> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

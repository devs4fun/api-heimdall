using ApiHeimdall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Data
{
    public class AplicationContext : DbContext
    {
        public AplicationContext(DbContextOptions<AplicationContext> options) : base(options) { }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Token> tokens { get; set; }
    } 
}

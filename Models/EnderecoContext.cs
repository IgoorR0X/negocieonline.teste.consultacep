using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace negocieonline.teste.consultacep.Models
{
    public class EnderecoContext : DbContext
    {
        public EnderecoContext(DbContextOptions<EnderecoContext> options) : base(options) { }
        
        public DbSet<EnderecoDB> EnderecoDBs { get; set; }
    }
}

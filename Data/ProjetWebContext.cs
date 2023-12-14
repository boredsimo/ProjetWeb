using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Models;

namespace ProjetWeb.Data
{
    public class ProjetWebContext : DbContext
    {
        public ProjetWebContext (DbContextOptions<ProjetWebContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetWeb.Models.Vol> Vol { get; set; } = default!;

        public DbSet<ProjetWeb.Models.Evenement> Evenement { get; set; } = default!;
    }
}

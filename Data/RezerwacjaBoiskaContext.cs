using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezerwacjaBoiska.Models;

namespace RezerwacjaBoiska.Data
{
    public class RezerwacjaBoiskaContext : DbContext
    {
        public RezerwacjaBoiskaContext (DbContextOptions<RezerwacjaBoiskaContext> options)
            : base(options)
        {
        }

        public DbSet<RezerwacjaBoiska.Models.Gracz> Gracz { get; set; } = default!;
        public DbSet<RezerwacjaBoiska.Models.Boiska> Boiska { get; set; } = default!;
        public DbSet<RezerwacjaBoiska.Models.Rezerwacje> Rezerwacje { get; set; } = default!;
        public DbSet<RezerwacjaBoiska.Models.Opinie> Opinie { get; set; } = default!;
    }
}

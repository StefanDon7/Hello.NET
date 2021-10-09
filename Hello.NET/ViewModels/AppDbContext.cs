using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hello.NET.Domain.Models;
using Hello.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace Hello.NET.ViewModels
{
    public class AppDbContext:DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

        }

        public AppDbContext() {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Rezervacija>()
               .HasOne(r => r.Let)
               .WithMany(k => k.Lista_Rezervacija)
               .HasForeignKey(fk => fk.RezervacijaID);

            modelBuilder.Entity<Rezervacija>()
             .HasOne(r => r.Korisnik)
             .WithMany(k => k.Lista_Rezervacija)
             .HasForeignKey(fk => fk.RezervacijaID);

            modelBuilder.Entity<Rezervacija>()
            .HasOne(r => r.Agent)
            .WithMany(k => k.Lista_Rezervacija)
            .HasForeignKey(fk => fk.AgentID);

            modelBuilder.Entity<Let>()
                .HasOne(l => l.MestoPolaska)
                .WithMany(m => m.Lista_letovaSaPolazak)
                .HasForeignKey(fk => fk.MestoPolaskaId);

            modelBuilder.Entity<Let>()
             .HasOne(l => l.MestoDolaska)
             .WithMany(m => m.Lista_letovaSaDolazak)
             .HasForeignKey(fk => fk.MestoDolaskaId);




        }
        public DbSet<Mesto> MestoDb { get; set; }

        public DbSet<Let> LetDb { get; set; }

        public DbSet<Rezervacija> RezervacijeDb { get; set; }

        public DbSet<Korisnik> KorisnikDb { get; set; }

        public DbSet<Admin> AdminDb { get; set; }

        public DbSet<Agent> AgentDb { get; set; }



    }
}

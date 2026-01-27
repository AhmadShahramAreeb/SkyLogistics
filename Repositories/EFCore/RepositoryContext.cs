using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore;
/*
 * PURPOSE: The database session. Maps C# classes to SQL tables.
   AMAÇ   : Veritabanı oturumu. C# sınıflarını SQL tablolarına eşler.
 */

// Database Representative
// RepositoryContext veritabanının kendisidir (patron).
// OnModelCreating ise patronun kuralları okuduğu andır.

public class RepositoryContext:DbContext
{


    public RepositoryContext(DbContextOptions options) : base(options)
    {
        /*
       DbContextOptions options => parameter coming from outside: Database connection settings.
      : base(options) => Pass this parameter to the parent class (DbContext)".
      */
    }

    // 1. TABLO TANIMI:
    // DbSet: Maps the "Drone" class to a "Drones" table.
    // DbSet: "Drone" sınıfını "Drones" tablosuna eşler.
    // Bu satır der ki: "Veritabanımda 'Drones' adında bir tablo olsun ve
    // bu tablo 'Drone' modelindeki özelliklere (sütunlara) sahip olsun."
    public DbSet<Drone> Drones { get; set; }



}
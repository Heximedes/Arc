using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Arc.Database.Models;
using Arc.Database.Models.Characters;
using Arc.Database.Models.Inventories.Items;

namespace Arc.Database
{
    public class DatabaseContext : DbContext, IDatabase
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<AvatarLook> AvatarLooks { get; set; }
        public DbSet<ItemStackable> Items { get; set; }
        public DbSet<ItemEquip> Equips { get; set; }

        public static readonly LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=127.0.0.1;port=3306;user=root;password=root;database=arc", new MySqlServerVersion(new Version(8, 0, 22)))
                .UseLoggerFactory(_myLoggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account { ID = 1, Username = "admin", Password = "admin", CreationDate = DateTime.UtcNow, Pic = "111111" });

            modelBuilder.Entity<User>()
                .Property(b => b.CharacterSlots)
                .HasDefaultValue(4);

            modelBuilder.Entity<ItemEquip>()
                .Property(e => e.Title)
                .HasDefaultValue("");
            /*
            modelBuilder.Entity<Account>()
                .Property(u => u.CreationDate)
                .HasDefaultValue(DateTime.UtcNow);
            *//*
            modelBuilder.Entity<Character>()
                .HasMany<AvatarLook>()
                .WithOne()
                .HasForeignKey("chr_id");

            */



                
        }
        
    }
}

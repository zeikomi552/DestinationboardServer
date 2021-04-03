using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite
{
    public class SQLiteDataContext : DbContext
    {
        public DbSet<ActionMasterBase> DbSet_ActionMaster { get; internal set; }
        public DbSet<ActionPlanTableBase> DbSet_ActionPlanTable { get; internal set; }
        public DbSet<DestinationMasterBase> DbSet_DestinationMaster { get; internal set; }
        public DbSet<StaffMasterBase> DbSet_StaffMaster { get; internal set; }


        // 最初にココを変更する
        string db_file_path = @"C:\Work\develop\DestinationBoard.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = db_file_path }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionMasterBase>().HasKey(c => new { c.ActionID });
            modelBuilder.Entity<ActionPlanTableBase>().HasKey(c => new { c.StaffID });
            modelBuilder.Entity<DestinationMasterBase>().HasKey(c => new { c.DestinationID });
            modelBuilder.Entity<StaffMasterBase>().HasKey(c => new { c.StaffID });

        }
    }
}

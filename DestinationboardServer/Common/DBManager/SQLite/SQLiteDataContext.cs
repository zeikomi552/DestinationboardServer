using DestinationboardServer.Common.DBManager.SQLite.Tables;
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
        public DbSet<ActionMaster_M> DbSet_ActionMaster_M { get; internal set; }
        public DbSet<ActionPlanTable_M> DbSet_ActionPlanTable_M { get; internal set; }
        public DbSet<DestinationMaster_M> DbSet_DestinationMaster_M { get; internal set; }
        public DbSet<StaffMaster_M> DbSet_StaffMaster_M { get; internal set; }

        // 最初にココを変更する
        string db_file_path = @"C:\Work\develop\DestinationBoard.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = db_file_path }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 複合キーの場合、以下のように指定してやる。
            // ない場合「Entity type 'Item' has composite primary key defined with data annotations. To set composite primary key, use fluent API.」と表示される
            modelBuilder.Entity<ActionMaster_M>().HasKey(c => new { c.ActionID });
            modelBuilder.Entity<ActionPlanTable_M>().HasKey(c => new { c.StaffID });
            modelBuilder.Entity<DestinationMaster_M>().HasKey(c => new { c.DestinationID });
            modelBuilder.Entity<StaffMaster_M>().HasKey(c => new { c.StaffID });

        }
    }
}

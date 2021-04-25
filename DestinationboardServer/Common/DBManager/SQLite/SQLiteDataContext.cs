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


		#region SQLiteのファイルパス[SQLitePath]プロパティ
		/// <summary>
		/// SQLiteのファイルパス[SQLitePath]プロパティ用変数
		/// </summary>
		static string _SQLitePath = @"C:\Work\DestinationBoard.db";
		/// <summary>
		/// SQLiteのファイルパス[SQLitePath]プロパティ
		/// </summary>
		public static string SQLitePath
		{
			get
			{
				return _SQLitePath;
			}
			set
			{
				if (!_SQLitePath.Equals(value))
				{
					_SQLitePath = value;
				}
			}
		}
		#endregion


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = _SQLitePath }.ToString();
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

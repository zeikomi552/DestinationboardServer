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
    public class SQLiteLogDataContext : DbContext
    {
        public DbSet<ActionPlanTableLogBase> DbSet_ActionPlanTableLog { get; internal set; }


		#region SQLiteのファイルパス[SQLitePath]プロパティ
		/// <summary>
		/// SQLiteのファイルパス[SQLitePath]プロパティ用変数
		/// </summary>
		static string _SQLitePath = @"C:\Work\DestinationBoardLog.db";
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
            modelBuilder.Entity<ActionPlanTableLogBase>().HasKey(c => new { c.RegTime, c.StaffID });

        }
    }
}

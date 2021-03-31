using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class StaffMaster_M : StaffMasterBase
    {

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(StaffMaster_M item)
		{
			using (var db = new SQLiteDataContext())
			{
				// Insert
				db.Add<StaffMaster_M>(item);

				// コミット
				db.SaveChanges();
			}
		}
		#endregion

		#region Update処理
		/// <summary>
		/// Update処理
		/// </summary>
		/// <param name="pk_item">更新する主キー（主キーの値のみ入っていれば良い）</param>
		/// <param name="update_item">テーブル更新後の状態</param>
		public static void Update(StaffMaster_M pk_item, StaffMaster_M update_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_StaffMaster_M.Single(x => x.StaffID == pk_item.StaffID);
				item = update_item;

				db.SaveChanges();
			}
		}
		#endregion

		#region Delete処理
		/// <summary>
		/// Delete処理
		/// </summary>
		/// <param name="pk_item">削除する主キー（主キーの値のみ入っていれば良い）</param>
		public static void Delete(StaffMaster_M pk_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_StaffMaster_M.Single(x => x.StaffID == pk_item.StaffID);
				db.DbSet_StaffMaster_M.Remove(item);
				db.SaveChanges();
			}
		}
		#endregion

		#region Select処理
		/// <summary>
		/// Select処理
		/// </summary>
		/// <returns>全件取得</returns>
		public static List<StaffMaster_M> Select()
		{
			using (var db = new SQLiteDataContext())
			{
				return db.DbSet_StaffMaster_M.ToList<StaffMaster_M>();
			}
		}
		#endregion
	}
}

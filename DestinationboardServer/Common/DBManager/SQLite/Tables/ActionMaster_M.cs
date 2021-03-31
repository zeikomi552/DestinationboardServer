using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class ActionMaster_M : ActionMasterBase
    {

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(ActionMaster_M item)
		{
			using (var db = new SQLiteDataContext())
			{
				// Insert
				db.Add<ActionMaster_M>(item);

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
		public static void Update(ActionMaster_M pk_item, ActionMaster_M update_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionMaster_M.Single(x => x.ActionID == pk_item.ActionID);
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
		public static void Delete(ActionMaster_M pk_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionMaster_M.Single(x => x.ActionID == pk_item.ActionID);
				db.DbSet_ActionMaster_M.Remove(item);
				db.SaveChanges();
			}
		}
		#endregion

		#region Select処理
		/// <summary>
		/// Select処理
		/// </summary>
		/// <returns>全件取得</returns>
		public static List<ActionMaster_M> Select()
		{
			using (var db = new SQLiteDataContext())
			{
				return db.DbSet_ActionMaster_M.ToList<ActionMaster_M>();
			}
		}
		#endregion
	}
}

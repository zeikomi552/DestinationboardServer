using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class ActionPlanTable_M : ActionPlanTableBase
    {

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(ActionPlanTable_M item)
		{
			using (var db = new SQLiteDataContext())
			{
				// Insert
				db.Add<ActionPlanTable_M>(item);

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
		public static void Update(ActionPlanTable_M pk_item, ActionPlanTable_M update_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionPlanTable_M.Single(x => x.StaffID == pk_item.StaffID);
				//item.StaffID = update_item.StaffID;
				item.StaffName = update_item.StaffName;
				item.ActionID = update_item.ActionID;
				item.ActionName = update_item.ActionName;
				item.DestinationID = update_item.DestinationID;
				item.DestinationName = update_item.DestinationName;
				item.FromTime = update_item.FromTime;
				item.ToTime = update_item.ToTime;
				item.Memo = update_item.Memo;

				db.SaveChanges();
			}
		}
		#endregion

		#region Delete処理
		/// <summary>
		/// Delete処理
		/// </summary>
		/// <param name="pk_item">削除する主キー（主キーの値のみ入っていれば良い）</param>
		public static void Delete(ActionPlanTable_M pk_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionPlanTable_M.Single(x => x.StaffID == pk_item.StaffID);
				db.DbSet_ActionPlanTable_M.Remove(item);
				db.SaveChanges();
			}
		}
		#endregion

		#region Select処理
		/// <summary>
		/// Select処理
		/// </summary>
		/// <returns>全件取得</returns>
		public static List<ActionPlanTable_M> Select()
		{
			using (var db = new SQLiteDataContext())
			{
				return db.DbSet_ActionPlanTable_M.ToList<ActionPlanTable_M>();
			}
		}
		#endregion
	}
}

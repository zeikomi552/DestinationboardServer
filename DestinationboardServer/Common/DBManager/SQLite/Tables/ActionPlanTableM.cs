using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class ActionPlanTableM : ActionPlanTableBase
    {
		#region 更新処理
		/// <summary>
		/// 更新処理
		/// </summary>
		/// <param name="request">リクエスト</param>
		public static void Update(RegistActionPlansRequest request)
        {
			using (var db = new SQLiteDataContext())
			{
				using (var tran = db.Database.BeginTransaction()) // トランザクション開始
				{
					try
					{
						var items = db.DbSet_ActionPlanTable.ToList<ActionPlanTableBase>();

						foreach (var item in items)
						{
							var del_item_tmp = db.DbSet_StaffMaster.SingleOrDefault(x => x.StaffID.Equals(item.StaffID));
							if (del_item_tmp != null)
							{
								db.DbSet_StaffMaster.Remove(del_item_tmp);
							}
						}

						foreach (var tmp in request.ActionPlans)
						{
							ActionPlanTableM insert_item = new ActionPlanTableM();
							insert_item.StaffID = tmp.StaffID;                 // 従業員情報
							insert_item.StaffName = tmp.StaffName;             // 従業員名
							insert_item.ActionID = tmp.ActionID;               // 行動ID
							insert_item.ActionName = tmp.ActionName;           // 行動名
							insert_item.DestinationID = tmp.DestinationID;     // 行先ID
							insert_item.DestinationName = tmp.DestinationName; // 行先
							insert_item.FromTime = tmp.FromTime.Equals(string.Empty) ? null : DateTime.ParseExact(tmp.FromTime, "yyyy/MM/dd HH:mm:ss", null);
							insert_item.ToTime = tmp.ToTime.Equals(string.Empty) ? null : DateTime.ParseExact(tmp.ToTime, "yyyy/MM/dd HH:mm:ss", null);
							db.Add<ActionPlanTableBase>(insert_item);
						}

						// コミット
						db.SaveChanges();
						tran.Commit();
					}
					catch (Exception)
					{
						tran.Rollback();
					}
				}
			}
		}
		#endregion
	}
}

using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class StaffMasterM : StaffMasterBase
    {
		#region Delete処理
		/// <summary>
		/// Delete処理
		/// </summary>
		public static void UpdateList(RegstStaffRequest request)
		{
			using (var db = new SQLiteDataContext())
			{
				using (var tran = db.Database.BeginTransaction()) // トランザクション開始
				{
					try
					{
						var items = db.DbSet_StaffMaster.ToList<StaffMasterBase>();

						foreach (var item in items)
						{
							var del_item_tmp = db.DbSet_StaffMaster.SingleOrDefault(x => x.StaffID.Equals(item.StaffID));
							if (del_item_tmp != null)
							{
								db.DbSet_StaffMaster.Remove(del_item_tmp);
							}
						}

						foreach (var tmp in request.StaffInfoList)
						{
							StaffMasterM insert_item = new StaffMasterM();
							insert_item.StaffID = tmp.StaffID;
							insert_item.StaffName = tmp.StaffName;
							insert_item.Display = tmp.Display;
							insert_item.CreateDate = DateTime.Today;
							insert_item.CreateUser = tmp.CreateUser;
							db.Add<StaffMasterBase>(insert_item);
						}

						// コミット
						db.SaveChanges();
						tran.Commit();
					}
					catch (Exception e)
					{
						tran.Rollback();
						throw e;
					}
				}
			}
		}
		#endregion
	}
}

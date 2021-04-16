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
		#region 従業員マスター情報をデータベースから取得する関数
		/// <summary>
		/// 従業員マスター情報をデータベースから取得する関数
		/// </summary>
		/// <param name="staff_id">従業員ID</param>
		/// <returns>スタッフマスター情報</returns>
		public static StaffMasterBase Select(string staff_id)
		{
			// コンテキストの作成
			using (var db = new SQLiteDataContext())
			{
				var query = from x in db.DbSet_StaffMaster
							where x.StaffID.Equals(staff_id)
							select x;

				return query.FirstOrDefault();
			}
		}
		#endregion

		#region 従業員マスターの更新処理
		/// <summary>
		/// 従業員マスターの更新処理
		/// </summary>
		public static void UpdateList(RegistStaffRequest request)
		{
			using (var db = new SQLiteDataContext())
			{
				using (var tran = db.Database.BeginTransaction()) // トランザクション開始
				{
					try
					{
						var items = db.DbSet_StaffMaster.ToList<StaffMasterBase>();

						/* EntityFrameworkに全データを一度に削除する方法がないので
						 * 一つずつ取り出して削除していく */
						foreach (var item in items)
						{
							// 従業員IDが一致するものを探す
							var del_item_tmp = db.DbSet_StaffMaster.SingleOrDefault(x => x.StaffID.Equals(item.StaffID));

							// 見つかった
							if (del_item_tmp != null)
							{
								// データを削除
								db.DbSet_StaffMaster.Remove(del_item_tmp);
							}
						}

						// 要求のあったデータをデータベースに登録していく
						foreach (var tmp in request.StaffInfoList)
						{
							StaffMasterM insert_item = new StaffMasterM();
							insert_item.StaffID = tmp.StaffID;			// 従業員ID
							insert_item.StaffName = tmp.StaffName;		// 従業員名
							insert_item.Display = tmp.Display;			// 表示・非表示
							insert_item.SortOrder = tmp.SortOrder;		// 並び順
							insert_item.CreateDate = DateTime.Today;	// 作成日時
							insert_item.CreateUser = tmp.CreateUser;	// 作成者
							db.Add<StaffMasterBase>(insert_item);		// データの追加
						}

						// コミット
						db.SaveChanges();
						tran.Commit();
					}
					catch (Exception)
					{
						// ロールバック
						tran.Rollback();
					}
				}
			}
		}
		#endregion
	}
}

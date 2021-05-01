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
		#region ロガー
		/// <summary>
		/// ロガー
		/// </summary>
		protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		#endregion

		#region 従業員マスター情報をデータベースから取得する関数
		/// <summary>
		/// 従業員マスター情報をデータベースから取得する関数
		/// </summary>
		/// <param name="staff_id">従業員ID</param>
		/// <returns>スタッフマスター情報</returns>
		public static StaffMasterBase Select(string staff_id)
		{
			try
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
			catch (Exception e)
			{
				_logger.Error(e.Message);
				Console.WriteLine(e.Message);
				return null;
			}
		}
		#endregion

		#region リクエスト情報をテーブル情報へ変換する
		/// <summary>
		/// リクエスト情報をテーブル情報へ変換する
		/// </summary>
		/// <param name="request">リクエスト</param>
		/// <returns>テーブル情報</returns>
		public static StaffMasterM RequestToTable(StaffMasterRequest request)
		{
			StaffMasterM ret = new StaffMasterM();
			ret.StaffID = request.StaffID;          // 従業員ID
			ret.StaffName = request.StaffName;      // 従業員名
			ret.Display = request.Display;          // 表示・非表示
			ret.SortOrder = request.SortOrder;      // 並び順
			ret.QRCode = request.QRCode;			// 従業員識別用QRコード
			ret.FelicaID = request.FelicaID;		// 従業員識別用FelicaID
			ret.CreateDate = CommonValues.ConvertDateTime(request.CreateDate, "yyyy/MM/dd HH:mm:ss");    // 作成日時
			ret.CreateUser = request.CreateUser;    // 作成者
			return ret;
		}
		#endregion

		#region Tableカラムからリクエストへの変換
		/// <summary>
		/// Tableカラムからリクエストへの変換
		/// </summary>
		/// <param name="table">テーブル要素</param>
		/// <returns>リクエスト</returns>
		public static StaffMasterRequest TableToRequest(StaffMasterBase table)
		{
			StaffMasterRequest ret = new StaffMasterRequest();
			ret.StaffID = table.StaffID;          // 従業員ID
			ret.StaffName = table.StaffName;      // 従業員名
			ret.Display = table.Display;          // 表示・非表示
			ret.SortOrder = table.SortOrder;      // 並び順
			ret.QRCode = table.QRCode;			  // 従業員識別用QRコード
			ret.FelicaID = table.FelicaID;        // 従業員識別用FelicaID
			ret.CreateDate = table.CreateDate.ToString("yyyy/MM/dd HH:mm:ss");    // 作成日時
			ret.CreateUser = table.CreateUser;    // 作成者
			return ret;
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
						var items_actionplans = db.DbSet_ActionPlanTable.ToList<ActionPlanTableBase>();

						/* EntityFrameworkに全データを一度に削除する方法がないので
						 * 一つずつ取り出して削除していく */
						foreach (var item in items)
						{
							var check_request_staff = (from x in request.StaffInfoList
											   where x.StaffID.Equals(item.StaffID)
											   select x).FirstOrDefault();

							var rv_action_plan = (from x in items_actionplans
												  where x.StaffID.Equals(item.StaffID)
												  select x).FirstOrDefault();

							// 行動情報には当該従業員IDが存在するが
							// 送信情報には当該従業員情報が存在しない
							// 従業員マスターから当該従業員情報が削除された
							if (check_request_staff == null && rv_action_plan != null)
							{
								db.DbSet_ActionPlanTable.Remove(rv_action_plan);
							}


							// 従業員IDが一致するものを探す
							var del_item_tmp = (from x in items
												where x.StaffID.Equals(item.StaffID)
												select x).FirstOrDefault();                         
							
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
							var insert_item = RequestToTable(tmp);
							db.Add<StaffMasterBase>(insert_item);		// データの追加
						}

						// コミット
						db.SaveChanges();
						tran.Commit();
					}
					catch (Exception e)
					{
						// ロールバック
						tran.Rollback();
						Console.WriteLine(e.Message);
						_logger.Error(e.Message);
					}
				}
			}
		}
		#endregion
	}
}

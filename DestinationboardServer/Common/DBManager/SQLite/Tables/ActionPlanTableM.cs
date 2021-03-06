using DestinationboardServer.Common.DBManager.SQLite.Interface;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class ActionPlanTableM : ActionPlanTableBase, IActionPlanTable
	{
		#region ロガー
		/// <summary>
		/// ロガー
		/// </summary>
		protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		#endregion

		#region 1人分の行動予定の取得関数
		/// <summary>
		/// 1人分の行動予定の取得関数
		/// </summary>
		/// <param name="staff_id">従業員ID</param>
		/// <returns>1人分の行動予定</returns>
		public static ActionPlanTableBase StaffActionPlanSelect(string staff_id)
		{
			try
			{
				using (var db = new SQLiteDataContext())
				{
					var query = from x in db.DbSet_ActionPlanTable
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

		#region gRPC用データからテーブルデータへ変換する
		/// <summary>
		/// gRPC用データからテーブルデータへ変換する
		/// </summary>
		/// <param name="request">gRPC用リクエストデータ</param>
		public static void RequestToTable(RegistActionPlanRequest request, ref IActionPlanTable action)
		{
			action.StaffID = request.ActionPlan.StaffID;                   // 従業員ID
			action.StaffName = request.ActionPlan.StaffName;               // 従業員名
			action.Status = request.ActionPlan.Status;                     // ステータス
			action.ActionID = request.ActionPlan.ActionID;                 // 行動ID
			action.ActionName = request.ActionPlan.ActionName;             // 行動名
			action.DestinationID = request.ActionPlan.DestinationID;       // 行先ID
			action.DestinationName = request.ActionPlan.DestinationName;   // 行先名

			DateTime ret;
			action.FromTime = null;

			// 開始時刻をデータベース登録用に型変換
			if (DateTime.TryParseExact(request.ActionPlan.FromTime, "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ret))
			{
				action.FromTime = ret; // 開始時刻のセット
			}


			action.ToTime = null;
			// 終了時刻をデータベース登録用に型変換
			if (DateTime.TryParseExact(request.ActionPlan.ToTime, "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ret))
			{
				// 終了時刻のセット
				action.ToTime = ret;
			}

			action.Memo = request.ActionPlan.Memo; // 備考


		}
		#endregion

		#region テーブルデータからgRPC用のデータに変換する
		/// <summary>
		/// テーブルデータからgRPC用のデータに変換する
		/// </summary>
		/// <param name="table">テーブルデータ</param>
		/// <returns>gRPC用データ</returns>
		public static RegistActionPlanRequest TableToRequest(ActionPlanTableBase table)
		{
			RegistActionPlanRequest request = new RegistActionPlanRequest();
			request.ActionPlan = new ActionPlanTableRequest();
			var req = request.ActionPlan;

			req.StaffID = table.StaffID;                   // 従業員ID
			req.StaffName = table.StaffName;               // 従業員名
			req.Status = table.Status;                     // ステータス
			req.ActionID = table.ActionID;                 // 行動ID
			req.ActionName = table.ActionName;             // 行動名
			req.DestinationID = table.DestinationID;       // 行先ID
			req.DestinationName = table.DestinationName;   // 行先名


			// 開始時刻をデータベース登録用に型変換
			req.FromTime = string.Empty;
			if (table.FromTime.HasValue)
			{
				req.FromTime = table.FromTime.Value.ToString("yyyy/MM/dd HH:mm:ss"); // 開始時刻のセット
			}

			// 開始時刻をデータベース登録用に型変換
			req.ToTime = string.Empty;
			if (table.ToTime.HasValue)
			{
				req.ToTime = table.ToTime.Value.ToString("yyyy/MM/dd HH:mm:ss"); // 開始時刻のセット
			}

			req.Memo = table.Memo; // 備考

			return request;
		}
		#endregion

		#region 1人分の行動予定を登録する処理
		/// <summary>
		/// 1人分の行動予定を登録する処理
		/// </summary>
		/// <param name="request">リクエスト</param>
		public static bool StaffActionPlanUpdate(RegistActionPlanRequest request)
		{
			// コンテキストの作成
			using (var db = new SQLiteDataContext())
			{
				using (var tran = db.Database.BeginTransaction()) // トランザクション開始
				{
					try
					{
						var items = db.DbSet_ActionPlanTable.ToList<ActionPlanTableBase>();

						// 従業員の行動予定を取得する
						var staff_item = (from x in items
										  where x.StaffID.Equals(request.ActionPlan.StaffID)
										  select x).FirstOrDefault();

						IActionPlanTable tmp = new ActionPlanTableM();
						RequestToTable(request, ref tmp);

						// 従業員の行動予定が存在する場合
						if (staff_item != null)
						{
							// 上書き
							staff_item.Copy((ActionPlanTableM)tmp);
						}
						// 従業員の行動予定が存在しない場合
						else
						{
							// 追加
							db.Add<ActionPlanTableBase>((ActionPlanTableM)tmp);
						}

						// コミット
						db.SaveChanges();
						tran.Commit();
						return true;
					}
					catch (Exception e)
					{
						// ロールバック
						tran.Rollback();
						Console.WriteLine(e.Message);
						_logger.Error(e.Message);
						return false;
					}
				}
			}
		}
		#endregion

		public static bool StaffActionPlanLogInsert(RegistActionPlanRequest request)
		{
			// コンテキストの作成
			using (var db = new SQLiteLogDataContext())
			{
				using (var tran = db.Database.BeginTransaction()) // トランザクション開始
				{
					try
					{
						var items = db.DbSet_ActionPlanTableLog.ToList<ActionPlanTableLogBase>();

						IActionPlanTable action = new ActionPlanTableLogM();
						RequestToTable(request, ref action);

						string format = "yyyy/MM/dd HH:mm:ss";
						((ActionPlanTableLogM)action).RegTime = CommonValues.ConvertDateTime(DateTime.Now.ToString(format), format); // ミリ秒を削除

						db.Add<ActionPlanTableLogBase>((ActionPlanTableLogM)action);

						// コミット
						db.SaveChanges();
						tran.Commit();
						return true;
					}
					catch (Exception e)
					{
						// ロールバック
						tran.Rollback();
						Console.WriteLine(e.Message);
						_logger.Error(e.Message);
						return false;
					}
				}
			}
		}

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
							insert_item.FromTime = tmp.FromTime.Equals(string.Empty) ? null : DateTime.ParseExact(tmp.FromTime, "yyyy/MM/dd HH:mm:ss", null);	// gRPC用に日付型を文字列に変換
							insert_item.ToTime = tmp.ToTime.Equals(string.Empty) ? null : DateTime.ParseExact(tmp.ToTime, "yyyy/MM/dd HH:mm:ss", null); // gRPC用に日付型を文字列に変換
							db.Add<ActionPlanTableBase>(insert_item);
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

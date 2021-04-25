﻿using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
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
			ret.CreateDate = CommonValues.ConvertDateTime(request.CreateDate, "yyyy/MM/dd HH:mm:ss");    // 作成日時
			ret.CreateUser = request.CreateUser;    // 作成者
			return ret;
		}
		#endregion

		public static StaffMasterRequest TableToRequest(StaffMasterBase table)
		{
			StaffMasterRequest ret = new StaffMasterRequest();
			ret.StaffID = table.StaffID;          // 従業員ID
			ret.StaffName = table.StaffName;      // 従業員名
			ret.Display = table.Display;          // 表示・非表示
			ret.SortOrder = table.SortOrder;      // 並び順
			ret.CreateDate = table.CreateDate.ToString("yyyy/MM/dd HH:mm:ss");    // 作成日時
			ret.CreateUser = table.CreateUser;    // 作成者
			return ret;
		}

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

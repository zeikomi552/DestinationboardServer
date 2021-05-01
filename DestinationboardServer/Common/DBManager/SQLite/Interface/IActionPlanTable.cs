using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Interface
{
	/// <summary>
	/// 現在の行動予定一覧表
	/// ActionPlanTableテーブルをベースに作成したインターフェースクラス
	/// 作成日：2021/03/28 作成者gohya
	/// </summary>
	public interface IActionPlanTable
	{
		#region スタッフID[StaffID]プロパティ
		/// <summary>
		/// スタッフID[StaffID]インターフェースクラス
		/// </summary>
		String StaffID { get; set; }
		#endregion
		#region スタッフ名[StaffName]プロパティ
		/// <summary>
		/// スタッフ名[StaffName]インターフェースクラス
		/// </summary>
		String StaffName { get; set; }
		#endregion
		#region 0:帰宅 1:出勤 2:テレワーク出勤[Status]プロパティ
		/// <summary>
		/// 0:帰宅 1:出勤 2:テレワーク出勤[Status]インターフェースクラス
		/// </summary>
		Int32 Status { get; set; }
		#endregion
		#region 行動ID[ActionID]プロパティ
		/// <summary>
		/// 行動ID[ActionID]インターフェースクラス
		/// </summary>
		String ActionID { get; set; }
		#endregion
		#region 行動名[ActionName]プロパティ
		/// <summary>
		/// 行動名[ActionName]インターフェースクラス
		/// </summary>
		String ActionName { get; set; }
		#endregion
		#region 行先ID[DestinationID]プロパティ
		/// <summary>
		/// 行先ID[DestinationID]インターフェースクラス
		/// </summary>
		String DestinationID { get; set; }
		#endregion
		#region 行先名[DestinationName]プロパティ
		/// <summary>
		/// 行先名[DestinationName]インターフェースクラス
		/// </summary>
		String DestinationName { get; set; }
		#endregion
		#region 開始時刻[FromTime]プロパティ
		/// <summary>
		/// 開始時刻[FromTime]インターフェースクラス
		/// </summary>
		DateTime? FromTime { get; set; }
		#endregion
		#region 終了時刻[ToTime]プロパティ
		/// <summary>
		/// 終了時刻[ToTime]インターフェースクラス
		/// </summary>
		DateTime? ToTime { get; set; }
		#endregion
		#region 備考[Memo]プロパティ
		/// <summary>
		/// 備考[Memo]インターフェースクラス
		/// </summary>
		String Memo { get; set; }
		#endregion

	}
}

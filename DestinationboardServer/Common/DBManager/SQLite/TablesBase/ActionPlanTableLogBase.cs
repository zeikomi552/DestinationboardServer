using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.TablesBase
{
	/// <summary>
	/// 行動予定のログ
	/// ActionPlanTableLogテーブルをベースに作成しています
	/// 作成日：2021/05/02 作成者gohya
	/// </summary>
	[Table("ActionPlanTableLog")]
	public class ActionPlanTableLogBase : INotifyPropertyChanged
	{
		#region パラメータ
		#region 登録日時[RegTime]プロパティ
		/// <summary>
		/// 登録日時[RegTime]プロパティ用変数
		/// </summary>
		DateTime _RegTime = DateTime.MinValue;
		/// <summary>
		/// 登録日時[RegTime]プロパティ
		/// </summary>
		[Key]
		[Column("RegTime")]
		public DateTime RegTime
		{
			get
			{
				return _RegTime;
			}
			set
			{
				if (!_RegTime.Equals(value))
				{
					_RegTime = value;
					NotifyPropertyChanged("RegTime");
				}
			}
		}
		#endregion

		#region 従業員ID[StaffID]プロパティ
		/// <summary>
		/// 従業員ID[StaffID]プロパティ用変数
		/// </summary>
		String _StaffID = string.Empty;
		/// <summary>
		/// 従業員ID[StaffID]プロパティ
		/// </summary>
		[Key]
		[Column("StaffID")]
		public String StaffID
		{
			get
			{
				return _StaffID;
			}
			set
			{
				if (_StaffID == null || !_StaffID.Equals(value))
				{
					_StaffID = value;
					NotifyPropertyChanged("StaffID");
				}
			}
		}
		#endregion

		#region 従業員名[StaffName]プロパティ
		/// <summary>
		/// 従業員名[StaffName]プロパティ用変数
		/// </summary>
		String _StaffName = string.Empty;
		/// <summary>
		/// 従業員名[StaffName]プロパティ
		/// </summary>
		[Column("StaffName")]
		public String StaffName
		{
			get
			{
				return _StaffName;
			}
			set
			{
				if (_StaffName == null || !_StaffName.Equals(value))
				{
					_StaffName = value;
					NotifyPropertyChanged("StaffName");
				}
			}
		}
		#endregion

		#region 0:帰宅 1:出勤 2:テレワーク出勤[Status]プロパティ
		/// <summary>
		/// 0:帰宅 1:出勤 2:テレワーク出勤[Status]プロパティ用変数
		/// </summary>
		Int32 _Status = 0;
		/// <summary>
		/// 0:帰宅 1:出勤 2:テレワーク出勤[Status]プロパティ
		/// </summary>
		[Column("Status")]
		public Int32 Status
		{
			get
			{
				return _Status;
			}
			set
			{
				if (!_Status.Equals(value))
				{
					_Status = value;
					NotifyPropertyChanged("Status");
				}
			}
		}
		#endregion

		#region 行動ID[ActionID]プロパティ
		/// <summary>
		/// 行動ID[ActionID]プロパティ用変数
		/// </summary>
		String _ActionID = string.Empty;
		/// <summary>
		/// 行動ID[ActionID]プロパティ
		/// </summary>
		[Column("ActionID")]
		public String ActionID
		{
			get
			{
				return _ActionID;
			}
			set
			{
				if (_ActionID == null || !_ActionID.Equals(value))
				{
					_ActionID = value;
					NotifyPropertyChanged("ActionID");
				}
			}
		}
		#endregion

		#region 行動名[ActionName]プロパティ
		/// <summary>
		/// 行動名[ActionName]プロパティ用変数
		/// </summary>
		String _ActionName = string.Empty;
		/// <summary>
		/// 行動名[ActionName]プロパティ
		/// </summary>
		[Column("ActionName")]
		public String ActionName
		{
			get
			{
				return _ActionName;
			}
			set
			{
				if (_ActionName == null || !_ActionName.Equals(value))
				{
					_ActionName = value;
					NotifyPropertyChanged("ActionName");
				}
			}
		}
		#endregion

		#region 行先ID[DestinationID]プロパティ
		/// <summary>
		/// 行先ID[DestinationID]プロパティ用変数
		/// </summary>
		String _DestinationID = string.Empty;
		/// <summary>
		/// 行先ID[DestinationID]プロパティ
		/// </summary>
		[Column("DestinationID")]
		public String DestinationID
		{
			get
			{
				return _DestinationID;
			}
			set
			{
				if (_DestinationID == null || !_DestinationID.Equals(value))
				{
					_DestinationID = value;
					NotifyPropertyChanged("DestinationID");
				}
			}
		}
		#endregion

		#region 行先名[DestinationName]プロパティ
		/// <summary>
		/// 行先名[DestinationName]プロパティ用変数
		/// </summary>
		String _DestinationName = string.Empty;
		/// <summary>
		/// 行先名[DestinationName]プロパティ
		/// </summary>
		[Column("DestinationName")]
		public String DestinationName
		{
			get
			{
				return _DestinationName;
			}
			set
			{
				if (_DestinationName == null || !_DestinationName.Equals(value))
				{
					_DestinationName = value;
					NotifyPropertyChanged("DestinationName");
				}
			}
		}
		#endregion

		#region 開始時刻[FromTime]プロパティ
		/// <summary>
		/// 開始時刻[FromTime]プロパティ用変数
		/// </summary>
		DateTime? _FromTime = null;
		/// <summary>
		/// 開始時刻[FromTime]プロパティ
		/// </summary>
		[Column("FromTime")]
		public DateTime? FromTime
		{
			get
			{
				return _FromTime;
			}
			set
			{
				if (_FromTime == null || !_FromTime.Equals(value))
				{
					_FromTime = value;
					NotifyPropertyChanged("FromTime");
				}
			}
		}
		#endregion

		#region 終了時刻[ToTime]プロパティ
		/// <summary>
		/// 終了時刻[ToTime]プロパティ用変数
		/// </summary>
		DateTime? _ToTime = null;
		/// <summary>
		/// 終了時刻[ToTime]プロパティ
		/// </summary>
		[Column("ToTime")]
		public DateTime? ToTime
		{
			get
			{
				return _ToTime;
			}
			set
			{
				if (_ToTime == null || !_ToTime.Equals(value))
				{
					_ToTime = value;
					NotifyPropertyChanged("ToTime");
				}
			}
		}
		#endregion

		#region メモ[Memo]プロパティ
		/// <summary>
		/// メモ[Memo]プロパティ用変数
		/// </summary>
		String _Memo = string.Empty;
		/// <summary>
		/// メモ[Memo]プロパティ
		/// </summary>
		[Column("Memo")]
		public String Memo
		{
			get
			{
				return _Memo;
			}
			set
			{
				if (_Memo == null || !_Memo.Equals(value))
				{
					_Memo = value;
					NotifyPropertyChanged("Memo");
				}
			}
		}
		#endregion


		#endregion

		#region 関数
		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ActionPlanTableLogBase()
		{

		}
		#endregion

		#region コピーコンストラクタ
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="item">コピー内容</param>
		public ActionPlanTableLogBase(ActionPlanTableLogBase item)
		{
			// 要素のコピー
			Copy(item);
		}
		#endregion

		#region コピー
		/// <summary>
		/// コピー
		/// </summary>
		/// <param name="item">コピー内容</param>
		public void Copy(ActionPlanTableLogBase item)
		{
			this.RegTime = item.RegTime;

			this.StaffID = item.StaffID;

			this.StaffName = item.StaffName;

			this.Status = item.Status;

			this.ActionID = item.ActionID;

			this.ActionName = item.ActionName;

			this.DestinationID = item.DestinationID;

			this.DestinationName = item.DestinationName;

			this.FromTime = item.FromTime;

			this.ToTime = item.ToTime;

			this.Memo = item.Memo;


		}
		#endregion

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(ActionPlanTableLogBase item)
		{
			using (var db = new SQLiteLogDataContext())
			{
				// Insert
				db.Add<ActionPlanTableLogBase>(item);

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
		public static void Update(ActionPlanTableLogBase pk_item, ActionPlanTableLogBase update_item)
		{
			using (var db = new SQLiteLogDataContext())
			{
				var item = db.DbSet_ActionPlanTableLog.SingleOrDefault(x => x.RegTime.Equals(pk_item.RegTime) && x.StaffID.Equals(pk_item.StaffID));

				if (item != null)
				{
					item.Copy(update_item);
					db.SaveChanges();
				}
			}
		}
		#endregion

		#region Delete処理
		/// <summary>
		/// Delete処理
		/// </summary>
		/// <param name="pk_item">削除する主キー（主キーの値のみ入っていれば良い）</param>
		public static void Delete(ActionPlanTableLogBase pk_item)
		{
			using (var db = new SQLiteLogDataContext())
			{
				var item = db.DbSet_ActionPlanTableLog.SingleOrDefault(x => x.RegTime.Equals(pk_item.RegTime) && x.StaffID.Equals(pk_item.StaffID));
				if (item != null)
				{
					db.DbSet_ActionPlanTableLog.Remove(item);
					db.SaveChanges();
				}
			}
		}
		#endregion

		#region Select処理
		/// <summary>
		/// Select処理
		/// </summary>
		/// <returns>全件取得</returns>
		public static List<ActionPlanTableLogBase> Select()
		{
			using (var db = new SQLiteLogDataContext())
			{
				return db.DbSet_ActionPlanTableLog.ToList<ActionPlanTableLogBase>();
			}
		}
		#endregion
		#endregion

		#region INotifyPropertyChanged 
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}

}

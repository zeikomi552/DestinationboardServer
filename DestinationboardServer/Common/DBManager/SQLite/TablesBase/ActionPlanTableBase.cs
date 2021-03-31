using DestinationboardServer.Common.DBManager.SQLite.Tables;
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
	/// 現在の行動予定一覧表
	/// ActionPlanTableテーブルをベースに作成しています
	/// 作成日：2021/03/28 作成者gohya
	/// </summary>
	[Table("ActionPlanTable")]
	public class ActionPlanTableBase : INotifyPropertyChanged
	{

		#region スタッフID[StaffID]プロパティ
		/// <summary>
		/// スタッフID[StaffID]プロパティ用変数
		/// </summary>
		String _StaffID = string.Empty;
		/// <summary>
		/// スタッフID[StaffID]プロパティ
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

		#region スタッフ名[StaffName]プロパティ
		/// <summary>
		/// スタッフ名[StaffName]プロパティ用変数
		/// </summary>
		String _StaffName = string.Empty;
		/// <summary>
		/// スタッフ名[StaffName]プロパティ
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

		#region 備考[Memo]プロパティ
		/// <summary>
		/// 備考[Memo]プロパティ用変数
		/// </summary>
		String _Memo = string.Empty;
		/// <summary>
		/// 備考[Memo]プロパティ
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

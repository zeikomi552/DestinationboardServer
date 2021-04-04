﻿using System;
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
	/// スタッフマスター
	/// StaffMasterテーブルをベースに作成しています
	/// 作成日：2021/03/28 作成者gohya
	/// </summary>
	[Table("StaffMaster")]
	public class StaffMasterBase : INotifyPropertyChanged
	{
		#region パラメータ
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

		#region 表示(True)/非表示(False)[Display]プロパティ
		/// <summary>
		/// 表示(True)/非表示(False)[Display]プロパティ用変数
		/// </summary>
		bool _Display = false;
		/// <summary>
		/// 表示(True)/非表示(False)[Display]プロパティ
		/// </summary>
		[Column("Display")]
		public bool Display
		{
			get
			{
				return _Display;
			}
			set
			{
				if (_Display == null || !_Display.Equals(value))
				{
					_Display = value;
					NotifyPropertyChanged("Display");
				}
			}
		}
		#endregion

		#region 作成日時[CreateDate]プロパティ
		/// <summary>
		/// 作成日時[CreateDate]プロパティ用変数
		/// </summary>
		DateTime _CreateDate = DateTime.MinValue;
		/// <summary>
		/// 作成日時[CreateDate]プロパティ
		/// </summary>
		[Column("CreateDate")]
		public DateTime CreateDate
		{
			get
			{
				return _CreateDate;
			}
			set
			{
				if (_CreateDate == null || !_CreateDate.Equals(value))
				{
					_CreateDate = value;
					NotifyPropertyChanged("CreateDate");
				}
			}
		}
		#endregion

		#region 作成者[CreateUser]プロパティ
		/// <summary>
		/// 作成者[CreateUser]プロパティ用変数
		/// </summary>
		String _CreateUser = string.Empty;
		/// <summary>
		/// 作成者[CreateUser]プロパティ
		/// </summary>
		[Column("CreateUser")]
		public String CreateUser
		{
			get
			{
				return _CreateUser;
			}
			set
			{
				if (_CreateUser == null || !_CreateUser.Equals(value))
				{
					_CreateUser = value;
					NotifyPropertyChanged("CreateUser");
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
		public StaffMasterBase()
		{

		}
		#endregion

		#region コピーコンストラクタ
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="item">コピー内容</param>
		public StaffMasterBase(StaffMasterBase item)
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
		public void Copy(StaffMasterBase item)
		{
			this.StaffID = item.StaffID;

			this.StaffName = item.StaffName;

			this.Display = item.Display;

			this.CreateDate = item.CreateDate;

			this.CreateUser = item.CreateUser;


		}
		#endregion

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(StaffMasterBase item)
		{
			using (var db = new SQLiteDataContext())
			{
				// Insert
				db.Add<StaffMasterBase>(item);

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
		public static void Update(StaffMasterBase pk_item, StaffMasterBase update_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_StaffMaster.SingleOrDefault(x => x.StaffID.Equals(pk_item.StaffID));

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
		public static void Delete(StaffMasterBase pk_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_StaffMaster.SingleOrDefault(x => x.StaffID.Equals(pk_item.StaffID));
				if (item != null)
				{
					db.DbSet_StaffMaster.Remove(item);
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
		public static List<StaffMasterBase> Select()
		{
			using (var db = new SQLiteDataContext())
			{
				return db.DbSet_StaffMaster.ToList<StaffMasterBase>();
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

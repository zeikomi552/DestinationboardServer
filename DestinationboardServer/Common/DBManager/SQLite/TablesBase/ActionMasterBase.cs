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
	/// 行動マスター
	/// ActionMasterテーブルをベースに作成しています
	/// 作成日：2021/03/28 作成者gohya
	/// </summary>
	[Table("ActionMaster")]
	public class ActionMasterBase : INotifyPropertyChanged
	{
		#region パラメータ
		#region 行動ID[ActionID]プロパティ
		/// <summary>
		/// 行動ID[ActionID]プロパティ用変数
		/// </summary>
		String _ActionID = string.Empty;
		/// <summary>
		/// 行動ID[ActionID]プロパティ
		/// </summary>
		[Key]
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
				if (!_CreateDate.Equals(value))
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

		#region 更新日時[UpdateDate]プロパティ
		/// <summary>
		/// 更新日時[UpdateDate]プロパティ用変数
		/// </summary>
		DateTime _UpdateDate = DateTime.MinValue;
		/// <summary>
		/// 更新日時[UpdateDate]プロパティ
		/// </summary>
		[Column("UpdateDate")]
		public DateTime UpdateDate
		{
			get
			{
				return _UpdateDate;
			}
			set
			{
				if (!_UpdateDate.Equals(value))
				{
					_UpdateDate = value;
					NotifyPropertyChanged("UpdateDate");
				}
			}
		}
		#endregion

		#region 更新者[UpdateUser]プロパティ
		/// <summary>
		/// 更新者[UpdateUser]プロパティ用変数
		/// </summary>
		String _UpdateUser = string.Empty;
		/// <summary>
		/// 更新者[UpdateUser]プロパティ
		/// </summary>
		[Column("UpdateUser")]
		public String UpdateUser
		{
			get
			{
				return _UpdateUser;
			}
			set
			{
				if (_UpdateUser == null || !_UpdateUser.Equals(value))
				{
					_UpdateUser = value;
					NotifyPropertyChanged("UpdateUser");
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
		public ActionMasterBase()
		{

		}
		#endregion

		#region コピーコンストラクタ
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="item">コピー内容</param>
		public ActionMasterBase(ActionMasterBase item)
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
		public void Copy(ActionMasterBase item)
		{
			this.ActionID = item.ActionID;

			this.ActionName = item.ActionName;

			this.CreateDate = item.CreateDate;

			this.CreateUser = item.CreateUser;

			this.UpdateDate = item.UpdateDate;

			this.UpdateUser = item.UpdateUser;


		}
		#endregion

		#region Insert処理
		/// <summary>
		/// Insert処理
		/// </summary>
		/// <param name="item">Insertする要素</param>
		public static void Insert(ActionMasterBase item)
		{
			using (var db = new SQLiteDataContext())
			{
				// Insert
				db.Add<ActionMasterBase>(item);

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
		public static void Update(ActionMasterBase pk_item, ActionMasterBase update_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionMaster.Single(x => x.ActionID.Equals(pk_item.ActionID));
				item.Copy(update_item);

				db.SaveChanges();
			}
		}
		#endregion

		#region Delete処理
		/// <summary>
		/// Delete処理
		/// </summary>
		/// <param name="pk_item">削除する主キー（主キーの値のみ入っていれば良い）</param>
		public static void Delete(ActionMasterBase pk_item)
		{
			using (var db = new SQLiteDataContext())
			{
				var item = db.DbSet_ActionMaster.Single(x => x.ActionID.Equals(pk_item.ActionID));
				db.DbSet_ActionMaster.Remove(item);
				db.SaveChanges();
			}
		}
		#endregion

		#region Select処理
		/// <summary>
		/// Select処理
		/// </summary>
		/// <returns>全件取得</returns>
		public static List<ActionMasterBase> Select()
		{
			using (var db = new SQLiteDataContext())
			{
				return db.DbSet_ActionMaster.ToList<ActionMasterBase>();
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

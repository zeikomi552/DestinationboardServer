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

        public void Insert(ActionMasterBase item)
        {
            using (var db = new SQLiteDataContext())
            {
                // Insert
                db.Add<ActionMasterBase>(item);

                // コミット
                db.SaveChanges();
            }
        }

    }


}

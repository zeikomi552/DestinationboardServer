using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables
{
    public class ActionMasterM : ActionMasterBase
    {
        #region ロガー
        /// <summary>
        /// ロガー
        /// </summary>
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion


        #region ActionMaster用変換処理
        #region テーブル情報→Replyに変換する
        /// <summary>
        /// テーブル情報→Replyに変換する
        /// </summary>
        /// <param name="item">テーブル要素</param>
        /// <returns>リプライ</returns>
        public static ActionMasterReply TableToReply(ActionMasterBase item)
        {
            try
            {
                ActionMasterReply tmp = new ActionMasterReply();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.ActionName = item.ActionName;                           // 行動名
                tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 作成日
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = item.UpdateDate.ToString("yyyy/MM/dd");    // 更新日
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region リプライ形式からテーブル形式に変換する
        /// <summary>
        /// リプライ形式からテーブル形式に変換する
        /// </summary>
        /// <param name="item">リプライ</param>
        /// <returns>テーブル要素</returns>
        public static ActionMasterBase ReplyToTable(ActionMasterReply item)
        {
            try
            {
                ActionMasterBase tmp = new ActionMasterBase();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.ActionName = item.ActionName;                           // 行動名
                tmp.CreateDate = CommonValues.ConvertDateTime(item.CreateDate, "yyyy/MM/dd");   // 作成日時
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = CommonValues.ConvertDateTime(item.UpdateDate, "yyyy/MM/dd");   // 更新日時
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region テーブル情報からリクエスト形式に変換する
        /// <summary>
        /// テーブル情報からリクエスト形式に変換する
        /// </summary>
        /// <param name="item">テーブル形式</param>
        /// <returns>リクエスト形式</returns>
        public static ActionMasterRequest TableToRequest(ActionMasterBase item)
        {
            try
            {
                ActionMasterRequest tmp = new ActionMasterRequest();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.ActionName = item.ActionName;                           // 行動名
                tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 作成日
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = item.UpdateDate.ToString("yyyy/MM/dd");    // 更新日
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region リクエスト形式からテーブル形式に変換する
        /// <summary>
        /// リクエスト形式からテーブル形式に変換する
        /// </summary>
        /// <param name="item">リクエスト形式</param>
        /// <returns>テーブル要素</returns>
        public static ActionMasterBase RequestToTable(ActionMasterRequest item)
        {
            try
            {
                ActionMasterBase tmp = new ActionMasterBase();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.ActionName = item.ActionName;                           // 行動名
                tmp.CreateDate = CommonValues.ConvertDateTime(item.CreateDate, "yyyy/MM/dd");   // 作成日時
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = CommonValues.ConvertDateTime(item.UpdateDate, "yyyy/MM/dd");   // 更新日時
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion
        #endregion

        #region テーブル情報→Replyに変換する
        /// <summary>
        /// テーブル情報→Replyに変換する
        /// </summary>
        /// <param name="item">テーブル要素</param>
        /// <returns>リプライ</returns>
        public static DestinationMasterReply TableToReply(DestinationMasterBase item)
        {
            try
            {
                DestinationMasterReply tmp = new DestinationMasterReply();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 作成日
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.DestinationID = item.DestinationID;                     // 行先ID
                tmp.DestinationName = item.DestinationName;                 // 行先名
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = item.UpdateDate.ToString("yyyy/MM/dd");    // 更新日
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region テーブル情報→Requestに変換する
        /// <summary>
        /// テーブル情報→Replyに変換する
        /// </summary>
        /// <param name="item">テーブル要素</param>
        /// <returns>リプライ</returns>
        public static DestinationMasterRequest TableToRequest(DestinationMasterBase item)
        {
            try
            {
                DestinationMasterRequest tmp = new DestinationMasterRequest();
                tmp.ActionID = item.ActionID;                               // 行動ID
                tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 作成日
                tmp.CreateUser = item.CreateUser;                           // 作成者
                tmp.DestinationID = item.DestinationID;                     // 行先ID
                tmp.DestinationName = item.DestinationName;                 // 行先名
                tmp.SortOrder = item.SortOrder;                             // ソート順
                tmp.UpdateDate = item.UpdateDate.ToString("yyyy/MM/dd");    // 更新日
                tmp.UpdateUser = item.UpdateUser;                           // 更新者
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region リクエスト形式からテーブル形式に変換する
        /// <summary>
        /// リクエスト形式からテーブル形式に変換する
        /// </summary>
        /// <param name="item">リクエスト形式</param>
        /// <returns>テーブル形式</returns>
        public static DestinationMasterBase RequestToTable(DestinationMasterRequest item)
        {
            try
            {
                DestinationMasterBase tmp = new DestinationMasterBase();
                tmp.ActionID = item.ActionID;
                tmp.CreateDate = CommonValues.ConvertDateTime(item.CreateDate, "yyyy/MM/dd");
                tmp.CreateUser = item.CreateUser;
                tmp.DestinationID = item.DestinationID;
                tmp.DestinationName = item.DestinationName;
                tmp.SortOrder = item.SortOrder;
                tmp.UpdateDate = CommonValues.ConvertDateTime(item.UpdateDate, "yyyy/MM/dd");
                tmp.UpdateUser = item.UpdateUser;
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region リクエスト形式からテーブル形式に変換する
        /// <summary>
        /// リクエスト形式からテーブル形式に変換する
        /// </summary>
        /// <param name="item">リクエスト形式</param>
        /// <returns>テーブル形式</returns>
        public static DestinationMasterBase ReplyToTable(DestinationMasterReply item)
        {
            try
            {
                DestinationMasterBase tmp = new DestinationMasterBase();
                tmp.ActionID = item.ActionID;
                tmp.CreateDate = CommonValues.ConvertDateTime(item.CreateDate, "yyyy/MM/dd");
                tmp.CreateUser = item.CreateUser;
                tmp.DestinationID = item.DestinationID;
                tmp.DestinationName = item.DestinationName;
                tmp.SortOrder = item.SortOrder;
                tmp.UpdateDate = CommonValues.ConvertDateTime(item.UpdateDate, "yyyy/MM/dd");
                tmp.UpdateUser = item.UpdateUser;
                return tmp;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region リストの更新処理
        /// <summary>
        /// リストの更新処理
        /// </summary>
        /// <param name="request"></param>
        public static void UpdateList(RegistActionsRequest request)
        {
            using (var db = new SQLiteDataContext())
            {
                using (var tran = db.Database.BeginTransaction()) // トランザクション開始
                {
                    try
                    {
                        // 行動マスターの削除
                        db.DbSet_ActionMaster.RemoveRange(db.DbSet_ActionMaster);

                        foreach (var tmp in request.ActionMasterList)
                        {
                            ActionMasterBase table = RequestToTable(tmp);
                            db.Add<ActionMasterBase>(table);
                        }

                        // 行先マスターの削除
                        db.DbSet_DestinationMaster.RemoveRange(db.DbSet_DestinationMaster);

                        foreach (var tmp in request.DestinationMasterList)
                        {
                            DestinationMasterBase table = RequestToTable(tmp);
                            db.Add<DestinationMasterBase>(table);
                        }

                        // コミット
                        db.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
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

using DestinationboardServer.Common;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer
{
    class Listener
    {
        #region プロパティ
        #region サービス
        /// <summary>
        /// サービス
        /// </summary>
        DestinationbardCommunicationAPIService _Service = null;
        #endregion

        #region ロガー
        /// <summary>
        /// ロガー
        /// </summary>
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region スタート状態かどうかを示すフラグ
        /// <summary>
        /// スタート状態かどうかを示すフラグ
        /// </summary>
        private bool _StartF = false;
        #endregion
        #endregion

        #region 関数
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Listener()
        {
            if (this._Service == null)
            {
                // サービスの作成
                this._Service = new DestinationbardCommunicationAPIService(CommonValues.GetInstance().ServerName, CommonValues.GetInstance().Port);
            }

            // スタッフ情報登録処理用イベント
            this._Service.RecieveRegistStaffEvent += Service_RecieveRegistStaffEvent;

            // スタッフ情報取得用イベント
            this._Service.RecieveGetStaffsEvent += Service_RecieveGetStaffEvent;

            // 行動情報登録用イベント
            this._Service.RecieveRegistActionsEvent += _Service_RecieveRegistActionsEvent;

            // 行動情報取得用イベント
            this._Service.RecieveGetActionsEvent += _Service_RecieveGetActionsEvent;

            // 行動予定情報登録用イベント
            this._Service.RecieveRegistActionPlansEvent += _Service_RecieveRegistActionPlansEvent;

            // 行動予定一覧取得用イベント
            this._Service.RecieveGetActionPlansEvent += _Service_RecieveGetActionPlansEvent;

            // 個人の行動予定の更新
            this._Service.RecieveGetActionPlanEvent += _Service_RecieveGetActionPlanEvent;

            // 個人の行動予定の取得
            this._Service.RecieveRegistActionPlanEvent += _Service_RecieveRegistActionPlanEvent;

        }
        #endregion

        #region 1人分の行動予定を登録するイベント
        /// <summary>
        /// 1人分の行動予定を登録するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveRegistActionPlanEvent(object sender, EventArgs e)
        {
            RegistActionPlanRequest request = ((gRPCArgsRcv)e).Request as RegistActionPlanRequest;    // リクエスト
            RegistActionPlanReply reply = ((gRPCArgsRcv)e).Replay as RegistActionPlanReply;           // リプライ

            try
            {
                // 行動予定の更新
                ActionPlanTableM.StaffActionPlanUpdate(request);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 一人分の行動予定を取得するイベント
        /// <summary>
        /// 一人分の行動予定を取得するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveGetActionPlanEvent(object sender, EventArgs e)
        {
            GetActionPlanRequest request = ((gRPCArgsRcv)e).Request as GetActionPlanRequest;    // リクエスト
            GetActionPlanReply reply = ((gRPCArgsRcv)e).Replay as GetActionPlanReply;           // リプライ
            reply.ActionPlan = new ActionPlanTableReply();

            try
            {
                // 行動予定の取得
                var tmp = ActionPlanTableM.StaffActionPlanSelect(request.StaffID);

                // 行動予定が登録されている
                if (tmp != null)
                {
                    reply.ActionPlan.StaffID = tmp.StaffID;                 // 従業員ID
                    reply.ActionPlan.StaffName = tmp.StaffName;             // 従業員名
                    reply.ActionPlan.Status = tmp.Status;                   // ステータス
                    reply.ActionPlan.ActionID = tmp.ActionID;               // 行動ID
                    reply.ActionPlan.ActionName = tmp.ActionName;           // 行動名
                    reply.ActionPlan.DestinationID = tmp.DestinationID;     // 行先ID
                    reply.ActionPlan.DestinationName = tmp.DestinationName; // 行先名
                    reply.ActionPlan.FromTime = tmp.FromTime.HasValue ? tmp.FromTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty;  // 開始時刻
                    reply.ActionPlan.ToTime = tmp.ToTime.HasValue ? tmp.ToTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty;        // 終了時刻
                }
                // 行動予定が登録されていない
                else
                {
                    // 従業員マスターからデータを取得する
                    var staff = StaffMasterM.Select(request.StaffID);

                    // 従業員マスターからデータがとれた
                    if (staff != null)
                    {
                        reply.ActionPlan.StaffID = staff.StaffID;       // 従業員IDのセット
                        reply.ActionPlan.StaffName = staff.StaffName;   // 従業員名をセット
                    }
                    else
                    {
                        reply.ErrorCode = 1;    // 1：従業員マスターにデータが登録されていません
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
                reply.ErrorCode = -1;    // -1:致命的なエラー

            }
        }
        #endregion

        #region サービスのスタート
        /// <summary>
        /// サービスのスタート
        /// </summary>
        public void Start()
        {
            try
            {
                // サービスがnullでない && サービスが開始していない
                if (this._Service != null && !this._StartF)
                {
                    // サービスの開始
                    this._Service.Listen();
                    this._StartF = true;    // スタートフラグON
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 行動予定一覧取得用イベント
        /// <summary>
        /// 行動予定一覧取得用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveGetActionPlansEvent(object sender, EventArgs e)
        {
            GetActionPlansRequest request = ((gRPCArgsRcv)e).Request as GetActionPlansRequest;    // リクエスト
            GetActionPlansReply reply = ((gRPCArgsRcv)e).Replay as GetActionPlansReply;           // リプライ

            try
            {
                // スタッフ情報の取得処理(DBアクセス)
                var list = ActionPlanTableM.Select();

                // 取得データを通信用に変換
                foreach (var item in list)
                {
                    ActionPlanTableReply tmp = new ActionPlanTableReply();
                    tmp.StaffID = item.StaffID;                 // 従業員情報
                    tmp.StaffName = item.StaffName;             // 従業員名
                    tmp.Status = item.Status;                   // ステータス情報
                    tmp.ActionID = item.ActionID;               // 行動ID
                    tmp.ActionName = item.ActionName;           // 行動名
                    tmp.DestinationID = item.DestinationID;     // 行先ID
                    tmp.DestinationName = item.DestinationName; // 行先

                    tmp.FromTime = string.Empty;
                    // 値がある場合
                    if (item.FromTime.HasValue)
                    {
                        tmp.FromTime = item.FromTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    }

                    tmp.ToTime = string.Empty;
                    // 値がある場合
                    if (item.ToTime.HasValue)
                    {
                        tmp.ToTime = item.ToTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    tmp.Memo = item.Memo;   // メモ

                    reply.ActionPlans.Add(tmp);                               // リストへ追加
                }

            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 行動予定登録用イベント
        /// <summary>
        /// 行動予定登録用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveRegistActionPlansEvent(object sender, EventArgs e)
        {
            RegistActionPlansRequest request = ((gRPCArgsRcv)e).Request as RegistActionPlansRequest;    // リクエスト
            RegistActionPlansReply reply = ((gRPCArgsRcv)e).Replay as RegistActionPlansReply;           // リプライ

            try
            {
                // スタッフ情報の取得処理(DBアクセス)
                ActionPlanTableM.Update(request);

            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 行動マスター取得用イベント
        /// <summary>
        /// 行動マスター取得用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveGetActionsEvent(object sender, EventArgs e)
        {

            GetActionsReply reply = ((gRPCArgsRcv)e).Replay as GetActionsReply;           // リプライ

            try
            {
                GetActionsRequest request = ((gRPCArgsRcv)e).Request as GetActionsRequest;    // リクエスト

                // 行動マスタ情報の取得処理(DBアクセス)
                var list = ActionMasterM.Select();

                // 取得データを通信用に変換
                foreach (var item in list)
                {
                    ActionMasterReply tmp = new ActionMasterReply();
                    tmp.ActionID = item.ActionID;                               // 行動ID
                    tmp.ActionName = item.ActionName;                           // 行動名
                    tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 作成日
                    tmp.CreateUser = item.CreateUser;                           // 作成者
                    tmp.SortOrder = item.SortOrder;                             // ソート順
                    tmp.UpdateDate = item.UpdateDate.ToString("yyyy/MM/dd");    // 更新日
                    tmp.UpdateUser = item.UpdateUser;                           // 更新者
                    reply.ActionList.Add(tmp);                                  // リストへ追加
                }

                // 行先マスタ情報の取得処理(DBアクセス)
                var dist_list = DestinationMasterM.Select();

                // 取得データを通信用に変換
                foreach (var item in dist_list)
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

                    reply.DestinationList.Add(tmp);                                  // リストへ追加
                }

            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 行動マスターリスト登録用イベント
        /// <summary>
        /// 行動マスターリスト登録用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveRegistActionsEvent(object sender, EventArgs e)
        {
            RegistActionsRequest request = ((gRPCArgsRcv)e).Request as RegistActionsRequest;    // リクエスト
            RegistStaffReply reply = ((gRPCArgsRcv)e).Replay as RegistStaffReply;           // リプライ

            try
            {
                Console.WriteLine("Recieved");
                Console.WriteLine(request.ActionMasterList.ToString());
                Console.WriteLine(request.DestinationMasterList.ToString());
                // データベース登録処理
                ActionMasterM.UpdateList(request);
            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region スタッフ情報取得用イベント
        /// <summary>
        /// スタッフ情報取得用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Service_RecieveGetStaffEvent(object sender, EventArgs e)
        {
            GetStaffsRequest request = ((gRPCArgsRcv)e).Request as GetStaffsRequest;    // リクエスト
            GetStaffsReply reply = ((gRPCArgsRcv)e).Replay as GetStaffsReply;           // リプライ

            try
            {
                // スタッフ情報の取得処理(DBアクセス)
                var list = StaffMasterM.Select();

                // 取得データを通信用に変換
                foreach (var item in list)
                {
                    StaffMasterReply tmp = new StaffMasterReply();
                    tmp.StaffID = item.StaffID;                                 // スタッフ情報
                    tmp.SortOrder = item.SortOrder;                             // ソート順
                    tmp.StaffName = item.StaffName;                             // スタッフ名
                    tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");    // 日付
                    tmp.CreateUser = item.CreateUser;                           // ユーザー名
                    tmp.Display = item.Display;                                 // 表示/非表示
                    reply.StaffInfoList.Add(tmp);                               // リストへ追加
                }

            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region スタッフ情報登録用イベント
        /// <summary>
        /// スタッフ情報登録用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Service_RecieveRegistStaffEvent(object sender, EventArgs e)
        {
            RegistStaffRequest request = ((gRPCArgsRcv)e).Request as RegistStaffRequest;    // リクエスト
            RegistStaffReply reply = ((gRPCArgsRcv)e).Replay as RegistStaffReply;           // リプライ

            try
            {
                // データベース登録処理
                StaffMasterM.UpdateList(request);

            }
            catch (Exception ex)
            {
                reply.ErrorCode = -1;
                _logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
        #endregion
    }
}

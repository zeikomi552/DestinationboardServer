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
        /// <summary>
        /// サービス
        /// </summary>
        DestinationbardCommunicationAPIService _Service = null;

        /// <summary>
        /// スタート状態かどうかを示すフラグ
        /// </summary>
        private bool _StartF = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Listener()
        {
            if (this._Service != null)
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
            this._Service.RecieveRegistActionPlanEvent += _Service_RecieveRegistActionPlanEvent;

            // 行動予定一覧取得用イベント
            this._Service.RecieveGetActionPlansEvent += _Service_RecieveGetActionPlansEvent;

        }

        /// <summary>
        /// 行動予定一覧取得用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveGetActionPlansEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 行動予定登録用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveRegistActionPlanEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 行動マスター取得用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveGetActionsEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 行動マスターリスト登録用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_RecieveRegistActionsEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// サービスのスタート
        /// </summary>
        public void Start()
        {
            // サービスがnullでない && サービスが開始していない
            if (this._Service != null && !this._StartF)
            {
                // サービスの開始
                this._Service.Listen();
                this._StartF = true;    // スタートフラグON
            }
        }


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
            catch (Exception)
            {

            }
        }


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
            catch (Exception)
            {
                reply.ErrorCode = 2;
            }

        }
    }
}

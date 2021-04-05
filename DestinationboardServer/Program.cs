using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;

namespace DestinationboardServer
{
    class Program
    {
        /// <summary>
        /// メイン関数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // サービスの作成
            DestinationbardCommunicationAPIService service 
                = new DestinationbardCommunicationAPIService(CommonValues.GetInstance().ServerName, CommonValues.GetInstance().Port);

            // スタッフ情報登録処理用イベント
            service.RecieveRegistStaffEvent += Service_RecieveRegistStaffEvent;

            // スタッフ情報取得用イベント
            service.RecieveGetStaffsEvent += Service_RecieveGetStaffEvent;



            service.Listen();

            Console.ReadLine();


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

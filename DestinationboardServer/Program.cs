using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common;
using DestinationboardServer.Common.Config;
using DestinationboardServer.Common.DBManager.SQLite;
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
            ConfigManager config = new ConfigManager();
            config.LoadConfig();

            // SQLite用のファイルパス
            SQLiteDataContext.SQLitePath = config.SQLitePath;

            // サービスの作成
            Listener listener = new Listener(config.HostName, config.Port);

            // サービスのスタート
            listener.Start();

            Console.WriteLine("行先ボードの通信処理サービスを開始しました。");
            Console.ReadLine();
        }

    }
}

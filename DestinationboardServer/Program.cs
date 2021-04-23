using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common;
using DestinationboardServer.Common.Config;
using DestinationboardServer.Common.DBManager.SQLite;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.IO;

namespace DestinationboardServer
{
    class Program
    {
        #region ロガー
        /// <summary>
        /// ロガー
        /// </summary>
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        /// <summary>
        /// メイン関数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _logger.Error("test");
            try
            {
                // Configの取得処理
                ConfigManager config = new ConfigManager();
                config.LoadConfig();

                // SQLite用のファイルパス
                SQLiteDataContext.SQLitePath = config.SQLitePath;
                // ログ出力
                Console.WriteLine(string.Format("SQLitePath={0}", config.SQLitePath));
                Console.WriteLine(string.Format("Port={0}", config.Port));

                if (!File.Exists(config.SQLitePath))
                {
                    Console.WriteLine(string.Format("SQLiteファイルが見つかりません。"));
                    Console.WriteLine(string.Format(@"Config\DestinationServer.confの設定を確認してください。"));
                    Console.ReadLine();
                    return;
                }

                // サービスの作成
                Listener listener = new Listener(config.HostName, config.Port);


                // サービスのスタート
                listener.Start();

                Console.WriteLine("行先ボードサービスを開始します。");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Console.WriteLine(e.Message);
            }
        }

    }
}

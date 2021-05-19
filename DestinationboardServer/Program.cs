using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common;
using DestinationboardServer.Common.Config;
using DestinationboardServer.Common.DBManager.SQLite;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DestinationboardServer
{
    public static class Program
    {
        #region ロガー
        /// <summary>
        /// ロガー
        /// </summary>
        public static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        /// <summary>
        /// メイン関数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                // カレントディレクトリのセット
                System.Environment.CurrentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

                // Configの取得処理
                ConfigManager config = new ConfigManager();
                config.LoadConfig();

                // SQLite用のファイルパス
                SQLiteDataContext.SQLitePath = config.SQLitePath;

                // SQLite用のログファイルパス
                SQLiteLogDataContext.SQLitePath = config.SQLiteLogPath;

                // ログ出力
                Console.WriteLine(string.Format("SQLitePath={0}", config.SQLitePath));
                Console.WriteLine(string.Format("SQLiteLogPath={0}", config.SQLiteLogPath));
                Console.WriteLine(string.Format("Port={0}", config.Port));

                // SQLiteファイルの存在確認
                if (!File.Exists(config.SQLitePath))
                {
                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine("データベースファイル(DestinationBoard.db)が見つかりません。");
                    msg.AppendLine(string.Format(@"{0}の設定を確認してください。", System.IO.Path.GetFullPath(config.ConfigPath)));
                    _logger.Error(msg.ToString());
                    Console.WriteLine(msg.ToString());
                    Console.ReadLine();
                    return;
                }

                // SQLiteファイルの存在確認(Log用)
                if (!File.Exists(config.SQLiteLogPath))
                {
                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine("データベースファイル(DestinationBoardLog.db)が見つかりません。");
                    msg.AppendLine(string.Format(@"{0}の設定を確認してください。", System.IO.Path.GetFullPath(config.ConfigPath)));
                    _logger.Error(msg.ToString());
                    Console.WriteLine(msg.ToString());
                    Console.ReadLine();
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

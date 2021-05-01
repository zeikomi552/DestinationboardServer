using DestinationboardServer.Common.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common.Config
{
	public class ConfigManager
	{
		/// <summary>
		/// コンフィグフォルダ
		/// </summary>
		const string _ConfigFolder = "Config";
		/// <summary>
		/// コンフィグファイル名
		/// </summary>
		const string _ConfigFileName = "DestinationServer.conf";

		/// <summary>
		/// コンフィグファイルパス
		/// </summary>
		string ConfigPath
		{
			get
			{
				return _ConfigFolder + @"\" + _ConfigFileName;
			}
		}


		#region ロガー
		/// <summary>
		/// ロガー
		/// </summary>
		protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		#endregion

		#region 接続先名[HostName]プロパティ
		/// <summary>
		/// 接続先名[HostName]プロパティ用変数
		/// </summary>
		string _HostName = "localhost";
		/// <summary>
		/// 接続先名[HostName]プロパティ
		/// </summary>
		public string HostName
		{
			get
			{
				return _HostName;
			}
			set
			{
				if (!_HostName.Equals(value))
				{
					_HostName = value;
				}
			}
		}
		#endregion
		#region ポート番号[Port]プロパティ
		/// <summary>
		/// ポート番号[Port]プロパティ用変数
		/// </summary>
		int _Port = 552;
		/// <summary>
		/// ポート番号[Port]プロパティ
		/// </summary>
		public int Port
		{
			get
			{
				return _Port;
			}
			set
			{
				if (!_Port.Equals(value))
				{
					_Port = value;
				}
			}
		}
		#endregion

		#region SQLiteのファイルパス[SQLitePath]プロパティ
		/// <summary>
		/// SQLiteのファイルパス[SQLitePath]プロパティ用変数
		/// </summary>
		string _SQLitePath = @".\db\DestinationBoard.db";
		/// <summary>
		/// SQLiteのファイルパス[SQLitePath]プロパティ
		/// </summary>
		public string SQLitePath
		{
			get
			{
				return _SQLitePath;
			}
			set
			{
				if (!_SQLitePath.Equals(value))
				{
					_SQLitePath = value;
				}
			}
		}
		#endregion

		#region ログ保存用パス[SQLiteLogPath]プロパティ
		/// <summary>
		/// ログ保存用パス[SQLiteLogPath]プロパティ用変数
		/// </summary>
		string _SQLiteLogPath = @".\db\DestinationBoardLog.db";
		/// <summary>
		/// ログ保存用パス[SQLiteLogPath]プロパティ
		/// </summary>
		public string SQLiteLogPath
		{
			get
			{
				return _SQLiteLogPath;
			}
			set
			{
				if (!_SQLiteLogPath.Equals(value))
				{
					_SQLiteLogPath = value;
				}
			}
		}
		#endregion

		#region コンフィグファイルの保存処理
		/// <summary>
		/// コンフィグファイルの保存処理
		/// </summary>
		public void SaveConfig()
		{
			try
			{
				// configファイルの出力
				XMLUtil.Seialize<ConfigManager>(ConfigPath, this);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				_logger.Error(e.Message);
			}
		}
        #endregion

        #region コンフィグファイルの存在確認と存在しない場合は初期値で作成
        /// <summary>
        /// コンフィグファイルの存在確認と存在しない場合は初期値で作成
        /// </summary>
        private void ConfigCheckCreate()
		{
			// Configフォルダパスの確認
			if (!Directory.Exists(_ConfigFolder))
			{
				Directory.CreateDirectory(_ConfigFolder);
			}

			// Configファイルの存在確認
			if (!File.Exists(ConfigPath))
			{
				// 初期値でコンフィグ情報を作成
				ConfigManager ini_conf = new ConfigManager();

				// configファイルの作成
				XMLUtil.Seialize<ConfigManager>(_ConfigFolder + @"\" + _ConfigFileName, ini_conf);
			}
		}
		#endregion

		#region 値のコピー
		/// <summary>
		/// 値のコピー
		/// </summary>
		/// <param name="conf">Configデータ</param>
		public void Copy(ConfigManager conf)
		{
			this.HostName = conf.HostName;	// ホスト名
			this.Port = conf.Port;          // ポート番号
			this.SQLitePath = conf.SQLitePath;	// SQLiteのファイルパス
		}
		#endregion

		#region コンフィグファイルのロード処理
		/// <summary>
		/// コンフィグファイルのロード処理
		/// </summary>
		public void LoadConfig()
        {
			try
			{
				// コンフィグフォルダの存在確認と読み込み
				ConfigCheckCreate();

				// configファイルの読み込み
				var conf = XMLUtil.Deserialize<ConfigManager>(ConfigPath);

				// 値のコピー
				this.Copy(conf);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				_logger.Error(e.Message);
			}
		}
		#endregion
	}
}

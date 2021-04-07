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
            Listener listener = new Listener();

            // サービスのスタート
            listener.Start();

            Console.ReadLine();
        }

    }
}

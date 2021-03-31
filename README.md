# DestinationboardServer
Destinationbord Server


必須パッケージ
・Microsoft.EntityFrameworkCore.Sqlite
・Microsoft.EntityFrameworkCore.Tools


以下の内容を修正してください
DestinationboardServer.Common.DBManager.SQLite.SQLiteDataContext.cs

```
        // 最初にココを変更する
        string db_file_path = @"C:\Work\develop\DestinationBoard.db";
```

パッケージマネージャーコンソールで以下を実行します

Enable-Migrations
Add-Migration DestinationBoard
Update-Database



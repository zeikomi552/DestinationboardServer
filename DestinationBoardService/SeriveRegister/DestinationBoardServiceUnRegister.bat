echo off
chcp 65001
cd /d %~dp0

echo on

sc stop DestinationBoardService
timeout /t 10 /nobreak >nul

sc delete DestinationBoardService

PAUSE
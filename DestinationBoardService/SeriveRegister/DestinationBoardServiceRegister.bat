echo off
chcp 65001
cd /d %~dp0
cd ..

set CUR_DIR=%CD%
set APP_NAME=\DestinationBoardService.exe

echo Regist Service For DestinationBoard
sc create DestinationBoardService binPath="%CUR_DIR%%APP_NAME%" start=auto


echo Wait For 10 second
timeout /t 10 /nobreak >nul

echo Start Serivce
sc start DestinationBoardService

PAUSE
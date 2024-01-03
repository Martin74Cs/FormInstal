del TeZak.exe
"c:\Program Files\7-Zip\7z.exe" a -t7z Tezak.7z c:\Users\Martin\OneDriveKopie\FormInstal\FormInstal\bin\Debug\net8.0-windows\*.*
copy /b 7zS2.sfx + config.txt + TeZak.7z TeZak.exe
del TeZak.7z
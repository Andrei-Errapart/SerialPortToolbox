del /q SerialToKeyboard.zip
vcbuild.exe SerialToKeyboard.sln
copy /b bin\Release\SerialToKeyboard.exe .
zip SerialToKeyboard.zip SerialToKeyboard.exe FixedQueue.cs FormMain.* Program.cs "Serial To Keyboard.pdf" SerialPortHelper.cs SerialToKeyboard.csproj SerialToKeyboard.sln Properties\*.*

del /q SerialToKeyboard.exe

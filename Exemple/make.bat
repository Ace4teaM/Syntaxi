..\ConsoleApp\app\bin\Debug\app.exe -a init -p ".\NoyauPortable.prj" -t "NoyauPortable" -v "1.0"
..\ConsoleApp\app\bin\Debug\app.exe -a add_cpp_syntax -p ".\NoyauPortable.prj"
..\ConsoleApp\app\bin\Debug\app.exe -a add -p ".\NoyauPortable.prj" -i ".\src" -f "*" -r -g "cpp"
..\ConsoleApp\app\bin\Debug\app.exe -a scan -p ".\NoyauPortable.prj"
pause
..\App\ConsoleApp\app\bin\Debug\app.exe -a init -p ".\NoyauPortable.prj" -t "NoyauPortable" -v "1.0"
..\App\ConsoleApp\app\bin\Debug\app.exe -a add_cpp_syntax -p ".\NoyauPortable.prj"
..\App\ConsoleApp\app\bin\Debug\app.exe -a add -p ".\NoyauPortable.prj" -i ".\src" -f "*" -r -g "cpp"
..\App\ConsoleApp\app\bin\Debug\app.exe -a scan -p ".\NoyauPortable.prj"
pause
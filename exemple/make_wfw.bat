# Initialise la librairie
syntaxi.exe -o "wfw.xml" -a "init" -t "Webframework" -v "1.8.1" -r

# Ajoute les sources de la librairie de base
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework\sql" -f "*.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework\wfw\php" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework\wfw\ctrl" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework\wfw\php\templates" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

# Ajoute les sources du module "User"
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-User-Module\sql\postgresql" -f "func.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-User-Module\wfw-1.8\lib\user" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-User-Module\wfw-1.8\ctrl\user" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

# Ajoute les sources du module "IO"
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\sql\postgresql" -f "func.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\wfw-1.8\lib\io" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\wfw-1.8\ctrl\io" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

pause
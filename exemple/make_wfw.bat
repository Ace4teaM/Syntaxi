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

# Ajoute les sources du module "Writer"
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Writer-Module\sql\postgresql" -f "func.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Writer-Module\wfw-1.8\lib\writer" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Writer-Module\wfw-1.8\ctrl\writer" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

# Ajoute les sources du module "Mail"
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Mail-Module\sql\postgresql" -f "func.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Mail-Module\wfw-1.8\lib\mail" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Mail-Module\wfw-1.8\ctrl\mail" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

# Ajoute les sources du module "Catalog"
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Catalog-Module\sql\postgresql" -f "func.sql" -d ".\sql" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Catalog-Module\wfw-1.8\lib\catalog" -f "*.php" -d ".\php" -t "Webframework" -v "1.8.1" -r
syntaxi.exe -o "wfw.xml" -a "add" -i "C:\Users\developpement\Documents\GitHub\Webframework-Catalog-Module\wfw-1.8\ctrl\catalog" -f "*.php" -d ".\wfw_objects" -t "Webframework" -v "1.8.1" -r

pause
# Syntaxi
Extracteur de documentation bas�e sur les expressions r�guli�res


## Arguments

 - a : Action a entreprendre
 - p : Fichier du projet en entr�e
 - i : Dossier a scanner en entr�e
 - o : Dossier de sortie
 - f : Filtre de s�lection en entr�e
 - d : Dossier des d�finitions d'objets
 - v : Version du projet cible
 - t : Titre du projet
 - r : Recherche r�cursive ?


## Commandes

Initialise un nouveau projet
	-a init -p ".\NoyauPortable.prj" -t "Noyau Portable" -v "1.0"

Ajoute des syntaxes pr�d�finit pour le langage C++
	-a add_cpp_syntax -p ".\NoyauPortable.prj"

Ajoute un dossier de scan
	a add -p ".\NoyauPortable.prj" -i ".\src" -f "*" -r

Scan le contenu du projet
	-a scan -p ".\NoyauPortable.prj"

Affiche les objets scann�es
	-a print -p ".\NoyauPortable.prj"



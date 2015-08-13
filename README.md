# Syntaxi
Extracteur de documentation basée sur les expressions régulières


## Arguments

 - a : Action a entreprendre
 - p : Fichier du projet en entrée
 - i : Dossier a scanner en entrée
 - o : Dossier de sortie
 - f : Filtre de sélection en entrée
 - d : Dossier des définitions d'objets
 - v : Version du projet cible
 - t : Titre du projet
 - r : Recherche récursive ?


## Commandes

Initialise un nouveau projet
	-a init -p ".\NoyauPortable.prj" -t "Noyau Portable" -v "1.0"

Ajoute des syntaxes prédéfinit pour le langage C++
	-a add_cpp_syntax -p ".\NoyauPortable.prj"

Ajoute un dossier de scan
	a add -p ".\NoyauPortable.prj" -i ".\src" -f "*" -r

Scan le contenu du projet
	-a scan -p ".\NoyauPortable.prj"

Affiche les objets scannées
	-a print -p ".\NoyauPortable.prj"



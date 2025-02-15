
# Livraison-Trans-Connect

Projet de 3 eme annèe. Programation orienté objet en C#

Pour ce projet nous avons créer une base de données initiale pour pouvoir faire des manipulations directement sans avoir a créer un client. Cette base de données est inscrite en dur au début du programme principal parceque la façon dont on a crée notre arbre n-aire ne nous permet pas de les glisser dans un fichier csv. Il aurait fallu reprendre tout le projet car on s’est rendu compte trop tard de notre erreur.

On a utilisé des fichier JSON pour sauvegarder et charger à chaque utilisation les salariés, clients, véhicules et chauffeur. Le seul bémol est encore une fois la façon dont on a crée notre organigramme ne permet pas d’afficher les salariés qu’on aurait éventuellement rajoutés bien qu’ils soient disponibles dans la liste extraite du fichier JSON. On a eu l’idée de connecter notre projet à un système STMP pour pouvoir envoyer par mail au client les facture lorsqu’il passe commande. Pour se faire lorsque vous creez votre client vous devez rentrer votre vraie adresse mail. Vous recevrez en pièce jointe une facture qui n’est pas encore très jolie et mise en forme.

Pour pouvoir passer des commandes on va avoir besoin d’accéder à la distance pour pouvoir fixer un prix et livrer logiquement. Dans notre projet on a eu l’idée de faire 2 méthode distinctes, la première utilise une API Google map on peut donc écrire directement les vrais adresse comme si on était sur google map et cela va prendre en compte le temps de trajet routier avec les sens interdits et travaux. Le problème de cette méthod est qu’on doit lancer l’application avec une connection à internet. Pour contourner le problème et pour répondre à la question on a créer une deuxième méthode utilisant l’algorithme de Dijkstra en ouvrant un fichier csv présent en local.

Quelles sont nos innovations :

La méthode pour récupérer la distance avec l’API google map. La sauvegarde des données d’une utilisation à l’autre du projet en JSON L’envoi par mail d’un pdf récapitulatif au client lorsqu’il a passé commande Des classes enum au lieu de créer des classes pour chaque type de véhicule ou chaque type de marchandise pour gagner en espace mémoire et en optimisation



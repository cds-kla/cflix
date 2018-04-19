# Hackonymousoflix

Ce conteneur correspond au serveur de l'attaquant dans le scénario global.

Il regroupe les épreuves 9, 10 et 11 :

- La contre-attaque (Shingeki no Kyojin) :  Exploitation de la désérialization JavaScript par NodeJS
- La vérité éclate (House of Cards) : Exploitation d'une jail JavaScript
- C étrange tout cela (Stranger Things) : Exploitation d'un binaire codé en C

NB : Le serveur web derrière l'épreuve 9 peut être aussi utilisé par les épreuves 1 et 2 (pour les failles XSS). Cependant pour que ces deux épreuves fonctionnent correctement il faut l'exposer en HTTPS (chrome bloque les requêtes xhr non sécurisées). Pour cela il faudrat passer par le reverse-proxy !

## Détails des épreuves

### Epreuve 9 : La contre-attaque (Shingeki no Kyojin)

Le but de cette épreuve est d'exploiter la vieille route `https://{hackonymousoflix}:1337/cookie` encore présente dans le code du serveur afin de récupérer les credentials pour accèder à l'épreuve suivante.

> Vous vous souvenez de son XSS sur Better Call Saul et Game of Thrones ? Eh bien, on pense avoir trouvé une faille dessus !
>
> En effet, il semblerait qu'une v1 de son service soit encore dispo et celle-ci serait exploitable. Pour cela, il faut envoyer un JSON en POST à l'addresse `https://{hackonymousoflix}:1337/cookie`
>
> Ah ! de ce que nous savons, son service tournerait en nodejs et il semblerait que ça aurait avoir avec de la désérialization... Bon courage !

NB : La route `https://{hackonymousoflix}:1337/cooooookie` est utilisée par les épreuves 1 et 2 (pour la réalisation de failles XSS) 


### Epreuve 10 : La vérité éclate (House of Cards)

Pour accéder à cette épreuve il faut donc utiliser les informations trouvées lors de l'épreuve précédente. Donc il faut se connecter en ssh sur le port 845.

> Nous avons eu besoin d'un peu de temps pour nous remettre de nos émotions mais ça devrait aller maintenant. Il va falloir réussir à sortir de cette vm JS maintenant au boulot sire !

Une fois connecté, on constate que l'on n'obtient pas un shell bash classique mais que l'on est dans un environnement bien particulier. Il faudra donc trouver comment en sortir afin d'aller lire le code source de ce shell (`vm.js`) et ainsi trouver le mot de passe permettant d'obtenir la dernière commande nous donnant accès à un vrai bash.

### Epreuve 11 : C étrange tout cela (Stranger Things)

Maintenant que nous avons récupéré un shell bash, il va nous rester une dernière petite chose à réaliser : aller lire le fichier `MyLittleToDoList.txt` présent dans le dossier du user `will.byers`.

> Bon allez rattrapez-vous et faites le nécessaire pour usurper will et vous aurez accès à cette to-do list. Quelque chose me dit que nous ne sommes plus très loin de la fin !

Pour cela, il faut exploiter le binaire ... `exploit` car celui-ci possède les droits nécessaires à la lecture du fichier.

## Build & Run

Il est possible de faire tourner ce conteneur en stand-alone en exécutant les commandes suivantes :

```bash
docker build -t hackonymousoflix .
docker run --rm -p 1337:1337 -p 845:845 -d hackonymousoflix
```
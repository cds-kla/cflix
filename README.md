# CFlix : Challenge de sécurité SI 2017

CFlix est un projet réalisé début 2017 à Cdiscount afin de sensibiliser les développeurs de la DSI aux problématiques de sécurité informatique. Pour cela, un concours de 3 semaines a été mis en place, le but pour les participants étant d'être le premier à résoudre l'ensemble des épreuves.

Les épreuves proposées ont quant à elles pour but de faire comprendre aux participants comment un attaquant s'y prend pour exploiter les failles. Ainsi chacune des épreuves expose un point spécifique du [top ten OWASP](https://www.owasp.org/index.php/Top_10_2013-Top_10) - fichier de référence des failles de sécurité les plus couramment rencontrés sur internet.

---

Ce repository contient donc toutes les sources qui ont servis à la réalisation de ce challenge sécurité. Il est découpé en deux parties.

Dans la partie [docs](docs) vous trouverez l'explication des épreuves, ainsi que leur résolution. Mais aussi certains des documents utilisés pour réaliser la campagne de communication.

Dans la partie [src](src) vous trouverez les sources des différents projets composant l'ensemble des épreuves.

## Explication du scénario

Le projet initial est celui de CFlix, il s'agit d'un site de streaming qui vient juste de s'ouvrir et qui propose à ces utilisateurs d'accéder aux dernières saisons de leurs séries favorites.

![JustDecompile Ressources](docs/images/home_example.png)

Cependant, un hacker de pseudo Hackonymousoflix (Vilain Petit Canard) a réussi à trouver des failles sur le site et s'est amusé à les exploiter...

Les participants doivent donc résoudre les différentes épreuves disponibles pour comprendre comment il s'y est pris pour prendre le contrôle de la plateforme.
Puis dans un second temps, les participants doivent s'attaquer à l'attaquant et donc exploiter les serveurs d'Hackonymousoflix.

Le rythme des épreuves était prévu pour se dérouler sur 3 semaines avec quatre nouvelles épreuves disponibles chaque semaine.

Des épreuves bonus sont également disponibles et sont au nombre de cinq.

NB : A chaque série disponible sur CFlix correspond une épreuve principale.

## Architecture et Déploiement du projet

Le projet se découpe principalement en 3 projets : CFlix, Hackonymousoflix et Hackonymousoflix.RobotShop.

Le premier a été réalisé en ASP .NET Core 1.1 (puis migré en 2.0) afin de démontrer comment une faille peut être inséré dans une application développé en C#.

Le second s'axe sur le développement en NodeJS et de l'exploitation système (exploit de binaire)

Le troisième propose une dernière épreuve en PHP.

Quoiqu'il en soit, tous ces différents projets ont été packagé en container et un [docker-compose](src/docker-compose.yml) est présent pour vous expliquer les interactions et l'organisation mise en place entre eux.

Pour pouvoir lancer le projet dans son intégralité, il suffit d'effectuer les manipulations décrites dans la doc de [CFlix](src/README.md).

## Ressources externes utilisées

Pour la réalisation de ce projet, les images utilisées pour les séries proviennent du site [The Movie DB](https://www.themoviedb.org/)

Les icônes utilisées pour les Easter Eggs proviennent du site [FlatIcon](http://www.flaticon.com/) sauf pour l'Easter Egg `La contre-attaque` qui provient de la série l'attaque des titans (Shingeki No Kyojin) qui a été refaite au format vectoriel

## Essayer par vous-même

Pour le tester localement, vous pouvez exécuter les commandes suivantes :

```bash
cd src
docker-compose build
docker-compose up -d
docker-compose exec cflix dotnet CFlix.dll database update all
```

Puis il faut naviguer sur `https://localhost/`

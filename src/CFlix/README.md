# CFlix

CFlix was originally an intern contest into Cdiscount IT. His main goal was to make developer aware of common vulnerability encountered everyday on internet. So to place a picture into it, CFlix have been developed like an netflix-like website but some flaws have been exploited by a malicious hacker (**hackonymousoflix**) In order to won the tournament, contestants need to resolve all the different challenges avalaible.

## Try CFlix by your own

In order to assure the security of the different availables challenges, CFlix is a docker-based project and have the following architecture :

- core projects :
  - CFlix -> the main website
  - CFlix.ImageViewer -> a website dedicated to a traversal directory challenge
  - PostgreSQL -> the main database (contains contestants informations)
  - MySQL -> main database of CFlix website-data used for the different challenge (no critical user information is available into it)
- support projects :
  - Nginx -> used as a reverse-proxy to expose CFlix and CFlix.ImageViewer as https
  - Redis -> used only to persist user session on server (it allow to restart all others containers)

- last challenges :
  - hackonymousoflix -> container with the last four challenges
  - cds.hackdumb.com -> the ultimate challenge available at <https://cds.hackdumb.com>

If you want to use CFlix into your organization, you can use an ActiveDirectory (or maybe any LDAP). See [configuration sections](#configuration) to understand how to do it.

### Configuration

You can tweak some settings by modifying the `docker-compose.yml` or eventually `appsettings.json`

1. **LDAP** : To use Active Directory or LDAP specify true to `CFLIX__USELDAP` env variable and set `ConnectionStrings__LdapUrl` with your ldap url. To check if your user can login into cflix, we try submitted credentials in the ldap (so you don't need to specify any system credentials)

2. **Redis** : if you want to disable Redis (because you want to debug locally CFlix project for example) add `CFLIX__USEREDIS=false` to `docker-compose.yml` file

3. **Stage** : cflix have 3 stages (each stage passed unlocked some challenges)

### Build and launch

After tweaking the configuration as you wanted, type the following command :

```bash
docker-compose -f docker-compose.ci.build.yml run --rm ci-build
docker-compose build
docker-compose up -d
docker exec -it cflix_cflix_1 dotnet CFlix.dll database update all
```

Then go to <https://localhost>

## Développement

### Migrations

To migrate the main database which contains authentication (Postgre database) :

```bash
dotnet ef migrations add "01-init" --context CFlixAuthContext
# remplacer le 0 par le MigrationId du précédent script généré
dotnet ef migrations script 0 --context CFlixAuthContext -o "..\Postgres\SQL_Scripts\01-init.sql"
dotnet ef database update
```

### Build

To build and debug locally cflix project, you will need to access the database. So to open the database ports type :

```bash
docker-compose -f docker-compose.opendbport.yml -f docker-compose.yml up -d
```

NB : if some dependency are marked as missing, try `bower install` before building containers

## Update database

To update the database with migration and feed it with datas, you can use the following commands :

```bash
dotnet cflix.dll database update all
# if you want to update them independently :
dotnet cflix.dll database update mysql
dotnet cflix.dll database update postgres
```

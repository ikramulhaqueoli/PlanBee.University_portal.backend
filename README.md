# PlanBee.University_portal.backend

## Database:
  * Database type: SQL
  * Database vendor: Microsoft SQL Server
  * Database Docker run: (MAC)
* Database Credentials:
  * name: `planbee-db`
  * Password: `1qaz!QAZ`
* Download docker image:
  ```
  sudo docker pull mcr.microsoft.com/mssql/server:2019-latest
  ```
* Docker Run:
  ```
  docker run -d --name planbee-db -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1qaz!QAZ' -p 1444:1444 mcr.microsoft.com/mssql/server:2019-latest
  ```
  
# PlanBee.University_portal.backend

## Database:
  * Database type: SQL
  * Database vendor: Microsoft SQL Server
  * Database Docker run: (MAC)
* Database Credentials:
  * username: `sa`
  * Password: `1qaz!QAZ`
  * Database Name: `planbee-university-db`
* Download docker image:
  ```
  sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
  ```
* Docker Run:
  ```
  docker run -d --name planbee-db-docker -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1qaz!QAZ' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
  ```
  
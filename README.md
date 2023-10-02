# PlanBee.University_portal.backend

## Database:
  * Database type: SQL
  * Database vendor: Microsoft SQL Server
  * Database Docker run: (MAC)
* Database Credentials:
  * username: `sa`
  * Password: `1qaz!QAZ`
  * Database Name: `planbee-university-db`
  * Port: `1433`
* Build Project Docker Image:
  * Open terminal in Project Solution Root folder (i.e. PlanBee.University_portal.backend)
  * Now, run the following command to build the docker image.
  ```console
  docker build -t softbee/university-backend:1 -f Dockerfile .
  ```
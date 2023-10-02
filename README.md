# PlanBee.University_portal.backend

## Build Project Docker Image:
* Open terminal in Project Solution Root folder (i.e. PlanBee.University_portal.backend)
* Now, run the following command to build the docker image.
```console
	docker build -t softbee/university-backend:latest -f Dockerfile .
```
## Run Project in Docker with Dependencies:
* Dependency 1: MongoDB
```console
	docker run -d --name mongodb-container -p 10120:27017 mongo
```
## Run this Backend Application in Docker:
```console
	docker run -d --name webapi-container --link mongodb-container -p 10110:80 softbee/university-backend:latest
```
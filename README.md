# PlanBee.University_portal.backend

## Build Project Docker Image:
* Open terminal in Project Solution Root folder (i.e. PlanBee.University_portal.backend)
* Now, run the following command to build the docker image.
```console
docker build -t ikramulhaqueoli/university-backend:latest -f Dockerfile .
```
## Setting up server and Run Project (Considering docker isn't installed in server):
* Dependency 1: MongoDB
```console
# 1. Add Docker official GPG key:
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install ca-certificates curl gnupg
sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
sudo chmod a+r /etc/apt/keyrings/docker.gpg

# 2. Add the repository to Apt sources:
echo \
  "deb [arch="$(dpkg --print-architecture)" signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  "$(. /etc/os-release && echo "$VERSION_CODENAME")" stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update

# 3. Install Docker Engine:
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

# 3. Enable docker to start on OS Startup:
sudo systemctl enable docker

# 3. Download and run mongo-db in mongo-container (docker)
sudo docker rm -f mongo-container && sudo docker run -d --name mongo-container --restart always -p 10120:27017 --pull always mongo

# 4. Download and run softbee-university-backend in softbee-container (docker)
sudo docker rm -f softbee-container && sudo docker run -d --name softbee-container --link mongo-container --restart always -p 10110:80 -p 10111:443 --pull always ikramulhaqueoli/university-backend:latest
```
## Ping backend healthcheck endpoint:
```curl
http://<server_public_ip>:10110/healthcheck
```
Example:
```curl
http://20.135.12.12:10110/healthcheck
```
## Some useful commands for docker engine:
```console
# Start Docker Daemon
sudo systemctl start docker
# Check Docker Run Status
sudo systemctl status docker
```
## Port Information:
```
MongoDB Port: 10120
University Backend Port: 10110(http), 10111(https)
```
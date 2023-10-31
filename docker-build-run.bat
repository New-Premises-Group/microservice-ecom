@echo off
REM Stop and remove any existing containers and volumes (optional)
docker-compose down

REM Build the Docker Compose project
docker-compose build --pull

REM Run the Docker Compose project in detached mode (background)
docker-compose up -d

REM Display the running containers
docker-compose ps

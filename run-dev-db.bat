@echo off
REM Start PostgreSQL Docker container on Windows

REM Define variables
set CONTAINER_NAME=my-postgres-container
set USER_ID=dev
set PASSWORD=dev
set DATABASE=devdb

REM Remove existing container if exists
docker rm -f %CONTAINER_NAME% > nul 2>&1

REM Start PostgreSQL container
docker run --name %CONTAINER_NAME% -e POSTGRES_USER=%USER_ID% -e POSTGRES_PASSWORD=%PASSWORD% -e POSTGRES_DB=%DATABASE% -p 5432:5432 -d postgres

REM Display container status
docker ps -a

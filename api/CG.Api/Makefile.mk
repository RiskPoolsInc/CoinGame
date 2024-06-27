tag=stas-0.0.1

build:
	docker-compose -f docker-compose.yml  build

start:
	docker-compose -f docker-compose.yml  up

local:
	docker-compose -f docker-compose.local.yml -f docker-compose.local.override.yml up

localsu:
	docker-compose -f docker-compose.local.su.yml -f docker-compose.dev.su.override.yml up

cleanup:
	dotnet clean

stop:
	docker container stop $(docker container ls -q --filter name=sbn-services)

push:
	docker push dockerhub.ubikiri.com/growf-webapp:latest

push-explicit:
 	docker push dockerhub.ubikiri.com/growf-webapp:latest

tag:
 	docker tag growf-webapp:latest dockerhub.ubikiri.com/growf-webapp:latest

tag-explicit:
 	docker tag growf-webapp:latest dockerhub.ubikiri.com/growf-webapp:latest

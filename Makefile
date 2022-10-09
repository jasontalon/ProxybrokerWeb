docker/dev/build:
	docker build --tag proxybrokerweb --file dev.Dockerfile .
docker/dev/run/it:
	docker run --volume $(PWD):/app -p 8080:8080 -it --rm proxybrokerweb
docker/dev/run:
	docker run --volume $(PWD):/app -p 8080:8080 --rm -d proxybrokerweb
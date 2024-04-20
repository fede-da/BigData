# Docker

Docker-compose contains only informations about **RabbitMq Message Broekr** container. It is used to run **RabbitMq** locally.

To start **RabbitMq** container run:

```
start-rabbitmq.sh
```

To stop **RabbitMq** container run:

```
stop-rabbitmq.sh
```

Otherwise, if you want to pull its image from Docker Hub and run it, execute the following commands:

```
 docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
```



# Architecture 

Read this in other languages: [English](architecture.md), [Russian/Русский](architecture.ru.md).

## Simple client-server approach

![ClientServerApproach](img/ClientServerApproach.png)

The main disadvantage of this approach is that the server could not process too many requests at the same time (approximately, 10-500 requests per second). 
Since we need to process 20,000 requests per second, we have to deploy 40 to 2,000 instances of the application. 

## Message queue architecture

![MessageQueueArchitecture](img/MessageQueueArchitecture.png)

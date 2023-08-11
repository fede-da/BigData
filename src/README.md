# Project overview

This folder contains all the source code to run the project, please remember to install RabbitMQ as mentioned [here](../docs/README.md)

Main actors are **FrontEndServer** and **BackEnd**, the first one interacts with the client and sends data to the backend through **RabbitMQ** while the 2nd one receives data, interacts with AI and then sends the response back.

Inside **FrontEndServer**, there are 3 more projects (Blazor Web assembly application):

1. FrontEndServer.Client: Handles the client side of the application, such as UI and user input.

2. FrontEndServer.Server: Handles the server side of the application (Business logic), such as the connection to the database, communication with the client, services to dialog with RabbitMq...

3. FrontEndServer.Shared: Contains the shared code between the client and the server, such as the models.

Each project has its own README.md file with more information.


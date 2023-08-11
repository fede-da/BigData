# General

In teoria il DI Container è stato configurato correttamente così come i servizi "rabbit" e "config" annessi.

Bisogna mandare tutto in esecuzione e verificare che un messaggio inviato da Assp.Net core venga effettivamente letto dal programma python. Successivamente bisogna fare in modo che anche asp.net core possa leggere dalla coda su cui scrive python.
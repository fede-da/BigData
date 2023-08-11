## ChatBot overview

Questo componente deve mostrare a schermo la finestra del **ChatBot** gestendo invio e ricezione dei messaggi.

Per aggiornare la grafica in tempo reale si può utilizzare il servizio di **SignalR** relativamente alla ricezione delle risposte dal server, mentre per gli altri aggiornamenti anche l'utilizzo di un Design Pattern di tipo Observer può rispondere concretamente alle esigenze.

Un caso d'uso tipico è il seguente:

1. L'utente scrive un messaggio e lo invia al server (ad esempio: *come faccio a pagare?*)
	
2. Il server risponde con un messaggio (ad esempio: *puoi pagare con carta di credito o con bonifico nel seguente modo...*)

3. Il client riceve il messaggio e lo mostra a schermo

4. Se il cliente è soddisfatto termina l'interazione, altrimenti il ciclo ricomincia da capo

Per quanto riguarda l'inizio e la chiusura dell'interazione possiamo supporre che il componente sia un pallino che quando viene cliccato si espande e riduce.

***

## Sviluppo

Nonostante vorrei sviluppare seguendo la TDD penso che sia una strada troppo complessa per una sola persona quindi preferisco optare per la DDD supportata dal CQRS. 

La CQRS potrebbe essere particolarmente utile in quanto tutte le operazioni che il componente esegue possono essere interpretate come comandi:

- apertura della chat

- invio di un messaggio

- chiusura della chat

Non mi è molto chiara la parte di Queries, in quanto non capisco cosa potrebbe essere una query in questo contesto dato che le risposte verranno gestite dal servizio SignalR.

Altri pattern che potrebbero essere utili sono:

- Observer per la gestione degli aggiornamenti della grafica

- Mediator per la gestione delle interazioni tra i vari componenti

Pensavo di mettere tutto il codice per la gestione del componente all'interno della stessa cartella, forse sarebbe meglio scrivere una libreria ma non essendo sicuro preferisco posticipare la scelta ed in caso muovere il codice in una libreria in un 2o momento.

***

## Design

Graficamente il componente rispecchia le immagini proposte nel PDF del progetto al seguente [link](https://progesoftware.sharepoint.com/:p:/r/sites/Risponditore_Automatico/Documenti%20condivisi/General/Risponditore.pptx?d=wdc8df45feae143e58a100c544be039c1&csf=1&web=1&e=nO7Bde)

Piccole variazioni permettendo, la linea di design è grosso modo già segnata.

La domanda è: come strutturo tutto? Probabilmente meglio iniziare con una finestra composta da window + textfield con annesso pulsante. Simulare la scrittura e ricezione dei messaggi, poi il resto si vedrà.



( •_•)

( •_•)>⌐■-■

(⌐■_■)




 (¬‿¬)ﾉ  
 
 ヽ(⌐■_■)ノ♪♬


 (＾◡＾)ﾉ

















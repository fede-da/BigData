# MessageDto exchanged by Rabbit

*MessageDTO* structure must reflect the one on Asp .Net server.

Until now it must reflect the following:

```
string Guid 
string Message
```

Before sending them, these data had been wrapped using `JsonSerializer` on _Asp .Net core server_ so now a **deserialization** is required.
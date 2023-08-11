# Test

**RabbitmqServiceTest** contains all the test cases for the service.

Let's have a look at its classes:

*. **RabbitmqServiceTest** is the main class that contains all the test cases:
    
    1. **when_a_single_message_is_sent_then_the_same_message_is_received** this test case sends a message to the queue and then it waits for the message to be received. Once the message is received, it checks that the message received is the same as the message sent.


Next test cases:

*. When a wrong queue name is used, then an exception is thrown.


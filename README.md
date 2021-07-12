
# RockContentCodingChallenge

RockContentCodingChallenge is a Coding Challenge required by Rock Content.

## Before you run

I used docker to run RabbitMQ

```bash
docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

## Used Technologies

* Mass Transit - To abstract RabbitMQ and make it easier to use;
* RabbitMQ;
* AutoMapper;
* MongoDB


* React
* Axios

## Bonus challenge

* How can you improve the feature to make it more resilient against abuse/exploitation
We could use some different approaches like: 
  * Using authentication, so the user would have to be logged in to use the like button, preventing anonymous requisitions.
  * Correctly configure CORS to receive only requisitions from the application.
  * Limit the amount of clicks in a time.
  
* How can you improve the feature to make it scale to millions of users and perform without issues?
We could separate the api in a readonly api and another to write data (use the like button);
Use Cache for faster fetching requisitions;

* A million concurrent users clicking the button at the same time
For this test, I have used RabbitMQ with Mass Transit and limited the Consumer by 1 item at a time. The Producer is working fine with fast response for the user while the Consumer is slower while writing the updated data

* A million concurrent users requesting the article's like count at the same time
I used JMeter for a load test and had 4~7 seconds on requests. Like I said before, we could segregate the apis. Mongo helped on the fast response.

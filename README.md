# server
=======

#### Docker Dependencies

- `docker run --name mongo -v ~/data/mongo:/data/db -p 27017:27017 -d mongo`
- `docker run --name eventstore -it -p 2113:2113 -p 1113:1113 -d eventstore/eventstore`
- `docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management`

####  Web Browsers

- EventStore @ http://localhost:2113/ admin changeit
- RabbitMQ @ http://localhost:15672/ guest guest
- POSTMAN

#### Initial Tests

##### Create User

```
POST localhost:5000/users
{
  "FirstName" : "Matt",
  "LastName": "Ginty",
  "Email": "matt@xyz.com",
  "Password": "pass"
}
```

##### Update Email

```
POST localhost:5000/users/user-1234235235 (<-- an id that u find in eventstore)
{
  "Email": "matt@abc.com"
}
```

##### Results

- EventStore should show 2 Events for the User
- MongoDB should have a User collection with a single record (email of matt@abc.com, version of 2)

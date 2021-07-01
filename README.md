# Game Of Life With React Hooks
View a [demo here](https://www.youtube.com/watch?v=sOicMZzA3E8 "Ssuag").

## About
Suagg is a cloud based web service for uploading and viewing memes.  The architecture of this project consists of an MVC Application, Comment Microservice and Projection Manager.

### MVC Application

HTML pages are rendered server side using an MVC architectural pattern.  The MVC application is responsible for Post and User data which is stored in an SQL database.  Entity framework core was used for all database access with Identity Framework Core managing the user information.  Data relating to a post's comments is either commanded or queried through the Comment Microservice.  This is done through REST api calls to the microservice.

### Comment Microservice

Comments are implemented via a microservice with CQRS and Event Sourcing.  All data transmission is done in JSON format. All commands generate events which are then stored into Event Store.  Comment Data is then be queried through RavenDB.

### Projection Manager

The Projection Manager subscribes to the events that occur in the Comment Microservice.  Events are pulled out of Event Stores event stream and asynchronously projected into RavenDB.

### Overall Architecture

![Architecture](https://user-images.githubusercontent.com/43660365/79053727-a1845200-7bfc-11ea-8f20-6594489f537c.jpg)

## Getting Started
### Prerequisites
Install Event Store: https://eventstore.com/ and RavenDB: https://ravendb.net/

### Installation
Clone the repo:
```bash
git clone https://github.com/BradenThomp/Ssuag.git
```

## Usage
Run Microservice, Projection Manager, and MVC through visual studio.

## Resources
N/A

## Contributors
Braden Thompson
Nathan Sehn
Quinn Ceplis
Eddy Gu
Hunter Reboul
Ethan Schafers

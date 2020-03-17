# Ssuag

Suagg is a cloud based web service for uploading and viewing memes.  HTML pages are rendered server side using an MVC architectural
pattern.  Comments are implemented via a microservice with CQRS and Event Sourcing.  Calls between the main MVC application and 
microservice are made using REST calls.  User data is stored in an SQL database using entitity framework core.  All comment events 
are saved to event store. A projection manager listens for new events which asyncronously updates the projections stored in 
RavenDB.  All comment data is queried through the microservice and RavenDB.

## Installation

Download the repository through visual studio code.
Run Update-Database in nuget console
Set up Event Store: https://eventstore.com/
Set up RavenDB: https://ravendb.net/
Run Microservice, Projection Manager, and MVC 

## Contributors
Braden Thompson
Nathan Sehn
Quinn Ceplis
Eddy Gu
Hunter Reboul
Ethan Schafers

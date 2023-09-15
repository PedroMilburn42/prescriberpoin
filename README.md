# Prescriberpoint Web Api 

Prescriberpoint Web Api is a WebSite implemented .NET Framework 4.7.2. It receives a Rest HTTP Requests to handle CRUD for users. It can be tested with PostMan adding a secret key for authentication in the headers.
It uses a local SQL Database to create, update, get and delete users. A specific web application has not been implemented and it has to be tested with Postman, Fiddler or any other tool for creating HttpRequests.
3 different test projects have been added with user cases to test the Presentation (controller) layer, the business layer and the data layer.

These are the user stories

1.As a user I can login to the system and create another user.

2.As a user I can login to the system and modify another users name.

3.As a user I can login to the system and get another user's data.

4.As a user I can login to the system and delete other user's user.

5.A user that is not in the database should not be able to login.

6.An admin user can notbe deleted, updated and added with the web api.

## Installation

Install VS 2019 or a later version and click on the "sln" file on the root folder. Execute the project. Another alternative is to add the code to IIS and map a virtual directory.

To run the tests, click on the Test menu VS 2019. It has a database connection string pointing to #

#### |DataDirectory|\Prescriberpoint.mdf
The database should initially be empty when added to Github or downloaded in order to not fill to much disk. The unit tests adds data and finally deletes everything, leaving an empty database. There are 2 different databases, one for the Web Api and another for the tests.

## Database 

### Create table
CREATE TABLE [dbo].[users] (
    [UserId]    INT          IDENTITY (1, 1) NOT NULL,
    [Firstname] VARCHAR (50) NOT NULL,
    [Lastname]  VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (20) NOT NULL,
    [IsAdmin] TINYINT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

### Data handling, alter database, delete records
To handle the database data, I have used the menu Server Explorer in VS

## Authentication
For a authentication I have a created a secret key that is hardcoded. This key is validated for all the request and has been added for testing purposes. It is a good idea move that key to a more secure storage and encrypt/decrypt it. The secret key can easily be removed to add other authentication methods like OAuth.

## Author

####  Name: Pedro Milburn
####  Email: pierre.milburn@hotmail.com

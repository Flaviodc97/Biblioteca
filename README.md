Biblioteca 
Requisiti Tecnici
1.	Stack Tecnologico
Framework: .NET 8.0 Web API
Database: SQL Server con Entity Framework Core
Autenticazione: JWT Bearer Token, Identity
Documentazione: Swagger/OpenAPI
Testing: xUnit
Logging: Serilog
Caching: Redis (opzionale)
2.	Architettura
Pattern: Repository + Unit of Work
Struttura: Clean Architecture (Domain, Application, Infrastructure, API)
Dependency Injection: Built-in .NET DI Container
Validazione: FluentValidation
Mapping: AutoMapper

3.	Modello Dati - Entity:
	- User
    User(ID, Name, Surname, DateOfBirth, PhoneNumber, PostalCode, Address, City, MembershipStartDate, MembershipEndDate, MaxLoansAllowed, MembershipType)

  - Notification
  Notification(ID, Message, UserID [FK → User])

  - Book
  Book(ID, Title, Subtitle, ISBN, Pages, Description, Language, Summary, PublisherID [FK → Publisher])

  - Author
  Author(ID, Name, LastName, DateOfBirth, DateOfDeath, Biography, Nationality)

  - Publisher
  Publisher(ID, Name, YearOfPublication)

  - Category
  Category(ID, Name, Description, Code, Website, Email)

  - Review (N:N tra User e Book)
  Review(ID, Title, Valuation, Content, Photo, UserID [FK → User], BookID [FK → Book])
  
  - Loan (N:N tra User e Book)
    Loan(ID, StartDate, EndDate, UserID [FK → User], BookID [FK → Book])

  - Reservation (N:N tra User e Book)
    Reservation(ID, StartDate, EndDate, UserID [FK → User], BookID [FK → Book])

  - BookAuthor (N:N tra Book e Author)
    BookAuthor(ID, BookID [FK → Book], AuthorID [FK → Author])

  - BookCategory (N:N tra Book e Category)
    BookCategory(ID, BookID [FK → Book], CategoryID [FK → Category])

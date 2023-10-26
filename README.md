# LibraryAPI

This project aims to manage a library's book collection, borrower information, contact details, and rental records. Here are the key functionalities available in my project:

Book Management:

Retrieve a list of books.
Get information about a specific book by its ID.
Search for books by title.
Search for books by author.
Check the availability of a specific book.
Add a new book to the library.
Update information about an existing book.
Delete a book from the library.
Borrower Management:

Retrieve a list of borrowers.
Get information about a specific borrower by their ID.
Add a new borrower to the system.
Update information about an existing borrower.
Delete borrower records.
Contact Information Management:

Retrieve contact information for a specific borrower by their ID.
Retrieve a list of contact information records.
Add new contact information for borrowers.
Update contact information.
Rental Management:

Retrieve a list of all rentals.
Get information about a specific rental by its ID.
Get a list of rentals associated with a specific borrower.
Get a list of rentals associated with a specific book.
Check the existence of a rental record.
Add a new rental record.
Update rental information (e.g., return a book).
Delete rental records

This project is built on the ASP.NET Core Web API technology and utilizes repositories that implement interfaces such as IBookRepository, IBorrowerRepository, IContactInfoRepository, and IRentalRepository to manage data in the database. These features enable effective management of the library's book collection, borrower information, contact details, and rental records.

Book endpoints:
<img width="1115" alt="image" src="https://github.com/MichalJ23/LibraryAPI/assets/122793735/8b095bc1-3849-42d1-b333-eabf7acf8321">

Borrower endpoints:
<img width="1101" alt="image" src="https://github.com/MichalJ23/LibraryAPI/assets/122793735/62a7bf59-0b80-488f-8ecd-915066a506f6">

Contact details endpoints:
<img width="1104" alt="image" src="https://github.com/MichalJ23/LibraryAPI/assets/122793735/27a63a77-9472-471f-89ec-9c99b76b1b5b">

Rental endpoints:
<img width="1118" alt="image" src="https://github.com/MichalJ23/LibraryAPI/assets/122793735/6130609d-731e-4ea1-a9e5-1222b584e0e5">

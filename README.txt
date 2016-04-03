Built with Visual Studio Community 2015.

I didn't write code to use the API from the entry point (APIEntryPoint class), it can only be run from unit tests, unless more code is written.
To run the tests one must open the solution with Visual Studio.

I've created 2 layers, represented by folders: Business layer and DAL layer
I've used Repository pattern in the DAL layer, so we can separate completely the data access from the business logic.
Interfaces were use which can provide a way to implement dependency injection, and helps when creating unit tests (use of mocks).

The notifications are simulated by the raise of events.
There's only a domain class, Item, and it's only stored in memory, on the respective repository.
I assumed that more than one Item can have the same label, therefore I included an Id field on the Item class.
When one tries to take one Item that doesn't exist, NULL is returned.

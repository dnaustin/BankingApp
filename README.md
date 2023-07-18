# BankingApp

Credentials to login to the console app: test1, 1234

Improvements that could be made:

- Unit tests (should have done it as I went but wanted to focus on getting the functional app instead)
- TransactionController should have been separate endpoints for the different functions
- Transfer only returns 1 account details rather than both accounts
- Transfer updating the 2 accounts should be done in a single db update transaction rather than 2 separate ones 
- Didn't really make use of the different account types from the factory

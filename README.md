Project #1

Stack:
    C#
    ASP.NET MVC (.NET Core)
    SQL Server
    Entity Framework (Code First)
    
A Net Banking Application
    Checking Account
    Business Account
    Loan
    Term Deposit
    
    Customer can have many accounts
    Checking Account can earn interest
    Business Account cannot earn interest
    Business Account can have an overdraft
        for e.g.; If there is $200 in my Business Account, I can still withdraw $300 from that account. The extra $100 will be considered as an overdraft facility, and interest will be charged on that $100, which will have to be repaid by the customer.
    All checking accounts have the same interest rate
    You cannot withdraw amount from a Term Deposit before maturity
    Operations that can be performed:
        Register
        Login
        Logout
        Open a new account
        Close an account
        Withdraw
        Deposit
        Transfer (between own accounts)
        Pay Loan installment (from one of the user's own accounts)
        Display list of accounts
        Display last 10 transactions for an account
        Display transactions for a date range for an account
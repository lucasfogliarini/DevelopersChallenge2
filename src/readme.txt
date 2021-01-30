The BankConciliatorDb.mdf database is inside the /Data folder.

Configure correctly the AttchDbFileName attribute:
1. Connect to Database
    - Tools > Connect to Database.
    - Data Source: Microsoft SQL Server Database File (SqlClient)
    - Browse to the file.
    - Test Connection
    - Ok

2. Copy the connection string path. 
    - Server explorer > BankConciliatorDb > Right Click > Properties > ConnectionString > Copy

3. Paste the copied connection string in files:
- Mvc project
    - ConnectionStrings:BankConciliator attribute in appsettings.Development.json file.

- Tests.Integration
    - bankConciliatorConnectionString variable in BankReconciliationServiceTests.cs file.
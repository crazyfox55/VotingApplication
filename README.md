# VotingApplication
An application creating ballots, collecting votes, and registering voters.


Install visual studio community 2017 to open project. 
https://www.visualstudio.com/downloads/

Setup an SQL database on your local system by downloading and installing SQL developer edition from Microsoft. 
https://www.microsoft.com/en-us/sql-server/sql-server-downloads

Manage the SQL server by downloading and installing SQL Server Management Studio (SSMS) on your local machine.
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

You can watch requests coming into your SQL server by going to Tools -> SQL Server Profiler. After connecting to the database within the profiler a popup for setting the trace configuartion will apear. Change the template to TSQL_Duration this will provide a good base for catching requests to the SQL server. Then click run, the profiler is now watching requests to your SQL server.

Your now ready to debug and program within this application. Make sure your work is done within a branch, instead of main. Then you can submit a pull request when the feature is complete. The expectation is that the branch will be deleted after the pull request is accepted.


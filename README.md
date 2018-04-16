# VotingApplication
An application creating ballots, collecting votes, and registering voters.


Install visual studio community 2017 to open project. 
https://www.visualstudio.com/downloads/

Setup an SQL database on your local system by downloading and installing SQL developer edition from Microsoft. 
https://www.microsoft.com/en-us/sql-server/sql-server-downloads

Manage the SQL server by downloading and installing SQL Server Management Studio (SSMS) on your local machine.
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

You can watch requests coming into your SQL server by going to Tools -> SQL Server Profiler. After connecting to the database within the profiler a popup for setting the trace configuartion will apear. Change the template to TSQL_Duration this will provide a good base for catching requests to the SQL server. Then click run, the profiler is now watching requests to your SQL server.

Your now ready to debug and program within this application. Make sure your work is done within a branch, instead of the Master. Then you can submit a pull request when the feature is complete. The expectation is that the branch will be deleted after the pull request is accepted.



Client side validation of forms:
https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation

Identity Tutorial:
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x

Identity Demo:
https://github.com/aspnet/Docs/tree/master/aspnetcore/security/authentication/identity/sample/src/ASPNETCore-IdentityDemoComplete/IdentityDemo

Identity Source:
https://github.com/aspnet/Identity/tree/85f8a49aef68bf9763cd9854ce1dd4a26a7c5d3c

Editing data Tutorial:
https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-edit-methods-and-edit-view

Getting Started with Entity Framework 6 Code First using MVC 5
https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application

Google API Key:
https://console.developers.google.com

<code>
<script src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY&callback=myMap"></script>
</code>
Replace YOUR_KEY with the key from Google to use Google maps



Important information about Entity Framework 6 Lazy Execution:
https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/reading-related-data-with-the-entity-framework-in-an-asp-net-mvc-application

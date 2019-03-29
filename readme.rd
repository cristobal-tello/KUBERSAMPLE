KUBERSAMPLE
===========

Simple web app to show how to use Azure Kubernetes Service (AKS)

* Entity framework

Create DB using:

dotnet ef migrations add teamMigration --context TeamDbContext
dotnet ef database update --context TeamDbContext

dotnet ef migrations add memeberMigration --context MemberDbContext
dotnet ef database update --context MemberDbContext
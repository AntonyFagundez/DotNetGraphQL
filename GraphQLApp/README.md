## Proyecto de GraphQL (HotChocolate) con .Net 5 y Entity Framework.


Comandos para iniciar un proyecto similar:

``` dotnet new web <nombredetuproyecto> ```

-----
**Comandos para las dependencias**

> ``` dotnet add package HotChocolate.AspNetCore```

> ``` dotnet add package HotCocolate.Data.Entityframework ```

> ``` dotnet add package Microsoft.EntityframeworkCore.Design ```

> ``` dotnet add package Microsoft.EntityframeworkCore.SqlServer ```

> ``` dotnet add package GraphQL.Server.Ui.Voyager ```

### Comandos para administrar la base de datos

> Recordando que esta aplicación tiene un enfoque codefirst.
 Situados desde la carpeta de la Web

 ``` dotnet ef migrations add test --project ../GraphQLApp.DataAccess ```

Eliminar la última migración

``` dotnet ef migrations remove --project ../GraphQLApp.DataAccess ```

Efectuar cambios en la BD

``` dotnet ef database update --project ../GraphQLApp.DataAccess  ```
# IfWhatTemplate
## Getting started
### Clone this repository to your computer
1. Create local repository:
  - Create a folder for your repository 
   ```shell
   mkdir [repo name]
   ```

  - Move to that folder
   ```shell
   cd [repo name]
   ```

  - Initialize local repository
   ```shell
   git init [repo name]
   ```

2. Clone this repository to your local machine:

   ```shell
   git clone https://github.com/New-Premises-Group/.NetGraphQLTemplate.git
   ```

3. Sync dependencies:

   ```shell
   dotnet restore
   ```
   
### Folder structure brief
  > IW
  > > Properties
>   > 
  > > Common
>   > 
  > > Controllers
>   > 
  > > Exceptions
>   > 
  > > Extensions
>   > 
  > > Interfaces
>   > 
  > > Models
>   > 
  > > > DTOs
>   > > 
  > > > Entities
>   > > 
  > > Repositories
>   > 
  > > Services
  >
  > Test
  > > Repositories

-----------------------------------------

> :information_source:__Note:__
>  * __Common folder__ : contain generic classes, interfaces to be used through out project.
>  * __Controllers__ : contain API classes.
>  * __Exceptions__ : contain custom exceptions for project and ErrorHandlerMiddleware to handler error.
>  * __Extensions__ : contain extended classes.
>  * __Interfaces__ : contain interfaces used in project.
>  * __Models__ : contain data transfer objects classes and entities.
>  * __Repositories__ : contain repository classes.
>  * __Services__ : contain service classes.
>  * __Test__ : contain unit tests, integration test classes.

### Classes and interfaces brief

>  * __GenericRepository__ : inherit this class to create your CustomRepository.
>  * __GraphQL__ : define endpoint to for graphQL to use.
>  * __AppException__ : example, follow it to make custom exception for specific error cases. 
>  * __ErrorHandlerMiddleware__ : middleware to hanlder exception thrown in project. 
>  * __ServicesRegistor__ : extend service class to inject service to project. 
>  * __CreateUser__ : convert data transfer parameter to a single object for easier read and modify
>  * __Entity Classes__ : define entity to use in database
>  * __Services__ : contain service classes
>  * __AppDbContext__ : context class to access database
>  * __UnitOfWork__ : combine query and command to database in each request into a single roundtrip
>  * __UserRepository__ : define code to access database
>  * __UserService__ : define service code
-------------------------------------

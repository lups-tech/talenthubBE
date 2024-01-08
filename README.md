# Talent Hub - .Net Backend

This repository forms part of a mob-programming project. The overall project is a job seeking system that can anaylse job adverts and display matching developers, to allow for easier and more effective recruitment.

The frontend for this project can be found [here](https://github.com/lups-tech/jobMatches).

## Start-Up
This project is built using Auth0. Before running this repository a number of secret variables will need to be set, with some relating to Auth0 settings that will need to be configured within Auth0. The database is set up for PostGreSQL (so to connect to a different database type, an amendment would be needed in the Program.cs file. 

### Auth0 Setup

A M2M Application should be set up for the frontend, along with an M2M application with permission to access the management API and an Auth0 API should be set up for the backend.

The M2M frontend application should be configured to allow login and logout from and to the frontend urls and authorized to access the backend API. 

The Auth0 backend API should include the following permissions to allow the role based authentication checks to work correctly (with the frontend M2M authorized to access them) - 

```create:users``` 

```create:admin```

The management API M2M application requires the following permissions - 

```read:client_grants``` 
```update:client_grants``` 
```update:users``` 
```update:users_app_metadata```
```read:roles``` 
```create:organization_members``` 
```create:organization_member_roles``` 
```create:organization_connections``` 
```create:organization_invitations``` 
```read:organization_invitations``` 
```read:organization_members``` 
```read:organization_connections```  
```read:client_credentials``` 
```create:client_credentials``` 
```update:client_credentials``` 
```delete:client_credentials``` 

### Variables
The following variables should be set using dotnet secrets.

Policy_url = The local host url you use to connect in development (eg. http://127.0.0.1:5173 to connect on port 5173)
ConnectionString = The database connection string
Auth0:ManagementClientID = The Id for the M2M application authorized to access the Management API
Auth0:ManagementAPI = Your Auth0 domain (not including ```https://```, ending in ```.com```)
Auth0:Domain = Your Auth0 domain (including ```https://```, ending in ```.com```)
Auth0:ClientSecret = Client Secrent from the frontend M2M application.
Auth0:ClientId = Id for the frontend M2M application.
Auth0:Audience = The Audience assigned to your backend project in Auth0 (the Auth0 API)

### Final Steps
Once the dotnet secrets have been entered, the program will run with the command 

```dotnet run ```

The endpoints can be explored via Swagger using the url assigned as (Your Policy Url)/swagger - eg. 

```http://127.0.0.1:5173/swagger```

## The Database
This backend is connected to a project hosted on Supabase. The project is therefore based on a PostgreSQL database.

Given that we are using this project to hone our .Net, ASP Net and C# skills, the decision was made to connect to the Supabase database via a connection string, rather than the scoped Supabase client (previous iterations of the backend used the scoped client and a minimal webAPI pattern for proof of concept testing, as can be seen [here](https://github.com/lups-tech/supabasecsharpapi), [here](https://github.com/lups-tech/supabaseJobAPI) and [here](https://github.com/lups-tech/supabaseDevAPI).

## The Endpoints
Given the need to have many developers with many skills matched with many jobs (also requiring many skills), we have separated the controllers for Developers, Skills, Users and Jobs.

Using Entity Framework, we set up Many-To-Many relationships between Developers and Skills, as well as Jobs and Skills. We also have many-to-many relationships between Users, Jobs and Developers. We have also added a "Organizaton" entity, that represents separate business clients. Each Organization has multiple users (eg. staff, with a one-to-many relationship) and multiple jobs and developers (both many-to-many). Users can be either classed as Sales or Admin, with each role providing different scoped access to the endpoints.

All controllers have CRUD functionality, using a REST API. We have also added the ability to add and remove relationships separately between entities, so as to allow for more flexbile interaction with the frontend.

Within the Skill Controller, we have added the ability to take a given job advert and return the skills contained within that advert, as well as a list of relevant ranked developers (based on matching skills).

The user controller is also able to directly call the Auth0 API, which allows for email invites to be sent to potential new organization members, as well as to upgrade existing members to an Admin role. This means that the project can utilize these capabilities directly, rather than via the Auth0 dashboard, allowing for more client autonomy.

## The Team
This project has been built by the following developers:
- [Luca Martinelli](https://github.com/Luega)
- [Feng Yang](https://github.com/Finns841594)
- [Panisara Bunawan Dachin](https://github.com/panisara-bd)
- [Stephen Moore](https://github.com/SMooreSwe)
- [Chris O'Brien](https://www.linkedin.com/in/chris-o-brien-314791212/)
- [Sonja Kitanoska](https://www.linkedin.com/in/sonja-kitanoska-986ba8a8/)

Together we are [Lups-Tech](https://github.com/lups-tech).

## Next Steps
We are approaching this project with an agile approach, consisting of 1 week sprint goals. Once we are happy with the MVP we have plans on refining the product and continuing to add value. Our current goals are to add:

~~Authorization and Authentication - to allow for multiple companies to use the product, with their own private lists of developers (user authorization and authentication is now in place with third party log-in via google, as well as email and password).~~(Now added)
~~The ability to save jobs to your account, to allow for ease of access to those details during hiring processes, or should a developer become available.~~(Now added)
~~Easier editing of existing developers.~~(Now added)
~~- Potential for a "process" entity is being discussed, that would allow for tracking of active recruitment processes. Implementation of this is currently being agreed upon.~~(Now added)

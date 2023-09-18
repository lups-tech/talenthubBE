# Talent Hub - .Net Backend

This repository forms part of a mob-programming project. The overall project is a job seeking system that can anaylse job adverts and display matching developers, to allow for easier and more effective recruitment.

The frontend for this project can be found [here](https://github.com/lups-tech/devFrontend).

## The Database
This backend is connected to a project hosted on Supabase. The project is therefore based on a PostgreSQL database.

Given that we are using this project to hone our .Net, ASP Net and C# skills, the decision was made to connect to the Supabase database via a connection string, rather than the scoped Supabase client (previous iterations of the backend used the scoped client and a minimal webAPI pattern for proof of concept testing, as can be seen [here](https://github.com/lups-tech/supabasecsharpapi), [here](https://github.com/lups-tech/supabaseJobAPI) and [here](https://github.com/lups-tech/supabaseDevAPI).

## The Endpoints
Given the need to have many developers with many skills matched with many jobs (also requiring many skills), we have separated the controllers for Developers, Skills and Jobs.

Using Entity Framework, we set up Many-To-Many relationships between Developers and Skills, as well as Jobs and Skills.

All three controllers have CRUD functionality, using a REST API. We have also added the ability to add and remove skills from Developers as separate endpoints, due to the structure of our frontend, and therefore the user journey.

Within the Skill Controller, we have added the ability to take a given job advert and return the skills contained within that advert, as well as a list of relevant ranked developers (based on matching skills).

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
We are approaching this project with an agile approach, consisting of 1 week sprint goals. At present, both frontend and backend are not yet deployed, though we hope to address this once authorization is properly integrated.

We are currently working on the MVP, with the required Many-To-Many relationships already set up on the backend.

Once this is done, we hope to refactor the controllers to add service injection and keep the business logic separate from the routes.

Test